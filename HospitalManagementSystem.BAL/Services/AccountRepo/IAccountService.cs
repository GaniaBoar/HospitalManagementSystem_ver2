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

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Get refresh Token using the expired access Token for a User.
        /// </summary>
        /// <param name="refreshToken">Refresh Token that was generated while generating the Access Tokemn</param>
        /// <param name="accessToken">Access Token of User</param>
        /// <param name="now">Current DateTime, used for Expiration</param>
        /// <returns>Bearer JWT Auth Access Token</returns>
        JwtAuthResult Refresh(string refreshToken, string accessToken, DateTime now);

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Remove the all Expired Refresh Token of user by Time
        /// </summary>
        /// <param name="now">Time of Token generated</param>
        void RemoveExpiredRefreshTokens(DateTime now, CancellationToken ct = default);

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Remove refresh Token of a User.
        /// </summary>
        /// <param name="userName"></param>
        void RemoveRefreshToken(string userName, CancellationToken ct = default);

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Verify the JWT Token received from Header of current logged user.
        /// </summary>
        /// <param name="token">JWT Bearer Auth Token</param>
        /// <returns></returns>
        (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token, CancellationToken ct = default);

 

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Verify OTP, generate JWT access token, get roles, permission and claims for user.
        /// </summary>
        /// <param name="values">OTP value</param>
        /// <param name="headers">Header that contains the OTP session</param>
        /// <param name="userManager">User logged</param>
        /// <param name="roleManager">Role Manager to get the Roles</param>
        /// <returns>JWT Access Token, Roles, Claims and permission of user</returns>
        Task<object> LoginAsync(SignIn values, CancellationToken ct = default);

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 05-05-2021
        /// Method to return all Roles from Db
        /// </summary>
        /// <param name="role"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<ServiceResult> GetRoles(string role, CancellationToken ct = default);

        /// <summary>
        /// Register a new user, add to roles
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userManager"></param>
        /// <returns></returns>
        Task<ServiceResult> SignUpAsync(SignUp model, CancellationToken ct = default);

        
        Task<ServiceResult> CreateRole(RolesInputModel model, CancellationToken ct = default);

        Task<ServiceResult> UpdateRole(RolesInputModel model, CancellationToken ct = default);

       
        Task<ServiceResult> GetRoles(CancellationToken ct = default);
        Task<ServiceResult> GetUsers(CancellationToken ct = default);
        Task<ServiceResult> UpdateUser(SignUp userInput, CancellationToken ct = default);
        void Logout(string userId);
        Task ResetPassword(PasswordInputModal passwordInput);
    }
}
