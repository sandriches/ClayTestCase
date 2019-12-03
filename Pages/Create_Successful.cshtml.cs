using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace ClayTestCase.Pages
{
    //Simple confirmation page for user creation, no logic needed
    [Authorize]
    public class Create_SuccessModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
