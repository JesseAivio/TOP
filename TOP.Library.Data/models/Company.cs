using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TOP.Library.Data.models
{
    public class Company : Base
    {
        [Required, MaxLength(50)]
        public string company { get; set; }
    }
}
