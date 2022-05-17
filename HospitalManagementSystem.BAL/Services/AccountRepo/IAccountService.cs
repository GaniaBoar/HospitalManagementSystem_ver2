using HospitalManagementSystem.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.BAL.Services.AccountRepo
{
    public interface IAccountService
    {
        Task<IdentityResult> SignUpAsync(SignUp signUp);
        Task<string> LoginAsync(SignIn signIn);
        void Logout(string userId);

    }
}
