using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.Utilities
{
    public class Static
    {
        public const string UserRole = "user";
        public const string AdminRole = "admin";
        public const  string ProgrammerRole = "programmer";

        //Id Of TBL_Setting For Dirst time wich will be Initialize
        public static int SettingId = 1;
    }
}