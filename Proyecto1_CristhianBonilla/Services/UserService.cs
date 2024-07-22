using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Proyecto1_CristhianBonilla.Models;
using Proyecto1_CristhianBonilla.Utils;
using Proyecto1_CristhianBonilla.ViewModels;

namespace Proyecto1_CristhianBonilla.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _appDbContext;
        private readonly HttpClient _httpClient;

        public UserService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public async Task<Users> getUserById(int id) 
        {        
            try
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(usr => usr.IdUser == id);
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }    

        public async Task<Users> GetUser(Users user, HttpContext httpContext)
        {
            HashData hashData = new HashData();
            try
            {
                var userLogin = await _appDbContext.Users.Where(u => u.IdUser == user.IdUser).FirstOrDefaultAsync();
                
                if (userLogin != null && hashData.VerifyPassword(user.Password,userLogin.Password)) {
                    Users ActualUser = userLogin;
                    httpContext.Session.SetObjectAsJson("CurrentUser", new CurrentUser(ActualUser));
                }
                else
                {
                   return null;
                }

                return userLogin;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Users> AddUser(Users user)
        {
            try
            {
                await _appDbContext.Users.AddAsync(user);
                await _appDbContext.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<bool> EditUser(CurrentUser userUpdate)
        {
            try
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(usr => usr.IdUser == userUpdate.IdUser); ;

                if (user == null)
                {
                    return false;
                }

                user.Name = userUpdate.Name;
                user.Surname = userUpdate.Surname;
                user.SecondSurname = userUpdate.SecondSurname;
                user.Age = userUpdate.Age;
                user.Preferences = userUpdate.Preferences;

                _appDbContext.Users.Update(user);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }

        public async Task<Users> updateUser(Users user)
        {
            try
            {
                _appDbContext.Users.Update(user);
                await _appDbContext.SaveChangesAsync();
                if (user == null)
                {
                    return null;
                }
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
