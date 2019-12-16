using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOP.API.Data.Enteties;
using TOP.Library.Data.models;

namespace TOP.API.Service
{
    public interface IMainService
    {
        bool AddTOP(TOPModel topModel);
        void DeleteTOP(Guid topId);
        TOPModel GetTOP(Guid topId);
        IEnumerable<TOPModel> GetTOPs();
        void UpdateTOP(TOPModel topModel);
        IEnumerable<Teacher> GetTeachers();
        Teacher AddTeacher(Teacher teacher);
        void UpdateTeacher(Teacher teacher);
        void DeleteTeacher(Guid Id);
        IEnumerable<VocationalQualificationUnit> GetVocationalQualificationUnits();
        VocationalQualificationUnit AddVocationalQualificationUnit(VocationalQualificationUnit vocationalQualificationUnit);
        void UpdateVocationalQualificationUnit(VocationalQualificationUnit vocationalQualificationUnit);
        void DeleteVocationalQualificationUnit(Guid Id);
    }

    public class MainService : IMainService
    {
        readonly IModelService<Company> _companyService;
        readonly IModelService<Address> _addressService;
        readonly IModelService<Teacher> _teacherService;
        readonly IModelService<VocationalQualificationUnit> _vocationalQualificationUnitService;
        readonly ITOPService _topService;

        public MainService(IModelService<Company> companyService, 
            IModelService<Address> addressService,
            IModelService<Teacher> teacherService,
            IModelService<VocationalQualificationUnit> vocationalQualificationUnitService,
            ITOPService topService)
        {
            _companyService = companyService;
            _addressService = addressService;
            _teacherService = teacherService;
            _vocationalQualificationUnitService = vocationalQualificationUnitService;
            _topService = topService;
        }

        #region TOP
        public bool AddTOP(TOPModel topModel)
        {
            if (CheckDetails(topModel) == false)
                return false;

            AddTOPTable(topModel);
            return true;
        }

        public void DeleteTOP(Guid topId)
        {
            var top = GetTOPTable(topId);
            _topService.DeleteTOP(top);
            DeleteCompany(top.Company);
            DeleteAddress(top.Address);
        }

        public TOPModel GetTOP(Guid topId)
        {
            var topTable = GetTOPTable(topId);
            var company = GetCompany(topTable.Company);
            var address = GetAddress(topTable.Address);
            var teacher = GetTeacher(topTable.Teacher);
            var vocationalQualificationUnit = GetVocationalQualificationUnit(topTable.VocationalQualificationUnit);

            TOPModel top = new TOPModel
            {
                PhoneNumber = topTable.PhoneNumber,
                EmailAddress = topTable.EmailAddress,
                Reserved = topTable.Reserved,
                ReservationEnds = topTable.ReservationEnds,
                Accepted = topTable.Accepted,
                Info = topTable.Info,
                Company = company.company,
                Teacher = teacher.teacher,
                Address = address.address,
                VocationalQualificationUnit = vocationalQualificationUnit.vocationalQualificationUnit
            };
            return top;
        }

        public IEnumerable<TOPModel> GetTOPs()
        {
            IEnumerable<TOPTable> topTables = _topService.GetTOPs();
            List<TOPModel> topModels = new List<TOPModel>();

            foreach (var topTable in topTables)
            {
                var company = GetCompany(topTable.Company);
                var address = GetAddress(topTable.Address);
                var teacher = GetTeacher(topTable.Teacher);
                var vocationalQualificationUnit = GetVocationalQualificationUnit(topTable.VocationalQualificationUnit);

                TOPModel top = new TOPModel
                {
                    Id = topTable.Id,
                    PhoneNumber = topTable.PhoneNumber,
                    EmailAddress = topTable.EmailAddress,
                    Reserved = topTable.Reserved,
                    ReservationEnds = topTable.ReservationEnds,
                    Accepted = topTable.Accepted,
                    Info = topTable.Info,
                    Company = company.company,
                    Teacher = teacher.teacher,
                    Address = address.address,
                    VocationalQualificationUnit = vocationalQualificationUnit.vocationalQualificationUnit
                };
                topModels.Add(top);
            }
            return topModels;
        }

        public void UpdateTOP(TOPModel topModel)
        {
            var top = GetTOPTable(topModel.Id);
            UpdateCompany(top.Company, topModel.Company);
            UpdateAddress(top.Address, topModel.Address);

            top.PhoneNumber = topModel.PhoneNumber;
            top.EmailAddress = topModel.EmailAddress;
            top.Reserved = topModel.Reserved;
            top.ReservationEnds = topModel.ReservationEnds;
            top.Accepted = topModel.Accepted;
            top.Info = topModel.Info;
            top.Teacher = ChangeTeacher(topModel.Teacher).Id;
            top.VocationalQualificationUnit = ChangeVocationalQualificationUnit(topModel.VocationalQualificationUnit).Id;

            _topService.UpdateTOP(top);
        }
        #endregion

        #region Teacher
        public IEnumerable<Teacher> GetTeachers()
        {
            return _teacherService.GetAll();
        }

        public Teacher AddTeacher(Teacher teacher)
        {
            return _teacherService.Add(teacher);
        }

