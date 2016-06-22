using Abp.AutoMapper;
using Helpers;
using Helpers.GenericTypes;
using ImageSaver.Manager;
using MercadoCinotam.Products.Admin.Dtos;
using MercadoCinotam.Products.Entities;
using MercadoCinotam.Products.Manager;
using System;
using System.Linq;
using System.Web;

namespace MercadoCinotam.Products.Admin
{
    public class ProductAdminService : MercadoCinotamAppServiceBase, IProductAdminService
    {
        private readonly IProductManager _productManager;
        private readonly IImageManager _imageManager;
        private const string ImageFolder = "/Content/Images/Tenants/{0}/Products/{1}/{2}/";
        private const string FolderSizeSmall = "64x64";
        private const string FolderSizeDefault = "Default";
        private const string FolderSizeMedium = "128x128";
        public ProductAdminService(IProductManager productManager, IImageManager imageManager)
        {
            _productManager = productManager;
            _imageManager = imageManager;
        }

        public Guid AddProductToStore(ProductInput input, HttpPostedFileBase httpPostedFileBase)
        {

            var slug = input.ProductName.CreateSlug();
            var tenantId = AbpSession.TenantId ?? 1;
            Product instance;
            if (input.Id.HasValue)
            {
                instance = _productManager.GetProduct(input.Id.Value);
                instance = input.MapTo(instance);
            }
            else
            {

                instance = Product.CreateProduct(input.ProductName, input.AvailableStock, input.TrackStock, input.ProductPrice, input.ProductDescription);

            }

            if (httpPostedFileBase.ContentLength > 0)
            {
                //Small image
                var formatedFolderSmall = string.Format(ImageFolder, tenantId, slug, FolderSizeSmall);
                var smallImage = _imageManager.SaveImage(64, 64, httpPostedFileBase, formatedFolderSmall);
                //Medium Image
                var formatedFolderMed = string.Format(ImageFolder, tenantId, slug, FolderSizeMedium);
                var medImage = _imageManager.SaveImage(128, 128, httpPostedFileBase, formatedFolderMed);

                //Default Image
                var formatedFolderDef = string.Format(ImageFolder, tenantId, slug, FolderSizeDefault);
                var defImage = _imageManager.SaveImage(null, null, httpPostedFileBase, formatedFolderDef);


                instance.SetMainImage(defImage);
                instance.SetMedImage(medImage);
                instance.SetSmallImage(smallImage);
                instance.SetDataImage(input.DataImage);
            }


            var id = _productManager.AddProduct(instance);
            return id;
        }

        public ReturnModel<ProductDto> GetProducts(RequestModel request)
        {
            int totalCount;
            var query = _productManager.GetQuery();
            var filterByLength = GenerateModel(request, query, "ProductName", out totalCount);
            return new ReturnModel<ProductDto>()
            {
                draw = request.draw,
                length = request.length,
                recordsTotal = totalCount,
                iTotalDisplayRecords = totalCount,
                iTotalRecords = query.Count(),
                data = filterByLength.Select(a => new ProductDto()
                {
                    ProductName = a.ProductName,
                    Id = a.Id,
                    ProductPrice = a.ProductPrice,
                    Active = a.Active,

                }).ToArray(),
                recordsFiltered = filterByLength.Count
            };
        }

        public ProductInput GetProductForEdit(Guid? id)
        {
            if (!id.HasValue)
            {
                return new ProductInput();
            }
            var product = _productManager.GetProduct(id.Value);
            return new ProductInput()
            {
                AvailableStock = product.AvailableStock,
                SmallImage = product.SmallImage,
                TrackStock = product.TrackStock,
                MainPicture = product.MainPicture,
                MediumImage = product.MediumImage,
                ProductPrice = product.ProductPrice,
                ProductDescription = product.ProductDescription,
                ProductName = product.ProductName,
                Id = product.Id
            };
        }

        public object GetGalardons(Guid? id)
        {
            return null;
        }
    }
}
