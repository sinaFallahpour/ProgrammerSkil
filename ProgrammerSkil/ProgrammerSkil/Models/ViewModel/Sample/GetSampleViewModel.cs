using ProgrammerSkil.Models.ViewModel.Image;
using ProgrammerSkil.Models.ViewModel.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.ViewModel.Sample
{
    public class GetSampleViewModel
    {
        public int Id { get; set; }
       
        public string Name { get; set; }

        public string Link { get; set; }
       
        public string Description { get; set; }

        #region
            // relation With TBL_User
            public virtual UserDbViewModel User { get; set; }

            [Display(Name = "User ")]
            public int? UserId { get; set; }

            public virtual ICollection<ImageDbViewModel> Images { get; set; }

        #endregion

    }
}