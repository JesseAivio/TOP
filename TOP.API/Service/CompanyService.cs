using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOP.API.Data.Context;
using TOP.API.Data.Helpers;
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
            Company company = new Company();
            company = _topContext.Companys.FirstOrDefault(x => x.company == companyParam.company);

            if (company != null)
                return null;
            _topContext.Companys.Add(companyParam);
            _topContext.SaveChanges();
            return companyParam;
        }

        public Company Get(Guid companyId)
        {
            Company company = new Company();
            company = _topContext.Companys.FirstOrDefault(x => x.Id == companyId);

            if (company == null)
                return null;

            return company;
        }

        public Company GetByName(string name)
        {
            Company company = new Company();
            company = _topContext.Companys.FirstOrDefault(x => x.company == name);

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
            Company dbCompany = new Company();
            dbCompany = _topContext.Companys.FirstOrDefault(x => x.Id == company.Id);
            dbCompany.company = company.company;
            _topContext.SaveChanges();
        }

        public IEnumerable<Company> GetAll()
        {
            return null;
        }
    }
}
