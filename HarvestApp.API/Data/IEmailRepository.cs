using System.Threading.Tasks;
using HarvestApp.API.Models;

namespace HarvestApp.API.Data
{
    public interface IEmailRepository
    {
        Task<User> GetEmailUser(int id);
    }
}