using Microsoft.AspNetCore.Identity;

namespace BibTrans.Areas.Identity.Data;

// Add profile data for application users by adding properties to the BibTransUser class
public class BibTransUser : IdentityUser
{
    public string First_name { get; set; }
    public string Last_name { get; set; }

}

