using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Runtime.CompilerServices;
using WebShop.Data;
using WebShop.Models;

namespace WebShop.Areas.Admin.Controllers
{


    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserManagerController : Controller
    {

        private RoleManager<IdentityRole> _roleManager;

        private UserManager<ApplicationUser> _userManager;

        public UserManagerController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManger)
        {

            _userManager = userManager;
            _roleManager = roleManger;
        }


        public async Task<IActionResult> Index()
        {

          List<ApplicationUser> users = await _userManager.Users.ToListAsync();
          List<UserRolesViewModel> userRolesViewModel = new List<UserRolesViewModel>();
            

            foreach(ApplicationUser user in users)
            {

                UserRolesViewModel viewModel = new UserRolesViewModel();

                viewModel.UserId = user.Id;
                viewModel.FirstName = user.FirstName;
                viewModel.LastName = user.LastName;
                viewModel.Email = user.Email;
                viewModel.Roles = await GetUserRoles(user);


                userRolesViewModel.Add(viewModel);



            }


            
            
            return View(userRolesViewModel);
        }
    
            private async Task<List<string>> GetUserRoles(ApplicationUser user)
           
            {
            var roles = await _userManager.GetRolesAsync(user);

            return roles.ToList();
            }
        


        public async Task<IActionResult> Manage(string userId)
        {
            ViewBag.UserId = userId;


            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                return NotFound();
            }

            ViewBag.UserName = user.UserName;


            var vm = new List<ManageUserRolesViewModel>();

            foreach(var role in _roleManager.Roles)
            {
                ManageUserRolesViewModel model = new ManageUserRolesViewModel()
                {
                    RoleId = role.Id,
                    RoleName = role.Name,

                };

                if(await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Selected = true;
                }

                else
                {

                    model.Selected = false;

                }


                vm.Add(model);
            
            }

            return View(vm);

        }




        [HttpPost]

        public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                return View(model);
            }


            var selectedRoles = model.Where(r => r.Selected == true).Select(r=>r.RoleName).ToList();

            result = await _userManager.AddToRolesAsync(user, selectedRoles);
            
            if (!result.Succeeded)
            {
                return View(model);
            }

            return RedirectToAction("Index");
        }


    }
}
