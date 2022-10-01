namespace Posterr.Api.Controllers.V1.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(int id, string name, DateTime creationDate, int postsCount)
        {
            Id = id;
            Name = name;
            CreationDate = creationDate;
            PostsCount = postsCount;
        }

        public UserViewModel()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public int PostsCount { get; set; }
        public IEnumerable<UserViewModel> Followers { get; set; }
        public IEnumerable<PostViewModel> Posts { get; set; }
    }
}
