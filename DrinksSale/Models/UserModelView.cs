namespace DrinksSale.Models
{
    public class UserModelView
    {
        public ConditionModel Condition { get; set; }
        public List<CoinModel> Coins { get; set; }
        public List<ProductModel> Products { get; set; }

        public UserModelView()
        {
            Condition = new ConditionModel();
            Coins = new List<CoinModel>();
            Products = new List<ProductModel>();
        }
    }
}
