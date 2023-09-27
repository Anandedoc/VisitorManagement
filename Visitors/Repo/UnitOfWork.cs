namespace Visitors.Repo
{
    public interface IUnitOfWork
    {
        IUserRepo User { get; }
        IVisitorDetailsRepo VisitorDetails { get; }
        IDepartmentsRepo Departments { get; }
        void Save();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Repository _repository;

        public UnitOfWork(Repository repository)
        {
            _repository = repository;
            User = new UserRepo(_repository);
            VisitorDetails = new VisitorDetailsRepo(_repository);
            Departments = new DepartmentsRepo(_repository);

        }

        public IUserRepo User { get; private set; }
        public IVisitorDetailsRepo VisitorDetails { get; private set; }
        public IDepartmentsRepo Departments { get; private set; }


        public void Save()
        {

            _repository.SaveChanges();
        }
    }
}
