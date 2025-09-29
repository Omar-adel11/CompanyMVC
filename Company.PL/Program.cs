using Company.BLL.Interfaces;
using Company.BLL.Repos;
using Company.DAL.Data.Contexts;
using Company.PL.Mapper;
using Company.PL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using IServiceScope = Company.PL.Services.IServiceScope;

namespace Company.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IDepartmentRepo,DepartmentRepo>(); //allow dependency injection for DepartmentRepo
            builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();//allow dependency injection for EmployeeRepo

            //builder.Services.AddTransient<DepartmentRepo>();
            //builder.Services.AddSingleton<DepartmentRepo>();

            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            }, ServiceLifetime.Scoped);//allow dependency injection for CompanyDbContext

            //Life Time
            //builder.Services.AddScoped();     //Create object life time per request (repo)
            //builder.Services.AddTransient();  //Create object life time per operation (profile)
            //builder.Services.AddSingleton();  //Create object life time per application (cache) 

            builder.Services.AddScoped<IServiceScope, ServiceScope>();     //Create object life time per request 
            builder.Services.AddTransient<IServiceTransient, ServiceTransient>();  //Create object life time per operation 
            builder.Services.AddSingleton<ISeriveSingleton, SeriveSingleton>();  //Create object life time per application 

            //builder.Services.AddAutoMapper(typeof(EmployeeProfile)); //allow dependency injection for automapper
            builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
