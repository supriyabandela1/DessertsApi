using System.ComponentModel.DataAnnotations.Schema;

namespace DessertsApi.Models
{
    [Table("Products")]
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }

        public string imageUrl { get; set; }
     
    }
}
