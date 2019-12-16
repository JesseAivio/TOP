using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOP.API.Data.Context;
using TOP.Library.Data.models;

namespace TOP.API.Service
{
    public class VocationalQualificationUnitService : IModelService<VocationalQualificationUnit>
    {
        readonly TOPContext _topContext;

        public VocationalQualificationUnitService(TOPContext topContext)
        {
            _topContext = topContext;
        }

        public VocationalQualificationUnit Add(VocationalQualificationUnit vocationalQualificationUnitParam)
        {
            var vocationalQualificationUnit = _topContext.VocationalQualificationUnits.FirstOrDefault(x => x.vocationalQualificationUnit == vocationalQualificationUnitParam.vocationalQualificationUnit);

            if (vocationalQualificationUnit != null)
                return vocationalQualificationUnit;

            _topContext.VocationalQualificationUnits.Add(vocationalQualificationUnitParam);
            _topContext.SaveChanges();
            return vocationalQualificationUnitParam;
        }

        public VocationalQualificationUnit Get(Guid vocationalQualificationUnitId)
        {
            var vocationalQualificationUnit = _topContext.VocationalQualificationUnits.FirstOrDefault(x => x.Id == vocationalQualificationUnitId);

            if (vocationalQualificationUnit == null)
                return null;

            return vocationalQualificationUnit;
        }

        public VocationalQualificationUnit GetByName(string name)
        {
            var vocationalQualificationUnit = _topContext.VocationalQualificationUnits.FirstOrDefault(x => x.vocationalQualificationUnit == name);

            if (vocationalQualificationUnit == null)
                return null;

            return vocationalQualificationUnit;
        }
        public void Delete(VocationalQualificationUnit vocationalQualificationUnit)
        {
            _topContext.VocationalQualificationUnits.Remove(vocationalQualificationUnit);
            _topContext.SaveChanges();
        }

        public void Update(VocationalQualificationUnit vocationalQualificationUnit)
        {
            var dbVocationalQualificationUnit = _topContext.VocationalQualificationUnits.FirstOrDefault(x => x.Id == vocationalQualificationUnit.Id);
            dbVocationalQualificationUnit.vocationalQualificationUnit = vocationalQualificationUnit.vocationalQualificationUnit;
            _topContext.SaveChanges();
        }

        public IEnumerable<VocationalQualificationUnit> GetAll()
        {
            return _topContext.VocationalQualificationUnits.ToList();
        }
    }
}
