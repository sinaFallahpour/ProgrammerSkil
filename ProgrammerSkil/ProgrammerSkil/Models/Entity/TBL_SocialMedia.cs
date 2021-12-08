using ProgrammerSkil.Models.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.Entity
{
    public class TBL_SocialMedia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataType(DataType.Url)]
        [Required(ErrorMessage = "لینک الزامیست")]
        public string Link { get; set; }


        #region RELATION
        // relation With TBL_Setting
        [ForeignKey("SettingId")]
        public virtual TBL_Setting Setting { get; set; }
        [Display(Name = "Seeting ")]
        public int? SettingId { get; set; } = Static.SettingId;
        #endregion

    }
}