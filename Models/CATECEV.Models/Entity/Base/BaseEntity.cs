namespace CATECEV.Models.Entity.Base
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;

        public DateTime CreatedOn { get; set; }
    }
}
