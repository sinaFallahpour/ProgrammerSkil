using ProgrammerSkil.Models.BLL.Interfaces;
using ProgrammerSkil.Models.DLL.Interfaces;
using ProgrammerSkil.Models.ExtentionMethods;
using ProgrammerSkil.Models.ViewModel.FeedBack;
using ProgrammerSkil.Models.ViewModel.ModelStateViewMode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ModelBinding;

namespace ProgrammerSkil.Models.BLL
{
    public class FeedBackManager : IFeedBackManager
    {
        private IFeedBackRepostory _repo;
        public FeedBackManager(IFeedBackRepostory Repo)
        {
            _repo = Repo;
        }





        /// <summary>
        /// Get Skill By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<FeedBackDTO> GetById(int? Id)
        {
            if (Id == null)
                return null;
            var FeedBacks = await _repo.GetById((int)Id);
            var FeedBacksDTO = AutoMapper.Mapper.Map<TBL_FeedBacks, FeedBackDTO>(FeedBacks);
            return FeedBacksDTO;
            //return ToFeedBackViewModel(FeedBack);
        }





        /// <summary>
        /// گرفتن لیست همه 
        /// </summary>
        /// <returns></returns>
        public async Task<List<FeedBackDTO>> GetAll()
        {
            var FeedBackses = await _repo.GetAll();
            var FeedBacksDTO = AutoMapper.Mapper.Map<List<TBL_FeedBacks>, List<FeedBackDTO>>(FeedBackses);
            return FeedBacksDTO;
            //return ToFeedBackViewModel(FeedBackses);
        }





        /// <summary>
        /// گرفتن لیست همه مهارت ها
        /// </summary>
        /// <returns></returns>
        public async Task<List<FeedBackDTO>> GetAllForPagination(int? Page, int? PageSize)
        {
            if (Page == null || PageSize == null)
                return null;
            var FeedBacks = await _repo.GetAllForPagination((int)Page, (int)PageSize);
            var FeedBascksDTO = AutoMapper.Mapper.Map<List<TBL_FeedBacks>, List<FeedBackDTO>>(FeedBacks);
            return FeedBascksDTO;
            //return ToFeedBackViewModel(FeedBacks);
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
        /// ایجاد FeedBask جدید
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Create(FeedBackViewModel Skill)
        {
            var TBL_FeedBack = ToTBL_FeedBack(Skill);
            return await _repo.Create(TBL_FeedBack);
        }





        /// <summary>
        /// ایجاد FedBack جدید
        /// </summary>
        /// <returns></returns>
        //public async Task<bool> Update(FeedBackViewModel FeedBack)
        //{
        //    var TBL_FeedBack = ToTBL_FeedBack(FeedBack);
        //    return await _repo.Update(TBL_FeedBack);
        //}





        /// <summary>
        /// حذف FeedBack
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
        /// <param name="ToTBL_FeedBack"></param>
        /// <returns></returns>
        public TBL_FeedBacks ToTBL_FeedBack(FeedBackViewModel FeedBackViewModel)
        {
            if (FeedBackViewModel == null)
                return null;
            var TBL_FeeBask = new TBL_FeedBacks()
            {
                Id = FeedBackViewModel.Id,
                Text = FeedBackViewModel.Text,
                RegDate = DateTime.Now,
                User = FeedBackViewModel.User,
                UserId =GetCurerentUser.GetUserId(), 
            };

            //get Current Claims Principal
            var Idemtity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            //get Current Claims Principal  value
            var UserId = Idemtity.Claims.Where(c => c.Type == "_Id")
                    .Select(c => c.Value).SingleOrDefault();
            int Id;
            if (int.TryParse(UserId, out Id))
            {
                TBL_FeeBask.UserId = Id;
            }
            return TBL_FeeBask;
        }





        /// <summary>
        ///
        /// </summary>
        /// <param name="FeedBack"></param>
        /// <returns></returns>
        public FeedBackViewModel ToFeedBackViewModel(TBL_FeedBacks FeedBack)
        {
            if (FeedBack == null)
                return null;
            return new FeedBackViewModel()
            {
                Id = FeedBack.Id,
                Text = FeedBack.Text,
                RegDate = FeedBack.RegDate,
                User = FeedBack.User,
                UserId = FeedBack.User?.Id
            };
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<FeedBackViewModel> ToFeedBackViewModel(List<TBL_FeedBacks> FeedBackses)
        {
            if (FeedBackses == null)
                return null;
            return FeedBackses.Select(s => new FeedBackViewModel
            {
                Id = s.Id,
                Text = s.Text,
                RegDate = s.RegDate,
                User = s.User,
                UserId = s.User?.Id
            }).ToList();
        }




        
    }
}