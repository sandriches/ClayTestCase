using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClayTestCase.Pages
{
    //This View page allows the admin to read all users and remove them
    [Authorize]
    public class ViewModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ViewModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public PageResult OnGetAsync()
        {
            //Check if the user is admin
            if (User.Identity.Name != "admin@clay.nl")
            {
                ModelState.AddModelError(string.Empty, "Not Authorized to View");
                Response.Redirect("/Index");
                return Page();
                //return Redirect("/Index");
            }
            return (Page());
        }
        public List<string> get_users()
        {
            //List all users
            var allusers = _userManager.Users.ToList();
            List<string> userList = new List<string>();
            foreach (var i in allusers)
            {
                userList.Add(i.ToString());
            }
            return userList;
        }
        public async Task<IActionResult> OnPostRemove_User(string name)
        {
            //Remove user - check first if user exists and isn't admin
            IdentityUser user = await _userManager.FindByEmailAsync(name);
            if (user != null && name != "admin@clay.nl")
            {
                await _userManager.DeleteAsync(user);
            }
            return Page();
        }
    }
}
