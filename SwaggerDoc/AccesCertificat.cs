using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace SwaggerDoc
{
    public class AccesCertificat
    {
        private static X509Certificate2? _serveurCertificat;

        public static X509Certificate2 ObtenirCertificatServeur()
        {
            if (_serveurCertificat is null)
            {
                var location = Environment.GetEnvironmentVariable("CERTIFICAT_LOCATION") ?? throw new Exception("La variable d'environnement CERTIFICAT_LOCATION n'est pas définit.");

                var certificatNom = Environment.GetEnvironmentVariable("CERTIFICAT") ?? throw new Exception("La variable d'environnement CERTIFICAT n'est pas définit."); ;

                var motDePasse = Environment.GetEnvironmentVariable("CERTIFICAT_MOT_DE_PASSE") ?? throw new Exception("La variable d'environnement MOT_DE_PASSE n'est pas définit."); ; ;

                _serveurCertificat = new X509Certificate2(Path.Combine(location, certificatNom), motDePasse);
            }

            return _serveurCertificat;
        }
    }
}