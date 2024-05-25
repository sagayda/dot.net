namespace Civilization;

public class Food : IResource
{
	public string Name => "Food";

	public override bool Equals(object? obj)
	{
		return obj is Food;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Name);
	}
}
