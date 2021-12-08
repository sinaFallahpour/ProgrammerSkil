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

namespace ProgrammerSkil.Models.BLL.Interfaces
{
    public interface IUserManager
    {
        Task<UserViewModel> GetUserById(int? Id);
        Task<ProgrammerDTO> GetProgrammerById(int? Id);
        Task<UserViewModel> GetAdminById(int? Id);
        Task<AdminDTO> GetAdminWithJoinById(int? Id);
        Task<TBL_User> CredentialForUser(string UserName, string Password);
        Task<TBL_User> CredentialForAdmin(string Username, string passWord);
        Task<List<UserViewModel>> GetAllAdmin();
        Task<List<UserViewModel>> GetAllAdnminForPagination(int? Page, int? PageSize);
        Task<List<UserViewModel>> GetAllUser();
        Task<List<UserViewModel>> GetAllUserForPagination(int? Page, int? PageSize);
        Task<List<UserDetailsViewModel>> GetAllProgrammer();
        Task<List<UserDetailsViewModel>> GetAllProgrammerForPagination(int? Page, int? PageSize);
        List<ModelStateViewMode> Validate(ModelStateDictionary ModelState);
        int AdminCount();
        int ProgrammerCount();
        int UserCount();
        int Count();
        bool IsUsertExistForChangePassword(int? Id, string PassWord);
        Task<UserDetailsViewModel> Crential(string userName, string password);
        Task<TBL_User> CrentialForAdmin(string userName, string password);
        Task<UserViewModel> IsUserExistById(int? UserId);
        Task<UserViewModel> ChangeRoleToProgrammer(UserViewModel User);
        Task<UserViewModel> ChangeRoleToUser(UserViewModel User);
        Task<UserViewModel> ChangeRoleToAdmin(UserViewModel User);
       Task< bool> ChangePasswrod(int Id, string NewPassword);
        bool LogOut(string Token);
        TBL_User ToTBL_User(UserViewModel UserViewModel);
        Task<UserDetailsViewModel> ToUserDetailsViewModel(TBL_User TBL_User);
        List<UserDetailsViewModel> ToUserDetailsViewModel(List<TBL_User> Users);
        UserViewModel ToUserViewModel(TBL_User User);
        List<UserViewModel> ToUserViewModel(List<TBL_User> Users);
        AdminDetailsViewModel ToAdminDetailsViewModel(TBL_User Admin);
        Task<object> Delete(int? Id);
        bool IsSkillsExist(List<int> SkillsId);
        Task<ProgrammerDTO> UpdateProgrammerProfile(ProgrammerUpdateProfileViewModel Profile);
        //Task<bool> SaveToken(TBL_User userDetails, string token, string SerialNumber);
        Task<UserDetailsViewModel> SaveToken(TBL_User User, string token, string SerialNumber);
        Task<UserViewModel> SaveTokenForRegister(TBL_User User, string token, string SerialNumber);
    }
}