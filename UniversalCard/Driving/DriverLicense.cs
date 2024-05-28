namespace UniversalCard;

public class DriverLicense : IDriverLicense
{
	public string Number { get; }
	public string Country { get; }
	public DateTime IssueDate { get; }
	public DateTime ExpiryDate { get; }
	public Person Holder { get; }

	public DriverLicense(string number, string country, DateTime issueDate, DateTime expiryDate, Person holder)
	{
		Number = number;
		Country = country;
		IssueDate = issueDate;
		ExpiryDate = expiryDate;
		Holder = holder;
	}

	public bool IsValid()
	{
		return DateTime.Now < ExpiryDate;
	}
}
