using Application.Common.Interfaces;
using Application.Exceptions;
using Application.Interfaces.Repositories.AcademicsRepository;
using Application.Interfaces.Repositories.CertificationRepository;
using Application.Interfaces.Repositories.CustomerMessageRepository;
using Application.Interfaces.Repositories.ExtracurricularActivitiesRepository;
using Application.Interfaces.Repositories.StudentRepository;
using Application.Interfaces.Repositories.TeacherRepository;
using Application.Interfaces.Repositories.UploadFileMorphRepository;
using Application.Interfaces.Repositories.UploadFileRepository;
using Application.Interfaces.Services.AcademicsService;
using Application.Interfaces.Services.CertificationService;
using Application.Interfaces.Services.CustomerMessageService;
using Application.Interfaces.Services.EmailSenderService;
using Application.Interfaces.Services.ExtracurricularActivitiesService;
using Application.Interfaces.Services.StudentPortfolioService;
using Application.Interfaces.Services.StudentService;
using Application.Interfaces.Services.TeacherService;
using Application.Interfaces.Services.UploadFileService;
using Domain.Settings;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories.AcademicsRepository;
using Infrastructure.Persistence.Repositories.CertificationRepositorys;
using Infrastructure.Persistence.Repositories.CustomerMessageRepository;
using Infrastructure.Persistence.Repositories.ExtracurricularActivitiesRepository;
using Infrastructure.Persistence.Repositories.StudentRepository;
using Infrastructure.Persistence.Repositories.TeacherRepository;
using Infrastructure.Persistence.Repositories.UploadFileMorphRepository;
using Infrastructure.Persistence.Repositories.UploadFileRepository;
using Infrastructure.Persistence.Services.AcademicsService;
using Infrastructure.Persistence.Services.CertificationService;
using Infrastructure.Persistence.Services.CustomerMessageService;
using Infrastructure.Persistence.Services.EmailSenderService;
using Infrastructure.Persistence.Services.IpHelperService;
using Infrastructure.Persistence.Services.StudentPortfolioService;
using Infrastructure.Persistence.Services.StudentService;
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
            services.AddTransient<IUploadFileService, UploadFileService>();
            services.AddTransient<ITeacherService, TeacherService>();
            services.AddTransient<IExtracurricularActivitiesService, ExtracurricularActivitiesService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IAcademicsService, AcademicsService>();
            services.AddScoped<IStudentPortfolioService, StudentPortfolioService>();

            services.AddTransient(typeof(Repository<>), typeof(Repository<>));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ICertificationRepository, CertificationRepository>();
            services.AddTransient<IUploadFileRepository, UploadFileRepository>();
            services.AddTransient<IUploadFileMorphRepository, UploadFileMorphRepository>();
            services.AddTransient<ITeacherRepository, TeacherRepository>();
            services.AddTransient<IExtracurricularActivitiesRepository, ExtracurricularActivitiesRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IAcademicsRepository, AcademicsRepository>();


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
