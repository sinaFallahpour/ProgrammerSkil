using ProgrammerSkil.Models.BLL.Interfaces;
using ProgrammerSkil.Models.DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.BLL
{
    public class SkillUserProfileManager: ISkillUserProfileManager
    {
        private ISkillUserProfileRepository _repo;
        public SkillUserProfileManager(ISkillUserProfileRepository Repo)
        {
            _repo = Repo;
        }
    }
}