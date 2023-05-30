using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCinemaTickets.Domain.DomainModels;
using OnlineCinemaTickets.Domain.Identity;
using OnlineCinemaTickets.Repository.Implementation;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineCinemaTickets.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<OnlineCinemaUser> userManager;
        private readonly SignInManager<OnlineCinemaUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AccountController(UserManager<OnlineCinemaUser> userManager, SignInManager<OnlineCinemaUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public IActionResult Register()
        {
            UserRegistrationDto model = new UserRegistrationDto();
            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationDto request)
        {
            if (ModelState.IsValid)
            {
                var userCheck = await userManager.FindByEmailAsync(request.Email);
                if (userCheck == null)
                {
                    var user = new OnlineCinemaUser
                    {
                        FirstName = request.Name,
                        LastName = request.LastName,
                        UserName = request.Email,
                        NormalizedUserName = request.Email,
                        Email = request.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        PhoneNumber = request.PhoneNumber,
                        UserCart = new ShoppingCart(),
                        Role = "Regular"
                    };
                    var result = await userManager.CreateAsync(user, request.Password);
                    var doesRoleExist = await roleManager.RoleExistsAsync("Regular");
                    if (!doesRoleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole("Regular"));
                    }
                    await userManager.AddToRoleAsync(user, "Regular");
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        if (result.Errors.Count() > 0)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("message", error.Description);
                            }
                        }
                        return View(request);
                    }
                }
                else
                {
                    ModelState.AddModelError("message", "Email already exists.");
                    return View(request);
                }
            }
            return View(request);

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            UserLoginDto model = new UserLoginDto();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError("message", "Email not confirmed yet");
                    return View(model);

                }
                if (await userManager.CheckPasswordAsync(user, model.Password) == false)
                {
                    ModelState.AddModelError("message", "Invalid credentials");
                    return View(model);

                }

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    await userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    return View(model);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult AddRole()
        {
            AddRoleModel model = new AddRoleModel();
            List<OnlineCinemaUser> users = userManager.Users.ToList();
            model.usernames = new List<string>();
            foreach (OnlineCinemaUser user in users)
            {
                model.usernames.Add(user.UserName);
            }
            model.roles = new List<string>() { "Administrator", "Regular" };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddRole(AddRoleModel model)
        {

            string[] roleNames = { "Administrator", "Regular" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var username = model.Username;
            var user = userManager.Users.Where(x => x.UserName.Equals(username)).FirstOrDefault();
            await userManager.AddToRoleAsync(user, model.SelectedRole);
            user.Role = model.SelectedRole;
            await userManager.UpdateAsync(user);
            return RedirectToAction("Index", "Tickets");
        }
    }
}
