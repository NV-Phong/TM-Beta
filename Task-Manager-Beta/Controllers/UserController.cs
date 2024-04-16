using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Task_Manager_Beta.Data;

namespace Task_Manager_Beta.Controllers
{
    public class UserController : Controller
    {
        private readonly TaskManagerContext _context;

        public UserController(TaskManagerContext context)
        {
            _context = context;
        }

//----------------------------------------------------------------------------------------------------------------------------------//

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
                return RedirectToAction("DashBoard", "DashBoard");
            }
            return View();
        }

//----------------------------------------------------------------------------------------------------------------------------------//

        [AllowAnonymous]
        public IActionResult Invite(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("DashBoard", "DashBoard", new { iduser = User.FindFirst("UserId")?.Value });
            }
            else
            {
                HttpContext.Session.SetString("ReturnUrl", returnUrl);
                return RedirectToAction("Login", "User");
            }
        }

//----------------------------------------------------------------------------------------------------------------------------------//

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user, string returnUrl = null)
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
                           new Claim("UserId", userLogin.Iduser.ToString()),
                      };

                    var claimsIdentity = new ClaimsIdentity(claims, "TaskManager");

                    var authProperties = new AuthenticationProperties
                    {
                    };

                    await HttpContext.SignInAsync("TaskManager", new ClaimsPrincipal(claimsIdentity), authProperties);
                    returnUrl = HttpContext.Session.GetString("ReturnUrl");
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        //   Cho phép truy cập và xử lý các thành phần khác nhau của URL.
                        var uri = new Uri(returnUrl);
                        //   Lấy danh sách các thành phần phân đoạn của path trong URL
                        var segments = uri.Segments;
                        //   Lấy phần tử thứ ba(chỉ mục 2) từ mảng các phân đoạn(segments).
                        //   Sử dụng phương thức Trim('/') để loại bỏ các dấu gạch chéo(/) ở đầu và cuối của chuỗi phân đoạn.
                        var idproject = int.Parse(segments[2].Trim('/'));

                        var member = new Member
                        {
                            Iduser = userLogin.Iduser,
                            Idproject = idproject
                        };
                        _context.Members.Add(member);
                        await _context.SaveChangesAsync();

                        HttpContext.Session.Remove("ReturnUrl");

                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("DashBoard", "DashBoard", new { iduser = userLogin.Iduser });
                }
                else
                {
                    ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
                    return View();
                }
            }
            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(User user)
        //{
        //    if (user != null)
        //    {
        //        var userLogin = await _context.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);

        //        //TODO:
        //        //var listPro = userLogin.Permisssions.ToList();

        //        if (userLogin != null && BCrypt.Net.BCrypt.Verify(user.Password, userLogin.Password))
        //        {
        //            var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, userLogin.UserName),
        //        new Claim("UserId", userLogin.Iduser.ToString()),
        //    };

        //            var claimsIdentity = new ClaimsIdentity(claims, "TaskManager");

        //            var authProperties = new AuthenticationProperties
        //            {
        //            };

        //            await HttpContext.SignInAsync("TaskManager", new ClaimsPrincipal(claimsIdentity), authProperties);

        //            return RedirectToAction("DashBoard", "DashBoard", new { iduser = userLogin.Iduser });
        //        }
        //        else
        //        {
        //            ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
        //            return View();
        //        }
        //    }
        //    return View();
        //}

//----------------------------------------------------------------------------------------------------------------------------------//

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("TaskManager");
            return RedirectToAction("LandingPage", "Home");
        }

//----------------------------------------------------------------------------------------------------------------------------------//

    }
}
