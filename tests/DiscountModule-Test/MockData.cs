using DiscountApp;
using DiscountModule;
using System.Text.Json;

namespace DiscountModule_Test;

public static class MockData
{
    public static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new CampaignConverter(), new OrderConverter() }
    };

    public static List<Item> Items =
    [
        new (){ Name = "Hood", Price = 100, Catagory = "Clothing" },
        new (){ Name = "T-Shirt", Price = 100, Catagory = "Clothing" },
        new() { Name = "Watch", Price = 100, Catagory = "Electronics" },
        new() { Name = "Bag", Price = 100, Catagory = "Accessories" }
    ];

    public static List<Order> orders = new List<Order>()
    {
        new() { No = 1, Campaigns = [CampaignEnum.CouponByAmount, CampaignEnum.CouponByPercent] },
        new() { No = 2, Campaigns = [CampaignEnum.OntopByPercentOfCatagory, CampaignEnum.OntopByPoint] },
        new() { No = 3, Campaigns = [CampaignEnum.Seasonal] }
    };
}
