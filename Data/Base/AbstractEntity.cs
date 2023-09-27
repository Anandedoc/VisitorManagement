
namespace Data.Base
{
    
    public abstract class AbstractEntity : IEntity
    {
        public long Id { get; set; }

        public bool IsNew => Id == 0;

        public string? CreatedById { get; set; }

        public DateTimeOffset? CreatedTime { get; set; }

        public string? UpdatedById { get; set; }

        public DateTimeOffset? UpdatedTime { get; set; }

        public void UpdateAuditFields(EntityState state, string userId, DateTimeOffset dateTimeOffset)
        {
            switch (state)
            {
                case EntityState.Added:
                    CreatedById = userId;
                    CreatedTime = dateTimeOffset;
                    break;

                case EntityState.Modified:
                    UpdatedById = userId;
                    UpdatedTime = dateTimeOffset;
                    break;
            }
        }
    }
}
