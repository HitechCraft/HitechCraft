namespace HitechCraft.WebAdmin.Models
{
    using System.ComponentModel.DataAnnotations;

    public class NewsEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public string Text { get; set; }

        public byte[] Image { get; set; }

        public string AuthorName { get; set; }
    }
}