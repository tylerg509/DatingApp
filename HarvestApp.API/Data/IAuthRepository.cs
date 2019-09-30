using HarvestApp.API.Models;
using System.Threading.Tasks;

namespace HarvestApp.API.Data

{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login (string user, string password);
         Task<bool> UserExists(string username);

        

    }
}