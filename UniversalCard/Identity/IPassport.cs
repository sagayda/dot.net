namespace UniversalCard;

public interface IPassport
{
	public Person Holder { get; }
	public bool IsValid();
	public int GetHolderAge();
}
