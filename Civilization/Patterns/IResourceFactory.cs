namespace Civilization;

public interface IResourceFactory : ITickable
{
	public UnitUnion AssignedUnits { get; }
	public ResourceRequest ProductionDescription { get; }
	public ResourceStack Storage { get; }

	public void ProduceResource();
}
