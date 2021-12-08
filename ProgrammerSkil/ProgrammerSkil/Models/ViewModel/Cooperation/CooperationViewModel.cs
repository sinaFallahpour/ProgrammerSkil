using ProgrammerSkil.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.ViewModel.Cooperation
{
    public class CooperationViewModel
    {
       
        public int Id { get; set; }

        [Required(ErrorMessage = "الزامیست {0}")]
        [Display(Name = "توضیحات ")]
        [MinLength(30, ErrorMessage = "حداقل 30 کاراکتر")]
        public string Text { get; set; }

        /// <summary>
        /// این یک  فایل پی دی اف
        /// </summary>
        [Required(ErrorMessage = "الزامیست {0}")]
        [Display(Name = "رزومه ")]
        public string ResumeLink { get; set; }

        [Required(ErrorMessage = "الزامیست {0}")]
        [Display(Name = "وضعیت")]
        public int Status { get; set; }

        #region RELATION
        // relation With TBL_USer
        [ForeignKey("UserId")]
        public virtual TBL_User User { get; set; }

        [Display(Name = "User ")]
        public int? UserId { get; set; }
        #endregion
    }
}