using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ClayTestCase.Pages
{
    //Create a user account here (only admin allowed)
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<CreateModel> _logger;
        private readonly IEmailSender _emailSender;

        public CreateModel(SignInManager<IdentityUser> signInManager,
            ILogger<CreateModel> logger,
            UserManager<IdentityUser> userManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            //User input details
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            //Check if user has admin rights
            if (User.Identity.Name != "admin@clay.nl")
            {
                ModelState.AddModelError(string.Empty, "Not Authorized to View");
                Response.Redirect("/Index");
            }
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                //Check if the user already exists
                var check_if_user_exists = await _userManager.FindByNameAsync(Input.Email);
                if (check_if_user_exists == null)
                {
                    //Create new user
                    var new_user = new IdentityUser(Input.Email)
                    {
                        Email = Input.Email,
                        EmailConfirmed = true
                    };
                    var result = await _userManager.CreateAsync(new_user, Input.Password);

                    //Check password length
                    if (result.ToString() == "Failed : PasswordTooShort")
                    {
                        ModelState.AddModelError(string.Empty, "Password must be minimum 6 characters");
                    }
                    else
                    {
                        //User created
                        return (Redirect("~/Create_Successful"));
                    }
                    return (Page());
                }
                else
                {
                    //User has already been added
                    ModelState.AddModelError(string.Empty, "User already exists");
                    return (Page());
                }
            }
            //Should never reach here
            ModelState.AddModelError(string.Empty, "Model failed - try again");
            return (Page());
        }
    }
}
