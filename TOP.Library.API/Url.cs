using System;
using System.Collections.Generic;
using System.Text;

namespace TOP.Library.API
{
    public static class Url
    {
        public const string TOP_API_Address = "https://localhost:44346/api/";

        public const string TOP_API_Address_Azure = "https://topapi.azurewebsites.net/api/";

        public const string Controller_Auth = "Auth";

        public const string Controller_TOP = "TOP";

        public const string Action_Authenticate = "Auth/Authenticate";

        public const string Action_Register = "Auth/Register";

        public const string Action_VocationalQualificationUnit = "TOP/VocationalQualificationUnit";

        public const string Action_Teacher = "TOP/Teacher";
    }
}
