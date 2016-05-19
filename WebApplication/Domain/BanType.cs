namespace WebApplication.Domain
{
    #region Using Directives

    using System.ComponentModel.DataAnnotations;

    #endregion

    public enum BanType
    {
        [Display(Name = "Бан")]
        Banned = 0,
        [Display(Name = "Кик")]
        Kicked = 3,
        [Display(Name = "Разбан")]
        Unbanned = 5,
        [Display(Name = "В тюрьме")]
        InJail = 6,
        [Display(Name = "Закрыт чат")]
        Muted = 7,
        [Display(Name = "Выпущен из тюрьмы")]
        UnJail = 8,
        [Display(Name = "Пермаментный бан")]
        Permabanned = 9
    }
}