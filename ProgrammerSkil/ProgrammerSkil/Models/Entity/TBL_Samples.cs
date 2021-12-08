using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models
{
    public class TBL_Samples
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "عنوان نمونه کار")]
        [Required(ErrorMessage = "عنوان الزامیست")]
        public string Name { get; set; }

        public string Link { get; set; }

        [Required(ErrorMessage = "توضیحات الزامیست")]
        [Display(Name = "توضیحات ")]
        [MinLength(30, ErrorMessage = "حداقل 30 کاراکتر")]
        public string Description { get; set; }


        #region RELATION

        // relation With TBL_USer
        [ForeignKey("UserId")]
        public virtual TBL_User User { get; set; }
        [Display(Name = "User ")]
        public int? UserId { get; set; }

        public virtual ICollection<TBL_Images> Images { get; set; }

        #endregion
    }
}