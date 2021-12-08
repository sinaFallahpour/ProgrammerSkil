using ProgrammerSkil.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.ViewModel.Setting
{
    public class SettingViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[Display(Name = "موضوع")]
        //public string Theme { get; set; }

        [Display(Name = "شماره تماس")]
        public string Phone { get; set; }

        [Display(Name = "درباره ما")]
        [MinLength(30,ErrorMessage ="حداقل 30 کاراکتر")]
        public string AboutUs { get; set; }

        #region RELATION
        public virtual ICollection<TBL_SocialMedia> SocialMedias { get; set; }
        #endregion    

    }
}