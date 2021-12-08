using ProgrammerSkil.Models.ViewModel.ModelStateViewMode;
using ProgrammerSkil.Models.ViewModel.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ModelBinding;

namespace ProgrammerSkil.Models.BLL.Interfaces
{
    public interface ISkillManager
    {
        Task<SkillViewModel> GetById(int? Id);
        Task<List<SkillViewModel>> GetAll();
        Task<List<SkillViewModel>> GetAllForPagination(int? Page, int? PageSize);
        List<ModelStateViewMode> Validate(ModelStateDictionary ModelState);
        int Count();
        Task<bool> Create(SkillViewModel Skill);
        Task<bool> Update(SkillViewModel Skill);
        Task<bool> Delete(int Id);
        TBL_Skill ToTBL_Skill(SkillViewModel SkillViewModel);
        SkillViewModel ToSkillViewModel(TBL_Skill Skill);
        List<SkillViewModel> ToSkillViewModel(List<TBL_Skill> Skills);
    }
}