using System.Diagnostics.CodeAnalysis;

namespace Civilization;

public interface IResource
{
	public string Name { get; }
}

// public abstract class Resource
// {
// 	public abstract string Name { get; }
// 	public int Count { get; private set; }

// 	public Resource()
// 	{
// 		Count = 0;
// 	}

// 	public Resource(int count)
// 	{
// 		Count = count;
// 	}

// 	public bool TryAdd(Resource other)
// 	{
// 		if (GetType() != other.GetType())
// 			return false;

// 		Count += other.Count;
// 		other.Count = 0;

// 		return true;
// 	}
	
// 	public bool TrySeperate(int count, out Resource seperated)
// 	{
// 		seperated = CreateEmpty();
		
// 		if(count > Count)
// 			return false;
		
// 		Count -= count;
// 		seperated.Count = count;
// 		return true;
// 	}
	
// 	public abstract Resource CreateEmpty();
// }
