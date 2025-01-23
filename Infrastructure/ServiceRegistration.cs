﻿using Application.Common.Interfaces;
using Application.Exceptions;
using Application.Interfaces.Repositories.AppointmentRepository;
using Application.Interfaces.Repositories.CertificationRepository;
using Application.Interfaces.Repositories.CustomerMessageRepository;
using Application.Interfaces.Repositories.ExtracurricularActivitiesRepository;
using Application.Interfaces.Repositories.HealthTestRepository;
using Application.Interfaces.Repositories.PatientRepository;
using Application.Interfaces.Repositories.TeacherRepository;
using Application.Interfaces.Repositories.UploadFileMorphRepository;
using Application.Interfaces.Repositories.UploadFileRepository;
using Application.Interfaces.Services.AppointmentService;
using Application.Interfaces.Services.CertificationService;
using Application.Interfaces.Services.CustomerMessageService;
using Application.Interfaces.Services.EmailSenderService;
using Application.Interfaces.Services.ExtracurricularActivitiesService;
using Application.Interfaces.Services.HealthTestService;
using Application.Interfaces.Services.PatientService;
using Application.Interfaces.Services.TeacherService;
using Application.Interfaces.Services.UploadFileService;
using Domain.Settings;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories.AppointmentRepository;
using Infrastructure.Persistence.Repositories.CertificationRepository;
using Infrastructure.Persistence.Repositories.CustomerMessageRepository;
using Infrastructure.Persistence.Repositories.ExtracurricularActivitiesRepository;
using Infrastructure.Persistence.Repositories.HealthTestRepository;
using Infrastructure.Persistence.Repositories.PatientRepository;
using Infrastructure.Persistence.Repositories.TeacherRepository;
using Infrastructure.Persistence.Repositories.UploadFileMorphRepository;
using Infrastructure.Persistence.Repositories.UploadFileRepository;
using Infrastructure.Persistence.Services.AppointmentService;
using Infrastructure.Persistence.Services.CertificationService;
using Infrastructure.Persistence.Services.CustomerMessageService;
using Infrastructure.Persistence.Services.EmailSenderService;
using Infrastructure.Persistence.Services.HealthTestService;
using Infrastructure.Persistence.Services.IpHelperService;
using Infrastructure.Persistence.Services.PatientService;
using Infrastructure.Persistence.Services.TeacherService;
using Infrastructure.Persistence.Services.UploadFileService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ApiExceptionFilter>();
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                     builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<ApplicationDbContextInitialiser>();


            services.AddAuthenticationConfigure(configuration);
            services.AddTransient<ICertificationService, CertificationService>();
            services.AddTransient<IIpHelperService, IpHelperService>();
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IUploadFileService, UploadFileService>();
            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<ITeacherService, TeacherService>();
            services.AddTransient<IExtracurricularActivitiesService, ExtracurricularActivitiesService>();


            services.AddTransient(typeof(Repository<>), typeof(Repository<>));
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IUploadFileRepository, UploadFileRepository>();
            services.AddTransient<IUploadFileMorphRepository, UploadFileMorphRepository>();
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<ITeacherRepository, TeacherRepository>();
            services.AddTransient<ICertificationRepository, CertificationRepository>();
            services.AddTransient<IExtracurricularActivitiesRepository, ExtracurricularActivitiesRepository>();

            //health Test
            services.AddTransient<IHealthTestService, HealthTestService>();
            services.AddTransient<IHealthTestRepository, HealthTestRepository>();


            //custom Message
            services.AddTransient<ICustomerMessageService, CustomerMessageService>();
            services.AddTransient<ICustomerMessageRepository, CustomerMessageRepository>();
            services.AddTransient<IEmailSender, EmailSender>();





        }

        public static void AddAuthenticationConfigure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSend"));
            services.Configure<JWTSettings>(configuration.GetSection("Jwt"));
            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]))
            });
        }
    }
}
