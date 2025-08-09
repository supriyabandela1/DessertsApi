namespace DessertsApi.Models
{
    public class Dessert
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal price { get; set; }
        public string ImageUrl { get; set; }
    }

}
