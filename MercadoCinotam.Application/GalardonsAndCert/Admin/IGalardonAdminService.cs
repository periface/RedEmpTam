using Abp.Application.Services;
using Helpers.GenericTypes;
using MercadoCinotam.GalardonsAndCert.Dtos;
using System;

namespace MercadoCinotam.GalardonsAndCert.Admin
{
    public interface IGalardonAdminService : IApplicationService
    {
        int AddGalardon(GalardonInput input);
        GalardonInput GetGalardonForEdit(int? id);
        int RemoveGalardonFromList(int id);
        ReturnModel<GalardonDto> GetGalardons(RequestModel request);
        GalardonProductInput GetGalardonAssignationModel(Guid productId);
        void AddGalardonToProduct(GalardonProductInput input);
    }
}
