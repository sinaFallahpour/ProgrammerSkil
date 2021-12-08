using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.ViewModel.Sample
{
    public class SampleDTO
    {


        public int Id { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        #region RELATION
        public virtual UserDTO User { get; set; }
       
        public virtual ICollection<ImageDTO> Images { get; set; }

        #endregion
    }
}