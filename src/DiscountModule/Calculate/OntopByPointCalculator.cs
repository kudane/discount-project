namespace DiscountModule;

public class OntopByPointCalculator(Campaign campaign, RowItem rowItem, double totalPriceInCart) : ICalculate
{
    private readonly Campaign campaign = campaign;
    private readonly RowItem rowItem = rowItem;
    private readonly double totalPriceInCart = totalPriceInCart;

    public double Execute()
    {
        campaign.Spec.ValidateThrowIfError();

        // check not over 20%
        var limit = totalPriceInCart * 0.20;

        var point = ((OntopByPointSpec)campaign.Spec).Point;

        if (point > limit)
        {
            point = limit;
        }

        var discount = rowItem.PercentOfTotalPrice * point;

        return rowItem.Price - discount;
    }
}