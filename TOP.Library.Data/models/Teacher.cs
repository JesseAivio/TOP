using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TOP.Library.Data.models
{
    public class Teacher : Base
    {
        [Required, MaxLength(50)]
        public string teacher { get; set; }

    }
}
