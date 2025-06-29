namespace DiscountModule;

public class RowItem
{
    public Item Item { get; set; } = null!;
    public double PercentOfTotalPrice { get; set; } = 0.0;
    public double Price { get; set; } = 0.0;
    public IEnumerable<Campaign>? Campaigns { get; set; }
}
