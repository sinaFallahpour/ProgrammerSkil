using ProgrammerSkil.Models.BaseDll;
using ProgrammerSkil.Models.DLL.Interfaces;
using ProgrammerSkil.Models.Entity;
using ProgrammerSkil.Models.Utilities;
using ProgrammerSkil.Models.ViewModel.Auth;
using ProgrammerSkil.Models.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TAD_Security;

namespace ProgrammerSkil.Models.DLL
{
    public class UserRepository : Base, IUserRepository
    {
        #region  Field
        private readonly DbSet<TBL_User> _Users;
        private readonly DbSet<TBL_UserProfile> _UserProfile;
        private readonly DbSet<TBL_Token> _tokens;
        #endregion  Field

        #region constructor
        public UserRepository(ProgrammerSkilContext db)
        {
            _Users = _db.TBL_User;
            _UserProfile = _db.TBL_UserProfile;
            _tokens = db.TBL_Token;
        }
        #endregion constructors




        /// <summary>
        /// گرفتن یک کاربر با جدول کاربر بدن شرط رول
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<TBL_User> GetById(int Id)
        {
            var user = await _Users.FindAsync(Id);
            return user;
        }




        /// <summary>
        /// گرفتن یک  کاربر عادی با آیدی
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<TBL_User> GetUserById(int Id)
        {
            _db.Configuration.LazyLoadingEnabled = false;
            var user = await _Users.AsNoTracking().FirstOrDefaultAsync(c => c.Id == Id && c.RoleType == Static.UserRole);
            return user;
        }





        /// <summary>
        /// گرفتن یک برنامه نویس با آیدی
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<TBL_User> GetProgrammerById(int Id)
        {
            var user = await _Users.AsNoTracking()
                .Where(c => c.Id == Id && c.RoleType == Static.ProgrammerRole)
                .Include(c => c.UserProfile)
                .Include(c => c.UserProfile.SkillUserProfile)
                .Include(c => c.UserProfile.SkillUserProfile.Select(r => r.Skill))
                .FirstOrDefaultAsync();
            return user;
        }





        /// <summary>
        /// گرفتن یک ادمین با آیدی
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<TBL_User> GetAdminById(int Id)
        {
            _db.Configuration.LazyLoadingEnabled = false;
            var Admin = await _Users
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == Id && c.RoleType == Static.AdminRole);
            return Admin;
        }





        /// <summary>
        /// گرفتن یک ادمین با جوین با آیدی
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<TBL_User> GetAdminWithJoinById(int Id)
        {
            _db.Configuration.LazyLoadingEnabled = false;
            var Admin = await _Users
                .AsNoTracking()
                .Where(c => c.Id == Id && c.RoleType == Static.AdminRole)
                .Include(c => c.Sampleses)
                .Include(c => c.Sampleses.Select(t => t.Images))
                .FirstOrDefaultAsync();
            return Admin;
        }





        /// <summary>
        /// Get All   Admin
        /// </summary>
        /// <returns></returns>
        public async Task<List<TBL_User>> GetAllAdmin()
        {
            return await _Users
                .AsNoTracking()
                .Where(c => c.RoleType == Static.AdminRole)
                                 .ToListAsync();
        }





        /// <summary>
        ///    Get All Admin for Pagination
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public async Task<List<TBL_User>> GetAllAdnminForPagination(int Page, int PageSize)
        {
            return await _Users
                .AsNoTracking()
                .Where(C => C.RoleType == Static.AdminRole)
                .OrderBy(c => c.Id)
                .Skip((Page - 1) * PageSize)
                .Take(PageSize).ToListAsync();
        }




        /// <summary>
        ///    get count of item in database
        /// </summary>
        /// <returns></returns>
        public int AdminCount()
        {
            return _Users.AsNoTracking()
                .Where(c => c.RoleType == Static.AdminRole)
                .Count();
        }





        /// <summary>
        /// Get All  User  with role off User
        /// </summary>
        /// <returns></returns>
        public async Task<List<TBL_User>> GetAllUser()
        {
            return await _Users
                .AsNoTracking()
                .Where(c => c.RoleType == Static.UserRole)
                                 .ToListAsync();
        }




