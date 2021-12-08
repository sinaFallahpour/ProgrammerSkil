using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.ViewModel.User
{
    public class UserViewModel
    {
        
        public int Id { get; set; }
        
        public string UserName { get; set; }
     
        public string Password { get; set; }
      
        public string RoleType { get; set; }

        public DateTime RegDate { get; set; }

        public string JwtToken { get; set; }

    }
}