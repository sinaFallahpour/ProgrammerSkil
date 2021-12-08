using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.ViewModel.User.DTO
{
    public class UserProfileDTO
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Biography { get; set; }

        public string Photo { get; set; }
        
        #region RELATION
        public virtual ICollection<TBL_SkillUserProfile> SkillUserProfile { get; set; }

        #endregion
    }
}













