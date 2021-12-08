using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.ViewModel.Skill
{
    public class SkillViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "عنوان مهارت")]
        [Required(ErrorMessage ="عنوان مهارت الزامیست")]
        [MinLength(5,ErrorMessage ="حداقل 5 کاراکتر")]
        public string Title { get; set; }

        public int count { get; set; }
    }
}