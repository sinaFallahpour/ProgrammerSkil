using ProgrammerSkil.Models.BLL.Interfaces;
using ProgrammerSkil.Models.DLL.Interfaces;
using ProgrammerSkil.Models.ExtentionMethods;
using ProgrammerSkil.Models.ViewModel.Image;
using ProgrammerSkil.Models.ViewModel.ModelStateViewMode;
using ProgrammerSkil.Models.ViewModel.Sample;
using ProgrammerSkil.Models.ViewModel.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ModelBinding;

namespace ProgrammerSkil.Models.BLL
{
    public class SamplesManager : ISamplesManager
    {
        private ISamplesRepository _repo;
        public SamplesManager(ISamplesRepository Repo)
        {
            _repo = Repo;
        }





        /// <summary>
        /// Get Sample By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<SampleDTO> GetById(int? Id)
        {
            if (Id == null)
                return null;
            var Sample = await _repo.GetById((int)Id);
            var SampleDTO = AutoMapper.Mapper.Map<TBL_Samples, SampleDTO>(Sample);
            return SampleDTO;
            //return ToGetSampleViewModel(Sample);
        }





        /// <summary>
        /// گرفتن لیست همه 
        /// </summary>
        /// <returns></returns>
        public async Task<List<SampleDTO>> GetAll()
        {
            var Samples = await _repo.GetAll();
            var SamplesDTO = AutoMapper.Mapper.Map<List<TBL_Samples>, List<SampleDTO>>(Samples);
            return SamplesDTO;
            //return ToSampleViewModel(Samples);
        }





        /// <summary>
        /// گرفتن لیست همه samples ها
        /// </summary>
        /// <returns></returns>
        public async Task<List<SampleDTO>> GetAllForPagination(int? Page, int? PageSize)
        {
            if (Page == null || PageSize == null)
                return null;
            var Samples = await _repo.GetAllForPagination((int)Page, (int)PageSize);
            var SamplesDTO = AutoMapper.Mapper.Map<List<TBL_Samples>, List<SampleDTO>>(Samples);
            return SamplesDTO;
            //return ToSampleViewModel(Samples);
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
        /// ایجاد Sample جدید
        /// </summary>
        /// <returns></returns>
        public async Task<TBL_Samples> Create(PostSampleViewModel Samples)
        {
            ///حذف   کاربر
            ///افزودن عکسای سمپل

            var TBL_Sample = ToTBL_Sample(Samples);
            return await _repo.Create(TBL_Sample);
        }





        /// <summary>
        /// ایجاد Sample آپدیت
        /// </summary>
        /// <returns></returns>
        public async Task<SampleDTO> Update(PostSampleViewModel Sample)
        {
            ///Update   Picture
            var TBL_Sample = ToTBL_Sample(Sample);
            //return await _repo.Update(TBL_Sample);
           
            var UpdatedSample = await _repo.Update(TBL_Sample);
            var SampleDTO = AutoMapper.Mapper.Map<TBL_Samples, SampleDTO>(UpdatedSample);
            return SampleDTO;

        }




        public bool IsSampleExist(int? Id)
        {
            if (Id == null) return false;
            return _repo.IsSampleExist((int)Id);
        }






        /// <summary>
        /// حذف sample
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(int? Id)
        {
            if (Id == null)
                return false;
            ////حف عکسای کاربر
            return await _repo.Remove((int)Id);
        }





        /// <summary>
        /// حذف 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<object> Delete2(int? Id, SampleDTO sample)
        {
            if (Id == null)
                return null;
            var TBL_Sample = await _repo.Remove2((int)Id);
            var sampleViewMode = ToSampleViewModel(TBL_Sample);
            if (sampleViewMode == null)
                return null;
            foreach (var item in sample.Images)
            {
                if (!string.IsNullOrEmpty(item.Photo) && File.Exists(HttpContext.Current.Server.MapPath(item.Photo)))
                    File.Delete(HttpContext.Current.Server.MapPath(item.Photo));
            }
            return sampleViewMode;
        }





        /// <summary>
        ///
        /// </summary>
        /// <param name="SampleViewModel"></param>
        /// <returns></returns>
        public TBL_Samples ToTBL_Sample(PostSampleViewModel SampleViewModel)
        {
            if (SampleViewModel == null)
                return null;
            return new TBL_Samples()
            {
                Id = SampleViewModel.Id,
                Link = SampleViewModel.Link,
                Description = SampleViewModel.Description,
                Name = SampleViewModel.Name,
                UserId = GetCurerentUser.GetUserId(),
                //Images = SampleViewModel.Images,
            };
        }





        /// <summary>
        ///
        /// </summary>
        /// <param name="Sample"></param>
        /// <returns></returns>
        public SampleViewModel ToSampleViewModel(TBL_Samples Sample)
        {
            if (Sample == null)
                return null;
            return new SampleViewModel()
            {
                Id = Sample.Id,
                Link = Sample.Link,
                Description = Sample.Description,
                Name = Sample.Name,
                UserId = Sample.UserId,
                Images = Sample.Images,
                User = Sample.User,
            };
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<SampleViewModel> ToSampleViewModel(List<TBL_Samples> Samples)
        {
            if (Samples == null)
                return null;
            return Samples.Select(s => new SampleViewModel
            {
                Id = s.Id,
                Link = s.Link,
                Description = s.Description,
                Name = s.Name,
                UserId = s.UserId,
                Images = s.Images,
                User = s.User,
            }).ToList();
        }





        /// <summary>
        ///
        /// </summary>
        /// <param name="Sample"></param>
        /// <returns></returns>
        public GetSampleViewModel ToGetSampleViewModel(TBL_Samples Sample)
        {
            if (Sample == null)
                return null;
            var GetSampleViewModel = new GetSampleViewModel()
            {
                Id = Sample.Id,
                Link = Sample.Link,
                Description = Sample.Description,
                Name = Sample.Name,
                UserId = Sample.UserId,
                Images = new List<ImageDbViewModel>(),
                User = new UserDbViewModel()
            };

            if (Sample.User != null)
            {
                GetSampleViewModel.User.Id = Sample.User.Id;
                GetSampleViewModel.User.RegDate = Sample.User.RegDate;
                GetSampleViewModel.User.RoleType = Sample.User.RoleType;
                GetSampleViewModel.User.UserName = Sample.User.UserName;
                GetSampleViewModel.User.Password = "";
            }
            foreach (var item in Sample.Images)
            {
                GetSampleViewModel.Images.Add(new ImageDbViewModel()
                {
                    Id = item.Id,
                    Photo = item.Photo,
                    SamplesId = item.SamplesId,
                });
            }
            return GetSampleViewModel;
        }

      





        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name=""></param>
        ///// <returns></returns>
        //public List<GetSampleViewModel> ToGetSampleViewModel(List<TBL_Samples> Samples)
        //{
        //    if (Samples == null)
        //        return null;
        //    return Samples.Select(s => new GetSampleViewModel
        //    {
        //        Id = s.Id,
        //        Link = s.Link,
        //        Description = s.Description,
        //        Name = s.Name,
        //        UserId = s.UserId,
        //        User = s.User != null ? new UserDbViewModel() { Id = s.User.Id, UserName = s.User.UserName, RoleType = s.User.RoleType, RegDate = s.User.RegDate, } :null,
        //       Images=  
        //        //Images = s.Images,
        //        //User = s.User,
        //    }).ToList();
        //}





    }
}