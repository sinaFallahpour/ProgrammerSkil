using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.BaseDll
{
    public interface IBase:IDisposable
    {
         bool saveChanges();
    }
}