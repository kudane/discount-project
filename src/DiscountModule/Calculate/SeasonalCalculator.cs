namespace DiscountModule;

public class SeasonalCalculator(Campaign campaign, RowItem rowItem, double totalPriceInCart) : ICalculate
{
    private readonly Campaign campaign = campaign;
    private readonly RowItem rowItem = rowItem;
    private readonly double totalPriceInCart = totalPriceInCart;

    public double Execute()
    {
        campaign.Spec.ValidateThrowIfError();

        var x = ((SeasonalSpec)campaign.Spec).EveryPrice;
        var y = ((SeasonalSpec)campaign.Spec).Amount;

        var count = totalPriceInCart / x;

        var amount = count * y;

        var discount = rowItem.PercentOfTotalPrice * amount;

        return rowItem.Price - discount;
    }
}
