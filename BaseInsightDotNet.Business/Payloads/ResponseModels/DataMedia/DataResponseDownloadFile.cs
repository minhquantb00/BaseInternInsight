using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.ResponseModels.DataMedia
{
    public class DataResponseDownloadFile
    {
        public Guid Id { get; set; }
        public Guid FileKey { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public string MimeType { get; set; }
        public string MediaType { get; set; }
    }
}
