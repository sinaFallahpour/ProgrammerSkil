using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models
{
    public class TBL_Images
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "تصویر")]
        public string Photo { get; set; }

        #region RELATION
        // relation With TBL_Sample
        [ForeignKey("SamplesId")]
        public virtual TBL_Samples Samples { get; set; }
        [Display(Name = "Samples ")]
        public int? SamplesId { get; set; }

        #endregion
    }
}