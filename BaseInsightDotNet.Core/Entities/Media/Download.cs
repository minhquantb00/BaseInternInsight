using BaseInsightDotNet.Commons.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Core.Entities.Media
{
    public partial class Download : BaseEntity
    {
        public Guid DownloadGuid { get; set; }
        public bool UseDownloadUrl { get; set; }
        public string DownloadUrl { get; set; }
        public bool IsTransient { get; set; }
        public Guid? MediaFileId { get; set; }
        public virtual MediaFile MediaFile { get; set; }
        public Guid EntityId { get; set; }
        public string EntityName { get; set; }
        public string FileVersion { get; set; }
        public string Changelog { get; set; }
    }
}
