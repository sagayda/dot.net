namespace Civilization;

public class Gold : IResource
{
	public string Name => "Gold";

	public override bool Equals(object? obj)
	{
		return obj is Gold;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Name);
	}
}
