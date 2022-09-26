using Posterr.Domain.Enums;

namespace Posterr.Api.Controllers.V1.InputModels
{
    public class SavePostInputModel
    {
        public string? Content { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public EPostType Type { get; set; }
        public Guid? RepostId { get; set; }
        public string? QuoteCommentary { get; set; }
    }
}
