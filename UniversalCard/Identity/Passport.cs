namespace UniversalCard;

public class Passport : IPassport
{
	public string DocumentNumber { get; }
	public string RecordNumber { get; }
	public string Nationality { get; }
	public DateTime IssueDate { get; }
	public DateTime ExpiryDate { get; }
	public Person Holder { get; }

	public Passport(string documentNumber, string recordNumber, string nationality, DateTime issueDate, DateTime expiryDate, Person holder)
	{
		DocumentNumber = documentNumber;
		RecordNumber = recordNumber;
		Nationality = nationality;
		IssueDate = issueDate;
		ExpiryDate = expiryDate;
		Holder = holder;
	}

	public bool IsValid()
	{
		return DateTime.Now < ExpiryDate;
	}

	public int GetHolderAge()
	{
		return Holder.GetAge();
	}
}