        /// <summary>
        ///    Get All User with role of  User for Pagination
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public async Task<List<TBL_User>> GetAllUserForPagination(int Page, int PageSize)
        {
            return await _Users
                .AsNoTracking()
                .Where(C => C.RoleType == Static.AdminRole)
                .OrderBy(c => c.Id)
                .Skip((Page - 1) * PageSize)
                .Take(PageSize).ToListAsync();
        }





        /// <summary>
        ///    get count of User in database
        /// </summary>
        /// <returns></returns>
        public int UserCount()
        {
            return _Users
                .AsNoTracking()
                 .Where(C => C.RoleType == Static.AdminRole)
                .Count();
        }





        /// <summary>
        /// Get All  Programmer  with 
        /// </summary>
        /// <returns></returns>
        public async Task<List<TBL_User>> GetAllProgrammer()
        {
            //_db.Configuration.LazyLoadingEnabled = false;
            return await _Users.Where(c => c.RoleType == Static.ProgrammerRole)
                .AsNoTracking()
                .Include(c => c.UserProfile)
                .Include(c => c.UserProfile.SkillUserProfile)
                .ToListAsync();
        }





        /// <summary>
        ///    Get All Programmer  for Pagination
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public async Task<List<TBL_User>> GetAllProgrammerForPagination(int Page, int PageSize)
        {
            return await _Users
                .AsNoTracking()
                .Where(C => C.RoleType == Static.ProgrammerRole)
                .OrderBy(c => c.Id)
                .Skip((Page - 1) * PageSize)
                .Take(PageSize).ToListAsync();
        }




        /// <summary>
        ///    get count of item in database
        /// </summary>
        /// <returns></returns>
        public int ProgrammerCount()
        {
            return _Users
                .AsNoTracking()
                .Where(C => C.RoleType == Static.ProgrammerRole)
               .Count();
        }





