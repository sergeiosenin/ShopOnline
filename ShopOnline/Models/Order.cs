using System;
using System.ComponentModel.DataAnnotations;

namespace ShopOnline.Models
{
    public class Order
    {
        public int ID { get; set; }

        [Display(Name = "Продукт")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public Product Product { get; set; }

        [Display(Name = "Клиент")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public Client Client { get; set; }

        [Display(Name = "Количество")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public int ProductCount { get; set; }

        [Display(Name = "Дата заказа")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}