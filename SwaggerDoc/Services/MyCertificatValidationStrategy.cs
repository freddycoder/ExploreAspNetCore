using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SwaggerDoc.Services
{
    public class MyCertificateValidationService
    {
        private readonly ILogger<MyCertificateValidationService> _logger;

        public MyCertificateValidationService(ILogger<MyCertificateValidationService> logger)
        {
            _logger = logger;
        }

        public bool ValidateCertificate(X509Certificate2 clientCertificate)
        {
            var cert = AccesCertificat.ObtenirCertificatServeur();

            if (clientCertificate.Thumbprint != cert.Thumbprint)
            {
                _logger.LogWarning($"Certificat non valide, le sujet était {clientCertificate.Subject}{Environment.NewLine}Le thumbprint du certificat n'est pas reconnu, le thumbprint était, {clientCertificate.Thumbprint}");

                return false;
            }

            return true;
        }
    }
}
