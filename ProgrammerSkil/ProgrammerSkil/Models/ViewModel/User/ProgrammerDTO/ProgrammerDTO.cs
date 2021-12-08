using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.ViewModel.User.ProgrammerDTO
{
    public class ProgrammerDTO
    {
       

        public int Id { get; set; }
    

        public string UserName { get; set; }
    

        public string Password { get; set; }
    

        public string RoleType { get; set; }


        public DateTime RegDate { get; set; }


        #region RELATION

        public virtual UserProFileDTO UserProfile { get; set; }
        public virtual ICollection<FeedBackDTO> FeedBackses { get; set; }
        public virtual ICollection<CooperationDTO> Cooperations { get; set; }

        #endregion
    }
}