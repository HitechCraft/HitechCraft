namespace WebApplication.Models
{
    using Domain;

    public class UserSkinViewModel
    {
        public int Id { get; set; }

        public byte[] Image { get; set; }

        public string UserId { get; set; }
    }
}