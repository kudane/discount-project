namespace DiscountModule;

public class CouponByPercentCalculator(Campaign campaign, RowItem rowItem, double totalPriceInCart) : ICalculate
{
    private readonly Campaign campaign = campaign;
    private readonly RowItem rowItem = rowItem;
    private readonly double totalPriceInCart = totalPriceInCart;

    public double Execute()
    {
        campaign.Spec.ValidateThrowIfError();

        var percent = ((CouponByPercentSpec)campaign.Spec).Percent;

        var amount = totalPriceInCart * (percent / 100);

        var discount = rowItem.PercentOfTotalPrice * amount;

        return rowItem.Price - discount;
    }
}
