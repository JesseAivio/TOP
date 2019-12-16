using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TOP.API.Data.Enteties;
using TOP.API.Service;
using TOP.Library.Data.models;

namespace TOP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TOPController : ControllerBase
    {
        readonly IMainService _mainService;

        public TOPController(IMainService mainService)
        {
            _mainService = mainService;
        }

        #region TOP
        [HttpGet("{topId}")]
        public IActionResult GetTOP(Guid topId)
        {
            try
            {
                if (topId == null)
                    return BadRequest(new { Error = "id is null" });
                var top = _mainService.GetTOP(topId);
                return Ok(top);
            }
            catch (Exception ex)
            {
                string message = ex.Message + " " + ex.StackTrace;
                return BadRequest(new { Error = message });
            }
        }

        [HttpGet]
        public IActionResult GetTOPs()
        {
            try
            {
                return Ok(_mainService.GetTOPs());
            }
            catch (Exception ex)
            {
                string message = ex.Message + " " + ex.StackTrace;
                return BadRequest(new { Error = message });
            }
        }

        [HttpPost]
        public IActionResult AddTOP([FromBody] TOPModel topModel)
        {
            try
            {
                if (topModel == null)
                    return BadRequest(new { Error = "top is null" });

                if (_mainService.AddTOP(topModel) == false)
                    return BadRequest(new { Error = "Error cant add TOP" });

                return Ok(new { message = "TOP is added to database" });
            }
            catch (Exception ex)
            {
                string message = ex.Message + " " + ex.StackTrace;
                return BadRequest(new { Error = message });
            }
        }

        [HttpDelete("{topId}")]
        public IActionResult DeleteTOP(Guid topId)
        {
            try
            {
                if (topId == null)
                    return BadRequest(new { Error = "id is null" });

                _mainService.DeleteTOP(topId);
                return Ok(new { message = "TOP is deleted from database" });
            }
            catch (Exception ex)
            {
                string message = ex.Message + " " + ex.StackTrace;
                return BadRequest(new { Error = message });
            }
        }

        [HttpPut]
        public IActionResult UpdateTOP([FromBody] TOPModel topModel)
        {
            try
            {
                if (topModel == null)
                    return BadRequest(new { Error = "top is null" });

                _mainService.UpdateTOP(topModel);

                return Ok(new { message = "TOP is updated to database" });
            }
            catch (Exception ex)
            {
                string message = ex.Message + " " + ex.StackTrace;
                return BadRequest(new { Error = message });
            }
        }
        #endregion

        #region Teacher
        [HttpGet("Teacher")]
        public IActionResult GetTeachers()
        {
            try
            {
                return Ok(_mainService.GetTeachers());
            }
            catch (Exception ex)
            {
                string message = ex.Message + " " + ex.StackTrace;
                return BadRequest(new { Error = message });
            }
        }

        [HttpPut("Teacher")]
        public IActionResult UpdateTeacher([FromBody] Teacher teacher)
        {
            try
            {
                if (teacher == null)
                    return BadRequest(new { Error = "teacher is null" });

                _mainService.UpdateTeacher(teacher);

                return Ok(new { message = "Teacher updated to database" });
            }
            catch (Exception ex)
            {
                string message = ex.Message + " " + ex.StackTrace;
                return BadRequest(new { Error = message });
            }
        }

        [HttpDelete("Teacher/{Id}")]
        public IActionResult DeleteTeacher(Guid Id)
        {
            try
            {
                if (Id == null)
                    return BadRequest(new { Error = "Id is null" });

                _mainService.DeleteTeacher(Id);

                return Ok(new { message = "Teacher deleted from database" });
            }
            catch (Exception ex)
            {
                string message = ex.Message + " " + ex.StackTrace;
                return BadRequest(new { Error = message });
            }
        }

        [HttpPost("Teacher")]
        public IActionResult AddTeacher([FromBody] Teacher teacher)
        {
            try
            {
                if (teacher == null)
                    return BadRequest(new { Error = "teacher is null" });

                if (_mainService.AddTeacher(teacher) == null)
                    return BadRequest(new { Error = "Can't add teacher" });

                return Ok(new { message = "Teacher added to database" });
            }
            catch (Exception ex)
            {
                string message = ex.Message + " " + ex.StackTrace;
                return BadRequest(new { Error = message });
            } 
            
        }
        #endregion

        #region VocationalQualificationUnit
        [HttpGet("VocationalQualificationUnit")]
        public IActionResult GetVocationalQualificationUnits()
        {
            try
            {
                return Ok(_mainService.GetVocationalQualificationUnits());
            }
            catch (Exception ex)
            {
                string message = ex.Message + " " + ex.StackTrace;
                return BadRequest(new { Error = message });
            }
        }

        [HttpPut("VocationalQualificationUnit")]
        public IActionResult UpdateVocationalQualificationUnit([FromBody] VocationalQualificationUnit vocationalQualificationUnit)
        {
            try
            {
                if (vocationalQualificationUnit == null)
                    return BadRequest(new { Error = "vocationalQualificationUnit is null" });

                _mainService.UpdateVocationalQualificationUnit(vocationalQualificationUnit);

                return Ok(new { message = "VocationalQualificationUnit updated to database" });
            }
            catch (Exception ex)
            {
                string message = ex.Message + " " + ex.StackTrace;
                return BadRequest(new { Error = message });
            }
        }

        [HttpDelete("VocationalQualificationUnit/{Id}")]
        public IActionResult DeleteVocationalQualificationUnit(Guid Id)
        {
            try
            {
                if (Id == null)
                    return BadRequest(new { Error = "Id is null" });

                _mainService.DeleteVocationalQualificationUnit(Id);

                return Ok(new { message = "VocationalQualificationUnit deleted from database" });
            }
            catch (Exception ex)
            {
                string message = ex.Message + " " + ex.StackTrace;
                return BadRequest(new { Error = message });
            }
        }

        [HttpPost("VocationalQualificationUnit")]
        public IActionResult AddVocationalQualificationUnit([FromBody] VocationalQualificationUnit vocationalQualificationUnit)
        {
            try
            {
                if (vocationalQualificationUnit == null)
                    return BadRequest(new { Error = "vocationalQualificationUnit is null" });

                if (_mainService.AddVocationalQualificationUnit(vocationalQualificationUnit) == null)
                    return BadRequest(new { Error = "Can't add vocationalQualificationUnit" });

                return Ok(new { message = "VocationalQualificationUnit added to database" });
            }
            catch (Exception ex)
            {
                string message = ex.Message + " " + ex.StackTrace;
                return BadRequest(new { Error = message });
            }
        } 
        #endregion
    }
}