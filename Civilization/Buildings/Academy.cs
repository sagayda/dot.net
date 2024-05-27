using System.Collections.ObjectModel;

namespace Civilization;

public class Academy : Building, IUnitFactory
{
	public Civilization? Owner => Location.Owner;
	public override string Name => "Academy";

	public override ReadOnlyCollection<ResourceRequest> ResourcesToBuild { get; }
	public ReadOnlyCollection<ResourceRequest> ResourcesPerUnit { get; }

	public UnitList TrainedUnits {get; private set;}

	public Academy(Territory territory) : base(territory)
	{
		TrainedUnits = [];
		
		ResourcesToBuild = new ReadOnlyCollection<ResourceRequest>(
		[
			new(Resource.Iron, 20),
			new(Resource.Wood, 50)
		]);

		ResourcesPerUnit = new ReadOnlyCollection<ResourceRequest>(
		[
			new(Resource.Civilians, 1),
			new(Resource.Food, 3)
		]);
	}

	public void Create(ResourceWallet wallet, Civilization requesting)
	{
		if(IsOwnedBy(requesting) == false)
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
