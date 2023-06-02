using iTunes_WebApp_API.Models;
using iTunes_WebApp_API.Models.Repositories;
using iTunes_WebApp_API.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace iTunes_WebApp_API.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.SignUpMessage = "No account found. Sign up below.";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the user exists in the repository
                var user = _userRepository.GetUserByUsernameAndPassword(model.Username, model.Password);
                if (user != null)
                {
                    // Create the claims for the authenticated user
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        // Add other claims if needed
                    };

                    // Create the authentication ticket
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var authProperties = new AuthenticationProperties
                    {
                        // Add any desired authentication properties
                    };

                    // Sign in the user
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

                    return RedirectToAction("Index", "iTunesStore");
                }

                ModelState.AddModelError(string.Empty, "Invalid username or password");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create the user using the provided information
                var user = new User
                {
                    Username = model.Username,
                    Password = model.Password
                };

                // Save the user in the repository
                _userRepository.AddUser(user);

                // Optionally, sign in the user after successful sign-up
                // ...

                // Redirect to the desired page
                return RedirectToAction("Login");
            }

            return View(model);
        }

        public async Task<IActionResult> SignOut()
        {
            // Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
