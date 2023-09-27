namespace Data.Base
{
    public interface IEntity
    {
        long Id { get; }
        bool IsNew { get; }
        string? CreatedById { get; }
        DateTimeOffset? CreatedTime { get; }
        string? UpdatedById { get; }
        DateTimeOffset? UpdatedTime { get; }
        void UpdateAuditFields(EntityState state, string userId, DateTimeOffset dateTimeOffset);
    }
}
