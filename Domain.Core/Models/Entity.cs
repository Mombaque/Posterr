namespace Domain.Core.Models
{
    [Serializable]
    public abstract class Entity<TID>
    {
        public TID Id { get; set; }
    }
}