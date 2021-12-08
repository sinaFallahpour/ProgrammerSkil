using ProgrammerSkil.Models.BaseDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgrammerSkil.Models.DLL.Interfaces
{
    public interface ISamplesRepository : IBase
    {
        Task<TBL_Samples> GetById(int Id);
        Task<List<TBL_Samples>> GetAll();
        Task<List<TBL_Samples>> GetAllForPagination(int Page, int PageSize);
        int Count();
        Task<TBL_Samples> Create(TBL_Samples Sample);
        Task<TBL_Samples> Update(TBL_Samples Sample);
        bool IsSampleExist(int Id);
        Task<bool> Remove(int Id);
        Task<TBL_Samples> Remove2(int Id);
    }
}