namespace UniversalCard;

public interface IInsurancePolicy
{
	public string Provider { get; }
	public bool IsValid();
}
