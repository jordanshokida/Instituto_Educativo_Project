using _2024_InstitutoEducativo.Data;
using Microsoft.EntityFrameworkCore;

namespace _2024_InstitutoEducativo
{
    public static class Startup
    {
        public static WebApplication InicializarApp(string[] args)
        {
            //Crear nueva instancia de nuestro servidor web
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);//lo configuramos con sus respectivos servicios
            var app = builder.Build();//sobre esta app, configuraremos luego los middleware
            Configure(app);//Configuramos los middleware
            return app;//retornamos la App ya inicializada
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<InstitutoContext>(options => options.UseInMemoryDatabase("InstitutoDb"));
            // Add services to the container.
            builder.Services.AddControllersWithViews();
        }
        public static void Configure(WebApplication app)
        {
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
        }       
    }
}
