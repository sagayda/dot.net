namespace Civilization;

public class ResourceStack
{
	public Resource Resource { get; }
	public int Amount { get; private set; }

	public ResourceStack(Resource resource)
	{
		Resource = resource;
	}

	public ResourceStack(Resource resource, int amount)
	{
		Resource = resource;
		Amount = amount;
	}

	public void Add(int amount)
	{
		ArgumentOutOfRangeException.ThrowIfNegative(amount);

		Amount += amount;
	}

	public void Unite(ResourceStack other)
	{
		if (this == other)
			throw new InvalidOperationException("Trying to unite one stack");

		if (Resource.Equals(other.Resource) == false)
			throw new InvalidOperationException("Trying to unite different resource stacks");

		Amount += other.Amount;
		other.Amount = 0;
	}

	public ResourceStack Split(int amount)
	{
		ArgumentOutOfRangeException.ThrowIfGreaterThan(amount, Amount);

		var newStack = new ResourceStack(Resource)
		{
			Amount = amount
		};

		Amount -= amount;

		return newStack;
	}
}
