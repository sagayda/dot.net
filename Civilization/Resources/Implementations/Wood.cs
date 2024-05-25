namespace Civilization;

public class Wood : IResource
{
	public string Name => "Wood";

	public override bool Equals(object? obj)
	{
		return obj is Wood;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Name);
	}
}
