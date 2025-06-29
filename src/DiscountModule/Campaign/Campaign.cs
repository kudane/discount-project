namespace DiscountModule;

public class Campaign
{
    public CampaignEnum Type { get; set; }
    public ICampaignSpec Spec { get; set; }
    public Func<Item, Campaign, bool>? Ignore { get; set; } = (item, campaign) 
        => campaign.Type == CampaignEnum.OntopByPercentOfCatagory && item.Catagory != ((OntopByPercentOfCatagorySpec)campaign.Spec).ItemCategory;
}
