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

        public object GetProperty(string property)
        {
            try
            {

                var info = _pymeManager.GetInfo(TenantId);
                var propertyValue = info.GetType().GetProperty(property).GetValue(info);

                return string.IsNullOrEmpty(propertyValue.ToString()) ? $"No se ha encontrado un valor para la propiedad {property} <a href='/Admin/MainMenu/'>Reparar</a>" : propertyValue;
            }
            catch (Exception)
            {
                return $"No se ha encontrado la propiedad {property} <a href='/Admin/MainMenu/'>Reparar</a>";
            }
        }
    }
}
