using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models
{
    public class TBL_Skill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "عنوان مهارت")]
        public string Title { get; set; }

        #region RELATION
        //n tp n with userProfile using interface TBL
        public virtual ICollection<TBL_SkillUserProfile> SkillUserProfile { get; set; }

        #endregion

    }
}