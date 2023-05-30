using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCinemaTickets.Domain.DomainModels;
using OnlineCinemaTickets.Domain.Identity;
using OnlineCinemaTickets.Services.Interface;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OnlineCinemaTickets.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<OnlineCinemaUser> _userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AdminController(IOrderService orderService, UserManager<OnlineCinemaUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _orderService = orderService;
            _userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet("[action]")]
        public List<Order> GetOrders()
        {
            var result = _orderService.getAllOrders();
            return result;
        }

        [HttpPost("[action]")]
        public Order GetDetailsForOrder(BaseEntity model)
        {
            var result = _orderService.getOrderDetails(model);
            return result;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> ImportAllUsers(IFormFile file)
        {
            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\files\\{file.FileName}";

            using (FileStream fileStream = System.IO.File.Create(pathToUpload))
            {
                file.CopyTo(fileStream);

                fileStream.Flush();
            }

            List<OnlineCinemaUser> users = getUsersFromExcelFile(file.FileName);

            bool status = true;
            string[] roleNames = { "Regular" };

            foreach (var item in users)
            {
                var userCheck = _userManager.FindByEmailAsync(item.Email).Result;
                if (userCheck == null)
                {
                    var user = new OnlineCinemaUser
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        UserName = item.Email,
                        NormalizedUserName = item.Email,
                        Email = item.Email,
                        Password = item.Password,
                        Role = item.Role,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        PhoneNumber = item.PhoneNumber,
                        UserCart = new ShoppingCart()
                    };

                    var roleExist = await roleManager.RoleExistsAsync(item.Role);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole(item.Role));
                    }

                    var result = _userManager.CreateAsync(user, item.Password).Result;
                    await _userManager.AddToRoleAsync(user, item.Role);

                    status = status & result.Succeeded;
                }
                else
                {
                    continue;
                }
            }

            return RedirectToAction("Index", "Order");
        }

        private List<OnlineCinemaUser> getUsersFromExcelFile(string fileName)
        {
            string pathToFile = $"{Directory.GetCurrentDirectory()}\\files\\{fileName}";

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            List<OnlineCinemaUser> userList = new List<OnlineCinemaUser>();

            using (var stream = System.IO.File.Open(pathToFile, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        userList.Add(new OnlineCinemaUser
                        {
                            Email = reader.GetValue(0).ToString(),
                            Password = reader.GetValue(1).ToString(),
                            Role = reader.GetValue(2).ToString()
                        });
                    }
                }
            }
            return userList;

        }

    }
}
