using AztuChat.DAL;
using AztuChat.Models;
using AztuChat.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace AztuChat.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager { get; }
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebHostEnvironment _environment;
        private readonly AppDbContext _context;

        public AccountController(UserManager<AppUser> userManager
                                ,SignInManager<AppUser> signInManager
                                ,IWebHostEnvironment environment
                                ,AppDbContext context)
        {
            _userManager=userManager;
            _signInManager=signInManager;
            _environment = environment;
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);
            if(!result.Succeeded)
            {
                ModelState.AddModelError("Password","Login or passwor incorrect");
                return View(login);
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            AppUser appUser = new AppUser
            {
                UserName = register.UserName,
            };
            await _userManager.CreateAsync(appUser,register.Password);
            string fileName = register.UserName + register.Image.FileName;
            using (FileStream fileStream = new FileStream(Path.Combine(_environment.WebRootPath, "assets/img", fileName), FileMode.Create))
            {
                register.Image.CopyTo(fileStream);
            }
            UserImage userImage = new UserImage {
                ImageUrl = fileName,
                User = appUser
            };
            await _context.UserImages.AddAsync(userImage);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login");
        }
    }
}
