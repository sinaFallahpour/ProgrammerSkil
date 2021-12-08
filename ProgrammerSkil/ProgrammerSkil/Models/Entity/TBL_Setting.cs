using ProgrammerSkil.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models
{
    public class TBL_Setting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[Display(Name = "موضوع")]
        //public string Theme { get; set; }

        [Display(Name = "شماره تماس")]
        public string Phone { get; set; }

        [Display(Name = "درباره ما")]
        public string AboutUs { get; set; }

        #region RELATION
        // relation With TBL_User
        [ForeignKey("UserId")]
        public virtual TBL_User User { get; set; }
        [Display(Name = "User ")]
        public int? UserId { get; set; }

        public virtual ICollection<TBL_SocialMedia> SocialMedias { get; set; }
        #endregion
    }
}