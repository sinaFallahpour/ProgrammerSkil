using ProgrammerSkil.Models.BLL.Interfaces;
using ProgrammerSkil.Models.DLL.Interfaces;
using ProgrammerSkil.Models.ViewModel.ModelStateViewMode;
using ProgrammerSkil.Models.ViewModel.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ModelBinding;

namespace ProgrammerSkil.Models.BLL
{
    public class SkillManager : ISkillManager
    {
        private ISkillRepository _repo;
        public SkillManager(ISkillRepository Repo)
        {
            _repo = Repo;
        }





        /// <summary>
        /// Get Skill By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<SkillViewModel> GetById(int? Id)
        {
            if (Id == null)
                return null;
            var Skill = await _repo.GetById((int)Id);
            return ToSkillViewModel(Skill);
        }





        /// <summary>
        /// گرفتن لیست همه 
        /// </summary>
        /// <returns></returns>
        public async Task<List<SkillViewModel>> GetAll()
        {
            var Skills = await _repo.GetAll();
            return ToSkillViewModel(Skills);
        }





        /// <summary>
        /// گرفتن لیست همه مهارت ها
        /// </summary>
        /// <returns></returns>
        public async Task<List<SkillViewModel>> GetAllForPagination(int? Page, int? PageSize)
        {
            if (Page == null || PageSize == null)
                return null;
            var Skills = await _repo.GetAllForPagination((int)Page, (int)PageSize);
            return ToSkillViewModel(Skills);
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




        /// <summary>
        /// get count
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return _repo.Count();
        }





        /// <summary>
        /// ایجاد Skill جدید
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Create(SkillViewModel Skill)
        {
            var TBL_Skill = ToTBL_Skill(Skill);
            return await _repo.Create(TBL_Skill);
        }





        /// <summary>
        /// ایجاد Skill جدید
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Update(SkillViewModel Skill)
        {
            var TBL_Skill = ToTBL_Skill(Skill);
            return await _repo.Update(TBL_Skill);
        }





        /// <summary>
        /// حذف اسلاید
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(int Id)
        {
            return await _repo.Remove(Id);
        }





        /// <summary>
        ///
        /// </summary>
        /// <param name="DataRow"></param>
        /// <returns></returns>
        public TBL_Skill ToTBL_Skill(SkillViewModel SkillViewModel)
        {
            if (SkillViewModel == null)
                return null;
            return new TBL_Skill()
            {
                Id = SkillViewModel.Id,
                Title = SkillViewModel.Title,
            };
        }





        /// <summary>
        ///
        /// </summary>
        /// <param name="DataRow"></param>
        /// <returns></returns>
        public SkillViewModel ToSkillViewModel(TBL_Skill Skill)
        {
            if (Skill == null)
                return null;
            return new SkillViewModel()
            {
                Id = Skill.Id,
                Title = Skill.Title,
            };
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<SkillViewModel> ToSkillViewModel(List<TBL_Skill> Skills)
        {
            if (Skills == null)
                return null;
            return Skills.Select(s => new SkillViewModel
            {
                Id = s.Id,
                Title = s.Title
            }).ToList();
        }





    }
}