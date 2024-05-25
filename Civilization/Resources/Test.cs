// namespace Civilization;

// public interface IResource
// {
// 	public string Name { get; }
// }

// public class Coal : IResource
// {
// 	public string Name => "Coal";
// }

// public class ResourceStack
// {
// 	public IResource Resource { get; }
// 	public int Amount { get; private set; }

// 	public ResourceStack(IResource resource)
// 	{
// 		Resource = resource;
// 	}
	
// 	public void Unite(ResourceStack other)
// 	{
// 		if(Resource != other.Resource)
// 			throw new InvalidOperationException("Trying to unite different resource stacks");
		
// 		Amount += other.Amount;
// 		other.Amount = 0;
// 	}
	
// 	public ResourceStack Split(int amount)
// 	{
// 		ArgumentOutOfRangeException.ThrowIfGreaterThan(amount, Amount);

// 		var newStack = new ResourceStack(Resource)
// 		{
// 			Amount = amount
// 		};

// 		Amount -= amount;
		
// 		return newStack;
// 	}

// }
