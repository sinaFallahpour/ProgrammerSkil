using ProgrammerSkil.Models.BLL.Interfaces;
using ProgrammerSkil.Models.DLL.Interfaces;
using ProgrammerSkil.Models.Utilities;
using ProgrammerSkil.Models.ViewModel.Auth;
using ProgrammerSkil.Models.ViewModel.ModelStateViewMode;
using ProgrammerSkil.Models.ViewModel.User;
using ProgrammerSkil.Models.ViewModel.User.DTO;
using ProgrammerSkil.Models.ViewModel.User.ProgrammerDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ModelBinding;
using TAD_Security;

namespace ProgrammerSkil.Models.BLL
{
    public class UserManager : IUserManager
    {
        private IUserRepository _repo;
        private IUserProfileRepository _userProfileRepo;

        public UserManager(IUserRepository Repo, IUserProfileRepository UserProfieRepo)
        {
            _repo = Repo;
            _userProfileRepo = UserProfieRepo;
        }





        /// گرفتن کاربر با آیدی
        /// </summary>
        /// <param name="Id">آیدی کاربر</param>
        /// <returns></returns>
        public async Task<UserViewModel> GetById(int? Id)
        {
            if (Id == null)
                return null;
            var User = await _repo.GetById((int)Id);
            return ToUserViewModel(User);
        }






        /// گرفتن کاربر با آیدی
        /// </summary>
        /// <param name="Id">آیدی کاربر</param>
        /// <returns></returns>
        public async Task<UserViewModel> GetUserById(int? Id)
        {
            if (Id == null)
                return null;
            var User = await _repo.GetUserById((int)Id);
            return ToUserViewModel(User);
        }





        /// گرفتن کاربر با آیدی
        /// </summary>
        /// <param name="Id">آیدی کاربر</param>
        /// <returns></returns>
        public async Task<ProgrammerDTO> GetProgrammerById(int? Id)
        {
            if (Id == null)
                return null;
            var Programmer = await _repo.GetProgrammerById((int)Id);
            var ProgrammerDTO = AutoMapper.Mapper.Map<TBL_User, ProgrammerDTO>(Programmer);
            return ProgrammerDTO;
            //return ToUserViewModel(User);
        }






        /// گرفتن کاربر با آیدی
        /// </summary>
        /// <param name="Id">آیدی کاربر</param>
        /// <returns></returns>
        public async Task<AdminDTO> GetAdminWithJoinById(int? Id)
        {
            if (Id == null)
                return null;
            var Admin = await _repo.GetAdminWithJoinById((int)Id);
            var AdminDTO = AutoMapper.Mapper.Map<TBL_User, AdminDTO>(Admin);
            return AdminDTO;
        }







        /// گرفتن کاربر با آیدی
        /// </summary>
        /// <param name="Id">آیدی کاربر</param>
        /// <returns></returns>
        public async Task<UserViewModel> GetAdminById(int? Id)
        {
            if (Id == null)
                return null;
            var User = await _repo.GetAdminById((int)Id);
            return ToUserViewModel(User);
        }




        public async Task<TBL_User> CredentialForUser(string Username, string passWord)
        {
            passWord = passWord.GetHash();
            return await _repo.Crential(Username, passWord);
        }





