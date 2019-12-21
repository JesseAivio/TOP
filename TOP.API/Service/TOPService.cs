using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOP.API.Data.Context;
using TOP.API.Data.Helpers;
using TOP.Library.Data.models;

namespace TOP.API.Service
{
    public interface ITOPService
    {
        TOPTable GetTOP(Guid Id);
        IEnumerable<TOPTable> GetTOPs();
        void AddTOP(TOPTable top);
        void UpdateTOP(TOPTable top);
        void DeleteTOP(TOPTable top);
    }
    public class TOPService : ITOPService
    {
        readonly TOPContext _topContext;
        readonly List<TOPTable> _TOPs = new List<TOPTable>();//Azure testing
        public TOPService(TOPContext topContext)
        {
            _topContext = topContext;
            if (AppSettings.isAzure)
            {
                AddDefaults();
            }
        }

        private void AddDefaults()
        {
            TOPTable top = new TOPTable()
            {
                Id = Guid.NewGuid(),
                PhoneNumber = "1234567",
                EmailAddress = "test@test.com",
                Reserved = true,
                ReservationEnds = DateTime.MaxValue,
                Accepted = true,
                Info = "This is Azure test",
                Company = Guid.Parse("9a1ee86a-93fc-47c3-ae15-8c9873bc0d82"),
                Teacher = Guid.Parse("0d66b512-b2dc-49aa-a4af-01842c15c342"),
                Address = Guid.Parse("125c5813-7542-4861-90d1-7ce9403fb4c4"),
                VocationalQualificationUnit = Guid.Parse("768b6a66-e2b4-4d18-95b8-18c402a7cf3a")
            };
            TOPTable top2 = new TOPTable()
            {
                Id = Guid.NewGuid(),
                PhoneNumber = "7654321",
                EmailAddress = "test2@test2.com",
                Reserved = true,
                ReservationEnds = DateTime.MaxValue,
                Accepted = true,
                Info = "This is Azure test2",
                Company = Guid.Parse("b186664b-c83c-4a8f-bba2-a4d18b342be8"),
                Teacher = Guid.Parse("4c4be516-9021-4031-a3e6-eb7fc8f2d760"),
                Address = Guid.Parse("e6a34daa-e635-4ab6-bd90-991a78d73125"),
                VocationalQualificationUnit = Guid.Parse("6be50d9b-fbf8-4cda-8bb1-84b93c01b1a5")
            };
            _TOPs.Add(top);
            _TOPs.Add(top2);
        }

        public TOPTable GetTOP(Guid Id)
        {
            if (AppSettings.isAzure)
            {
                return _TOPs.FirstOrDefault(x => x.Id == Id);
            }
            return _topContext.TOPs.FirstOrDefault(x => x.Id == Id);
        }

        public IEnumerable<TOPTable> GetTOPs()
        {
            if (AppSettings.isAzure)
            {
                return _TOPs.ToList();
            }
            return _topContext.TOPs.ToList();
        }

        public void AddTOP(TOPTable top)
        {
            if (AppSettings.isAzure)
            {
                top.Id = Guid.NewGuid();
                _TOPs.Add(top);
                return;
            }
            _topContext.TOPs.Add(top);
            _topContext.SaveChanges();
        }

        public void UpdateTOP(TOPTable top)
        {
            TOPTable dbTOP = new TOPTable();
            if (AppSettings.isAzure)
            {
                dbTOP = _TOPs.FirstOrDefault(x => x.Id == top.Id);
                dbTOP.PhoneNumber = top.PhoneNumber;
                dbTOP.EmailAddress = top.EmailAddress;
                dbTOP.Reserved = top.Reserved;
                dbTOP.ReservationEnds = top.ReservationEnds;
                dbTOP.Accepted = top.Accepted;
                dbTOP.Info = top.Info;
                dbTOP.Company = top.Company;
                dbTOP.Address = top.Address;
                dbTOP.Teacher = top.Teacher;
                dbTOP.VocationalQualificationUnit = top.VocationalQualificationUnit;
                return;
            }
            dbTOP = _topContext.TOPs.FirstOrDefault(x => x.Id == top.Id);
            dbTOP.PhoneNumber = top.PhoneNumber;
            dbTOP.EmailAddress = top.EmailAddress;
            dbTOP.Reserved = top.Reserved;
            dbTOP.ReservationEnds = top.ReservationEnds;
            dbTOP.Accepted = top.Accepted;
            dbTOP.Info = top.Info;
            dbTOP.Company = top.Company;
            dbTOP.Address = top.Address;
            dbTOP.Teacher = top.Teacher;
            dbTOP.VocationalQualificationUnit = top.VocationalQualificationUnit;
            _topContext.SaveChanges();
        }

        public void DeleteTOP(TOPTable top)
        {
            if (AppSettings.isAzure)
            {
                _TOPs.Remove(top);
                return;
            }
            _topContext.TOPs.Remove(top);
            _topContext.SaveChanges();
        }
    }
}
