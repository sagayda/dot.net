namespace Civilization;

public class Fuel : IResource
{
	public string Name => "Fuel";

	public override bool Equals(object? obj)
	{
		return obj is Fuel;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Name);
	}
}
