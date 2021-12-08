using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.ViewModel.FeedBack
{
    public class FeedBackDTO
    {

        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime RegDate { get; set; }


        #region RELATION
        public virtual UserDTO User { get; set; }
        #endregion
    }
}