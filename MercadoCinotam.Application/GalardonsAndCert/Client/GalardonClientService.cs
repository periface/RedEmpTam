using Abp.AutoMapper;
using MercadoCinotam.Certifications;
using MercadoCinotam.GalardonsAndCert.Dtos;
using MercadoCinotam.Products.Client.Dtos;
using System.Linq;

namespace MercadoCinotam.GalardonsAndCert.Client
{
    public class GalardonClientService : IGalardonClientService
    {
        private readonly CertificationProvider _certificationManager;

        public GalardonClientService(CertificationProvider certificationManager)
        {
            _certificationManager = certificationManager;
        }

        public GalardonList GetAllGalardons()
        {
            var galardons = _certificationManager.GetCertifications();
            var model = new GalardonList()
            {
                Galardons = galardons.Select(a => a.MapTo<GalardonDto>()).ToList()
            };
            return model;
        }
    }
}
