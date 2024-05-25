namespace Civilization;

public class ResourceWallet
{
	private readonly List<ResourceStack> _resources = [];

	public ResourceStack this[IResource resource]
	{
		get
		{
			return GetStack(resource);
		}
	}

	public void Deposite(ResourceStack stack)
	{
		var stored = GetStack(stack.Resource);

		stored.Unite(stack);
	}

	public ResourceStack Withdraw(IResource resource, int amount)
	{
		var stored = GetStack(resource, true);

		return stored.Split(amount);
	}

	public bool CanSatisfy(ResourceRequest request)
	{
		var stored = GetStack(request.Resource);

		return stored.Amount >= request.Amount;
	}

	public bool CanSatisfy(IEnumerable<ResourceRequest> requests)
	{
		foreach (var request in requests)
			if (CanSatisfy(request) == false)
				return false;

		return true;
	}

	public void Satisfy(ResourceRequest request)
	{
		var stored = GetStack(request.Resource, true);
		stored.Split(request.Amount);
	}

	public void Satisfy(IEnumerable<ResourceRequest> requests)
	{
		foreach (var request in requests)
			Satisfy(request);
	}

	private ResourceStack GetStack(IResource resource, bool throwOnFailure = false)
	{
		var stored = _resources.FirstOrDefault(r => r.Resource.Equals(resource));

		if (stored is not null)
			return stored;

		if (throwOnFailure)
			throw new InvalidOperationException("No such resource in wallet");

		var newStack = new ResourceStack(resource);
		_resources.Add(newStack);
		return newStack;
	}
}
