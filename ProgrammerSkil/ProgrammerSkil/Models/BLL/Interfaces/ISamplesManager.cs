using ProgrammerSkil.Models.ViewModel.ModelStateViewMode;
using ProgrammerSkil.Models.ViewModel.Sample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ModelBinding;

namespace ProgrammerSkil.Models.BLL.Interfaces
{
    public interface ISamplesManager
    {
        Task<SampleDTO> GetById(int? Id);
        Task<List<SampleDTO>> GetAll();
        Task<List<SampleDTO>> GetAllForPagination(int? Page, int? PageSize);
        List<ModelStateViewMode> Validate(ModelStateDictionary ModelState);
        int Count();
        Task<TBL_Samples> Create(PostSampleViewModel Samples);
        Task<SampleDTO> Update(PostSampleViewModel Sample);
        bool IsSampleExist(int? id);
        Task<bool> Delete(int? Id);
        Task<object> Delete2(int? Id, SampleDTO sample);
        TBL_Samples ToTBL_Sample(PostSampleViewModel SampleViewModel);
        SampleViewModel ToSampleViewModel(TBL_Samples Sample);
        List<SampleViewModel> ToSampleViewModel(List<TBL_Samples> Samples);
        GetSampleViewModel ToGetSampleViewModel(TBL_Samples Sample);
    }
}