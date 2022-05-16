using AslMonitor.DAL;
using AslMonitor.DAL.Models;
using AslMonitor.DAL.Repositories;
using AslMonitor.Forms;
using AslMonitor.Services;
using AslMonitor.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AslMonitor
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());



            var services = new ServiceCollection();
            ConfigureServices(services);

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var form1 = serviceProvider.GetRequiredService<Form1>();
                Application.Run(form1);



            }
        }


        /// <summary>
        /// Register Types for dependency injection
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddScoped<Form1>();
            services.AddScoped<Dashboard>();
            services.AddScoped<SignUp>();
            services.AddDbContext<DatabaseContext>();

            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));

            services.AddScoped<IJsonDBService, JsonDBService>();
            //services.AddScoped<IFileUploader, FileUploader>();
            //services.AddScoped<IScreenShotService, ScreenShotService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IUserStateRepo, UserStateRepo>();
            services.AddScoped<IUserStateService, UserStateService>();
            services.AddTransient<GlobalFunctions>();



        }
    }
}