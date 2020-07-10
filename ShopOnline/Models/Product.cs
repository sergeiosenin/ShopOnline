using System;
using System.ComponentModel.DataAnnotations;

namespace ShopOnline.Models
{
    public class Product
    {
        public int ID { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage ="Поле обязательно для заполнения")]
        public string Name { get; set; }

        [Display(Name = "Стоимость")]
        [DataType(DataType.Currency)]
        //[DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Cost { get; set; }

        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name ="Дата добавления")]
        [DataType(DataType.Date)]
        public DateTime DateAdd { get; set; }
        
    }
}