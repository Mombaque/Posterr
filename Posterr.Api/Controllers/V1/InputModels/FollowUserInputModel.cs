namespace Posterr.Api.Controllers.V1.InputModels
{
    public class FollowUserInputModel
    {
        public int UserId { get; set; }
        public int UserFollowerId { get; set; }
        public bool Follow { get; set; }
    }
}
