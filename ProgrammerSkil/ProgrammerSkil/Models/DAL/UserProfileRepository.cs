using ProgrammerSkil.Models.BaseDll;
using ProgrammerSkil.Models.DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgrammerSkil.Models.DLL
{

    public class UserProfileRepository : Base, IUserProfileRepository
    {
        #region  Field
        private readonly DbSet<TBL_UserProfile> _UserProfile;
        private readonly DbSet<TBL_SkillUserProfile> _SkillUserProfile;

        #endregion  Field
        #region constructor
        public UserProfileRepository(ProgrammerSkilContext db)
        {
            _UserProfile = _db.TBL_UserProfile;
            _SkillUserProfile = _db.TBL_SkillUserProfile;

        }

    
        #endregion constructors

        public async Task<List<string>> ProgrammerSkills(int Id)
        {
          return await  _SkillUserProfile.AsNoTracking().Where(c => c.UserProfileId == Id).Select(c => c.Skill.Title).ToListAsync();
        }

    }
}