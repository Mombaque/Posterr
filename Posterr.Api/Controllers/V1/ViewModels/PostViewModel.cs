namespace Posterr.Api.Controllers.V1.ViewModels
{
    public class PostViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
    }
}
