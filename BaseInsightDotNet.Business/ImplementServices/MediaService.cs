using BaseInsightDotNet.Business.Guard;
using BaseInsightDotNet.Business.Handle.Media;
using BaseInsightDotNet.Business.InterfaceServices;
using BaseInsightDotNet.Business.Payloads.RequestModels.MediaRequest;
using BaseInsightDotNet.Business.Payloads.ResponseModels.DataMedia;
using BaseInsightDotNet.Commons.Extensions;
using BaseInsightDotNet.Commons.Media;
using BaseInsightDotNet.Core.Entities.Media;
using BaseInsightDotNet.DataAccess.Repository.Interfaces;
using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.ImplementServices
{
    public partial class MediaService : IMediaService
    {
        private readonly IMediaTypeResolver _mediaTypeResolver;
        private readonly ISpecificationFactory _specificationFactory;
        private readonly IImageProcessor _imageProcessor;
        private readonly IRepository<MediaFile> _mediaFileRepository;
        private readonly IRepository<MediaFolder> _mediaFolderRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IWebHostEnvironment _hostingEnvironment;

        public MediaService(IWebHostEnvironment hostingEnvironment, IMediaTypeResolver mediaTypeResolver, IImageProcessor imageProcessor, IRepository<MediaFile> mediaFileRepository, IRepository<MediaFolder> mediaFolderRepository, ISpecificationFactory specificationFactory, IHttpContextAccessor httpContextAccessor)
        {
            _mediaTypeResolver = mediaTypeResolver;
            _imageProcessor = imageProcessor;
            _mediaFileRepository = mediaFileRepository;
            _mediaFolderRepository = mediaFolderRepository;
            _specificationFactory = specificationFactory;
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;
        }

        #region Upload and download file
        public async Task<DataResponseDownloadFile> HandleDownloadFile(Request_DownloadFile request)
        {
            var ownerId = _httpContextAccessor.HttpContext?.User?.FindFirst("Id")?.Value;
            var file = GetFileById(request.Id);

            if (file == null) return null;

            return new DataResponseDownloadFile
            {
                Extension = file.Extension,
                FileKey = file.FileKey,
                Id = file.Id,
                MediaType = file.MediaType,
                MimeType = file.MimeType,
                Name = file.Name,
                Path = file.Path,
            };
        }
        public async Task<DataResponseUploadPhoto> HandleUploadPhoto(Request_UploadPhoto request)
        {
            var ownerId = _httpContextAccessor.HttpContext?.User?.FindFirst("Id")?.Value;
            var defaultFolder = GetMediaFolders("FilesUpload").FirstOrDefault();
            var mediaFile = await SaveFileAsync(defaultFolder.Id, request.FileName, request.SavePath, request.FileStream, true, ownerId, false);

            return new DataResponseUploadPhoto
            {
                FileKey = mediaFile.FileKey,
                Id=mediaFile.Id,
                Name = mediaFile.Name,
            };
        }
        #endregion

        #region Read

        public bool FileExists(Guid folderId, string name)
        {
            Guard.Guard.NotEmpty(name, nameof(name));

            Guard.Guard.NotEmpty(folderId, nameof(folderId));

            var mediaFileExists = _mediaFileRepository.GetAllAsync(x => x.Name == name && x.FolderId == folderId);

            return mediaFileExists != null;
        }

        public MediaFile GetFileById(Guid id)
        {
            if (id == null)
                return null;

            var mediaFile = _mediaFileRepository.GetAsync(x => x.Id == id);

            return mediaFile.Result;
        }

        public IList<MediaFile> GetFilesByIds(Guid[] ids)
        {
            Guard.Guard.NotNull(ids, nameof(ids));

            var query = _mediaFileRepository.GetAllAsync(x => ids.Contains(x.Id));

            return query.Result.ToList();
        }
        public IQueryable<MediaFile> GetMediaFiles(string keywords, Guid? folderId = null, string owner = null)
        {
            var query = _mediaFileRepository.GetAllAsync().Result;

            if (!string.IsNullOrEmpty(keywords))
            {
                query = query
                    .Where(x =>
                        x.Name.Contains(keywords)
                    );
            }
            if (folderId.HasValue)
            {
                query = query
                    .Where(x =>
                        x.FolderId == folderId
                    );
            }
            if (!string.IsNullOrEmpty(owner))
            {
                query = query
                    .Where(x =>
                        x.Owner == owner
                        || x.Owner == null
                    );
            }
            return query;
        }

        #endregion

        #region Create/Update/Delete
        protected Stream ProcessFile(
           ref MediaFile file,
           Guid folderId,
           string fileName,
           Stream inStream,
           bool isTransient = true)
        {
            file = file ?? new MediaFile
            {
                FileKey = Guid.NewGuid(),
                IsTransient = isTransient,
                FolderId = folderId
            };

            file.Name = fileName;
            file.Extension = Path.GetExtension(fileName).EmptyNull().TrimStart('.');
            file.MimeType = MimeTypes.MapNameToMimeType(fileName);
            if (file.MediaType == null)
            {
                file.MediaType = _mediaTypeResolver.Resolve(file.Extension, file.MimeType);
            }

            // Process image
            if (inStream != null && inStream.Length > 0 && file.MediaType == MediaType.Image && ProcessImage(file, inStream, out var outStream, out var size))
            {
                inStream = outStream;
                file.Size = (int)outStream.Length;
                file.Width = size.Width;
                file.Height = size.Height;
                file.PixelSize = size.Width * size.Height;
            }
            else
            {
                file.RefreshMetadata(inStream);
            }

            return inStream;
        }
        public MediaFile SaveFile(Guid folderId, string fileName, string path, Stream stream, bool isTransient = true)
        {
            var file = _mediaFileRepository
                .GetAsync(x => x.Name == fileName && x.FolderId == folderId).Result;
            var folder = _mediaFolderRepository
                .GetAsync(x => x.Id == folderId).Result;
            stream = ProcessFile(ref file, folderId, fileName, stream, isTransient);

            if (file.Id == null)
            {
                _mediaFileRepository.Add(file);
            }
            else
            {
                _mediaFileRepository.Update(file);
            }
            if (stream != null)
            {
                // create folder if it does not exist yet
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filePath = Path.Combine(path, file.FileKey.ToString());

                using (stream)
                {
                    using (var outputStream = File.Create(path))
                    {
                        outputStream.SetLength(0);
                        stream.CopyTo(outputStream);
                    }
                }
            }

            return file;
        }

        public async Task<MediaFile> SaveFileAsync(Guid folderId, string fileName, string path, Stream stream, bool isTransient = true, string owner = null, bool isReplaceIfExits = true)
        {
            MediaFile file = null;
            if (isReplaceIfExits)
            {
                file = _mediaFileRepository
                    .GetAsync(x => x.Name == fileName && x.FolderId == folderId).Result;
            }
            var folder = _mediaFolderRepository
                .GetAsync(x => x.Id == folderId).Result;
            stream = ProcessFile(ref file, folderId, fileName, stream, isTransient);
            file.Owner = owner;
            if (file.Id == null || file.Id == new Guid())
            {
                file = await _mediaFileRepository.CreateAsync(file);
            }
            else
            {
                file.Deleted = false;
                _mediaFileRepository.Update(file);
            }
            if (stream != null)
            {
                // create folder if it does not exist yet
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                file.Path = Path.Combine(path, file.FileKey.ToString());
                using (stream)
                {
                    using (var outputStream = File.Create(file.Path))
                    {
                        outputStream.SetLength(0);
                        stream.CopyTo(outputStream);
                    }
                }
                _mediaFileRepository.Update(file);
            }
            return file;
        }

        public async Task DeleteFile(Guid fileId, bool permanent)
        {
            var file = await _mediaFileRepository.GetAsync(x => x.Id == fileId);
            if (!permanent)
            {
                file.Deleted = true;
                _mediaFileRepository.Update(file);
            }
            else
            {
                //todo: Delete from storage
                //_storageProvider.Remove(file);
                if (File.Exists(file.Path))
                    File.Delete(file.Path);

                // Delete entity
                _mediaFileRepository.Delete(file);
            }
        }

        protected bool ProcessImage(MediaFile file, Stream inStream, out Stream outStream, out Size size)
        {
            outStream = null;
            size = Size.Empty;

            var originalSize = ImageHeader.GetDimensions(inStream, file.MimeType);

            if (!_imageProcessor.IsSupportedImage(file.Extension))
            {
                outStream = inStream;
                size = originalSize; // e.g.: image/svg+xml
                return true;
            }

            var maxSize = 1024;
            var defaultImageQuality = 100;

            var query = new ProcessImageQuery(inStream)
            {
                Quality = defaultImageQuality,
                Format = file.Extension,
                DisposeSource = true,
                IsValidationMode = true
            };

            query.MaxSize = maxSize;

            using (var result = _imageProcessor.ProcessImage(query, false))
            {
                size = new Size(result.Width, result.Height);
                outStream = result.OutputStream;
                return true;
            }
        }

        #endregion

        #region Copy & Move

        public MediaFile CopyFile(MediaFile file, string destinationFileName)
        {
            throw new NotImplementedException();
        }

        public MediaFile MoveFile(MediaFile file, string destinationFileName)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region Utils
        public IQueryable<MediaFolder> GetMediaFolders(string keywords, Guid? parentFolderId = null, string owner = null)
        {
            var query = _mediaFolderRepository
                    .GetAllAsync(mediaFolder =>
                        mediaFolder.ParentId == parentFolderId
                        && !mediaFolder.Deleted
                    ).Result;

            if (!string.IsNullOrEmpty(keywords))
            {
                query = query
                    .Where(mediaFolder =>
                        mediaFolder.Name.Contains(keywords)
                    );
            }
            if (!string.IsNullOrEmpty(owner))
            {
                query = query
                    .Where(mediaFolder =>
                        mediaFolder.Owner == owner
                        || mediaFolder.Owner == null
                    );
            }
            return query;
        }

        public async Task<MediaFolder> GetMediaFolderById(Guid id, string owner = null)
        {
            var specification = _specificationFactory.Create<MediaFolder>();
            specification = specification
                .And(x => x.Id == id && !x.Deleted);
            specification.AddInclude(x => x.Parent);
            specification.AddInclude(x => x.Children);

            var folder = (await _mediaFolderRepository.GetAsync(specification))
                .FirstOrDefault();
            if (!string.IsNullOrEmpty(owner) && folder.Owner != null && folder.Owner != owner)
            {
                return null;
            }
            folder.Files = await _mediaFileRepository.GetAllAsync(x => x.FolderId == folder.Id && !x.Deleted && (x.Owner == owner || x.Owner == null));
            return folder;
        }
        public bool FolderExists(string path)
        {
            throw new NotImplementedException();
        }

        public async Task<MediaFolder> CreateFolder(MediaFolder folder, string account)
        {
            if (_mediaFolderRepository.GetAllAsync(x => x.ParentId == folder.ParentId
                && x.Name == folder.Name
                && x.Owner == account).Result != null)
            {
                return null;
            }
            folder.Owner = account;
            await _mediaFolderRepository.CreateAsync(folder);
            return folder;
        }

        public MediaFolder MoveFolder(string path, string destinationPath)
        {
            throw new NotImplementedException();
        }

        public MediaFolder CopyFolder(string path, string destinationPath)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteFolder(Guid folderId, bool permanent = false)
        {
            var specification = _specificationFactory.Create<MediaFolder>();
            specification = specification
                .And(x => x.Id == folderId);
            specification.AddInclude(x => x.Files);
            specification.AddInclude(x => x.Children);

            var folder = (await _mediaFolderRepository.GetAsync(specification))
                .FirstOrDefault();
            foreach (var folderChild in folder.Children)
            {
                await DeleteFolder(folderChild.Id, permanent);
            }
            foreach (var file in folder.Files)
            {
                await DeleteFile(file.Id, permanent);
            }
            if (permanent)
            {
                folder.Deleted = true;
                _mediaFolderRepository.Update(folder);
            }
            else
            {
                _mediaFolderRepository.Delete(folder);
            }
        }

        #endregion
    }
}
