namespace Civilization;

public readonly struct ResourceRequest
{
	public readonly Resource Resource;
	public readonly int Amount;

	public ResourceRequest(Resource resource, int amount)
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