using BaseInsightDotNet.Business.Payloads.ResponseModels.DataUser;
using BaseInsightDotNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.Converters
{
    public class UserConverter
    {
        public DataResponseUser EntityToDTO(ApplicationUser user)
        {
            return new DataResponseUser
            {
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                Gender = user.Gender == Commons.Enums.Enumerate.Gender.Unknown ? "Không xác định" : user.Gender == Commons.Enums.Enumerate.Gender.Male ? "Nam" : "Nữ",
                Id = Guid.Parse(user.Id),
                PhoneNumber = user.PhoneNumber,
                AvatarUrl = user.AvatarUrl,
                FullName = user.FullName,
            };
        }
    }
}
