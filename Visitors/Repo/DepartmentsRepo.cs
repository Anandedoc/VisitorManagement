namespace Visitors.Repo
{
    public interface IDepartmentsRepo : IRepo<Department>
    {
        void Update(Department user);
    }
    public class DepartmentsRepo : Repo<Department>, IDepartmentsRepo
    {
        private readonly Repository _repository;
        public DepartmentsRepo(Repository repository) : base(repository)
        {
            _repository = repository;
        }
        public void Update(Department user)
        {
            _repository.Departments.Update(user);
        }
    }
}
