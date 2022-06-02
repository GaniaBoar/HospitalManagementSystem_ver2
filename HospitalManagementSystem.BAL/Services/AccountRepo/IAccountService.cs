using HospitalManagementSystem.Common.Entities;
using HospitalManagementSystem.Common.Modal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalManagementSystem.BAL.Services.AccountRepo
{
    public interface IAccountService
    {
      
        IImmutableDictionary<string, RefreshToken> UsersRefreshTokensReadOnlyDictionary { get; }

        
        JwtAuthResult GenerateTokens(string username, Claim[] claims, DateTime now);

        JwtAuthResult Refresh(string refreshToken, string accessToken, DateTime now);

        void RemoveExpiredRefreshTokens(DateTime now, CancellationToken ct = default);

        
        void RemoveRefreshToken(string userName, CancellationToken ct = default);

        (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token, CancellationToken ct = default);

       
        Task<ServiceResult> GenerateOTP(string phoneNumber, CancellationToken ct = default);

       
        Task<object> LoginAsync(SignIn values, CancellationToken ct = default);

       
        Task<ServiceResult> GetRoles(string role, CancellationToken ct = default);

        Task<ServiceResult> SignUpAsync(SignUp model, CancellationToken ct = default);

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Creates Roles
        /// </summary>
        /// <param name="model"></param>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        Task<ServiceResult> CreateRole(RolesInputModel model, CancellationToken ct = default);

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Edit Role and its permissions
        /// </summary>
        /// <param name="model">Role Input details from Client</param>
        /// <param name="roleManager">Identity RoleManager Service</param>
        /// <returns>Service Result containing the process result.</returns>
        Task<ServiceResult> UpdateRole(RolesInputModel model, CancellationToken ct = default);

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Awaitable method to return all roles with their permissions
        /// </summary>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        Task<ServiceResult> GetRoles(CancellationToken ct = default);
        Task<ServiceResult> UpdateUser(SignUp userInput, CancellationToken ct = default);
        void Logout(string userId);
        Task ResetPassword(PasswordInputModal passwordInput);
    }
}
