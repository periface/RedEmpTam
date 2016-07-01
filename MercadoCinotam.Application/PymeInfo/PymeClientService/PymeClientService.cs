using MercadoCinotam.Enums;
using MercadoCinotam.Pyme.Manager;
using System;

namespace MercadoCinotam.PymeInfo.PymeClientService
{
    public class PymeClientService : MercadoCinotamAppServiceBase, IPymeClientService
    {
        private readonly IPymeManager _pymeManager;

        public PymeClientService(IPymeManager pymeManager)
        {
            _pymeManager = pymeManager;
        }

        public object GetProperty(string property, PymePropertyListing info1)
        {
            try
            {

                switch (info1)
                {
                    case PymePropertyListing.Info:
                        var info = _pymeManager.GetInfo(TenantId);
                        var propertyValue = info.GetType().GetProperty(property).GetValue(info);
                        return string.IsNullOrEmpty(propertyValue.ToString()) ? $"No se ha encontrado un valor para la propiedad {property} <a href='/Admin/MainMenu/'>Reparar</a>" : propertyValue;
                    case PymePropertyListing.Contact:
                        var contactInfo = _pymeManager.GetContactInfo(TenantId);
                        var propertyContactInfoValue = contactInfo.GetType().GetProperty(property).GetValue(contactInfo);
                        return propertyContactInfoValue;
                    case PymePropertyListing.ContentSections:
                        var result = _pymeManager.GetMainPageContent(property, TenantId);
                        return result;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(info1), info1, null);
                }
            }
            catch (Exception)
            {
                return $"No se ha encontrado la propiedad {property} <a href='/Admin/MainMenu/'>Reparar</a>";
            }
        }
    }
}
