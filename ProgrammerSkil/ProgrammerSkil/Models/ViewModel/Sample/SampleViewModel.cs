using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.ViewModel.Sample
{
    public class SampleViewModel
    {
       
        public int Id { get; set; }

        [Display(Name = "عنوان نمونه کار")]
        [Required(ErrorMessage = "  الزامیست{0}")]
        public string Name { get; set; }

        [Required(ErrorMessage ="لینک الزامیست")]
        public string Link { get; set; }

        [Required(ErrorMessage = "الزامیست {0}")]
        [Display(Name = "توضیحات ")]
        [MinLength(30, ErrorMessage = "حداقل 30 کاراکتر")]
        public string Description { get; set; }


        #region RELATION

        // relation With TBL_USer
        public virtual TBL_User User { get; set; }
        [Display(Name = "User ")]
        public int? UserId { get; set; }
        public virtual ICollection<TBL_Images> Images { get; set; }

        #endregion 
    }
}