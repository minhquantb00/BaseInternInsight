using BaseInsightDotNet.Commons.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Core.Entities.Media
{
    public partial class MediaFolder : BaseEntity
    {
        private IEnumerable<MediaFile> _files;
        private IEnumerable<MediaFolder> _children;
        public Guid? ParentId { get; set; }
        public virtual MediaFolder Parent { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public bool CanDetectTracks { get; set; }
		public virtual IEnumerable<MediaFolder> Children
        {
            get; set;
        }

        public string Metadata { get; set; }
        public int FilesCount { get; set; }

        public string Owner { get; set; }
        public bool Deleted { get; set; }

        public bool IsPublic { get; set; }

        public string IsProtected { get; set; }

        public string IsPrivate { get; set; }

        public virtual IEnumerable<MediaFile> Files
        {
            get; set;
        }
    }
}
