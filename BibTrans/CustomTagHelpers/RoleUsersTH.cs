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
        private readonly ILogger<RoleUsersTH> logger;

        public RoleUsersTH(UserManager<BibTransUser> usermgr, RoleManager<IdentityRole> rolemgr, ILogger<RoleUsersTH> logger)
        {
            userManager = usermgr;
            roleManager = rolemgr;
            this.logger = logger;
        }

        [HtmlAttributeName("i-role")]
        public string Role { get; set; } = string.Empty;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            logger.LogInformation($"Processing role: {Role}");
            List<string> names = new();
            IdentityRole role = await roleManager.FindByIdAsync(Role);
            if (role != null)
            {
                logger.LogInformation($"Role found: {role.Name}");
                foreach (var user in userManager.Users)
                {
                    if (user != null && await userManager.IsInRoleAsync(user, role.Name))
                    {
                        names.Add(user.UserName);
                        logger.LogInformation($"User {user.UserName} is in role {role.Name}");
                    }
                }
            }
            else
            {
                logger.LogWarning($"Role not found: {Role}");
            }
            output.Content.SetContent(names.Count == 0 ? "Brak użytkowników" : string.Join(", ", names));
        }
    }
}