        public async Task<TBL_User> CredentialForAdmin(string Username, string passWord)
        {
            passWord = passWord.GetHash();
            return await _repo.CrentialForAdmin(Username, passWord);
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





        public int AdminCount()
        {
            return _repo.AdminCount();
        }





        public int ProgrammerCount()
        {
            return _repo.ProgrammerCount();
        }





        public int UserCount()
        {
            return _repo.UserCount();
        }





        public int Count()
        {
            return _repo.Count();
        }





        public bool IsUsertExistForChangePassword(int? Id, string PassWord)
        {
            if (Id == null)
                return false;
            PassWord = PassWord.GetHash();
            return _repo.IsUsertExistForChangePassword((int)Id, PassWord);
        }





        /// <summary>
        /// گرفتن لیست همه 
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserViewModel>> GetAllAdmin()
        {
            var Admins = await _repo.GetAllAdmin();
            return ToUserViewModel(Admins);
        }





        /// <summary>
        ///    Get All Admin for Pagination
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public async Task<List<UserViewModel>> GetAllAdnminForPagination(int? Page, int? PageSize)
        {
            if (Page == null || PageSize == null)
                return null;
            var Admins = await _repo.GetAllAdnminForPagination((int)Page, (int)PageSize);
            return ToUserViewModel(Admins);
        }





        /// <summary>
        /// گرفتن لیست همه 
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserViewModel>> GetAllUser()
        {
            var Users = await _repo.GetAllUser();
            return ToUserViewModel(Users);
        }





        /// <summary>
        ///    Get All User for Pagination
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public async Task<List<UserViewModel>> GetAllUserForPagination(int? Page, int? PageSize)
        {
            if (Page == null || PageSize == null)
                return null;
            var Users = await _repo.GetAllUserForPagination((int)Page, (int)PageSize);
            return ToUserViewModel(Users);
        }





        /// <summary>
        /// گرفتن لیست همه 
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserDetailsViewModel>> GetAllProgrammer()
        {
            var Programmer = await _repo.GetAllProgrammer();
            return ToUserDetailsViewModel(Programmer);
        }





        /// <summary>
        ///    Get All Programmer for Pagination
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public async Task<List<UserDetailsViewModel>> GetAllProgrammerForPagination(int? Page, int? PageSize)
        {
            if (Page == null || PageSize == null)
                return null;
            var Programmer = await _repo.GetAllProgrammerForPagination((int)Page, (int)PageSize);
            return ToUserDetailsViewModel(Programmer);
        }





        public async Task<UserDetailsViewModel> SaveToken(TBL_User User, string token, string SerialNumber)
        {
            User.SerialNumber = SerialNumber;
            var TBL_User = await _repo.CreateJwt(User, token.GetHash(), SerialNumber);
            if (TBL_User == null)
                return null;
            return await ToUserDetailsViewModel(TBL_User);
        }





        public async Task<UserViewModel> SaveTokenForRegister(TBL_User User, string token, string SerialNumber)
        {
            User.SerialNumber = SerialNumber;
            var TBL_User = await _repo.CreateJwt(User, token.GetHash(), SerialNumber);
            if (TBL_User == null)
                return null;
            return ToUserViewModel(TBL_User);
        }





        public async Task<UserDetailsViewModel> Crential(string userName, string password)
        {
            password = password.GetHash();
            var user = await _repo.Crential(userName, password);
            return await ToUserDetailsViewModel(user);
        }





        public async Task<TBL_User> CrentialForAdmin(string userName, string password)
        {
            password = password.GetHash();
            var Admin = await _repo.CrentialForAdmin(userName, password);
            return Admin;
            //return ToAdminDetailsViewModel(Admin);
        }





        public async Task<UserViewModel> IsUserExistById(int? UserId)
        {
            if (UserId == null)
                return null;
            var User = await _repo.IsUserExistById((int)UserId);
            return ToUserViewModel(User);
        }





        public async Task<UserViewModel> ChangeRoleToProgrammer(UserViewModel User)
        {
            if (User == null)
                return null;
            var user = ToTBL_User(User);
            var Use = await _repo.ChangeRoleToProgrammer(user);
            return ToUserViewModel(Use);
        }





        public async Task<UserViewModel> ChangeRoleToUser(UserViewModel User)
        {
            if (User == null)
                return null;
            var user = ToTBL_User(User);
            var Use = await _repo.ChangeRoleToUser(user);
            return ToUserViewModel(Use);
        }





        public async Task<UserViewModel> ChangeRoleToAdmin(UserViewModel User)
        {
            if (User == null)
                return null;
            var user = ToTBL_User(User);
            var Use = await _repo.ChangeRoleToAdmin(user);
            return ToUserViewModel(Use);
        }





        public async Task<bool> ChangePasswrod(int Id, string NewPassword)
        {
            NewPassword = NewPassword.GetHash();
            var TBL_User =await _repo.ChangePasswrod(Id, NewPassword);
            if (TBL_User == null)
                return false;
            return true;
        }





        public bool LogOut(string Token)
        {
            return _repo.LogOut(Token);
        }





        /// <summary>
        ///
        /// </summary>
        /// <param name="TBL_User"></param>
        /// <returns></returns>
        public TBL_User ToTBL_User(UserViewModel UserViewModel)
        {
            if (UserViewModel == null)
                return null;
            return new TBL_User()
            {
                RegDate = DateTime.Now,
                UserName = UserViewModel?.UserName,
                Password = UserViewModel?.Password,
                Id = UserViewModel.Id,
                RoleType = UserViewModel?.RoleType,
                SerialNumber = Guid.NewGuid().ToString()
            };
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="TBL_User"></param>
        /// <returns></returns>
        public async Task<UserDetailsViewModel> ToUserDetailsViewModel(TBL_User TBL_User)
        {
            if (TBL_User == null)
                return null;
            var User = new UserDetailsViewModel()
            {
                RegDate = TBL_User.RegDate,
                UserName = TBL_User?.UserName,
                Password = TBL_User?.Password,
                Id = TBL_User.Id,
                RoleType = TBL_User?.RoleType,
            };

            if (TBL_User.UserProfile != null)
            {
                User.Photo = TBL_User.UserProfile.Photo;
                User.Name = TBL_User.UserProfile.Name;
                User.Email = TBL_User.UserProfile.Email;
                User.Biography = TBL_User.UserProfile.Biography;
                //User.UserProfile = TBL_User.UserProfile;
                User.Skills = await _userProfileRepo.ProgrammerSkills(TBL_User.UserProfile.UserId);
            }
            return User;
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="TBL_User"></param>
        /// <returns></returns>
        public List<UserDetailsViewModel> ToUserDetailsViewModel(List<TBL_User> Users)
        {
            if (Users == null)
                return null;
            var User = Users.Select(s => new UserDetailsViewModel
            {
                RegDate = s.RegDate,
                UserName = s.UserName,
                Password = s.Password,
                Id = s.Id,
                RoleType = s?.RoleType,
                Photo = s.UserProfile?.Photo,
                Name = s.UserProfile?.Name,
                Email = s.UserProfile?.Email,
                Biography = s.UserProfile?.Biography,
                Skills = s.UserProfile?.SkillUserProfile?.Select(c => c.Skill.Title).ToList(),
                //UserProfile = s.UserProfile,
                JwtToken = ""
            }).ToList();
            return User;
        }





        /// <summary>
        ///
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public UserViewModel ToUserViewModel(TBL_User User)
        {
            if (User == null)
                return null;
            return new UserViewModel()
            {
                RegDate = DateTime.Now,
                UserName = User?.UserName,
                Password = User?.Password,
                Id = User.Id,
                RoleType = User?.RoleType,
                JwtToken = "",
            };
        }





        /// <summary>
        ///
        /// </summary>
        /// <param name="Users"></param>
        /// <returns></returns>
        public List<UserViewModel> ToUserViewModel(List<TBL_User> Users)
        {
            if (Users == null)
                return null;
            var UserViewModel = Users.Select(s => new UserViewModel()
            {
                RegDate = s.RegDate,
                UserName = s.UserName,
                Password = s.Password,
                Id = s.Id,
                RoleType = s.RoleType,
                JwtToken = "",
            }).ToList();
            return UserViewModel;
        }








        /// <summary>
        ///
        /// </summary>
        /// <param name="Admin"></param>
        /// <returns></returns>
        public AdminDetailsViewModel ToAdminDetailsViewModel(TBL_User Admin)
        {
            if (Admin == null)
                return null;
            return new AdminDetailsViewModel()
            {
                RegDate = Admin.RegDate,
                UserName = Admin?.UserName,
                Password = Admin?.Password,
                Id = Admin.Id,
                RoleType = Admin.RoleType,
            };
        }




        /// <summary>
        /// حذف 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<object> Delete(int? Id)
        {
            if (Id == null)
                return null;
            var User = await _repo.Remove((int)Id);
            if (User == null)
                return null;
            if (User.RoleType == Static.ProgrammerRole)
                return await ToUserDetailsViewModel(User);
            return ToUserViewModel(User);
        }





        public bool IsSkillsExist(List<int> SkillsId)
        {
            if (SkillsId == null)
                return false;
            return _repo.IsSkillsExist(SkillsId);
        }





        public async Task<ProgrammerDTO> UpdateProgrammerProfile(ProgrammerUpdateProfileViewModel Profile)
        {
            if (Profile == null)
                return null;
            var Programmer = await _repo.UpdateProgrammerProfile(Profile);
            var ProgrammerDTO = AutoMapper.Mapper.Map<TBL_User, ProgrammerDTO>(Programmer);
            return ProgrammerDTO;
        }





    }
}