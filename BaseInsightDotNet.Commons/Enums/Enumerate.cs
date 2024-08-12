using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Commons.Enums
{
    public class Enumerate
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum UserStatus
        {
            Unactivated = 1,
            Activated = 2,
            IsDeleted = 3,
            IsLocked = 4
        }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Gender
        {
            Unknown = 0,
            Male = 1,
            Female = 2
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum ContractStatus
        {
            HetHan = 0,
            BanNhap = 1,
            DaHuy = 2,
            DaGiaHan = 3,
            DaChamDut = 4,
            DangCho = 5
        }
    }
}