        public void UpdateTeacher(Teacher teacher)
        {
            _teacherService.Update(teacher);
        }

        public void DeleteTeacher(Guid Id)
        {
            var teacher = _teacherService.Get(Id);
            _teacherService.Delete(teacher);
        }
        #endregion

        #region VocationalQualificationUnit
        public IEnumerable<VocationalQualificationUnit> GetVocationalQualificationUnits()
        {
            return _vocationalQualificationUnitService.GetAll();
        }

        public VocationalQualificationUnit AddVocationalQualificationUnit(VocationalQualificationUnit vocationalQualificationUnit)
        {
            return _vocationalQualificationUnitService.Add(vocationalQualificationUnit);
        }

        public void UpdateVocationalQualificationUnit(VocationalQualificationUnit vocationalQualificationUnit)
        {
            _vocationalQualificationUnitService.Update(vocationalQualificationUnit);
        }

        public void DeleteVocationalQualificationUnit(Guid Id)
        {
            var vocationalQualificationUnit = _vocationalQualificationUnitService.Get(Id);
            _vocationalQualificationUnitService.Delete(vocationalQualificationUnit);
        } 
        #endregion

        #region Update
        private void UpdateCompany(Guid companyId, string companyName)
        {
            Company company = new Company
            {
                Id = companyId,
                company = companyName
            };
            _companyService.Update(company);
        }

        private void UpdateAddress(Guid addressId, string addressName)
        {
            Address address = new Address
            {
                Id = addressId,
                address = addressName
            };
            _addressService.Update(address);
        }

        private Teacher ChangeTeacher(string teacherName)
        {
            return _teacherService.GetByName(teacherName);
        }

        private VocationalQualificationUnit ChangeVocationalQualificationUnit(string vocationalQualificationUnitName)
        {
            return _vocationalQualificationUnitService.GetByName(vocationalQualificationUnitName);
        }
        #endregion
        #region Get
        private TOPTable GetTOPTable(Guid topId)
        {
            return _topService.GetTOP(topId);
        }

        private Company GetCompany(Guid companyId)
        {
            var company = _companyService.Get(companyId);
            return company;
        }

        private Address GetAddress(Guid addressId)
        {
            var address = _addressService.Get(addressId);
            return address;
        }

        private Teacher GetTeacher(Guid teacherId)
        {
            var teacher = _teacherService.Get(teacherId);
            return teacher;
        }

        private VocationalQualificationUnit GetVocationalQualificationUnit(Guid vocationalQualificationUnitId)
        {
            var vocationalQualificationUnit = _vocationalQualificationUnitService.Get(vocationalQualificationUnitId);
            return vocationalQualificationUnit;
        } 
        #endregion
        #region Delete
        private void DeleteCompany(Guid companyId)
        {
            var company = _companyService.Get(companyId);
            _companyService.Delete(company);
        }

        private void DeleteAddress(Guid addressId)
        {
            var address = _addressService.Get(addressId);
            _addressService.Delete(address);
        } 
        #endregion
        #region Adding
        private bool CheckDetails(TOPModel topModel)
        {
            var company = _companyService.Add(CreateCompany(topModel.Company));
            var address = _addressService.Add(CreateAddress(topModel.Address));
            var teacher = _teacherService.Add(CreateTeacher(topModel.Teacher));
            var vocationalQualificationUnit = _vocationalQualificationUnitService.Add(
                CreateVocationalQualificationUnit(topModel.VocationalQualificationUnit));
            if (company == null || address == null || teacher == null || vocationalQualificationUnit == null)
                return false;
            return true;
        }

        private void AddTOPTable(TOPModel topModel)
        {
            var company = _companyService.GetByName(topModel.Company);
            var address = _addressService.GetByName(topModel.Address);
            var teacher = _teacherService.GetByName(topModel.Teacher);
            var vocationalQualificationUnit = _vocationalQualificationUnitService.GetByName(topModel.VocationalQualificationUnit);
            TOPTable top = new TOPTable
            {
                PhoneNumber = topModel.PhoneNumber,
                EmailAddress = topModel.EmailAddress,
                Reserved = topModel.Reserved,
                ReservationEnds = topModel.ReservationEnds,
                Accepted = topModel.Accepted,
                Info = topModel.Info,
                Company = company.Id,
                Address = address.Id,
                Teacher = teacher.Id,
                VocationalQualificationUnit = vocationalQualificationUnit.Id
            };
            _topService.AddTOP(top);
        } 
        #endregion
        #region Create
        private Company CreateCompany(string companyName)
        {
            Company company = new Company
            {
                company = companyName
            };
            return company;
        }

        private Address CreateAddress(string addressName)
        {
            Address address = new Address
            {
                address = addressName
            };
            return address;
        }

        private Teacher CreateTeacher(string teacherName)
        {
            Teacher teacher = new Teacher
            {
                teacher = teacherName
            };
            return teacher;
        }

        private VocationalQualificationUnit CreateVocationalQualificationUnit(string vocationalQualificationUnitName)
        {
            VocationalQualificationUnit vocationalQualificationUnit = new VocationalQualificationUnit
            {
                vocationalQualificationUnit = vocationalQualificationUnitName
            };
            return vocationalQualificationUnit;
        }
        #endregion
    }
}
