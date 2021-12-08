using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.ViewModel.Image
{
    public class ImageDbViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "تصویر")]
        public string Photo { get; set; }

        #region RELATION
        [Display(Name = "Samples ")]
        public int? SamplesId { get; set; }

        #endregion  
    }
}