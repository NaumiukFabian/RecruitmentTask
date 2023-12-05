using RecruitmentTask.Logic.Interfaces.Dtos;
using RecruitmentTask.Logic.Interfaces.ServiceResponses;

namespace RecruitmentTask.Logic.Interfaces.Interfaces
{
    public interface IProductLogic
    {
        Task<ServiceResponse<bool>> DownloadAndSave();
        ServiceResponse<VProductionInfoDto> GetVproductinfo(string sku);
    }
}
