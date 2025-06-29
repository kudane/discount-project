namespace DiscountModule;

public class OntopByPercentOfCatagoryCalculator(Campaign campaign, RowItem rowItem) : ICalculate
{
    private readonly Campaign campaign = campaign;
    private readonly RowItem rowItem = rowItem;

    public double Execute()
    {
        campaign.Spec.ValidateThrowIfError();

        var percent = ((OntopByPercentOfCatagorySpec)campaign.Spec).Percent;

        var discount = rowItem.Price * (percent / 100);

        return rowItem.Price - discount;
    }
}