using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactManager.Data
{
    public static class SeedData
    {
        //Initialize Database with String of users:
        public static async Task Initialize(IServiceProvider serviceProvider,
                                            List<string> userList)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            if (userManager != null)
            {

                //Generate Password and UID for each user:
                //foreach (var userName in userList)
                //    {
                //        var userPassword = GenerateSecurePassword();
                //        var userId = await EnsureUser(userManager, userName, userPassword);

                //        NotifyUser(userName, userPassword);
                //    }

                //Check if admin exists, if not create with default settings
                var check_admin = await userManager.FindByNameAsync("admin@clay.nl");
                if (check_admin == null)
                {
                    var admin = new IdentityUser("admin@clay.nl")
                    {
                        Email = "admin@clay.nl",
                        EmailConfirmed = true,
                        Id = "admin"
                    };
                    string admin_password = "P455w0rd!";
                    await userManager.CreateAsync(admin, admin_password);
                }
            }

        }

        private static async Task<string> EnsureUser(UserManager<IdentityUser> userManager,
                                                     string userName, string userPassword)
        {
            var user = await userManager.FindByNameAsync(userName);

            //Check if user exists already:
            if (user == null)
            {
                user = new IdentityUser(userName)
                {
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, userPassword);
            }

            return user.Id;
        }

        private static string GenerateSecurePassword()
        {
            // Generate a secure password
            return "P455W0RD!";
        }

        private static void NotifyUser(string userName, string userPassword)
        {
            // Notify user
        }
    }
}