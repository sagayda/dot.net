namespace Civilization;

public abstract class Civilization : ITickable
{
	// private readonly List<Unit> _units = [];
	private readonly UnitList _unassignedUnits = [];
	private readonly List<UnitUnion> _unitUnions = [];
	private readonly Territory _base;
	private readonly ResourceWallet _resourceWallet = new();

	public IUnitEnumerable UnassignedUnits => _unassignedUnits;
	public IReadOnlyCollection<UnitUnion> UnitsUnions => _unitUnions;
	public IReadOnlyList<Territory> Territories => Singleton<WorldMap>.Instance.GetTerritoriesFor(this);
	public IReadOnlyList<Building> Buildings => Singleton<WorldMap>.Instance.GetBuildingsFor(this);

	public abstract string Name { get; }

	public ResourceWallet Wallet => _resourceWallet;
	public Territory Base => _base;
	public CivilizationStats Stats { get; }
	public Race Race { get; }

	public Civilization(CivilizationStats stats, Race race, Territory baseTerritory)
	{
		Stats = stats;
		Race = race;
		_base = baseTerritory;

		OnSpawn();
	}

	public void OnSpawn()
	{
		for (int i = 0; i < 20; i++)
			_unassignedUnits.Add(new Worker(this));

		for (int i = 0; i < 10; i++)
			_unassignedUnits.Add(new Swordsman(this));

		_resourceWallet[Resource.Wood].Add(100);
		_resourceWallet[Resource.Iron].Add(100);
		_resourceWallet[Resource.Fuel].Add(100);
		_resourceWallet[Resource.Food].Add(100);
		_resourceWallet[Resource.Civilians].Add(50);

		_base.Capture(this);
	}

	public void Tick()
	{
		var population = _resourceWallet[Resource.Civilians];

		population.Add((int)Math.Floor(population.Amount * Race.Stats.ReproductionRate));
	}

	public void CollectResources()
	{
		var resourceSources = Territories.Where(t => t is IResourceFactory).Select(t => (IResourceFactory)t);

		foreach (var source in resourceSources)
			_resourceWallet.Deposite(source.Storage);
	}

	public void CollectTrainedUnits()
	{
		var unitFactories = Buildings.Where(b => b is IUnitFactory).Select(b => (IUnitFactory)b);

		foreach (var unitFactory in unitFactories)
		{
			_unassignedUnits.Add(unitFactory.TrainedUnits);
			unitFactory.TrainedUnits.Clear();
		}
	}
	
	public UnitUnion CreateUnion(IUnitEnumerable units, string name)
	{
		var union = units.ToUnion(this);
		
		_unitUnions.Add(union);
		_unassignedUnits.Remove(union);
		
		union.Name = name;
		
		return union;
	}

	public bool TryBuildAccademy(Territory territory, out Academy academy)
	{
		academy = null;

		if (Territories.Contains(territory) == false)
			return false;

		var toBuild = new Academy(territory);

		if (_resourceWallet.CanSatisfy(toBuild.ResourcesToBuild) == false)
			return false;

		_resourceWallet.Satisfy(toBuild.ResourcesToBuild);
		territory.AddBuilding(toBuild);

		academy = toBuild;
		return true;
	}
}
