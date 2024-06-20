using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MyResume.API.Hubs;
using MyResume.Application.Services;
using MyResume.Domain.Interfaces;
using MyResume.Domain.Interfaces.Repositories;
using MyResume.Domain.Interfaces.Services;
using MyResume.Infrasctructure;
using MyResume.Infrasctructure.Repositories;
using System.Text;

namespace MyResume.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            builder.Services.AddControllers();

            #region JwtAuthentication
            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
            var jwtSection = builder.Configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
            
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.SecretKey!))
                };
            });
            #endregion

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CORSPolicy", builder =>
                {
                    builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithOrigins("https://localhost:7266", "http://localhost:5128");
                });
            });

            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblies(typeof(Application.AssemblyReference).Assembly));

            #region IoC
            builder.Services.AddScoped<ILocationService, LocationService>();
            builder.Services.AddScoped<ICountryRepository, CountryRepository>();
            builder.Services.AddScoped<ICityRepository, CityRepository>();

            builder.Services.AddScoped<IBranchRepository, BranchRepository>();
            builder.Services.AddScoped<IBranchService, BranchService>();

            builder.Services.AddScoped<IImageRepository, ImageRepository>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<IResumeRepository,ResumeRepository>();
            builder.Services.AddScoped<IVacancyRepository, VacancyRepository>();
            builder.Services.AddScoped<IDocumentService, DocumentService>();
            builder.Services.AddScoped<IVacancyService,  VacancyService>();

            builder.Services.AddScoped<IJobSeekerRepository, JobSeekerRepository>();
            builder.Services.AddScoped<IJobSeekerService, JobSeekerService>();
            builder.Services.AddScoped<IEmployerRepository, EmployerRepository>();
            builder.Services.AddScoped<IEmployerService, EmployerService>();

            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<IJwtProvider, JwtProvider>();
            builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
            #endregion

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("CORSPolicy");
            
            app.MapControllers();
            app.MapBlazorHub();
            app.MapHub<ChatHub>("/chathub");

            app.Run();
        }
    }
}
