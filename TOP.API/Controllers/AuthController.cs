using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TOP.API.Data.Enteties;
using TOP.API.Service;
using TOP.Library.Data.models;

namespace TOP.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody]Account accountParam)
        {
            try
            {
                if (accountParam is null)
                {
                    return BadRequest(new { Error = "Input is null" });
                }

                var account = _authService.Authenticate(accountParam.Username, accountParam.Password);

                if (account == null)
                    return BadRequest(new { Error = "Incorrect username or password" });

                return Ok(account);
            }
            catch (Exception ex)
            {
                string message = ex.Message + " " + ex.StackTrace;
                return BadRequest(new { Error = message });
            }
        }
        [Authorize(Roles = Role.Teacher)]
        [HttpPost("Register")]
        public IActionResult Register([FromBody]Account accountParam)
        {
            try
            {
                if (accountParam is null)
                {
                    return BadRequest(new { Error = "Input is null" });
                }

                if (_authService.Register(accountParam) == false)
                    return BadRequest(new { Error = "Username already registered" });

                return Ok(new { message = "Your account is registered!" });
            }
            catch (Exception ex)
            {
                string message = ex.Message + " " + ex.StackTrace;
                return BadRequest(new { Error = message });
            }
        }

        [HttpGet("{accountId}")]
        public IActionResult GetAccount(Guid accountId)
        {
            try
            {
                if (accountId == null)
                    return BadRequest(new { Error = "Id is null" });

                return Ok(_authService.GetAccount(accountId));
            }
            catch(Exception ex)
            {
                string message = ex.Message + " " + ex.StackTrace;
                return BadRequest(new { Error = message });
            }
        }

        [HttpGet]
        public IActionResult GetAccounts()
        {
            try
            {
                return Ok(_authService.GetAccounts());
            }
            catch(Exception ex)
            {
                string message = ex.Message + " " + ex.StackTrace;
                return BadRequest(new { Error = message });
            }
        }

        [HttpDelete("{username}")]
        public IActionResult DeleteAccount(string username)
        {
            try
            {
                _authService.DeleteAccount(username);
                return Ok(new { message = "Account deleted" });
            }
            catch(Exception ex)
            {
                string message = ex.Message + " " + ex.StackTrace;
                return BadRequest(new { Error = message });
            }
        }

        [HttpPut]
        public IActionResult UpdateAccount([FromBody]Account account)
        {
            try
            {
                _authService.UpdateAccount(account);
                return Ok(new { message = "Your account is updated!" });
            }
            catch (Exception ex)
            {
                string message = ex.Message + " " + ex.StackTrace;
                return BadRequest(new { Error = message });
            }
        }
    }
}