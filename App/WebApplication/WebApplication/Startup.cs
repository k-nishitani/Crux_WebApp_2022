using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
            });


            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = @"vue_app/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}
            //app.UseStaticFiles();

            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles(); // これを追加！！

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            // これを追加！！
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "vue_app";
                if (env.IsDevelopment())
                {
                    // 開発環境では npm run serve をして 8080 ポートへ自動的にリダイレクトしてくれるようにする。
                    spa.UseProxyToSpaDevelopmentServer(async () =>
                    {
                        var pi = new ProcessStartInfo
                        {
                            FileName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "cmd" : "npm",
                            Arguments = $"{(RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "/c npm " : "")}run serve",
                            WorkingDirectory = "vue_app",
                            RedirectStandardError = true,
                            RedirectStandardInput = true,
                            RedirectStandardOutput = true,
                            UseShellExecute = false,
                        };
                        var p = Process.Start(pi);
                        var lf = app.ApplicationServices.GetService<ILoggerFactory>();
                        var logger = lf.CreateLogger("npm");
                        var tcs = new TaskCompletionSource<int>();
                        _ = Task.Run(() =>
                        {
                            var line = "";
                            while ((line = p.StandardOutput.ReadLine()) != null)
                            {
                                if (line.Contains("DONE  Compiled successfully in ")) // 開発用サーバーの起動待ち
                                {
                                    tcs.SetResult(0);
                                }

                                logger.LogInformation(line);
                            }
                        });
                        _ = Task.Run(() =>
                        {
                            var line = "";
                            while ((line = p.StandardError.ReadLine()) != null)
                            {
                                logger.LogError(line);
                            }
                        });
                        await Task.WhenAny(Task.Delay(20000), tcs.Task);
                        return new Uri("http://localhost:8080");
                    });
                }
            });
        }
    }
}
