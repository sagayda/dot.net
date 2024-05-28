namespace UniversalCard;

public interface IBankCard
{
	public string Bank { get; }
	public bool IsValid();
	public string GetPaymentDetails();
}
