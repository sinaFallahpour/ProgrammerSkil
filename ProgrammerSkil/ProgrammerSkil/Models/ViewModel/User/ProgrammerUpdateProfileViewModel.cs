using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.ViewModel.User
{
    public class ProgrammerUpdateProfileViewModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required(ErrorMessage = "الزامیست {0}")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "الزامیست {0}")]
        //[Display(Name = "نقش")]
        //public string RoleType { get; set; }

        [Required(ErrorMessage = "الزامیست {0}")]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "ایمیل")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "الزامیست {0}")]
        public string Email { get; set; }

        [Display(Name = "بیو گرافی")]
        [Required(ErrorMessage = "الزامیست {0}")]
        public string Biography { get; set; }

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "الزامیست {0}")]
        public string Photo { get; set; }

        public virtual List<int> SkillsId { get; set; }
    }
}