namespace Civilization;

public class Iron : IResource
{
	public string Name => "Iron";

	public override bool Equals(object? obj)
	{
		return obj is Iron;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Name);
	}
}
