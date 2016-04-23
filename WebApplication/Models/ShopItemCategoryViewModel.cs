using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class ShopItemCategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Имя категории")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Описание")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
    }
}