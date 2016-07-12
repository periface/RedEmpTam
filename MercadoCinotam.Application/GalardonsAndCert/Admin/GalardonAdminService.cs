using Abp.AutoMapper;
using Helpers;
using Helpers.GenericTypes;
using ImageSaver;
using MercadoCinotam.Certifications;
using MercadoCinotam.Certifications.Entities;
using MercadoCinotam.GalardonsAndCert.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MercadoCinotam.GalardonsAndCert.Admin
{
    public class GalardonAdminService : MercadoCinotamAppServiceBase, IGalardonAdminService
    {
        private readonly ImageProvider _imageProvider;
        private readonly CertificationProvider _certificationProvider;
        private const string GalardonFolder = "/Content/Images/Galardons/Tenants/{0}/{1}/";
        public GalardonAdminService(ImageProvider imageProvider, CertificationProvider certificationProvider)
        {
            _imageProvider = imageProvider;
            _certificationProvider = certificationProvider;
        }

        public int AddGalardon(GalardonInput input)
        {
            if (input.Id != 0)
            {
                var galardonDb = _certificationProvider.GetCertification(input.Id);
                var edited = input.MapTo(galardonDb);
                var id = _certificationProvider.AddCertification(edited);
                if (input.ImageFile.ContentLength <= 0) return id;
                var computedFolder = string.Format(GalardonFolder, AbpSession.TenantId, edited.GalardonName.CreateSlug());
                var folder = _imageProvider.SaveImage(200, 200, input.ImageFile, computedFolder);
                edited.SetImage(folder);
                return id;
            }
            else
            {
                var model = Certification.Create(input.GalardonName, input.UniqueCode, input.Description);
                var id = _certificationProvider.AddCertification(model);
                var computedFolder = string.Format(GalardonFolder, AbpSession.TenantId, model.GalardonName.CreateSlug());
                var folder = _imageProvider.SaveImage(200, 200, input.ImageFile, computedFolder);
                model.SetImage(folder);
                return id;
            }
        }

        public GalardonInput GetGalardonForEdit(int? id)
        {
            if (id == null) return new GalardonInput();
            var galardon = _certificationProvider.GetCertification(id.Value);
            if (galardon == null)
            {
                return new GalardonInput();
            }
            return new GalardonInput()
            {
                Id = galardon.Id,
                GalardonName = galardon.GalardonName,
                UniqueCode = galardon.UniqueCode,
                Image = galardon.Image,
                Description = galardon.Description
            };
        }

        public int RemoveGalardonFromList(int id)
        {
            throw new NotImplementedException();
        }

        public ReturnModel<GalardonDto> GetGalardons(RequestModel request)
        {
            int count;
            var query = _certificationProvider.GetCertificationsQuery();
            var model = GenerateModel(request, query, "GalardonName", out count);
            return new ReturnModel<GalardonDto>()
            {
                draw = request.draw,
                iTotalDisplayRecords = count,
                iTotalRecords = query.Count(),
                recordsFiltered = model.Count,
                length = request.length,
                recordsTotal = count,
                data = model.Select(a => new GalardonDto()
                {
                    GalardonName = a.GalardonName,
                    Id = a.Id,
                    UniqueCode = a.UniqueCode,
                    Image = a.Image
                }).ToArray()
            };
        }

        public GalardonProductInput GetGalardonAssignationModel(Guid productId)
        {
            var galardonAssignationFromProduct = _certificationProvider.GetCertifications(productId).ToList();
            var activeGalardon = new List<GalardonDto>();
            foreach (var assignation in galardonAssignationFromProduct)
            {
                var cert = _certificationProvider.GetCertification(assignation.CertId);
                activeGalardon.Add(cert.MapTo<GalardonDto>());

            }
            var inactives = _certificationProvider.GetCertifications(galardonAssignationFromProduct).ToList();

            return new GalardonProductInput()
            {
                ActiveGalardonDtos = activeGalardon,
                InActiveGalardonDtos = inactives.Select(a => a.MapTo<GalardonDto>()),
                ProductId = productId,
            };
        }

        public void AddGalardonToProduct(GalardonProductInput input)
        {
            foreach (var id in input.Actives)
            {
                _certificationProvider.AddCertificationToProduct(input.ProductId, id);
            }
        }
    }
}
