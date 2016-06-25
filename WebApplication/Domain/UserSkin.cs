namespace WebApplication.Domain
{
    public class UserSkin
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public byte[] Image { get; set; }

        public ApplicationUser User { get; set; }
    }
}