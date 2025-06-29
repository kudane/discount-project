namespace DiscountModule;

public class CalculateFactory
{
    public static ICalculate? GetCalculator(Campaign campaign, RowItem rowItem, double totalPriceInCart)
    {
        return campaign.Type switch
        {
            CampaignEnum.CouponByAmount => new CouponByAmountCalculator(campaign, rowItem),
            CampaignEnum.CouponByPercent => new CouponByPercentCalculator(campaign, rowItem, totalPriceInCart),
            CampaignEnum.OntopByPoint => new OntopByPointCalculator(campaign, rowItem, totalPriceInCart),
            CampaignEnum.OntopByPercentOfCatagory => new OntopByPercentOfCatagoryCalculator(campaign, rowItem),
            CampaignEnum.Seasonal => new SeasonalCalculator(campaign, rowItem, totalPriceInCart),
            _ => null,
        };
    }
}
