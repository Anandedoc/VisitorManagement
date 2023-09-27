using Data.Models;
using Data.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Data
{
    public class Repository : IdentityDbContext<User>
    {

        private readonly ILocalTimeHelper _localTimeHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Repository(DbContextOptions<Repository> options, IUserContext userContext, ILocalTimeHelper localTimeHelper, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _localTimeHelper = localTimeHelper;
            _httpContextAccessor = httpContextAccessor;

        }

        public DbSet<User> Users { get; set; }
        public DbSet<VisitorDetails> VisitorDetails { get; set; }
        public DbSet<Department> Departments { get; set; }


        public override int SaveChanges()
        {
            UpdateAuditFields();

            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return -1;
            }
        }

        private void UpdateAuditFields()
        {
            var currentUser = _httpContextAccessor.HttpContext.User.Claims.First();

            var eligibleEnties = ChangeTracker.Entries<IEntity>()
                .Where(x => x.State is EntityState.Added or EntityState.Modified).ToList();

            foreach (var entry in eligibleEnties)
            {
                if (entry.Entity is IEntity entity)
                    entity.UpdateAuditFields(entry.State, currentUser.Value, _localTimeHelper.GetLocalDateTime());
            }
        }

    }
}
