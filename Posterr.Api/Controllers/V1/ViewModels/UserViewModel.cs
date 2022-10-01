namespace Posterr.Api.Controllers.V1.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public int PostsCount { get; set; }
        public ICollection<UserViewModel> Followers { get; set; }
    }
}
