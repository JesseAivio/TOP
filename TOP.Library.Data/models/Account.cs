using System;
using System.Collections.Generic;
using System.Text;

namespace TOP.Library.Data.models
{
    public class Account : Base
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public string Role { get; set; }

        public string Token { get; set; }
    }
}
