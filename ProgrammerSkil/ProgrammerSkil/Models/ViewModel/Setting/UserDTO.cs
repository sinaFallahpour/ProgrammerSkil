using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.ViewModel.Setting
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string RoleType { get; set; }
        public DateTime RegDate { get; set; }

        #region RELATION
        public virtual UserProfileDTO UserProfile { get; set; }
        #endregion
    }
}