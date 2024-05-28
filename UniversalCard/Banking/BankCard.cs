namespace UniversalCard;

public class BankCard : IBankCard
{
	public string Bank { get; }
	public string Number { get; }
	public string CVV { get; }
	public DateTime ExpiryDate { get; }
	public Person Holder { get; }

	public BankCard(string bank, string number, string cvv, DateTime expiryDate, Person holder)
	{
		Bank = bank;
		Number = number;
		CVV = cvv;
		ExpiryDate = expiryDate;
		Holder = holder;
	}

	public bool IsValid()
	{
		return DateTime.Now < ExpiryDate;
	}

	public string GetPaymentDetails()
	{
		return $"Number: {Number} CVV: {CVV} Expire date: {ExpiryDate.Month}/{ExpiryDate.Year}";
	}
}
