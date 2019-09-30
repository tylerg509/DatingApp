using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using HarvestApp.API.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace HarvestApp.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public Seed(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedUsers()
        {

            if(!_userManager.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                var roles = new List<Role>
                {
                    new Role{Name = "Member"},
                    new Role{Name = "Admin"},
                    new Role{Name = "Moderator"},
                    new Role{Name = "VIP"},

                };

                foreach(var role in roles)
                {
                    _roleManager.CreateAsync(role).Wait();
                }


                foreach(var user in users)
                {
                    //change for prod due to weak password
                    user.Photos.SingleOrDefault().IsApproved = true;
                    _userManager.CreateAsync(user, "password").Wait();
                    _userManager.AddToRoleAsync(user, "Member").Wait();
                }

                var adminUser = new User 
                {
                    UserName = "Admin"
                };

                IdentityResult result = _userManager.CreateAsync(adminUser, "H$+vG)q7H6:J").Result;
                if(result.Succeeded)
                {
                    var admin = _userManager.FindByNameAsync("Admin").Result;
                    _userManager.AddToRolesAsync(admin, new[] {"Admin", "Moderator"}).Wait();
                }

            }

            

        }

    }
}