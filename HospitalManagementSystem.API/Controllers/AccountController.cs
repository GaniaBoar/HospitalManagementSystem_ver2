using HospitalManagementSystem.BAL.Services.AccountRepo;
using HospitalManagementSystem.Common.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {

            _accountService = accountService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUp model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _accountService.SignUpAsync(model));

        }

        [HttpPost("login")]
        public async Task<IActionResult> VerifyAsync(SignIn values,CancellationToken ct=default)
        {
            var _result = await _accountService.LoginAsync(values, ct);

            return Ok(_result);
        }

        [HttpPost("Roles/Create")]
        public async Task<IActionResult> CreateRoles(RolesInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _accountService.CreateRole(model));
        }

       

        [HttpPut("Roles/Edit/{id}")]
        public async Task<IActionResult> UpdateRoles(string id, RolesInputModel model)
        {
            if (id == null)
                return BadRequest();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _accountService.UpdateRole(model));
        }

        [HttpGet("Roles")]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _accountService.GetRoles());
        }

        [HttpGet("Users")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _accountService.GetUsers());
        }

        [HttpGet("Roles/{role}")]
        public async Task<IActionResult> GetRoles(string role)
        {
            return Ok(await _accountService.GetRoles(role));
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(string id, SignUp model)
        {
            if (id == null)
                throw new ArgumentNullException(id);
            return Ok(await _accountService.UpdateUser(model));
        }

        [HttpPut("users/{id}/resetPassword")]
        public async Task<IActionResult> ResetPasswordAsync(string id, PasswordInputModal passwordInput)
        {
            passwordInput.UserId = id;
            await _accountService.ResetPassword(passwordInput);
            return Ok();
        }
    


    [HttpPost("Logout")]
        public IActionResult Logout(string Email)
        {
            _accountService.Logout(Email);
            return Ok();
        }
    }
}
