using Abp.AutoMapper;
using Abp.UI;
using Helpers;
using Helpers.GenericTypes;
using ImageSaver.Manager;
using MercadoCinotam.Products.Admin.Dtos;
using MercadoCinotam.Products.Entities;
using MercadoCinotam.Products.Manager;
using System;
using System.Linq;

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

        public Guid AddProductToStore(ProductInput input)
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

                instance = Product.CreateProduct(input.ProductName, input.AvailableStock, input.TrackStock, input.ProductPrice, input.ProductDescription, input.Sku);

            }

            if (input.Imagen.ContentLength > 0)
            {
                //Small image
                var formatedFolderSmall = string.Format(ImageFolder, tenantId, slug, FolderSizeSmall);
                var smallImage = _imageManager.SaveImage(64, 64, input.Imagen, formatedFolderSmall);
                //Medium Image
                var formatedFolderMed = string.Format(ImageFolder, tenantId, slug, FolderSizeMedium);
                var medImage = _imageManager.SaveImage(128, 128, input.Imagen, formatedFolderMed);

                //Default Image
                var formatedFolderDef = string.Format(ImageFolder, tenantId, slug, FolderSizeDefault);
                var defImage = _imageManager.SaveImage(null, null, input.Imagen, formatedFolderDef);


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
                    Sku = a.Sku

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
                Id = product.Id,
                Sku = product.Sku
            };
        }

        public object GetGalardons(Guid? id)
        {
            return null;
        }

        public int AddGalardon(GalardonInput input)
        {
            if (input.ProductId == null) throw new UserFriendlyException("Producto no definido");
            var galardon = ProductCertification.Create(input.GalardonId, input.ProductId);
            return _productManager.AddGalardon(galardon);
        }

    }
}
