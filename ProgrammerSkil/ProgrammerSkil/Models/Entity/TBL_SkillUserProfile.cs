using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models
{
    public class TBL_SkillUserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        #region RELATION
        // relation With TBL_Skill
        [ForeignKey("SkillId")]
        public virtual TBL_Skill Skill { get; set; }
        [Display(Name = "Skill ")]
        public int? SkillId { get; set; }

        // relation With TBL_USer
        [ForeignKey("UserProfileId")]
        public virtual TBL_UserProfile UserProfile { get; set; }
        [Display(Name = "User Profile ")]
        public int? UserProfileId { get; set; }


        #endregion  
    }
}