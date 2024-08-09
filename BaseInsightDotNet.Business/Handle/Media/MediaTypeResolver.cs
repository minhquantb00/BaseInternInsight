using BaseInsightDotNet.Commons.Extensions;
using BaseInsightDotNet.Commons.Media;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Handle.Media
{
    public partial class MediaTypeResolver : IMediaTypeResolver
    {
        const string MapCacheKey = "media:exttypemap";

        public MediaType Resolve(string extension, string mimeType = null)
        {
            if (extension.IsEmpty() && mimeType.HasValue())
            {
                extension = MimeTypes.MapMimeTypeToExtension(mimeType);
            }

            var map = GetExtensionMediaTypeMap();

            string mediaType = null;
            if (extension.HasValue() && map.TryGetValue(extension.TrimStart('.').ToLower(), out mediaType))
            {
                return (MediaType)mediaType;
            }

            if (mimeType.HasValue())
            {
                // Get first mime token (e.g. IMAGE/png, VIDEO/mp4 etc.)
                var mimeGroup = mimeType.Split('/')[0];
                mediaType = MediaType.GetMediaType(mimeGroup);
            }

            return (MediaType)mediaType ?? MediaType.Binary;
        }
        private Dictionary<string, string> GetExtensionMediaTypeMap()
        {
            var extensionMediaTypeMap = new Dictionary<string, string>();
            foreach (var ext in MediaType.Image.DefaultExtensions)
            {
                extensionMediaTypeMap.Add(ext, MediaType.Image.Name);
            }
            foreach (var ext in MediaType.Video.DefaultExtensions)
            {
                extensionMediaTypeMap.Add(ext, MediaType.Video.Name);
            }
            foreach (var ext in MediaType.Audio.DefaultExtensions)
            {
                extensionMediaTypeMap.Add(ext, MediaType.Audio.Name);
            }
            foreach (var ext in MediaType.Document.DefaultExtensions)
            {
                extensionMediaTypeMap.Add(ext, MediaType.Document.Name);
            }
            foreach (var ext in MediaType.Text.DefaultExtensions)
            {
                extensionMediaTypeMap.Add(ext, MediaType.Text.Name);
            }
            return extensionMediaTypeMap;
        }
    }
}
