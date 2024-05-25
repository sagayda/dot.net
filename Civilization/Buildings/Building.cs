using System.Collections.ObjectModel;

namespace Civilization;

public abstract class Building
{
	public abstract string Name { get; }
	public Territory Location { get; }
	public abstract ReadOnlyCollection<ResourceRequest> ResourcesToBuild { get; }

	public Building(Territory territory)
	{
		Location = territory;
	}
}