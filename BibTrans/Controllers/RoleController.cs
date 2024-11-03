using BibTrans.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BibTrans.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<BibTransUser> userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<BibTransUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public ViewResult Index() => View(roleManager.Roles);

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    Errors(result);
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    Errors(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "No role found");
            }
            return View("Index", roleManager.Roles);
        }

        public async Task<IActionResult> Update(string? id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> ModifyMember([FromForm] string userEmail, string roleName, string action)
        {
            if (userEmail == null || roleName == null || action == null)
            {
                return BadRequest();
            }

            BibTransUser user = await userManager.FindByEmailAsync(userEmail);

            IdentityResult? result = new();

            if (action == "Create")
            {
                result = await userManager.AddToRoleAsync(user, roleName);
            }
            else if (action == "Delete")
            {
                result = await userManager.RemoveFromRoleAsync(user, roleName);
            }

            if (!result.Succeeded)
            {
                return StatusCode(500);
            }

            return RedirectToAction("Index");
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}
