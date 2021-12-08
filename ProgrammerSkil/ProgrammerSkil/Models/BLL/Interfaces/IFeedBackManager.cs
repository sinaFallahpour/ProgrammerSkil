using ProgrammerSkil.Models.ViewModel.FeedBack;
using ProgrammerSkil.Models.ViewModel.ModelStateViewMode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ModelBinding;

namespace ProgrammerSkil.Models.BLL.Interfaces
{
    public interface IFeedBackManager
    {
        Task<FeedBackDTO> GetById(int? Id);
        Task<List<FeedBackDTO>> GetAll();
        Task<List<FeedBackDTO>> GetAllForPagination(int? Page, int? PageSize);
        List<ModelStateViewMode> Validate(ModelStateDictionary ModelState);
        int Count();
        Task<bool> Create(FeedBackViewModel Skill);
        Task<bool> Delete(int Id);
        TBL_FeedBacks ToTBL_FeedBack(FeedBackViewModel FeedBackViewModel);
        FeedBackViewModel ToFeedBackViewModel(TBL_FeedBacks FeedBack);
        List<FeedBackViewModel> ToFeedBackViewModel(List<TBL_FeedBacks> FeedBackses);
    }
}