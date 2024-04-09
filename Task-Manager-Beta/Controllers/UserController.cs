using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Task_Manager_Beta.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.AspNetCore.Authentication.Google;
namespace Task_Manager_Beta.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly TaskManagerContext _context;
        public UserController(TaskManagerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {

            if (user != null)
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
                if (existingUser != null)
                {
                    ViewBag.ErrorMessage = "Tên đăng nhập đã tồn tại.";
                    return View();
                }
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.Hide = 0;  
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Projects");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            if (user != null)
            {
                var userLogin = await _context.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
                var listPro = userLogin.Permisssions.ToList();
                if (userLogin != null && BCrypt.Net.BCrypt.Verify(user.Password, userLogin.Password))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userLogin.UserName),
                        new Claim(ClaimTypes.Role, userLogin.Iduser.ToString()),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, "TaskManger");

                    var authProperties = new AuthenticationProperties
                    {
                    };

                    await HttpContext.SignInAsync("TaskManger", new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Projects");
                }
                else
                {
                    ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
                    return View();
                }
            }
            return View();
        }
        [HttpGet]
        [AllowAnonymous]    
        public IActionResult ExternalLogin(string returnUrl = null)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "User");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, "Google");
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                // Xử lý lỗi từ nhà cung cấp bên ngoài nếu có
                return RedirectToAction("ExternalLoginError");
            }

            var userEmail = HttpContext.Request.Query["Email"].ToString();
           // var userName = HttpContext.Request.Query["UserName"].ToString();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
               
                user = new User
                {
                    Email = userEmail,
                    UserName = null, // Đặt tên người dùng là null
                    Password = null,
                    Hide = 0,           
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Projects");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("TaskManger");
            // Redirect đến trang chính hoặc trang đăng nhập
            return RedirectToAction("LandingPage", "Home");
        }
    }
}

