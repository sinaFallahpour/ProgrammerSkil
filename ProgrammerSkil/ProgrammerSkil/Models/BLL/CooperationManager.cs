using ProgrammerSkil.Models.BLL.Interfaces;
using ProgrammerSkil.Models.DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProgrammerSkil.Models;
using ProgrammerSkil.Models.ViewModel.Cooperation;
using System.Threading.Tasks;
using ProgrammerSkil.Models.Enums;
using AutoMapper;
using ProgrammerSkil.Models.ViewModel.ModelStateViewMode;
using System.Web.Http.ModelBinding;

namespace ProgrammerSkil.Models.BLL
{
    public class CooperationManager : ICooperationManager
    {
        private ICooperationRepository _repo;
        public CooperationManager(ICooperationRepository Repo)
        {
            _repo = Repo;
        }


        /// <summary>
        /// Get Skill By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<CooperationDTO> GetById(int? Id)
        {
            if (Id == null)
                return null;
            var Coopertaion = await _repo.GetById((int)Id);
            var CooperationDTOs = AutoMapper.Mapper.Map<TBL_Cooperation, CooperationDTO>(Coopertaion);
            return CooperationDTOs;
            //return ToCooperationViewModel(Coopertaion);
        }





        /// <summary>
        /// گرفتن لیست همه 
        /// </summary>
        /// <returns></returns>
        public async Task<List<CooperationDTO>> GetAll()
        {
            var Coopertaion = await _repo.GetAll();
            var CooperationDTOs = AutoMapper.Mapper.Map<List<TBL_Cooperation>, List<CooperationDTO>>(Coopertaion);
            return CooperationDTOs;
            //return ToCooperationViewModel(cooperationDto);
        }





        /// <summary>
        /// گرفتن لیست همه cooperation  ها
        /// </summary>
        /// <returns></returns>
        public async Task<List<CooperationDTO>> GetAllForPagination(int? Page, int? PageSize)
        {
            if (Page == null || PageSize == null)
                return null;
            var Coopertaion = await _repo.GetAllForPagination((int)Page, (int)PageSize);
            var CooperationDTOs = AutoMapper.Mapper.Map<List<TBL_Cooperation>, List<CooperationDTO>>(Coopertaion);
            return CooperationDTOs;
            //return ToCooperationViewModel(Coopertaion);
        }





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




        public bool IsExist(int? Id)
        {
            if (Id == null) return false;
            return _repo.IsExist((int)Id);
        }





        public int Count()
        {
            return _repo.Count();
        }





        /// <summary>
        /// ایجاد Coopertaion جدید
        /// </summary>
        /// <returns></returns>
        public async Task<CooperationViewModel> Create(CooperationViewModel CooperationViewModel)
        {
            var TBL_Cooperation = ToTBL_Cooperation(CooperationViewModel);
            var Cooperation = await _repo.Create(TBL_Cooperation);
            return ToCooperationViewModel(Cooperation);
        }





        /// <summary>
        /// update cooperation
        /// </summary>
        /// <returns></returns>
        public async Task<CooperationViewModel> Update(CooperationViewModel Coopertaion)
        {
            var TBL_Cooperation = ToTBL_Cooperation(Coopertaion);
            var Cooperation = await _repo.Update(TBL_Cooperation);
            return ToCooperationViewModel(Cooperation);
            //var TBL_Coopertaion = ToTBL_Cooperation(Coopertaion);
            //return await _repo.Update(TBL_Coopertaion);
        }




        public async Task<CooperationDTO> ChangeCooporationStatus(int? Id, CooperationStatus Status )
        {
            if (Id == null)
                return null;
            var Cooperation = await _repo.ChangeCooporationStatus((int)Id,  Status);
           var CooperationDTOs = AutoMapper.Mapper.Map<TBL_Cooperation, CooperationDTO>(Cooperation);
            return CooperationDTOs;
        }





        /// <summary>
        /// حذف اسلاید
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
        /// <param name="CooperationViewModel"></param>
        /// <returns></returns>
        public TBL_Cooperation ToTBL_Cooperation(CooperationViewModel CooperationViewModel)
        {
            if (CooperationViewModel == null)
                return null;
            return new TBL_Cooperation()
            {
                Id = CooperationViewModel.Id,
                Status = ToEnumType(CooperationViewModel.Status),
                Text = CooperationViewModel.Text,
                ResumeLink = CooperationViewModel.ResumeLink,
                UserId = CooperationViewModel.UserId
            };
        }





        public int ToEnumValue(CooperationStatus Status)
        {
            return (int)Status;
        }





        public CooperationStatus ToEnumType(int StatusValue)
        {
            if (StatusValue == 0)
                return CooperationStatus.NoChecked;
            else if (StatusValue == 1)
                return CooperationStatus.rejected;
            else
                return CooperationStatus.Reviewed;
        }





        /// <summary>
        ///
        /// </summary>
        /// <param name="Cooperation"></param>
        /// <returns></returns>
        public CooperationViewModel ToCooperationViewModel(TBL_Cooperation Cooperation)
        {
            if (Cooperation == null)
                return null;
            return new CooperationViewModel()
            {
                Id = Cooperation.Id,
                Status = (int)Cooperation.Status,
                Text = Cooperation.Text,
                ResumeLink = Cooperation.ResumeLink,
                User = Cooperation.User,
                UserId = Cooperation.UserId,
            };
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<CooperationViewModel> ToCooperationViewModel(List<TBL_Cooperation> Cooperations)
        {
            if (Cooperations == null)
                return null;
            return Cooperations.Select(s => new CooperationViewModel
            {
                Id = s.Id,
                Status = (int)s.Status,
                Text = s.Text,
                ResumeLink = s.ResumeLink,
                User = s.User,
                UserId = s.UserId
            }).ToList();
        }

       
    }
}