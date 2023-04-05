namespace DrinksSale.Models
{
    public class AdminModelView
    {
        public List<CoinModel> Coins { get; set; }
        public List<ProductModel> Products { get; set; }
        public ProductModel Product { get; set; }

        public AdminModelView() 
        {
            Coins = new List<CoinModel>();
            Products = new List<ProductModel>();
            Product = new ProductModel();
        }
    }
}
