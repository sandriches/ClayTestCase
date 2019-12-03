using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace ClayTestCase.Pages
{
    [Authorize]
    public class Door_1Model : PageModel
    {
        public void OnGet()
        {
            //Add some super cool, ninja james bond style security confirmation that I'm not really how to do
        }
    }
}
