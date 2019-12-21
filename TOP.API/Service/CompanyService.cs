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
        readonly List<Company> _companies = new List<Company>();//Azure testing 
        public CompanyService(TOPContext topContext)
        {
            _topContext = topContext;
            if (AppSettings.isAzure)
            {
                AddDefaults();
            }
        }

        private void AddDefaults()
        {
            Company company = new Company()
            {
                Id = Guid.Parse("9a1ee86a-93fc-47c3-ae15-8c9873bc0d82"),
                company = "test"
            };
            Company company2 = new Company()
            {
                Id = Guid.Parse("b186664b-c83c-4a8f-bba2-a4d18b342be8"),
                company = "test2"
            };
            _companies.Add(company);
            _companies.Add(company2);
        }

        public Company Add(Company companyParam)
        {
            Company company = new Company();
            if (AppSettings.isAzure)
            {
                company = _companies.FirstOrDefault(x => x.company == companyParam.company);

                if (company != null)
                    return null;

                companyParam.Id = Guid.NewGuid();
                _companies.Add(companyParam);
                return companyParam;
            }
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
            if (AppSettings.isAzure)
            {
                company = _companies.FirstOrDefault(x => x.Id == companyId);

                if (company == null)
                    return null;

                return company;
            }
            company = _topContext.Companys.FirstOrDefault(x => x.Id == companyId);

            if (company == null)
                return null;

            return company;
        }

        public Company GetByName(string name)
        {
            Company company = new Company();
            if (AppSettings.isAzure)
            {
                company = _companies.FirstOrDefault(x => x.company == name);

                if (company == null)
                    return null;

                return company;
            }
            company = _topContext.Companys.FirstOrDefault(x => x.company == name);

            if (company == null)
                return null;

            return company;
        }

        public void Delete(Company company)
        {
            if (AppSettings.isAzure)
            {
                _companies.Remove(company);
                return;
            }
            _topContext.Companys.Remove(company);
            _topContext.SaveChanges();
        }

        public void Update(Company company)
        {
            Company dbCompany = new Company();
            if (AppSettings.isAzure)
            {
                dbCompany = _companies.FirstOrDefault(x => x.Id == company.Id);
                dbCompany.company = company.company;
                return;
            }
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
