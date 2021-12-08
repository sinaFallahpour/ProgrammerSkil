using ProgrammerSkil.Models.Entity;
using ProgrammerSkil.Models.ViewModel.ModelStateViewMode;
using ProgrammerSkil.Models.ViewModel.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ModelBinding;

namespace ProgrammerSkil.Models.BLL.Interfaces
{
    public interface ISettingManager
    {

        Task<SettingDTO> GetById(int? Id);
        Task<List<SettingDTO>> GetAll();
        Task<List<SettingDTO>> GetAllForPagination(int? Page, int? PageSize);
        Task<SettingViewModel> Update(SettingForUpdateViewModel Setting, List<TBL_SocialMedia> SocialMedias);
        TBL_Setting ToTBL_Setting(SettingForUpdateViewModel SettingViewModel, ICollection<TBL_SocialMedia> Solicials);
        List<ModelStateViewMode> Validate(ModelStateDictionary ModelState);
        SettingViewModel ToSettingViewModel(TBL_Setting Setting);
        List<SettingViewModel> ToSettingViewModel(List<TBL_Setting> Settings);
        bool IsExist(SettingForUpdateViewModel SettingViewMode);
        List<TBL_SocialMedia> IsSoCialMediasMatched(List<int> SocialMedias);






        //Task<SettingViewModel> GetById(int? Id);
        //Task<List<SettingViewModel>> GetAll();
        //Task<List<SettingViewModel>> GetAllForPagination(int? Page, int? PageSize);
        ////TBL_Setting ToTBL_Setting(SettingViewModel SettingViewModel);
        //TBL_Setting ToTBL_Setting(SettingForUpdateViewModel SettingViewModel);
        //SettingViewModel ToSettingViewModel(TBL_Setting Setting);
        //List<SettingViewModel> ToSettingViewModel(List<TBL_Setting> Settings);
        //bool IsExist(SettingForUpdateViewModel SettingViewMode);
        //List<TBL_SocialMedia> IsSoCialMediasMatched(List<int> SocialMedias);


    }
}