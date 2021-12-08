using ProgrammerSkil.Models.BaseDll;
using ProgrammerSkil.Models.ViewModel.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgrammerSkil.Models.DLL.Interfaces
{
    public interface IAuthRepository : IBase
    {
        Task<TBL_User> RegisterUser(TBL_User User);
        bool IsUsernameExist(string UserName);
        bool IsUsernameExistForEdit(string UserName, int Id);
    }
}