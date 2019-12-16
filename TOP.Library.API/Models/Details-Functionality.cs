using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TOP.Library.Data.models;

namespace TOP.Library.API.Models
{
    public class Details_Functionality
    {
        readonly Teacher_Functionality teacher_Functionality = new Teacher_Functionality();
        readonly VocationalQualificationUnit_Functionality vocationalQualificationUnit_Functionality = new VocationalQualificationUnit_Functionality();

        #region Teacher
        public async Task<IEnumerable<Teacher>> GetTeachersAsync()
        {
            return await teacher_Functionality.GetTeachersAsync();
        }

        public async Task<Teacher> AddTeacherAsync(Teacher teacher)
        {
            return await teacher_Functionality.AddTeacherAsync(teacher);
        }

        public async Task<string> UpdateTeacherAsync(Teacher teacher)
        {
            return await teacher_Functionality.UpdateTeacherAsync(teacher);
        }

        public async Task<string> DeleteTeacherAsync(Teacher teacher)
        {
            return await teacher_Functionality.DeleteTeacherAsync(teacher);
        }
        #endregion

        #region VocationalQualificationUnit
        public async Task<IEnumerable<VocationalQualificationUnit>> GetVocationalQualificationUnitsAsync()
        {
            return await vocationalQualificationUnit_Functionality.GetVocationalQualificationUnitsAsync();
        }

        public async Task<VocationalQualificationUnit> AddVocationalQualificationUnitAsync(VocationalQualificationUnit vocationalQualificationUnit)
        {
            return await vocationalQualificationUnit_Functionality.AddVocationalQualificationUnitAsync(vocationalQualificationUnit);
        }

        public async Task<string> UpdateVocationalQualificationUnitAsync(VocationalQualificationUnit vocationalQualificationUnit)
        {
            return await vocationalQualificationUnit_Functionality.UpdateVocationalQualificationUnitAsync(vocationalQualificationUnit);
        }

        public async Task<string> DeleteVocationalQualificationUnitAsync(VocationalQualificationUnit vocationalQualificationUnit)
        {
            return await vocationalQualificationUnit_Functionality.DeleteVocationalQualificationUnitAsync(vocationalQualificationUnit);
        } 
        #endregion
    }
}
