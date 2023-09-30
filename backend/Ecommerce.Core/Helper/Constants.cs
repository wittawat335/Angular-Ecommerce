using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Helper
{
    public class Constants
    {
        public struct AppSettings
        {
            public const string Client_URL = "AppSettings:Client_URL";
            public const string CorsPolicy = "AppSettings:CorsPolicy";
        }
        public struct StatusMessage
        {
            public const string RegisterSuccess = "Register Successfully";
            public const string LoginSuccess = "Login Successfully";
            public const string InvaildPassword = "invaild password";
            public const string NotFoundUser = "not found user";
            public const string Success = "OK";
            public const string No_Data = "No Data";
            public const string Could_Not_Create = "Could not create";
            public const string No_Delete = "No Deleted";
            public const string DuplicateUser = "Username is Duplicate";
            public const string DuplicatePosition = "Position is Duplicate";
            public const string Cannot_Update_Data = "Cannot Update Data";
            public const string Cannot_Map_Data = "Cannot Map Data";
            public const string InActive = "This username is inactive";
            public const string AddSuccessfully = "Add successfully";
            public const string UpdateSuccessfully = "Update successfully";
            public const string DeleteSuccessfully = "Delete successfully";
        }
        public struct Status
        {
            public const bool True = true;
            public const bool False = false;
            public static string Active = "A";
            public static string ActiveText = "Active";
            public static string Inactive = "I";
            public static string InactiveText = "Inactive";
        }
    }
}
