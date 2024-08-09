using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Commons.Constants
{
    public class Constant
    {
        public class AppSettingKeys
        {
            public const string DEFAULT_CONTROLLER_ROUTE = "api/[controller]";
            public const string DEFAULT_CONNECTION = "DefaultConnection";
            public const string AUTH_SECRET = "AuthSecret";
        }

        public class Roles
        {
            public const string Admin = "Admin";
            public const string User = "User";
        }

        public class ExceptionMessage
        {
            public const string NOT_FOUND = "{0} không tồn tại.";
            public const string ITEM_NOT_FOUND = "Không tồn tại bản ghi.";
            public const string ALREADY_EXIST = "{0} đã tồn tại.";
            public const string SUCCESS = "{0} thành công.";
            public const string SHOULD_GREATER_TODAY = "{0} Date is late.";
            public const string INVALID = "{0} invalid.";
            public const string EMAIL_NOT_ACTIVATED = "Email not activated";
            public const string FILE_NOT_FOUND = "File {0} not found";
            public const string EXPIRATION_TIME = "Expiration time";
            public const string VERIFIED = "Account has been verified";
        }

        public class ContextItem
        {
            public const string USER = "User";
            public const string PERMISSIONS = "Permissions";
        }

        public class DefaultValue
        {
            public const int DEFAULT_PAGE_SIZE_VIDEO = 9;
            public const int DEFAULT_PAGE_SIZE = 10;
            public const int DEFAULT_PAGE_NO = 1;
            public const string DEFAULT_CONTROLLER_ROUTE = "api/[controller]/[action]";
            public const string DEFAULT_CONTROLLER_ROUTE_WITHOUT_ACTION = "api/[controller]";
            public const string DEFAULT_ACTION_ROUTE = "[action]";
        }
    }
}
