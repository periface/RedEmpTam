using Abp.AutoMapper;
using MercadoCinotam.Certifications.Manager;
using MercadoCinotam.GalardonsAndCert.Dtos;
using MercadoCinotam.Products.Client.Dtos;
using System.Linq;

namespace MercadoCinotam.GalardonsAndCert.Client
{
    public class GalardonClientService : IGalardonClientService
    {
        private readonly ICertificationManager _certificationManager;

        public GalardonClientService(ICertificationManager certificationManager)
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
