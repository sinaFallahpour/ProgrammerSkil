using ProgrammerSkil.Models.BaseDll;
using ProgrammerSkil.Models.DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.DLL
{
    public class SkillUserProfileRepository: Base, ISkillUserProfileRepository
    {
        #region  Field
        private readonly DbSet<TBL_SkillUserProfile> _SkillUserProfile;
        #endregion  Field

        #region constructor
        public SkillUserProfileRepository(ProgrammerSkilContext db)
        {
            _SkillUserProfile = _db.TBL_SkillUserProfile;
        }
        #endregion constructor
    }
}