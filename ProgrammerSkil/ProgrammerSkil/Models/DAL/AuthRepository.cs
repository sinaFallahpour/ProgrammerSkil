using ProgrammerSkil.Models.BaseDll;
using ProgrammerSkil.Models.DLL.Interfaces;
using ProgrammerSkil.Models.ViewModel.Auth;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgrammerSkil.Models.DLL
{
    public class AuthRepository : Base, IAuthRepository
    {
        #region  Field
        private readonly DbSet<TBL_User> _Users;
        #endregion  Field

        #region constructor
        public AuthRepository()
        {
            _Users = _db.TBL_User;
        }
        #endregion constructor





        public bool IsUsernameExist(string username)
        {
            return _db.TBL_User.AsNoTracking().Any(o => o.UserName == username);
        }




        /// <summary>
        /// IsExist for Edit
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool IsUsernameExistForEdit(string UserName, int Id)
        {
            return _db.TBL_User.AsNoTracking().Any(o => o.UserName == UserName && o.Id != Id);
        }





        /// <summary>
        /// register User with Role User
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public async Task<TBL_User> RegisterUser(TBL_User User)
        {
            var user = _Users.Add(User);
            var result = await SaveChangeAsync();
            if (result)
            {
                return user;
            }
            else
                return null;
        }





    }
}