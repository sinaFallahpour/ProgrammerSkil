using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.ViewModel.Auth
{
    public class RegisterViewModel
    {
  
        [Required(ErrorMessage = "نام کاربری الزامیست")]
        [MinLength(4, ErrorMessage = "حداقل 4 کاراکتر")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "پسورد الزامیست")]
        [MinLength(5, ErrorMessage = "حداقل ۵ کاراکتر")]
        public string Password { get; set; }

        [Required(ErrorMessage = "نقش الزامیست")]
        public string Role { get; set; }
    }
}