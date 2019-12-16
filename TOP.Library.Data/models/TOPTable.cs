using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TOP.Library.Data.models
{
    [Table("TOP")]
    public class TOPTable : Base
    {
        [Required, MaxLength(20)]
        public string PhoneNumber { get; set; }
        [Required, MaxLength(30)]
        public string EmailAddress { get; set; }

        public bool Reserved { get; set; }

        public DateTime ReservationEnds { get; set; }

        public bool Accepted { get; set; }
        [MaxLength(1000)]
        public string Info { get; set; }
        [ForeignKey("CompanyLink")]
        public Guid Company { get; set; }
        [ForeignKey("TeacherLink")]
        public Guid Teacher { get; set; }
        [ForeignKey("AddressLink")]
        public Guid Address { get; set; }
        [ForeignKey("VocationalQualificationUnitLink")]
        public Guid VocationalQualificationUnit { get; set; }
        public virtual Company CompanyLink { get; set; }
        public virtual Teacher TeacherLink { get; set; }
        public virtual Address AddressLink { get; set; }
        public virtual VocationalQualificationUnit VocationalQualificationUnitLink { get; set; }
    }
}
