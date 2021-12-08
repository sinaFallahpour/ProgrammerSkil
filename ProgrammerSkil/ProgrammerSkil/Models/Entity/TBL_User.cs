using ProgrammerSkil.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models
{
    public class TBL_User
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

        public string SerialNumber { get; set; }

        #region RELATION
        public virtual TBL_UserProfile UserProfile { get; set; }
        public virtual ICollection<TBL_FeedBacks> FeedBackses { get; set; }
        public virtual ICollection<TBL_Cooperation> Cooperations { get; set; }
        public virtual ICollection<TBL_Samples> Sampleses { get; set; }
        public virtual ICollection<TBL_Token> Tokens { get; set; }

        #endregion


    }
}