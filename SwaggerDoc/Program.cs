using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace SwaggerDoc
{
    /// <summary>
    /// Classe program. Contient le point d'entré de l'application
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Point d'entré de l'application
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Création et initialisation de l'api
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(o =>
                    {
                        o.ConfigureHttpsDefaults(o =>
                        {
                            o.ClientCertificateMode = ClientCertificateMode.RequireCertificate;
                            o.ServerCertificate = new X509Certificate2(Path.Combine("c:\\root_ca_swaggerdoc.pfx"), "1234");
                        });
                    });
                });
    }
}
