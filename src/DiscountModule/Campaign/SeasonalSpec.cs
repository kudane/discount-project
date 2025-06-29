namespace DiscountModule;

public class SeasonalSpec : ICampaignSpec
{
    public double EveryPrice { get; set; }
    public double Amount { get; set; }

    public void ValidateThrowIfError()
    {
        if (EveryPrice < 0.0)
        {
            throw new ArgumentException("EveryPrice must be zero or greater for SeasonalSpec.");    
        }

        if (Amount < 0.0)
        {
            throw new ArgumentException("EveryPrice must be zero or greater for SeasonalSpec.");
        }
    }
}