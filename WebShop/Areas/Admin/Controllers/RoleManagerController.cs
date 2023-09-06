using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebShop.Data;

namespace WebShop.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class RoleManagerController : Controller
    {


        private RoleManager<IdentityRole> _roleManager;


        public RoleManagerController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }


        public IActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }


        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if(roleName != null)
            {


                IdentityRole role = new IdentityRole()
                {
                    Name = roleName,
                };
                    await _roleManager.CreateAsync(role);
            }


            return RedirectToAction("Index");
        }




        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id.ToString());

            if(role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);


                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }


                else
                {
                    ModelState.AddModelError("","Error while deleting role");
                }


                
            }

            else
            {
                ModelState.AddModelError("", "Role not found");
            }

            return View("Index");

        }



        [HttpGet]
        public async Task<IActionResult> UpdateRole(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
              
                return View("Edit", role);
            }
            else
            {
                ModelState.AddModelError("", "Role not found");
                return View("Index");
            }
        }





        [HttpPost]
        public async Task<IActionResult> UpdateRole(string id, string roleName)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                role.Name = roleName;
                IdentityResult result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Error while updating role");
                }
            }
            else
            {
                ModelState.AddModelError("", "Role not found");
            }

            return View("Index");
        }









    }
}
