namespace Visitors.Repo
{
    public interface IVisitorDetailsRepo : IRepo<VisitorDetails>
    {
        void Update(VisitorDetails visitingDetails);
        void UpdateStatus(VisitorDetails visitingDetails);
        void UpdateTime(VisitorDetails visitingDetails);
    }
    public class VisitorDetailsRepo : Repo<VisitorDetails>, IVisitorDetailsRepo
    {
        private readonly Repository _repository;
        public VisitorDetailsRepo(Repository repository) : base(repository)
        {
            _repository = repository;
        }
        public void Update(VisitorDetails visitingDetails)
        {

            _repository.VisitorDetails.Update(visitingDetails);
        }

        public void UpdateStatus(VisitorDetails visitorDetails)
        {
            var visitorDetailsToBeUpdated = _repository.VisitorDetails.FirstOrDefault(x => x.Id == visitorDetails.Id);
            if (visitorDetailsToBeUpdated != null)
            {
                visitorDetailsToBeUpdated.VisitingStatus = visitorDetails.VisitingStatus;
            }
            _repository.VisitorDetails.Update(visitorDetailsToBeUpdated);
        }
        public void UpdateTime(VisitorDetails visitorDetails)
        {
            var visitorDetailsToBeUpdated = _repository.VisitorDetails.FirstOrDefault(x => x.Id == visitorDetails.Id);
            if (visitorDetailsToBeUpdated != null)
            {
                if (!string.IsNullOrEmpty(visitorDetails.OutTime.ToString()))
                    visitorDetailsToBeUpdated.OutTime = visitorDetails.OutTime;
                if (!string.IsNullOrEmpty(visitorDetails.InTime.ToString()))
                    visitorDetailsToBeUpdated.InTime = visitorDetails.InTime;
            }
            _repository.VisitorDetails.Update(visitorDetailsToBeUpdated);
        }
    }
}
