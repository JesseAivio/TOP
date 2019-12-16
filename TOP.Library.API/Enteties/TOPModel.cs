using System;
using System.Collections.Generic;
using System.Text;

namespace TOP.Library.API.Enteties
{
    public class TOPModel
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public bool Reserved { get; set; }

        public DateTime ReservationEnds { get; set; }

        public bool Accepted { get; set; }

        public string Info { get; set; }

        public string Company { get; set; }

        public string Teacher { get; set; }

        public string Address { get; set; }

        public string VocationalQualificationUnit { get; set; }
    }
}
