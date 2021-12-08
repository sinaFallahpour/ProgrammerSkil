using ProgrammerSkil.Models.BaseDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgrammerSkil.Models.DLL.Interfaces
{
    public interface ISkillRepository : IBase
    {
        Task<TBL_Skill> GetById(int Id);
        Task<List<TBL_Skill>> GetAll();
        int Count();
        Task<bool> Create(TBL_Skill Skill);
        Task<bool> Update(TBL_Skill Skill);
        Task<bool> Remove(int Id);
        Task<List<TBL_Skill>> GetAllForPagination(int Page, int PageSize);
    }
}