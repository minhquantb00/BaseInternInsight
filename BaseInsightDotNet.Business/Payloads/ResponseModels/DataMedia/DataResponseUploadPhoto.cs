using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.ResponseModels.DataMedia
{
    public class DataResponseUploadPhoto
    {
        public Guid Id { get; set; }
        public Guid FileKey { get; set; }
        public string Name { get; set; }
    }
}
