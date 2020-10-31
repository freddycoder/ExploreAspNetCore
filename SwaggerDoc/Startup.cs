using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SwaggerDoc.Extension;
using SwaggerDoc.Model;
using SwaggerDoc.Services;
using static SwaggerDoc.Extension.DistributedCacheExtension;
using System.Security.Cryptography.X509Certificates;
using System;
using Microsoft.AspNetCore.Authentication.Certificate;
using System.Threading.Tasks;

namespace SwaggerDoc
{
    /// <summary>
    /// Classe de démarrage de l'api
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructeur d'initialisation
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Accédéer au configuration de l'application
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MyCertificateValidationService>();

            services.AddCertificateForwarding(options =>
            {
                options.CertificateHeader = "X-SSL-CERT";
                options.HeaderConverter = (headerValue) =>
                {
                    X509Certificate2? clientCertificate = null;

                    if (!string.IsNullOrWhiteSpace(headerValue))
                    {
                        byte[] bytes = StringToByteArray(headerValue);
                        clientCertificate = new X509Certificate2(bytes);
                    }

                    return clientCertificate;
                };
            });

            services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
                    .AddCertificate(o =>
                    {
                        o.AllowedCertificateTypes = CertificateTypes.SelfSigned;

                        o.Events = new CertificateAuthenticationEvents()
                        {
                            OnCertificateValidated = context =>
                            {
                                var validator = context.HttpContext.RequestServices.GetService(typeof(MyCertificateValidationService))
                                                        as MyCertificateValidationService
                                                        ?? throw new ApplicationException("MyCertificatValidationService is not register");

                                if (validator.ValidateCertificate(context.ClientCertificate) == false)
                                {
                                    context.Fail("Thumbprint dont match");
                                }

                                return Task.CompletedTask;
                            }
                        };
                    });

            services.AddScoped<ApiContexte>()
                    .AddSingleton<JournalTransaction>()
                    .AddTransient<IInterpretationService, InterpretationService>()
                    .AddCustomController()
                    .AddSwagger()
                    .AddDistributedCache()
                    .AddSingleton<UniqueIdentifier>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHsts();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
                c.InjectStylesheet("/css/swagger_custom.css");
            });

            app.UseRouting();

            app.UseCertificateForwarding();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static byte[] StringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];

            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return bytes;
        }
    }
}
