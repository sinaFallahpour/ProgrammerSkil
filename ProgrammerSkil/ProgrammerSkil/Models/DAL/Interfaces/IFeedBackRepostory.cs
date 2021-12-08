using ProgrammerSkil.Models.BaseDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgrammerSkil.Models.DLL.Interfaces
{
    public interface IFeedBackRepostory : IBase
    {
        Task<TBL_FeedBacks> GetById(int Id);
        Task<List<TBL_FeedBacks>> GetAll();
        Task<List<TBL_FeedBacks>> GetAllForPagination(int Page, int PageSize);
        int Count();
        Task<bool> Create(TBL_FeedBacks FeedBack);
        Task<bool> Remove(int Id);
    }
}