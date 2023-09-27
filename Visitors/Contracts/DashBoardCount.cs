namespace Visitors.Contracts
{
    public class DashBoardCount
    {
        public long TotalAppointments { get; set; }
        public long TotalPending { get; set; }
        public long TotalCanceled { get; set; }
        public long TotalCompleted { get; set; }
    }
}
