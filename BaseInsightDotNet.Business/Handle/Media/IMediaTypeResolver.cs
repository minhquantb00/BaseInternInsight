using BaseInsightDotNet.Business.Guard;
using BaseInsightDotNet.Business.Handle.Media;
using BaseInsightDotNet.Core.Entities.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Commons.Media
{
    public interface IMediaTypeResolver
    {
        MediaType Resolve(string extension, string mimeType = null);
    }

    public static class IMediaTypeResolverExtensions
    {
        public static MediaType Resolve(this IMediaTypeResolver resolver, MediaFile file)
        {
            Guard.NotNull(file, nameof(file));
            return resolver.Resolve(file.Extension, file.MimeType);
        }
    }
}
