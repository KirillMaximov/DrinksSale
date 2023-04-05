using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DrinksSale.Models
{
    public class SaleModel
    {
        public int Id { get; set; }
        public int ConditionId { get; set; }
        public int Amount { get; set; }
        public int Revenue { get; set; }
        public int ProductId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
