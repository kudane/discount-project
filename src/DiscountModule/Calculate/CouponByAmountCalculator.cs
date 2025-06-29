namespace DiscountModule;

public class CouponByAmountCalculator(Campaign campaign, RowItem rowItem) : ICalculator
{
    private readonly Campaign campaign = campaign;
    private readonly RowItem rowItem = rowItem;

    public double Execute()
    {
        campaign.Spec.ValidateThrowIfError();

        var amount = ((CouponByAmountSpec)campaign.Spec).Amount;

        var discount = rowItem.PercentOfTotalPrice * amount;

        return rowItem.Price - discount;
    }
}