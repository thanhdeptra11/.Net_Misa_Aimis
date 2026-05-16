
using BL.Service;
using Common.DTO;
using Common.Model;
using DL;
using DL.Interface;
using DL.Repository;
using Microsoft.AspNetCore.Mvc;


namespace web_06
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                //Override hành vi mặc định của API khi model validation thất bại
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var errors = context.ModelState
                            //Giữ lại field bị lỗi
                            .Where(x => x.Value != null && x.Value.Errors.Count > 0)
                            .ToDictionary(
                                //Lấy field
                                x => x.Key,
                                //Lấy lỗi của field
                                x => x.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                            );
                        //Đồng bộ response trả về cho client
                        var response = new ApiErrorResponse
                        {
                            DevMsg = "Model validation failed.",
                            UserMsg = "Dữ liệu gửi lên không hợp lệ.",
                            StatusCode = StatusCodes.Status400BadRequest,
                            MoreInfor = errors,
                            TraceId = context.HttpContext.TraceIdentifier
                        };

                        return new BadRequestObjectResult(response);
                    };
                }); ;
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IDbConnectionFactory, MySqlConnectionFactory>();



            // Register GridConfig DL & BL
            builder.Services.AddScoped<DL.Interface.IGridConfigRepository, GridConfigRepository>();
            builder.Services.AddScoped<DL.Interface.IBaseDL<GridConfig>, GridConfigRepository>();
            builder.Services.AddScoped<BL.Interface.IGridConfigBL, GridConfigBL>();
            builder.Services.AddScoped<BL.Interface.IBaseBL<GridConfig>, GridConfigBL>();

            // Register Organization DL & BL
            builder.Services.AddScoped<DL.Interface.IOrganizationRepository, OrganizationRepository>();
            builder.Services.AddScoped<DL.Interface.IBaseDL<Organization>, OrganizationRepository>();
            builder.Services.AddScoped<BL.Interface.IOrganizationBL, OrganizationBL>();
            builder.Services.AddScoped<BL.Interface.IBaseBL<Organization>, OrganizationBL>();

            // Register SalaryComposition DL & BL
            builder.Services.AddScoped<DL.Interface.ISalaryCompositionRepository, SalaryCompositionRepository>();
            builder.Services.AddScoped<DL.Interface.IBaseDL<SalaryComposition>, SalaryCompositionRepository>();
            builder.Services.AddScoped<BL.Interface.ISalaryCompositionBL, SalaryCompositionBL>();
            builder.Services.AddScoped<BL.Interface.IBaseBL<SalaryComposition>, SalaryCompositionBL>();

            // Register SalaryCompositionSystem DL & BL
            builder.Services.AddScoped<DL.Interface.ISalaryCompositionSystemRepository, SalaryCompositionSystemRepository>();
            builder.Services.AddScoped<DL.Interface.IBaseDL<SalaryCompositionSystem>, SalaryCompositionSystemRepository>();
            builder.Services.AddScoped<BL.Interface.ISalaryCompositionSystemBL, SalaryCompositionSystemBL>();
            builder.Services.AddScoped<BL.Interface.IBaseBL<SalaryCompositionSystem>, SalaryCompositionSystemBL>();

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
