using ProgrammerSkil.Models.ViewModel.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgrammerSkil.Models.BLL.Interfaces
{
    public interface IAuthManager
    {
        //Register USer with Role User
        Task<TBL_User> RegisterUser(RegisterViewModel Register);
        //Change to Data Model
        TBL_User ToRegisterUserDataViewModel(RegisterViewModel RegisterViewModel);
        bool IsUsernameExist(string UserName);
        bool IsUsernameExistForEdit(string UserName,int Id);
    }
}