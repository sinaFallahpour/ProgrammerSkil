using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.ViewModel.User.DTO
{
    public class SampleDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public int? UserId { get; set; }

        public List<string> Images { get; set; }
      
    }
}