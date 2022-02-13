using ApiAuth.Models;

namespace ApiAuth.Repositorio
{
    public static class UserRepository
    {
        public static User Get(string Username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Username = "batman", Password = "batman", Role = "manager" });
            users.Add(new User { Id = 2, Username = "robin", Password = "robin", Role = "employee" });
            return users.Where(x => x.Username.ToLower() == Username.ToLower() && x.Password == x.Password).FirstOrDefault();
        }
    }
    
}