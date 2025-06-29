namespace DiscountModule;

public class OntopByPointSpec : ICampaignSpec
{
    public double Point { get; set; }

    public void ValidateThrowIfError()
    {
        if (Point < 0.0)
        {
            throw new Exception("Point must be zero or greater for OntopByPointSpec."); 
        }
    }
}   