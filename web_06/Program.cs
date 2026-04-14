
using BL.Service;
using Common.Model;
using DL;
using DL.Interface;
using DL.Repository;

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

            // Register Customer DL & BL
            builder.Services.AddScoped<DL.Interface.ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<DL.Interface.IBaseDL<Customer>, CustomerRepository>();
            builder.Services.AddScoped<BL.Interface.ICustomerBL, CustomerBL>();
            builder.Services.AddScoped<BL.Interface.IBaseBL<Customer>, CustomerBL>();

            // Register SaleOrder DL & BL
            builder.Services.AddScoped<DL.Interface.ISaleOrderRepository, SaleOrderRepository>();
            builder.Services.AddScoped<DL.Interface.IBaseDL<SaleOrder>, SaleOrderRepository>();
            builder.Services.AddScoped<BL.Interface.ISaleOrderBL, SaleOrderBL>();
            builder.Services.AddScoped<BL.Interface.IBaseBL<SaleOrder>, SaleOrderBL>();

            // Register SaleOrderDetail DL & BL
            builder.Services.AddScoped<DL.Interface.ISaleOrderDetailRepository, SaleOrderDetailRepository>();
            builder.Services.AddScoped<DL.Interface.IBaseDL<SaleOrderDetail>, SaleOrderDetailRepository>();
            builder.Services.AddScoped<BL.Interface.ISaleOrderDetailBL, SaleOrderDetailBL>();
            builder.Services.AddScoped<BL.Interface.IBaseBL<SaleOrderDetail>, SaleOrderDetailBL>();

            // Register Employee DL & BL
            builder.Services.AddScoped<DL.Interface.IEmployeesRepository, EmployeesRepository>();
            builder.Services.AddScoped<DL.Interface.IBaseDL<Employees>, EmployeesRepository>();
            builder.Services.AddScoped<BL.Interface.IEmployeesBL, EmployeesBL>();
            builder.Services.AddScoped<BL.Interface.IBaseBL<Employees>, EmployeesBL>();


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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
