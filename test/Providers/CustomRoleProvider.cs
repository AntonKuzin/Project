using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using System.Linq;
using System.Web;
using ORM;
using Roles = System.Web.Security.Roles;

namespace test.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            bool outputResult = false;
            // Находим пользователя
            using (ProjectDbEntities1 _db = new ProjectDbEntities1())
            {
                try
                {
                    // Получаем пользователя
                    Users agent = (from u in _db.Users
                                   where u.Email == username
                                   select u).FirstOrDefault();
                    if (agent != null)
                    {
                        // получаем роль
                        ORM.Roles agentRole = agent.Roles.FirstOrDefault(u => u.Name == roleName);

                        //сравниваем
                        if (agentRole != null && agentRole.Name == roleName)
                        {
                            outputResult = true;
                        }
                    }
                }
                catch
                {
                    outputResult = false;
                }
            }
            return outputResult;
        }

        public override string[] GetRolesForUser(string email)
        {
            using (var context = new ProjectDbEntities1())
            {
                var roles = new string[]{ };
                var user = context.Users.FirstOrDefault(u => u.Email == email);

                if (user == null) return roles;

                var userRoles = user.Roles.ToList();

                int i = 0;
                roles = new string[userRoles.Count];
                foreach (var role in userRoles)
                {
                    roles[i] = role.Name;
                    i++;
                }
                return roles;
            }
        }

        public override void CreateRole(string roleName)
        {
            var newRole = new ORM.Roles() { Name = roleName };
            using (var context = new ProjectDbEntities1())
            {
                context.Roles.Add(newRole);
                context.SaveChanges();
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }

    }
}