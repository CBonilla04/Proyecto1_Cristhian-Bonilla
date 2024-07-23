using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Proyecto1_CristhianBonilla.Models;
using Proyecto1_CristhianBonilla.Utils;
using Proyecto1_CristhianBonilla.ViewModels;
using System.Security.Claims;

namespace Proyecto1_CristhianBonilla.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _appDbContext;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _appDbContext = appDbContext;
            _httpContextAccessor = httpContextAccessor;
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
        // Method to get a user by id
        public async Task<Users> GetUser(Users user, HttpContext httpContext)
        {
            HashData hashData = new HashData();
            try
            {
                var userLogin = await _appDbContext.Users.Where(u => u.IdUser == user.IdUser).FirstOrDefaultAsync();
                
                if (userLogin != null && hashData.VerifyPassword(user.Password,userLogin.Password)) {
                    user = userLogin;

                    httpContext.Session.SetObjectAsJson("CurrentUser", new CurrentUser(user));
                }
                else
                {
                   return null;
                }
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };
                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                return userLogin;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        // Method to add a new user
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
        // Method to get all users
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
        //  Method to update a user
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
