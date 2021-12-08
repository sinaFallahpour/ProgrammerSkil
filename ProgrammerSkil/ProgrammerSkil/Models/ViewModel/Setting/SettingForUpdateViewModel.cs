using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.ViewModel.Setting
{
    public class SettingForUpdateViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "شماره تماس")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name = "درباره ما")]
        [MinLength(30, ErrorMessage = "حداقل 30 کاراکتر")]
        public string AboutUs { get; set; }

        #region RELATION
        [Required(ErrorMessage ="لیست شبکه های اجتماعی الزامیست")]
        public virtual ICollection<int> SocialMedias { get; set; }
        #endregion    
    }
}