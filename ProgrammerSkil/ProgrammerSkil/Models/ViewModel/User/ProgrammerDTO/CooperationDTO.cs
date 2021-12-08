using ProgrammerSkil.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.ViewModel.User.ProgrammerDTO
{
    public class CooperationDTO
    {
        public int Id { get; set; }
        
        public string Text { get; set; }
     
        public string ResumeLink { get; set; }

        public CooperationStatus Status { get; set; }

    }
}