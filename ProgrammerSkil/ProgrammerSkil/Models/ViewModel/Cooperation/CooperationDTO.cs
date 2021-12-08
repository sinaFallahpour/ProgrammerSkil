using Newtonsoft.Json;
using ProgrammerSkil.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.ViewModel.Cooperation
{
    public class CooperationDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string ResumeLink { get; set; }
        public CooperationStatus Status { get; set; }

        [JsonProperty("User")]
        public virtual UserDTO User { get; set; }

    }
}