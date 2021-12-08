using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.ViewModel.Setting
{
    public class SettingDTO
    {

        public int Id { get; set; }

        //[Display(Name = "موضوع")]
        //public string Theme { get; set; }

        public string Phone { get; set; }

        public string AboutUs { get; set; }

        #region RELATION
        public virtual UserDTO User { get; set; }

        public virtual ICollection<SocialMediaDTO> SocialMedias { get; set; }
        #endregion   
    }
}