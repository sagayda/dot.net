namespace Civilization;

public class UnitUnion : UnitGroup
{
	public Territory Location { get; private set; }

	public UnitUnion() : base()
	{
		UnitAdded += OnUnitAdded;
		UnitRemoved += OnUnitRemoved;

		Location = Singleton<WorldMap>.Instance.Territories[0];
	}

	public UnitUnion(Territory location) : base()
	{
		UnitAdded += OnUnitAdded;
		UnitRemoved += OnUnitRemoved;

		Location = location;
	}

	public UnitUnion(string name) : base(name)
	{
		UnitAdded += OnUnitAdded;
		UnitRemoved += OnUnitRemoved;

		Location = Singleton<WorldMap>.Instance.Territories[0];
	}

	public UnitUnion(string name, Territory location) : base(name)
	{
		UnitAdded += OnUnitAdded;
		UnitRemoved += OnUnitRemoved;

		Location = location;
	}

	private void OnUnitAdded(object? sender, UnitEnumerableEventArgs args)
	{
		args.Unit.Union = this;
		args.Unit.Location = Location;
	}

	private void OnUnitRemoved(object? sender, UnitEnumerableEventArgs args)
	{
		args.Unit.Union = null;
	}

	public void MoveTo(Territory location)
	{
		foreach (var unit in Units)
			unit.MoveTo(location);

		Location = location;
	}

}
