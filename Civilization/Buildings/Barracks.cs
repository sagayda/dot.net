using System.Collections.ObjectModel;

namespace Civilization;

public class Barracks : Building, IUnitFactory
{
	public Civilization? Owner => Location.Owner;
	public override string Name => "Barracks";

	public override ReadOnlyCollection<ResourceRequest> ResourcesToBuild { get; }
	public ReadOnlyCollection<ResourceRequest> ResourcesPerUnit { get; }

	public UnitGroup TrainedUnits { get; private set; }

	public Barracks(Territory territory) : base(territory)
	{
		TrainedUnits = [];

		ResourcesToBuild = new ReadOnlyCollection<ResourceRequest>(
		[
			new(new Iron(), 20),
			new(new Wood(), 50)
		]);

		ResourcesPerUnit = new ReadOnlyCollection<ResourceRequest>(
		[
			new(new Civilians(), 1),
			new(new Iron(), 5),
			new(new Food(), 5)
		]);
	}

	public void Create(ResourceWallet wallet, Civilization requesting)
	{
		if (IsOwnedBy(requesting) == false)
		{
			throw new InvalidOperationException("Tequesting civilization is not equals owner");
		}

		foreach (var request in ResourcesPerUnit)
			wallet.Satisfy(request);

		TrainedUnits.Add(new Worker(requesting, Location));
	}

	private bool IsOwnedBy(Civilization civilization)
	{
		return Owner == civilization;
	}
}
