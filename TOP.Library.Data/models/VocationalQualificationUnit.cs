using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TOP.Library.Data.models
{
    public class VocationalQualificationUnit : Base
    {
        [Required, MaxLength(50)]
        public string vocationalQualificationUnit { get; set; }
    }
}
