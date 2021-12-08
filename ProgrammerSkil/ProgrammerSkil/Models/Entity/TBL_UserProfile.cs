using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models
{
    public class TBL_UserProfile
    {

        ////[Key]
        ////[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        ////public int Id { get; set; }

        //[Required(ErrorMessage = "الزامیست {0}")]
        [Display(Name = "نام")]
        [DataType(DataType.EmailAddress)]
        public string Name { get; set; }

        //[Required(ErrorMessage = "الزامیست {0}")]
        [Display(Name = "ایمیل")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "بیو گرافی")]
        public string Biography { get; set; }

        [Display(Name = "تصویر")]
        public string Photo { get; set; }





        #region RELATION

        // relation With TBL_USer
        public virtual TBL_User User { get; set; }
        [Key, ForeignKey("User")]
        public int UserId { get; set; }

        //n tp n with Skill using interface TBL
        public virtual ICollection<TBL_SkillUserProfile> SkillUserProfile { get; set; }
        #endregion





    }
}