        /// <summary>
        ///    get count of item in database
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return _Users
                    .AsNoTracking()
                         .Count();
        }





        /// <summary>
        ///آیا کاربر موجود است برای تغییر پسورد
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        public bool IsUsertExistForChangePassword(int Id, string PassWord)
        {
            return _Users.AsNoTracking().Any(c => c.Id == Id && c.Password == PassWord);
        }





        public async Task<TBL_User> Crential(string userName, string password)
        {
            var user = _Users.AsNoTracking().Where(c => c.UserName == userName && c.Password == password);
            var userInProgrammerRole = await user.Where(c => c.RoleType.ToLower() == Static.ProgrammerRole).SingleOrDefaultAsync();
            if (userInProgrammerRole == null)
                return await user.SingleOrDefaultAsync();
            return await user.Include(c => c.UserProfile).SingleOrDefaultAsync();
        }





        public async Task<TBL_User> CrentialForAdmin(string userName, string password)
        {
            var Admin = await _Users.AsNoTracking().Where(c => c.UserName == userName && c.Password == password && c.RoleType == Static.AdminRole)
                .FirstOrDefaultAsync();
            return Admin;
        }





        public Task<TBL_User> IsUserExistById(int UserId)
        {
            var User = _Users
                .AsNoTracking()
                .Where(c => c.Id == UserId)
                        .FirstOrDefaultAsync();
            return User;
        }




        public async Task<TBL_User> ChangeRoleToAdmin(TBL_User User)
        {
            //if (User == null)
            //    return null;
            //User.RoleType = Static.AdminRole;
            //_db.Set<TBL_User>().Attach(User);

            //_db.Entry(User).State = EntityState.Detached;
            ////_db.Entry(User).State = EntityState.Modified;

            var userFromDb = _Users.Find(User.Id);
            if (User == null) return null;
            userFromDb.RoleType = Static.AdminRole;
            userFromDb.SerialNumber = User.SerialNumber;
            var result = await SaveChangeAsync();
            if (result)
                return userFromDb;
            return null;
        }





        public async Task<TBL_User> ChangeRoleToProgrammer(TBL_User User)
        {
            var userFromDb = _Users.Find(User.Id);
            if (User == null) return null;
            userFromDb.RoleType = Static.ProgrammerRole;
            userFromDb.SerialNumber = User.SerialNumber;
            if (userFromDb.UserProfile == null)
            {
                var UserProfile = new TBL_UserProfile()
                {
                    UserId = userFromDb.Id
                };
                userFromDb.UserProfile = UserProfile;
            }
            ////_UserProfile.Add(UserProfile);

            var result = await SaveChangeAsync();
            if (result)
                return userFromDb;
            return null;
        }





        public async Task<TBL_User> ChangeRoleToUser(TBL_User User)
        {
            var userFromDb = _Users.Find(User.Id);
            if (User == null) return null;
            userFromDb.RoleType = Static.UserRole;
            userFromDb.SerialNumber = User.SerialNumber;
            var result = await SaveChangeAsync();
            if (result)
                return userFromDb;
            return null;
        }





        /// <summary>
        /// Change PAsswrod
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="NewPassword"></param>
        /// <returns></returns>
        public async Task<TBL_User> ChangePasswrod(int Id, string NewPassword)
        {
            var UserFromDb = await _Users.FindAsync(Id);
            UserFromDb.Password = NewPassword;
            UserFromDb.SerialNumber = Guid.NewGuid().ToString();
            try
            {
                saveChanges();
                return UserFromDb;
            }
            catch
            {
                return null;
            }
        }







        /// <summary>
        /// LogOut The User
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public bool LogOut(string Token)
        {
            string hashToken = Token.GetHash();
            var tokenFromDB = _db.TBL_Token.Where(c => c.AccessTokenHash == hashToken).FirstOrDefault();
            if (tokenFromDB == null)
                return false;
            _db.TBL_Token.Remove(tokenFromDB);
            var result = saveChanges();
            if (result)
                return true;
            return false;
        }





        public async Task<int?> RegisterUser(TBL_User User)
        {
            if (User == null)
                return null;
            var user = _Users.Add(User);
            var result = await SaveChangeAsync();
            if (result)
            {
                return user.Id;
            }
            else
                return null;
        }



        /// <summary>
        ///Remove
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<TBL_User> Remove(int Id)
        {
            var userFromDb = await _Users.FindAsync(Id);
            _db.TBL_FeedBacks.Where(c => c.UserId == userFromDb.Id).Load();
            _db.TBL_Samples.Where(c => c.UserId == userFromDb.Id).Load();
            _db.TBL_Cooperation.Where(c => c.UserId == userFromDb.Id).Load();
            _db.TBL_Setting.Where(c => c.UserId == userFromDb.Id).Load();
            //_db.TBL_Token.Where(c => c.UserId == userFromDb.Id).Load();
            if (userFromDb == null)
                return null;
            _Users.Remove(userFromDb);
            var result = await SaveChangeAsync();
            if (result)
                return userFromDb;
            else
                return null;
        }





        public bool IsSkillsExist(List<int> SkillsId)
        {
            if (SkillsId == null)
                return false;
            if (SkillsId.Count == 0)
                return true;
            foreach (var skilId in SkillsId)
            {
                var skill = _db.TBL_Skill.AsNoTracking().FirstAsync(c => c.Id == skilId);
                if (skill == null)
                    return false;
            }
            return true;
            //return _db.TBL_Skill.Any(c => !SkillsId.Contains(c.Id));
        }





        public async Task<TBL_User> UpdateProgrammerProfile(ProgrammerUpdateProfileViewModel Profile)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var userFromDb = await _Users.FindAsync(Profile.Id);
                    if (userFromDb == null)
                        return null;
                    userFromDb.UserName = Profile.UserName;
                    var UserProfile = userFromDb.UserProfile;
                    if (UserProfile == null)
                    {
                        var newUserProfile = new TBL_UserProfile
                        {
                            Biography = Profile.Biography,
                            Email = Profile.Email,
                            Name = Profile.Name,
                            Photo = Profile.Photo,
                            UserId = userFromDb.Id,
                        };
                        newUserProfile.SkillUserProfile = new List<TBL_SkillUserProfile>();
                        foreach (var SkilId in Profile.SkillsId)
                        {
                            UserProfile.SkillUserProfile.Add(
                                new TBL_SkillUserProfile()
                                {
                                    SkillId = SkilId,
                                    UserProfileId = userFromDb.Id
                                });
                        }
                        userFromDb.UserProfile = newUserProfile;
                        var result = await SaveChangeAsync();
                        if (result)
                            return userFromDb;
                        return null;
                    }
                    userFromDb.UserProfile.Biography = Profile.Biography;
                    userFromDb.UserProfile.Photo = Profile.Photo;
                    userFromDb.UserProfile.Name = Profile.Name;
                    userFromDb.UserProfile.Email = Profile.Email;

                    var SkilUserProfile = userFromDb.UserProfile.SkillUserProfile;
                    _db.TBL_SkillUserProfile.RemoveRange(SkilUserProfile);
                    foreach (var SkillId in Profile.SkillsId)
                    {
                        userFromDb.UserProfile.SkillUserProfile.Add(new TBL_SkillUserProfile() { SkillId = SkillId, UserProfile = userFromDb.UserProfile });
                    }
                    try
                    {
                        await SaveChangeAsync();
                        return userFromDb;
                    }
                    catch
                    {
                        return null;
                    }


                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return null;
                }
            }



        }





        public bool CreateUserToken(TBL_Token userToken)
        {
            if (userToken == null)
                return false;
            if (userToken.UserId == null)
                return false;
            InvalidateUserTokens((int)userToken.UserId);
            _tokens.Add(userToken);
            var result = saveChanges();
            if (result)
                return true;
            return false;
        }


        //public void DeleteExpiredTokens()
        //{
        //    var now = DateTime.Now;
        //    var userTokens = _tokens.Where(x => x.AccessTokenExpirationDateTime < now).ToList();
        //    foreach (var userToken in userTokens)
        //    {
        //        _tokens.Remove(userToken);
        //    }
        //    _db.SaveChanges();
        //}

        public bool DeleteToken(string TokenHash)
        {
            var token = FindToken(TokenHash);
            if (token == null)
                return false;
            _tokens.Remove(token);
            var result = saveChanges();
            if (result)
                return true;
            return false;
        }


        public TBL_Token FindToken(string tokenHash)
        {
            return _tokens.AsNoTracking().FirstOrDefault(x => x.AccessTokenHash == tokenHash);
        }

        public bool InvalidateUserTokens(int userId)
        {
            var userTokens = _tokens.Where(x => x.UserId == userId).ToList();
            _tokens.RemoveRange(userTokens);
            //foreach (var userToken in userTokens)
            //{
            //    _tokens.Remove(userToken);
            //}
            var result = saveChanges();
            if (result)
                return true;
            return false;
        }

        public bool IsValidToken(string tokenHash, int userId)
        {
            //var accessTokenHash = _securityService.GetSha256Hash(tokenHash);
            var userToken = _tokens.FirstOrDefault(x => x.AccessTokenHash == tokenHash && x.UserId == userId);
            return userToken?.AccessTokenExpirationDateTime >= DateTime.Now;
        }


        public void UpdateUserToken(int userId, string tokenHash)
        {
            var token = _tokens.FirstOrDefault(x => x.UserId == userId);
            if (token != null)
            {
                token.AccessTokenHash = tokenHash;
                saveChanges();
            }
        }





        /// <summary>
        /// save  toke to Token table
        /// remove users last token from tokens table
        /// update users SerialNumber
        /// </summary>
        /// <param name="User"></param>
        /// <param name="token"></param>
        /// <param name="SerialNumber"></param>
        /// <returns></returns>
        public async Task<TBL_User> CreateJwt(TBL_User User, string token, string SerialNumber)
        {
            //using (var transaction = _db.Database.BeginTransaction())
            //{
            //    try
            //    {
                    //var UserFromDb = await _db.TBL_User.Where(c => c.Id == User.Id).FirstOrDefaultAsync();
                    var usersToken = _db.TBL_Token.Where(c => c.UserId == User.Id);
                    if (User == null)
                        return null;
                    //UserFromDb.SerialNumber = User.SerialNumber;

                    _db.TBL_Token.RemoveRange(usersToken);
                    var Token = new TBL_Token
                    {
                        User = User,
                        AccessTokenHash = token,
                        AccessTokenExpirationDateTime = DateTime.UtcNow.AddDays(10),
                        UserId = User.Id
                    };
                    _db.TBL_Token.Add(Token);
            if (User.UserProfile !=null )
            {
                _db.Entry(User.UserProfile).State = EntityState.Unchanged;
                _db.Entry(User).State = EntityState.Modified;
            }
                    //_db.TBL_User.
                    var result = await SaveChangeAsync();
                    //commit transaction‎
                    //transaction.Commit();
                    if (result)
                        return User;
            return null;
                //    return null;
                //}
                //catch (Exception ex)
                //{
                //    transaction.Rollback();
                //    return null;
                //}
            //}
        }
    }
}