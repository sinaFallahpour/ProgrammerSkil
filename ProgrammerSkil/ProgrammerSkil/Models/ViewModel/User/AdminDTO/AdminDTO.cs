using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.ViewModel.User.DTO
{
    public class AdminDTO
    {
        
        public int Id { get; set; }

        public string UserName { get; set; }

        public string RoleType { get; set; }
        
        public DateTime RegDate { get; set; }

        public virtual ICollection<SampleDTO> Sampleses { get; set; }

    }
}