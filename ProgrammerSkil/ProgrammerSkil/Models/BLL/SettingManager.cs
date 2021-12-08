using ProgrammerSkil.Models.BLL.Interfaces;
using ProgrammerSkil.Models.DLL.Interfaces;
using ProgrammerSkil.Models.Entity;
using ProgrammerSkil.Models.ViewModel.ModelStateViewMode;
using ProgrammerSkil.Models.ViewModel.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ModelBinding;

namespace ProgrammerSkil.Models.BLL
{
    public class SettingManager : ISettingManager
    {
        private ISettingRepository _repo;
        public SettingManager(ISettingRepository Repo)
        {
            _repo = Repo;
        }





        /// <summary>
        /// Get Skill By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<SettingDTO> GetById(int? Id)
        {
            if (Id == null)
                return null;
            var Setting = await _repo.GetById((int)Id);
            var SettingDTO = AutoMapper.Mapper.Map<TBL_Setting, SettingDTO>(Setting);
            return SettingDTO;
            //return ToSettingViewModel(Setting);
        }





        /// <summary>
        /// گرفتن لیست همه 
        /// </summary>
        /// <returns></returns>
        public async Task<List<SettingDTO>> GetAll()
        {
            var Settings = await _repo.GetAll();
            var SettingsDTO = AutoMapper.Mapper.Map<List<TBL_Setting>, List<SettingDTO>>(Settings);
            return SettingsDTO;
            //return ToSettingViewModel(Skills);
        }





        /// <summary>
        /// گرفتن لیست همه تنظیمات ها
        /// </summary>
        /// <returns></returns>
        public async Task<List<SettingDTO>> GetAllForPagination(int? Page, int? PageSize)
        {
            if (Page == null || PageSize == null)
                return null;
            var Settings = await _repo.GetAllForPagination((int)Page, (int)PageSize);
            var SettingsDTO = AutoMapper.Mapper.Map<List<TBL_Setting>, List<SettingDTO>>(Settings);
            return SettingsDTO;
            //return ToSettingViewModel(Settings);
        }





        /// <summary>
        /// validation For ModelState
        /// </summary>
        /// <param name="ModelState"></param>
        /// <returns></returns>
        public List<ModelStateViewMode> Validate(ModelStateDictionary ModelState)
        {
            var ValidateResult = new List<ModelStateViewMode>() { };
            var Keys = ModelState.Keys;
            foreach (var key in Keys)
            {
                var value = ModelState[key];
                var ValidationViewModel = new ModelStateViewMode();
                if (key.Contains('.'))
                {
                    ValidationViewModel.ErrorKey = key.Split('.')[1];
                }
                else
                    ValidationViewModel.ErrorKey = key;
                var Errors = new List<string>();
                foreach (var error in value.Errors)
                {
                    Errors.Add(error.ErrorMessage);
                }
                ValidationViewModel.ErrorValue = Errors;
                ValidateResult.Add(ValidationViewModel);
            }
            return ValidateResult;
        }






        ///// <summary>
        ///// ایجاد Skill جدید
        ///// </summary>
        ///// <returns></returns>
        //public async Task<bool> Create(SkillViewModel Skill)
        //{
        //    var TBL_Skill = ToTBL_Skill(Skill);
        //    return await _repo.Create(TBL_Skill);
        //}





        /// <summary>
        /// ایجاد Skill جدید
        /// </summary>
        /// <returns></returns>
        public async Task<SettingViewModel> Update(SettingForUpdateViewModel Setting, List<TBL_SocialMedia> SocialMedias)
        {
            var TBL_Setting = ToTBL_Setting(Setting, SocialMedias);
            //get Current Claims Principal
            var Idemtity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            //get Current Claims Principal  value
            var UserId = Idemtity.Claims.Where(c => c.Type == "_Id")
                    .Select(c => c.Value).SingleOrDefault();
            int userId;
            if (int.TryParse(UserId, out userId))
            {
                TBL_Setting.UserId = userId;
            }
            var UpdatedSetting = await _repo.Update(TBL_Setting);
            return ToSettingViewModel(UpdatedSetting);
        }





        ///// <summary>
        ///// حذف اسلاید
        ///// </summary>
        ///// <param name="Id"></param>
        ///// <returns></returns>
        //public async Task<bool> Delete(int Id)
        //{
        //    return await _repo.Remove(Id);
        //}





        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public TBL_Setting ToTBL_Setting(SettingForUpdateViewModel SettingViewModel, ICollection<TBL_SocialMedia> Solicials)
        {
            if (SettingViewModel == null)
                return null;
            return new TBL_Setting()
            {
                Id = SettingViewModel.Id,
                Phone = SettingViewModel.Phone,
                AboutUs = SettingViewModel.AboutUs,
                SocialMedias = Solicials,
            };
        }





        /// <summary>
        ///
        /// </summary>
        /// <param name="Setting"></param>
        /// <returns></returns>
        public SettingViewModel ToSettingViewModel(TBL_Setting Setting)
        {
            if (Setting == null)
                return null;
            return new SettingViewModel()
            {
                Id = Setting.Id,
                Phone = Setting.Phone,
                AboutUs = Setting.AboutUs,
                SocialMedias = Setting.SocialMedias
            };
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<SettingViewModel> ToSettingViewModel(List<TBL_Setting> Settings)
        {
            if (Settings == null)
                return null;
            return Settings.Select(s => new SettingViewModel
            {
                Id = s.Id,
                Phone = s.Phone,
                AboutUs = s.AboutUs,
                SocialMedias = s.SocialMedias
            }).ToList();
        }





        public bool IsExist(SettingForUpdateViewModel SettingViewMode)
        {
            if (SettingViewMode == null)
                return false;
            return _repo.IsExists(SettingViewMode.Id);
        }



        public List<TBL_SocialMedia> IsSoCialMediasMatched(List<int> SocialMedias)
        {
            if (SocialMedias == null)
                return null;
            return _repo.IsSoCialMediasMatched(SocialMedias)?.ToList();
        }


    }
}