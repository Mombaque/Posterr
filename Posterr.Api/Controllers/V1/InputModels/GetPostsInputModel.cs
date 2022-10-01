namespace Posterr.Api.Controllers.V1.InputModels
{
    public class GetPostsInputModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? FinalDate { get; set; }
        public int UserId { get; set; }
        public int Page { get; set; }
        public int Quantity { get; set; }
    }
}
