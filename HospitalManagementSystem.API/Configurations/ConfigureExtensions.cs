﻿using HospitalManagementSystem.BAL.Services.AccountRepo;
using HospitalManagementSystem.BAL.Services.BedConfigRepo;
using HospitalManagementSystem.BAL.Services.BedNoRepo;
using HospitalManagementSystem.BAL.Services.BedRepo;
using HospitalManagementSystem.BAL.Services.BillRepo;
using HospitalManagementSystem.BAL.Services.MedicineRepo;
using HospitalManagementSystem.BAL.Services.PatientRepo;
using HospitalManagementSystem.BAL.Services.StockRepo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace HospitalManagementSystem.API.Configurations
{
    public static class ConfigureExtensions
    {
        static readonly string _authTokenPolicy = "_@JwtAuthPolicy";
        public static void CorsConfiguration(this IServiceCollection services, IConfiguration configuration, string policyName)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(policyName,
                    builder =>
                    builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                    .AllowCredentials());
            });
        }

        public static void SwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HospitalManagement", Version = "v1" });
                c.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "BearerAuth" }
                        },
                        new string[] {}
                    }
                });
            });

        }

        public static void ConfigureAuth(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration.GetSection("JWTSettings:Issuer").Value,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("JWTSettings:SecretKey").Value)),
                    ValidAudience = configuration.GetSection("JWTSettings:Audience").Value,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            })
            .AddCookie();

            //services.AddAuthorization(opt => opt.
            //AddPolicy(_authTokenPolicy, policy =>
            //{
            //    policy.RequireClaim(CustomClaimTypes.Permission);
            //}));
        }
        public static IServiceCollection DIBALResolver(this IServiceCollection services,IConfiguration configuration)
        {
            // services.AddScoped<interface, service>(); reference
            services.AddScoped<IMedicineService, MedicineService>();
            services.AddScoped<IBedTypeService, BedTypeService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IPatientRegistrationService, PatientRegistrationService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IBedConfiService, BedConfigService>();
            services.AddScoped<IBedAllocationRepo, BedAllocationRepo>();
            return services;
        }
    }
}
