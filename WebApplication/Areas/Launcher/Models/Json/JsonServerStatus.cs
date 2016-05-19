namespace WebApplication.Areas.Launcher.Models.Json
{
    using System.ComponentModel.DataAnnotations;

    public enum JsonServerStatus
    {
        [Display(Name="Пустой")]
        Empty,
        [Display(Name = "Заполнен")]
        Full,
        [Display(Name = "В сети")]
        Online,
        [Display(Name = "Оффлайн")]
        Offline,
        [Display(Name = "Ошибка")]
        Error
    }
}