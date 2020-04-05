using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.ViewModels.Orders
{
    public class OrderViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required]
        public string Adress { get; set; }
    }
}
