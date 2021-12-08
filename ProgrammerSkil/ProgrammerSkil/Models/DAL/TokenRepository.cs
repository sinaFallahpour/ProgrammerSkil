using ProgrammerSkil.Models.BaseDll;
using ProgrammerSkil.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models.DAL
{
    public class TokenRepository : Base, ITokenRepository
    {
        #region  Field
        private readonly DbSet<TBL_Token> _tokens;

        #endregion  Field
        #region constructor
        public TokenRepository(ProgrammerSkilContext db)
        {
            _tokens = _db.TBL_Token;
        }
        #endregion constructors









        //private readonly ISecurityService _securityService;




        public bool CreateUserToken(TBL_Token userToken)
        {
            if (userToken == null)
                return false;
            InvalidateUserTokens((int)userToken.UserId);
            _tokens.Add(userToken);
            var result = _db.SaveChanges();
            return true;
        }

        public void DeleteExpiredTokens()
        {
            var now = DateTime.Now;
            var userTokens = _tokens.Where(x => x.AccessTokenExpirationDateTime < now).ToList();
            foreach (var userToken in userTokens)
            {
                _tokens.Remove(userToken);
            }
            _db.SaveChanges();
        }

        //public void DeleteToken(string TokenHash)
        //{
        //    var token = FindToken(TokenHash);
        //    if (token != null)
        //    {
        //        _tokens.Remove(token);
        //    }
        //}

        //public UserToken FindToken(string refreshTokenIdHash)
        //{
        //    return _tokens.FirstOrDefault(x => x.RefreshTokenIdHash == refreshTokenIdHash);
        //}

        public void InvalidateUserTokens(int userId)
        {
            var userTokens = _tokens.Where(x => x.UserId == userId).ToList();
            foreach (var userToken in userTokens)
            {
                _tokens.Remove(userToken);
            }
        }

        //public bool IsValidToken(string accessToken, int userId)
        //{
        //    var accessTokenHash = _securityService.GetSha256Hash(accessToken);
        //    var userToken = _tokens.FirstOrDefault(x => x.AccessTokenHash == accessTokenHash && x.OwnerUserId == userId);
        //    return userToken?.AccessTokenExpirationDateTime >= DateTime.UtcNow;
        //}

        //public void UpdateUserToken(int userId, string accessTokenHash)
        //{
        //    var token = _tokens.FirstOrDefault(x => x.OwnerUserId == userId);
        //    if (token != null)
        //    {
        //        token.AccessTokenHash = accessTokenHash;
        //    }
        //}


    }
}