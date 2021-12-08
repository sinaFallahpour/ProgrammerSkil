using ProgrammerSkil.Models.Enums;
using ProgrammerSkil.Models.ViewModel.Cooperation;
using ProgrammerSkil.Models.ViewModel.ModelStateViewMode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ModelBinding;

namespace ProgrammerSkil.Models.BLL.Interfaces
{
    public interface ICooperationManager
    {
        Task<CooperationDTO> GetById(int? Id);
        Task<List<CooperationDTO>> GetAll();
        Task<List<CooperationDTO>> GetAllForPagination(int? Page, int? PageSize);
        List<ModelStateViewMode> Validate(ModelStateDictionary ModelState);
        bool IsExist(int? Id);
        int Count();
        Task<CooperationViewModel> Create(CooperationViewModel CooperationViewModel);
        Task<CooperationViewModel> Update(CooperationViewModel Coopertaion);
         Task<CooperationDTO> ChangeCooporationStatus(int?  Id, CooperationStatus Status);
        Task<bool> Delete(int Id);
        TBL_Cooperation ToTBL_Cooperation(CooperationViewModel CooperationViewModel);
        int ToEnumValue(CooperationStatus Status);
        CooperationStatus ToEnumType(int StatusValue);
        CooperationViewModel ToCooperationViewModel(TBL_Cooperation Cooperation);
        List<CooperationViewModel> ToCooperationViewModel(List<TBL_Cooperation> Cooperations);
    }
}