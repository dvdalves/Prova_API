using System.Net.Http.Headers;
using Prova_MVC.Services;
using Prova_MVC.Services.Interfaces;

namespace Prova_MVC.Configurations
{
    public static class ConfigurationHelper
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            ConfigureHttpClient(services);
        }

        public static void Configure(IApplicationBuilder app)
        {
            if (!app.ApplicationServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void ConfigureHttpClient(IServiceCollection services)
        {
            services.AddHttpClient<IProdutoService, ProdutoService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7037/");
            })
            .ConfigureHttpClient(client =>
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddHttpClient<UtilsService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7037/");
            })
            .ConfigureHttpClient(client =>
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
        }
    }
}
