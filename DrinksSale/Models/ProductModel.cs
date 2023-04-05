namespace DrinksSale.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }

        public ProductModel()
        {
            Name = String.Empty;
        }
    }
}
