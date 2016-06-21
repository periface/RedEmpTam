using Abp.Domain.Services;
using MercadoCinotam.Pyme.Entities;

namespace MercadoCinotam.Pyme.Manager
{
    public interface IPymeManager : IDomainService
    {
        int AddInfo(PymeInfo info);
        int AddOwner(PymeOwner owner);
        int AddContactInfo(PymeContactInfo contactInfo);

    }
}
