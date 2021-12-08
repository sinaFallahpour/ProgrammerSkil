using ProgrammerSkil.Models.BaseDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgrammerSkil.Models.DLL.Interfaces
{
    public interface IUserProfileRepository:IBase
    {
        Task<List<string>> ProgrammerSkills(int Id);
    }
}