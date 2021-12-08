using ProgrammerSkil.Models.BaseDll;
using ProgrammerSkil.Models.Entity;
using ProgrammerSkil.Models.ViewModel.Auth;
using ProgrammerSkil.Models.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgrammerSkil.Models.DLL.Interfaces
{
    public interface IUserRepository : IBase
    {
        Task<TBL_User> GetById(int Id);
        Task<TBL_User> GetUserById(int Id);
        Task<TBL_User> GetProgrammerById(int Id);
        Task<TBL_User> GetAdminById(int Id);
        Task<TBL_User> GetAdminWithJoinById(int Id);
        Task<List<TBL_User>> GetAllAdmin();
        Task<List<TBL_User>> GetAllAdnminForPagination(int Page, int PageSize);
        int AdminCount();
        Task<List<TBL_User>> GetAllUser();
        Task<List<TBL_User>> GetAllUserForPagination(int Page, int PageSize);
        int UserCount();
        Task<List<TBL_User>> GetAllProgrammer();
        Task<List<TBL_User>> GetAllProgrammerForPagination(int Page, int PageSize);
        int ProgrammerCount();
        int Count();
        bool IsUsertExistForChangePassword(int Id, string PassWord);
        Task<TBL_User> Crential(string userName, string password);
        Task<TBL_User> CrentialForAdmin(string userName, string password);
        Task<TBL_User> IsUserExistById(int UserId);
        Task<TBL_User> ChangeRoleToAdmin(TBL_User User);
        Task<TBL_User> ChangeRoleToProgrammer(TBL_User User);
        Task<TBL_User> ChangeRoleToUser(TBL_User User);
       Task<TBL_User> ChangePasswrod(int Id, string NewPassword);
        bool LogOut(string Token);
        Task<int?> RegisterUser(TBL_User User);
        Task<TBL_User> Remove(int Id);
        bool IsSkillsExist(List<int> SkillsId);
        Task<TBL_User> UpdateProgrammerProfile(ProgrammerUpdateProfileViewModel Profile);


        Task<TBL_User> CreateJwt(TBL_User User, string token, string SerialNumber);

        bool CreateUserToken(TBL_Token userToken);

        TBL_Token FindToken(string tokenHash);


    }
}