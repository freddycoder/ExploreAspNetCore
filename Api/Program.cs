using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Hosting;

namespace ExploreAspNetCore
{
    /// <summary>
    /// Classe program. Contient le point d'entr� de l'application
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Point d'entr� de l'application
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Cr�ation et initialisation de l'api
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
                        //o.ConfigureHttpsDefaults(o =>
                        //{
                        //    o.ClientCertificateMode = ClientCertificateMode.RequireCertificate;
                        //    o.ServerCertificate = AccesCertificat.ObtenirCertificatServeur();
                        //});
                    });
                });
    }
}
