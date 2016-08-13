namespace HitechCraft.WebApplication.Models
{
    #region Using Directives

    

    #endregion

    public class CommentCreateViewModel
    {
        public int NewsId { get; set; }

        public string Text { get; set; }

        public string Recaptcha { get; set; }
    }
}