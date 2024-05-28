namespace UniversalCard;

public class InsurancePolicy : IInsurancePolicy
{
    public string Number { get; }
    public string Provider { get; }
    public DateTime IssueDate { get; }
    public DateTime ExpiryDate { get; }

    public InsurancePolicy(string number, string provider, DateTime issueDate, DateTime expiryDate)
    {
        Number = number;
        Provider = provider;
        IssueDate = issueDate;
        ExpiryDate = expiryDate;
    }

    public bool IsValid()
    {
        return DateTime.Now < ExpiryDate;
    }
}
