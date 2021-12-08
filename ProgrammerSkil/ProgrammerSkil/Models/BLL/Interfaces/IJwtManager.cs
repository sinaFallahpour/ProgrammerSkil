using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace ProgrammerSkil.Models.BLL.Interfaces
{
    public interface IJwtManager
    {
        // generate Token
        string GenerateToken(int Id, string Role, string UserName, int expireMinutes = 10);

        //get Principal from token
        ClaimsPrincipal GetPrincipal(string token);
    }
}