using Abp.Domain.Services;
using MercadoCinotam.Certifications.Entities;
using System.Collections.Generic;

namespace MercadoCinotam.Certifications.Manager
{
    public interface ICertificationManager : IDomainService
    {
        int AddCertification(Certification input);
        Certification GetCertification(int certificationId);
        IEnumerable<Certification> GetCertifications();

    }
}
