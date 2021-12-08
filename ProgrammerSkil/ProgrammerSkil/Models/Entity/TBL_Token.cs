using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.Entity
{
    public class TBL_Token
    {
        [Key]
        public int Id { get; set; }

        public string AccessTokenHash { get; set; }

        public DateTime AccessTokenExpirationDateTime { get; set; }

        #region  relation
        [ForeignKey("UserId")]
        public virtual TBL_User User { get; set; }
        [Display(Name = "User ")]
        public int? UserId { get; set; }
        #endregion


    }
}