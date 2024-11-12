using BibTrans.Areas.Identity.Data;
using BibTrans.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;

namespace BibTrans.CustomTagHelpers
{
    [HtmlTargetElement("td", Attributes = "i-role")]
    public class RoleUsersTH : TagHelper
    {
        private readonly UserManager<BibTransUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleUsersTH(UserManager<BibTransUser> usermgr, RoleManager<IdentityRole> rolemgr, ILogger<RoleUsersTH> logger)
        {
            userManager = usermgr;
            roleManager = rolemgr;
        }

        [HtmlAttributeName("i-role")]
        public string Role { get; set; } = string.Empty;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            List<string> names = new();
            IdentityRole role = await roleManager.FindByIdAsync(Role);

            foreach(var user in userManager.Users)
            {
                if (user == null || role == null)
                {
                    break;
                }

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    names.Add(user.UserName);
                }
            }

            output.Content.SetContent(names.Count == 0 ? "Brak użytkowników" : string.Join(", ", names));
        }
    }
}
