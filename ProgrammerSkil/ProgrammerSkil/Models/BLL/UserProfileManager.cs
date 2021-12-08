using ProgrammerSkil.Models.BLL.Interfaces;
using ProgrammerSkil.Models.DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.BLL
{
    public class UserProfileManager: IUserProfileManager
    {
        private IUserProfileRepository _repo;
        public UserProfileManager(IUserProfileRepository Repo)
        {
            _repo = Repo;
        }
    }
}