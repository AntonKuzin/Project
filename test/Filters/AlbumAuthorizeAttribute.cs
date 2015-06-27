using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace test.Filters
{
    public class AlbumAuthorizeAttribute : AuthorizeAttribute
    {
        private string[] _allowedUsers = new string[] { };
        private string[] _allowedRoles = new string[] { };

        public AlbumAuthorizeAttribute()
        { }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!String.IsNullOrEmpty(base.Users))
            {
                _allowedUsers = base.Users.Split(new char[] { ',' });
                for (int i = 0; i < _allowedUsers.Length; i++)
                {
                    _allowedUsers[i] = _allowedUsers[i].Trim();
                }
            }
            if (!String.IsNullOrEmpty(base.Roles))
            {
                _allowedRoles = base.Roles.Split(new char[] { ',' });
                for (int i = 0; i < _allowedRoles.Length; i++)
                {
                    _allowedRoles[i] = _allowedRoles[i].Trim();
                }
            }

            return httpContext.Request.IsAuthenticated &&
                 User(httpContext) && Role(httpContext);
        }

        private bool User(HttpContextBase httpContext)
        {
            if (_allowedUsers.Length > 0)
            {
                return _allowedUsers.Contains(httpContext.User.Identity.Name);
            }
            return true;
        }

        private bool Role(HttpContextBase httpContext)
        {
            if (httpContext.User.IsInRole("banned"))
                return false;
            if (_allowedRoles.Length > 0)
            {
                for (int i = 0; i < _allowedRoles.Length; i++)
                {
                    if (httpContext.User.IsInRole(_allowedRoles[i]))
                        return true;
                }
                return false;
            }
            return true;
        }
    }
}