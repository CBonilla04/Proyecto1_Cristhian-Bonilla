using Proyecto1_CristhianBonilla.Models;
using Proyecto1_CristhianBonilla.ViewModels;

namespace Proyecto1_CristhianBonilla.Services
{
    public interface IUserService
    {
        Task<Users> getUserById(int id);
        Task<Users> GetUser(Users user, HttpContext httpContext);
        Task<bool> EditUser(CurrentUser userUpdate);
        Task<Users> AddUser(Users user);
        Task<Users> updateUser(Users user);
    }
}
