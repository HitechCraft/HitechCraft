﻿using HitechCraft.Core.Models.Enum;

namespace HitechCraft.WebApplication.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SkinEditViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Изображение")]
        public byte[] Image { get; set; }

        [Display(Name = "Пол")]
        public Gender Gender { get; set; }
    }
}