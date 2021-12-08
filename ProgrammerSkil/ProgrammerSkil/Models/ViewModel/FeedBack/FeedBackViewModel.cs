using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.ViewModel.FeedBack
{
    public class FeedBackViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "الزامیست {0}")]
        [Display(Name = "توضیحات ")]
        [MinLength(4, ErrorMessage = "حداقل 4 کاراکتر")]
        public string Text { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RegDate { get; set; }

        #region RELATION

        // relation With TBL_USer
        [ForeignKey("UserId")]
        public virtual TBL_User User { get; set; }
        [Display(Name = "User ")]
        public int? UserId { get; set; }

        #endregion
    }
}