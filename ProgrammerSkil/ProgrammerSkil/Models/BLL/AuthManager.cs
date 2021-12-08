using ProgrammerSkil.Models.BLL.Interfaces;
using ProgrammerSkil.Models.DLL.Interfaces;
using ProgrammerSkil.Models.Utilities;
using ProgrammerSkil.Models.ViewModel.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TAD_Security;

namespace ProgrammerSkil.Models.BLL
{
    public class AuthManager : IAuthManager
    {
        private IAuthRepository _repo;
        public AuthManager(IAuthRepository Repo)
        {
            _repo = Repo;
        }





        public bool IsUsernameExist(string userName)
        {
            return _repo.IsUsernameExist(userName);
        }





        public bool IsUsernameExistForEdit(string UserName,int Id)
        {
            return _repo.IsUsernameExistForEdit(UserName, Id);
        }





        /// <summary>
        /// ایجاد کاربر جدید با رول یوزر
        /// </summary>
        /// <returns></returns>
        public async Task<TBL_User> RegisterUser(RegisterViewModel Register)
        {
            var TBL_User = ToRegisterUserDataViewModel(Register);
            TBL_User.RoleType = Static.UserRole;
            TBL_User.Password = TBL_User.Password.GetHash();
            return await _repo.RegisterUser(TBL_User);
        }





        /// <summary>
        /// تبدیل  یوز ویو مدل به تی بی ال یوز
        /// </summary>
        /// <param name="DataRow"></param>
        /// <returns></returns>
        public TBL_User ToRegisterUserDataViewModel(RegisterViewModel RegisterViewModel)
        {
            if (RegisterViewModel == null)
                return null;
            return new TBL_User()
            {
                RegDate = DateTime.Now,
                UserName = RegisterViewModel?.UserName,
                Password = RegisterViewModel?.Password,
            };
        }





    }
}