using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class ShopItemViewModel
    {
        public string GameId { get; set; }

        [Display(Name = "Наименование")]
        [Required(ErrorMessage = "Обязательное поле")]
        [MinLength(3, ErrorMessage = "Минимальная длина - 3 символа")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Обязательное поле")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Категория")]
        public int Category { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Изображение")]
        public byte[] Image { get; set; }

        [Required]
        [Display(Name = "Цена")]
        [Range(0.01f, float.MaxValue, ErrorMessage = "Минимальное значение цены 0.01 рубля")]
        public float Price { get; set; }
    }
}