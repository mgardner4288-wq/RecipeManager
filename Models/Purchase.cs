namespace RecipeManager.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public string BuyerEmail { get; set; } = "";
        public int Quantity { get; set; }
    }
}