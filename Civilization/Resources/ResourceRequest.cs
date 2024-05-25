namespace Civilization;

public readonly struct ResourceRequest
{
	public readonly IResource Resource;
	public readonly int Amount;

	public ResourceRequest(IResource resource, int amount)
	{
		Resource = resource;
		Amount = amount;
	}
}


public class ResourceRequestList
{
	private readonly List<ResourceRequest> _requests = [];
	
	public void Add(ResourceRequest request)
	{
		_requests.Add(request);
	}
}