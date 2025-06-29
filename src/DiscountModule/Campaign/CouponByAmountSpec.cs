namespace DiscountModule;

public class CouponByAmountSpec : ICampaignSpec
{
    public double Amount { get; set; }

    public void ValidateThrowIfError()
    {
        if(Amount < 0.0)
        {
            throw new ArgumentException("Amount must be zero for CouponByAmountSpec.");
        }
    }
}