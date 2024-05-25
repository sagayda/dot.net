namespace Civilization;

public abstract class Civilization : ITickable
{
	// private readonly List<Unit> _units = [];
	private readonly HashSet<Unit> _unassignedUnits = [];
	private readonly List<UnitGroup> _unitsGroups = [];
	private readonly Territory _base;
	private readonly ResourceWallet _resourceWallet = new();

	public IReadOnlyCollection<UnitGroup> UnitsGroups => _unitsGroups;
	public IReadOnlyList<Territory> Territories => Singleton<WorldMap>.Instance.GetTerritoriesFor(this);

	public abstract string Name { get; }

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

		_resourceWallet[new Wood()].Add(100);
		_resourceWallet[new Iron()].Add(100);
		_resourceWallet[new Fuel()].Add(100);
		_resourceWallet[new Food()].Add(100);
		_resourceWallet[new Civilians()].Add(50);

		_base.Capture(this);
	}

	public void Tick()
	{
		var population = _resourceWallet[new Civilians()];

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
		var unitFactories = Territories.SelectMany(t => t.Buildings).Where(b => b is IUnitFactory).Select(b => (IUnitFactory)b);
		//problem not unassign
		foreach (var unitFactory in unitFactories)
			foreach (var unit in unitFactory.TrainedUnits)
				_unassignedUnits.Add(unit);
	}

	public UnitGroup GroupUnassignedUnits()
	{
		UnitGroup group = new(_unassignedUnits);

		_unassignedUnits.Clear();
		_unitsGroups.Add(group);

		return group;
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
