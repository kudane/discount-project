using System.Text.Json.Serialization;

namespace DiscountModule;

public class Order
{
    public int No { get; set; }

    public IEnumerable<CampaignEnum> Campaigns { get; set; }
}
