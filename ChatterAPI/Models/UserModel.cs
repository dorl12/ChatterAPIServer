using ChatterDB;

namespace ChatterAPI
{
    public interface IUserModel
    {
        List<User> GetAllUsers();
        List<String> GetAllUserIds();
        User GetUser(string Id);
        void AddUser(User user);
        //void UpdateUser(User user, string id);
        void DeleteUser(string id);
    }
    public class UserModel : IUserModel
    {
        // Get all Users
        public List<User> GetAllUsers()
        {
            using (var db = new UsersContext())
            {
                var users = db.Users.ToList();
                return users;
            }
        }

        // Get all User Ids
        public List<String> GetAllUserIds()
        {
            using (var db = new UsersContext())
            {
                var users = db.Users.ToList();
                var ids = new List<String>();
                for (int i = 0; i < users.Count; i++)
                {
                    ids.Add(users[i].id);
                }
                return ids;
            }
        }

        // Get User
        public User GetUser(string Id)
        {
            using (var db = new UsersContext())
            {
                User user = db.Users.Find(Id);
                return user;
            }
        }

        // Add User
        public void AddUser(User user)
        {
            using (var db = new UsersContext())
            {
                List<User> usersList = db.Users.ToList();
                foreach(var u in usersList)
                {
                    if(u.id == user.id)
                    {
                        return;
                    }
                }
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        // Update User
        //public void UpdateUser(User user, string id)
        //{
        //    using (var db = new UsersContext())
        //    {
        //        User u = db.Users.Find(id);
        //        u.name = user.name;
        //        u.server = user.server;
        //    }
        //}

        // Delete User
        public void DeleteUser(string id)
        {
            using (var db = new UsersContext())
            {
                User u = db.Users.Find(id);
                db.Users.Remove(u);
                db.SaveChanges();
            }
        }
    }
}


