using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.ViewModel.User
{
    public class UserDbViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

    }
}