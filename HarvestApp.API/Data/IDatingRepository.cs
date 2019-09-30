using System.Collections.Generic;
using System.Threading.Tasks;
using HarvestApp.API.Helpers;
using HarvestApp.API.Models;

namespace HarvestApp.API.Data
{
    public interface IDatingRepository
    {
         void Add<T> (T entity) where T: class;
         void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<PagedList<User>> GetUsers(UserParams UserParams);
        Task<User> GetUser(int id, bool isCurrentUser);
        Task<Photo> GetPhoto(int id);
        Task<Photo> GetMainPhotoForUser(int userId);
        Task<Like> GetLike(int userId, int recipientId);
        Task<Message> GetMessage(int id);
        Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);
        Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId);
    
        
    }
}