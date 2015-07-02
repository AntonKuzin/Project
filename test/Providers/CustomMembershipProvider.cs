using System;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using ORM;
//using test.Models;

namespace test.Providers
{
    //провайдер членства помогает системе идентифицировать пользователя
    public class CustomMembershipProvider : MembershipProvider
    {
        //private IUserRepository userRepository;//IAccountService!!! 
        public MembershipUser CreateUser(string email, string password)
        {
            //userRepository = (IUserRepository)DependencyResolver.Current.GetService(typeof(IUserRepository));

            MembershipUser membershipUser = GetUser(email, false);

            if (membershipUser != null)
            {
                return null;
            }

            using (var context = new AlbumDbEntities())
            {
                var user = new Users
                {
                    Email = email,
                    Password = Crypto.HashPassword(password),
                    //http://msdn.microsoft.com/ru-ru/library/system.web.helpers.crypto(v=vs.111).aspx
                };


                var role = context.Roles.FirstOrDefault(r => r.Name == "user");
                if (role != null)
                {
                    user.Roles.Add(role);
                }
                if (!context.Users.Any())
                {
                    var adminRole = context.Roles.FirstOrDefault(r => r.Name == "admin");
                    if (adminRole != null)
                    {
                        user.Roles.Add(adminRole);
                    }
                }


                user.Name = "name";

                context.Users.Add(user);
                context.SaveChanges();
                membershipUser = GetUser(user.Email, false);
                return membershipUser;
            }

        }

        public override bool ValidateUser(string email, string password)
        {
            using (var context = new AlbumDbEntities())
            {
                Users user = (from u in context.Users
                             where u.Email == email
                             select u).FirstOrDefault();

                if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
                //Определяет, соответствуют ли заданный хэш RFC 2898 и пароль друг другу
                {
                    return true;
                }
            }
            return false;
        }

        public override MembershipUser GetUser(string email, bool userIsOnline)
        {
            using (var context = new AlbumDbEntities())
            {
                var user = (from u in context.Users
                            where u.Email == email
                            select u).FirstOrDefault();

                if (user == null) return null;

                var memberUser = new MembershipUser("CustomMembershipProvider", user.Name,
                    null, null, null, null,
                    false, false, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);

                return memberUser;
            }
        }

        #region Stabs
        public override MembershipUser CreateUser(string username, string password, string email,
            string passwordQuestion,
            string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override string ApplicationName { get; set; }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
        #endregion
    }
}