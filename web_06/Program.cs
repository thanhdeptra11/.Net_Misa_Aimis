
using BL.Service;
using Common.Model;
using DL;
using DL.Interface;
using DL.Repository;
using Model.Model;

namespace web_06
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IDbConnectionFactory, MySqlConnectionFactory>();

            // Register Employee DL & BL
            builder.Services.AddScoped<DL.Interface.IEmployeesRepository, EmployeesRepository>();
            builder.Services.AddScoped<DL.Interface.IBaseDL<Employees>, EmployeesRepository>();
            builder.Services.AddScoped<BL.Interface.IEmployeesBL, EmployeesBL>();
            builder.Services.AddScoped<BL.Interface.IBaseBL<Employees>, EmployeesBL>();

            // Register Candidates DL & BL
            builder.Services.AddScoped<DL.Interface.ICandidatesRepository, CandidatesRepository>();
            builder.Services.AddScoped<DL.Interface.IBaseDL<Candidates>, CandidatesRepository>();
            builder.Services.AddScoped<BL.Interface.ICandidatesBL, CandidatesBL>();
            builder.Services.AddScoped<BL.Interface.IBaseBL<Candidates>, CandidatesBL>();

            // Register Region DL & BL
            builder.Services.AddScoped<DL.Interface.IRegionRepository, RegionRepository>();
            builder.Services.AddScoped<DL.Interface.IBaseDL<Region, int>, RegionRepository>();
            builder.Services.AddScoped<BL.Interface.IRegionBL, RegionBL>();
            builder.Services.AddScoped<BL.Interface.IBaseBL<Region, int>, RegionBL>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            var app = builder.Build();


            app.UseMiddleware<Middleware.ExceptionHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowFrontend");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
