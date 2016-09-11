using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataStuff
{
    public class Repo
    {
        private readonly string _connectionString;

        public Repo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(User user)
        {
            var ctx = new RolesTestDataContext(_connectionString);
            ctx.Users.InsertOnSubmit(user);
            ctx.SubmitChanges();
        }
        public Role CreateRole(string roleName)
        {
            var ctx = new RolesTestDataContext(_connectionString);
            Role role = new Role { Name = roleName };
            ctx.Roles.InsertOnSubmit(role);
            ctx.SubmitChanges();
            return role;
        }
        public void AddUserToRole(int userId,int roleId)
        {
            var ctx = new RolesTestDataContext(_connectionString);
            UserRole userRole = new UserRole { UserId = userId, RoleId = roleId };
            ctx.UserRoles.InsertOnSubmit(userRole);
            ctx.SubmitChanges();
        }
        public User Login(string username, string password)
        {
            var ctx = new RolesTestDataContext(_connectionString);
            var user = ctx.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return null;
            }

            var hash = FakeHasher.Hash(password);
            if (hash != user.PasswordHash)
            {
                return null;
            }

            return user;
        }

        public bool IsUserInRole(string username, string roleName)
        {
            var ctx = new RolesTestDataContext(_connectionString);
            var user = ctx.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return false;
            }
            return user.UserRoles.Any(r => r.Role.Name == roleName);
        }

        public string[] GetRolesForUser(string username)
        {
            var ctx = new RolesTestDataContext(_connectionString);
            var user = ctx.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return new string[0];
            }

            return user.UserRoles.Select(r => r.Role.Name).ToArray();
        }

        public string[] GetAllRoles()
        {
            var ctx = new RolesTestDataContext(_connectionString);
            return ctx.Roles.Select(r => r.Name).ToArray();
        }

    }
}
