using Abp.AutoMapper;
using Helpers.TenancyHelpers;
using ImageSaver.Manager;
using MercadoCinotam.Pyme.Manager;
using MercadoCinotam.PymeInfo.Dtos;

namespace MercadoCinotam.PymeInfo.PymeAdminService
{
    public class PymeAdminService : IPymeAdminService
    {
        private readonly IPymeManager _pymeManager;
        private readonly IImageManager _imageManager;
        private const string ImageFolder = "/Content/Images/Logos/Tentants/{0}/";
        public PymeAdminService(IPymeManager pymeManager, IImageManager imageManager)
        {
            _pymeManager = pymeManager;
            _imageManager = imageManager;
        }

        public int AddInfo(PymeInfoInput input)
        {
            Pyme.Entities.PymeInfo info;
            if (input.Id != 0)
            {
                var infoDb = _pymeManager.GetInfo();
                info = input.MapTo(infoDb);
            }
            else
            {
                info = input.MapTo<Pyme.Entities.PymeInfo>();
            }
            if (input.Image.ContentLength > 0)
            {
                var formatedFolder = string.Format(ImageFolder, TenantHelper.TenantId);
                var image = _imageManager.SaveImage(null, null, input.Image, formatedFolder);
                info.PymeLogo = image;
            }
            var id = _pymeManager.AddInfo(info);
            return id;
        }

        public PymeInfoInput GetInfoForEdit()
        {
            var pymeInfo = _pymeManager.GetInfo();
            return pymeInfo == null ? new PymeInfoInput() : pymeInfo.MapTo<PymeInfoInput>();
        }
    }
}
