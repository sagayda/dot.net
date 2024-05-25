using System.Collections.ObjectModel;

namespace Civilization;

public interface IUnitFactory
{
	public Civilization? Owner { get; }
	public ReadOnlyCollection<ResourceRequest> ResourcesPerUnit { get; }
	public UnitGroup TrainedUnits { get; }
	public void Create(ResourceWallet wallet, Civilization requesting);
}
