using MyResume.Application.Services;
using MyResume.Domain.Interfaces.Repositories;
using MyResume.Domain.Interfaces.Services;
using MyResume.Infrasctructure.Repositories;

namespace MyResume.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            builder.Services.AddControllers();
            
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

            builder.Services.AddScoped<ILocationService, LocationService>();
            builder.Services.AddScoped<ICountryRepository, CountryRepository>();
            builder.Services.AddScoped<ICityRepository, CityRepository>();

            builder.Services.AddScoped<IBranchRepository, BranchRepository>();
            builder.Services.AddScoped<IBranchService, BranchService>();

            builder.Services.AddScoped<IImageRepository, ImageRepository>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<IResumeRepository,ResumeRepository>();
            builder.Services.AddScoped<IDocumentService, DocumentService>();

            builder.Services.AddScoped<IJobSeekerRepository, JobSeekerRepository>();
            builder.Services.AddScoped<IJobSeekerService, JobSeekerService>();
            builder.Services.AddScoped<IEmployerRepository, EmployerRepository>();
            builder.Services.AddScoped<IEmployerService, EmployerService>();

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

            app.UseAuthorization();

            app.UseCors("CORSPolicy");

            app.MapControllers();

            app.Run();
        }
    }
}
