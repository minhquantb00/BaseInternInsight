using BaseInsightDotNet.Commons.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Core.Entities.Media
{
    public partial class MediaFile : BaseEntity, ISoftDeletable
    {
        public Guid FileKey { get; set; }
        public Guid? FolderId { get; set; }
        public virtual MediaFolder Folder { get; set; }
        public string Name { get; set; }
        public string Alt { get; set; }
        public string Title { get; set; }
        public string Extension { get; set; }
        public string MimeType { get; set; }
        public string MediaType { get; set; }
        public int Size { get; set; }
        public int? PixelSize { get; set; }
        public string Metadata { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public bool IsTransient { get; set; }
        public bool Deleted { get; set; }
        public bool Hidden { get; set; }
        public int Version { get; set; } = 2;
        public int? MediaStorageId { get; set; }
        public string Owner { get; set; }
        public string Path { get; set; }
        public virtual MediaStorage MediaStorage { get; set; }
    }
}
