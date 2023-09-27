using Data.Models;
using Data.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace Data.DbInitializer
{
    public interface IDbInitializer
    {
        void Initialize();
    }
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Repository _repository;

        public DbInitializer(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            Repository repository
) 
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _repository = repository;
        }
        public void Initialize()
        {
            //migrations if they are not applied
            try
              
            {
                _repository.Database.EnsureCreated();
                if (_repository.Database.GetPendingMigrations().Count() > 0)
                {
                    _repository.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }
            if (!_roleManager.RoleExistsAsync(nameof(UserRoleTypeValues.SuperAdmin)).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(nameof(UserRoleTypeValues.SuperAdmin))).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(nameof(UserRoleTypeValues.GateAdmin))).GetAwaiter().GetResult();

                _roleManager.CreateAsync(new IdentityRole(nameof(UserRoleTypeValues.User))).GetAwaiter().GetResult();


                //if roles are not created, then we will create admin user as well

                _userManager.CreateAsync(new User
                {
                    
                    UserName = "1212",
                    UserId = 1,
                    Email = "admin@gmail.com",
                    Name = "Anand",
                    PhoneNumber = "9898989898",
                    Address = "kuniyamuthur",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    UserRole = nameof(UserRoleTypeValues.SuperAdmin),
                    TwoFactorEnabled = false,
                    LockoutEnabled = false
                }, "Password-2").GetAwaiter().GetResult();

                User user = _repository.Users.FirstOrDefault(x => x.Email == "admin@gmail.com");
                _userManager.AddToRoleAsync(user, nameof(UserRoleTypeValues.SuperAdmin)).GetAwaiter().GetResult();
            }
            return;
        }
    }
}
