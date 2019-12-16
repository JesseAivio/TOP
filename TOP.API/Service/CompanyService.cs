using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOP.API.Data.Context;
using TOP.Library.Data.models;

namespace TOP.API.Service
{
    public class CompanyService : IModelService<Company>
    {
        readonly TOPContext _topContext;
        public CompanyService(TOPContext topContext)
        {
            _topContext = topContext;
        }

        public Company Add(Company companyParam)
        {
            var company = _topContext.Companys.FirstOrDefault(x => x.company == companyParam.company);

            if (company != null)
                return null;
            _topContext.Companys.Add(companyParam);
            _topContext.SaveChanges();
            return companyParam;
        }

        public Company Get(Guid companyId)
        {
            var company = _topContext.Companys.FirstOrDefault(x => x.Id == companyId);

            if (company == null)
                return null;

            return company;
        }

        public Company GetByName(string name)
        {
            var company = _topContext.Companys.FirstOrDefault(x => x.company == name);

            if (company == null)
                return null;

            return company;
        }

        public void Delete(Company company)
        {
            _topContext.Companys.Remove(company);
            _topContext.SaveChanges();
        }

        public void Update(Company company)
        {
            var dbCompany = _topContext.Companys.FirstOrDefault(x => x.Id == company.Id);
            dbCompany.company = company.company;
            _topContext.SaveChanges();
        }

        public IEnumerable<Company> GetAll()
        {
            return null;
        }
    }
}
