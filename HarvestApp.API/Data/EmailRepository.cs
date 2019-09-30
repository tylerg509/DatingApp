using System.Threading.Tasks;
using HarvestApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HarvestApp.API.Data
{
    public class EmailRepository : IEmailRepository
    {
        private readonly DataContext _context;
        public EmailRepository(DataContext context)
        {
            _context = context;
        }
        
        
        public async Task<User> GetEmailUser(int id)
        {
            var user = await _context.Users.Include(p=>p.Photos).FirstOrDefaultAsync(u=>u.Id==id);
            return user;
        }
    }
}