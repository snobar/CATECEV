using CATECEV.Models.Entity.Base;

namespace CATECEV.Models.Entity.AMPECO.Resources.User
{
    public class UserOptions : BaseEntity
    {
        public int UserId { get; set; }
        public string SessionsAllowed { get; set; }

        public User User { get; set; }
    }
}
