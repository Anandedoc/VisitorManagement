namespace Visitors.Repo
{
    public interface IUserRepo : IRepo<User>
    {
        void Update(User user);
    }
    public class UserRepo : Repo<User>, IUserRepo
    {
        private readonly Repository _repository;
        public UserRepo(Repository repository) : base(repository)
        {
            _repository = repository;
        }
        public void Update(User user)
        {
            _repository.Users.Update(user);
        }
    }
}
