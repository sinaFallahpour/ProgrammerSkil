using ProgrammerSkil.Models.BaseDll;
using ProgrammerSkil.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgrammerSkil.Models.DLL.Interfaces
{
    public interface ICooperationRepository : IBase
    {
        Task<TBL_Cooperation> GetById(int Id);
        Task<List<TBL_Cooperation>> GetAll();
        Task<List<TBL_Cooperation>> GetAllForPagination(int Page, int PageSize);
        bool IsExist(int Id);
        int Count();
        Task<TBL_Cooperation> Create(TBL_Cooperation Cooperation);
        Task<TBL_Cooperation> Update(TBL_Cooperation cooperation);
        Task<TBL_Cooperation> ChangeCooporationStatus(int Id, CooperationStatus Status);
        Task<bool> Remove(int Id);


    }
}