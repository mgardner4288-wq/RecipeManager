namespace RecipeManager.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = "";
        public decimal Price { get; set; }
        public int Inventory { get; set; }
        public string SellerEmail { get; set; } = "";
    }
}