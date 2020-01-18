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
        public TOPService(TOPContext topContext)
        {
            _topContext = topContext;
        }

        public TOPTable GetTOP(Guid Id)
        {
            return _topContext.TOPs.FirstOrDefault(x => x.Id == Id);
        }

        public IEnumerable<TOPTable> GetTOPs()
        {
            return _topContext.TOPs.ToList();
        }

        public void AddTOP(TOPTable top)
        {
            _topContext.TOPs.Add(top);
            _topContext.SaveChanges();
        }

        public void UpdateTOP(TOPTable top)
        {
            TOPTable dbTOP = new TOPTable();
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
            _topContext.TOPs.Remove(top);
            _topContext.SaveChanges();
        }
    }
}
