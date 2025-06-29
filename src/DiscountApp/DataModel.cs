using DiscountModule;

namespace DiscountApp;

public class DataModel
{
    public List<DiscountModule.Item> Items { get; set; }
    public List<Campaign> Campaigns { get; set; }
    public List<Order> Orders { get; set; }
}