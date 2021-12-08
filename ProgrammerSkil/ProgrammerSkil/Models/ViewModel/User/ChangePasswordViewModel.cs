using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.ViewModel.User
{
    public class ChangePasswordViewModel
    {

        [Required(ErrorMessage ="پسورد قبلی الزامیست")]
        [DataType(DataType.Password)]
        public string OldPasswrod { get; set; }



        [Required(ErrorMessage ="پسورد جدید الزامیست")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }



        [Required(ErrorMessage ="پسورد جدید االزامیست")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="تایید پسورد با پسورد مطابقت ندارد")]
        public string NewPasswordConfirm { get; set; }
       


    }
}