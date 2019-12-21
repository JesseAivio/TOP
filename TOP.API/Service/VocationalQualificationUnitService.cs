using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOP.API.Data.Context;
using TOP.API.Data.Helpers;
using TOP.Library.Data.models;

namespace TOP.API.Service
{
    public class VocationalQualificationUnitService : IModelService<VocationalQualificationUnit>
    {
        readonly TOPContext _topContext;
        readonly List<VocationalQualificationUnit> _vocationalQualificationUnits = new List<VocationalQualificationUnit>();//Azure testing

        public VocationalQualificationUnitService(TOPContext topContext)
        {
            _topContext = topContext;
            if (AppSettings.isAzure)
            {
                AddDefault();
            }
        }

        private void AddDefault()
        {
            VocationalQualificationUnit vocationalQualificationUnit = new VocationalQualificationUnit()
            {
                Id = Guid.Parse("768b6a66-e2b4-4d18-95b8-18c402a7cf3a"),
                vocationalQualificationUnit = "Test"
            };
            VocationalQualificationUnit vocationalQualificationUnit2 = new VocationalQualificationUnit()
            {
                Id = Guid.Parse("6be50d9b-fbf8-4cda-8bb1-84b93c01b1a5"),
                vocationalQualificationUnit = "Test2"
            };
            _vocationalQualificationUnits.Add(vocationalQualificationUnit);
            _vocationalQualificationUnits.Add(vocationalQualificationUnit2);

        }

        public VocationalQualificationUnit Add(VocationalQualificationUnit vocationalQualificationUnitParam)
        {
            VocationalQualificationUnit vocationalQualificationUnit = new VocationalQualificationUnit();
            if (AppSettings.isAzure)
            {
                vocationalQualificationUnit = _vocationalQualificationUnits.FirstOrDefault(
                    x => x.vocationalQualificationUnit == vocationalQualificationUnitParam.vocationalQualificationUnit);

                if (vocationalQualificationUnit != null)
                    return vocationalQualificationUnit;

                vocationalQualificationUnitParam.Id = Guid.NewGuid();
                _vocationalQualificationUnits.Add(vocationalQualificationUnitParam);
                return vocationalQualificationUnitParam;
            }

            vocationalQualificationUnit = _topContext.VocationalQualificationUnits.FirstOrDefault(
                x => x.vocationalQualificationUnit == vocationalQualificationUnitParam.vocationalQualificationUnit);

            if (vocationalQualificationUnit != null)
                return vocationalQualificationUnit;

            _topContext.VocationalQualificationUnits.Add(vocationalQualificationUnitParam);
            _topContext.SaveChanges();
            return vocationalQualificationUnitParam;
        }

        public VocationalQualificationUnit Get(Guid vocationalQualificationUnitId)
        {
            VocationalQualificationUnit vocationalQualificationUnit = new VocationalQualificationUnit();
            if (AppSettings.isAzure)
            {
                vocationalQualificationUnit = _vocationalQualificationUnits.FirstOrDefault(x => x.Id == vocationalQualificationUnitId);

                if (vocationalQualificationUnit == null)
                    return null;

                return vocationalQualificationUnit;
            }
            vocationalQualificationUnit = _topContext.VocationalQualificationUnits.FirstOrDefault(x => x.Id == vocationalQualificationUnitId);

            if (vocationalQualificationUnit == null)
                return null;

            return vocationalQualificationUnit;
        }

        public VocationalQualificationUnit GetByName(string name)
        {
            VocationalQualificationUnit vocationalQualificationUnit = new VocationalQualificationUnit();
            if (AppSettings.isAzure)
            {
                vocationalQualificationUnit = _vocationalQualificationUnits.FirstOrDefault(
                    x => x.vocationalQualificationUnit == name);

                if (vocationalQualificationUnit == null)
                    return null;

                return vocationalQualificationUnit;
            }
            vocationalQualificationUnit = _topContext.VocationalQualificationUnits.FirstOrDefault(
                x => x.vocationalQualificationUnit == name);

            if (vocationalQualificationUnit == null)
                return null;

            return vocationalQualificationUnit;
        }
        public void Delete(VocationalQualificationUnit vocationalQualificationUnit)
        {
            if (AppSettings.isAzure)
            {
                _vocationalQualificationUnits.Remove(vocationalQualificationUnit);
                return;
            }
            _topContext.VocationalQualificationUnits.Remove(vocationalQualificationUnit);
            _topContext.SaveChanges();
        }

        public void Update(VocationalQualificationUnit vocationalQualificationUnit)
        {
            VocationalQualificationUnit dbVocationalQualificationUnit = new VocationalQualificationUnit();
            if (AppSettings.isAzure)
            {
                dbVocationalQualificationUnit = _vocationalQualificationUnits.FirstOrDefault(
                    x => x.Id == vocationalQualificationUnit.Id);
                dbVocationalQualificationUnit.vocationalQualificationUnit = vocationalQualificationUnit.vocationalQualificationUnit;
                return;
            }
            dbVocationalQualificationUnit = _topContext.VocationalQualificationUnits.FirstOrDefault(
                x => x.Id == vocationalQualificationUnit.Id);
            dbVocationalQualificationUnit.vocationalQualificationUnit = vocationalQualificationUnit.vocationalQualificationUnit;
            _topContext.SaveChanges();
        }

        public IEnumerable<VocationalQualificationUnit> GetAll()
        {
            if (AppSettings.isAzure)
            {
                return _vocationalQualificationUnits.ToList();
            }
            return _topContext.VocationalQualificationUnits.ToList();
        }
    }
}
