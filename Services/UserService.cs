using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentsManagerMW.Models.Inputs;

namespace StudentsManagerMW.Services
{
    public class UserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityUser> RegisterUser(RegisterModel model)
        {
            // Registration logic with role assignment as discussed previously
            var user = new IdentityUser { UserName = model.Username };
            var createResult = await _userManager.CreateAsync(user, model.Password);

            if (createResult.Succeeded)
            {
                string roleName = model.Role ?? "User"; // Assign "User" by default if no role provided
                await _userManager.AddToRoleAsync(user, roleName);
            }

            return user;
        }
    }
}
