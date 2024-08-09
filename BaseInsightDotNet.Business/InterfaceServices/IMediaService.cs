using BaseInsightDotNet.Core.Entities.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.InterfaceServices
{
    public partial interface IMediaService
    {
        bool FileExists(Guid folderId, string name);
        MediaFile GetFileById(Guid id);
        IList<MediaFile> GetFilesByIds(Guid[] ids);
        public IQueryable<MediaFile> GetMediaFiles(string keywords, Guid? folderId = null, string owner = null);

        MediaFile SaveFile(Guid folderId, string fileName, string path, Stream stream, bool isTransient = true);
        Task<MediaFile> SaveFileAsync(Guid folderId, string fileName, string path, Stream stream, bool isTransient = true, string owner = null, bool isReplaceIfExits = true);
        Task DeleteFile(Guid fileId, bool permanent);
        MediaFile CopyFile(MediaFile file, string destinationFileName);
        MediaFile MoveFile(MediaFile file, string destinationFileName);

        public IQueryable<MediaFolder> GetMediaFolders(string keywords, Guid? parentFolderId = null, string owner = null);

        //public Task<MediaFolder> GetMediaFolderById(Guid id, string owner = null);
        bool FolderExists(string path);
        public Task<MediaFolder> CreateFolder(MediaFolder folder, string account);
        public MediaFolder MoveFolder(string path, string destinationPath);
        public MediaFolder CopyFolder(string path, string destinationPath);
        //public Task DeleteFolder(Guid folderId, bool permanent = false);
    }
}
