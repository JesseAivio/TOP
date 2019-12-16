using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TOP.Library.Data.models
{
    public class Base
    {
        [Key]
        public Guid Id { get; set; }
    }
}
