using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.ViewModel.User
{
    public class UserDetailsViewModel
    {
     
        public int Id { get; set; }

        [Required(ErrorMessage = "الزامیست {0}")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "الزامیست {0}")]
        [Display(Name = "پسورد ")]
        public string Password { get; set; }

        [Required(ErrorMessage = "الزامیست {0}")]
        [Display(Name = "نقش")]
        public string RoleType { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RegDate { get; set; }


       ///
        [Required(ErrorMessage = "الزامیست {0}")]
        [Display(Name = "نام")]
        [DataType(DataType.EmailAddress)]
        public string Name { get; set; }

        [Required(ErrorMessage = "الزامیست {0}")]
        [Display(Name = "ایمیل")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "بیو گرافی")]
        public string Biography { get; set; }

        [Display(Name = "تصویر")]
        public string Photo { get; set; }


        public List<string> Skills { get; set; }

        //public TBL_UserProfile UserProfile { get; set; }

        public string JwtToken { get; set; }
    }
}