namespace StriderBackend.Api.Controllers.V1.InputModels
{
    public class PostInputModel
    {
        public string? Content { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
