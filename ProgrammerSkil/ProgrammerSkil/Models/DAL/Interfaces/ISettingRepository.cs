using ProgrammerSkil.Models.BaseDll;
using ProgrammerSkil.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgrammerSkil.Models.DLL.Interfaces
{
    public interface ISettingRepository : IBase
    {
        Task<TBL_Setting> GetById(int Id);
        Task<List<TBL_Setting>> GetAll();
        Task<List<TBL_Setting>> GetAllForPagination(int Page, int PageSize);
        bool IsExists(int id);
        ICollection<TBL_SocialMedia> IsSoCialMediasMatched(List<int> socialMedias);
        Task<TBL_Setting> Update(TBL_Setting setting);

    }
}