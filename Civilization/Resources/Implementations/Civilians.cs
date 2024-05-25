namespace Civilization;

public class Civilians : IResource
{
	public string Name => "Civilians";

	public override bool Equals(object? obj)
	{
		return obj is Civilians;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Name);
	}
}
