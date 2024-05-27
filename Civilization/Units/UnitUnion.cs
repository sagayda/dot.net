namespace Civilization;

public class UnitUnion : UnitList
{
	public Territory Location { get; private set; }
	public Civilization Owner { get; }

	public UnitUnion(IEnumerable<Unit> units, Civilization owner) : base()
	{
		Location = owner.Base;
		Owner = owner;
		
		Add(units);
	}

	public UnitUnion(IEnumerable<Unit> units, Territory location, Civilization owner)
	{
		Location = location;
		Owner = owner;

		Add(units);
	}

	public UnitUnion(Civilization owner) : base()
	{
		Location = owner.Base;
		Owner = owner;
	}

	public UnitUnion(Territory location, Civilization owner) : base()
	{
		Location = location;
		Owner = owner;
	}

	// public UnitUnion(string name) : base(name)
	// {
	// 	Location = Singleton<WorldMap>.Instance.Territories[0];
	// }

	// public UnitUnion(string name, Territory location) : base(name)
	// {
	// 	Location = location;
	// }

	protected override void OnUnitAdded(Unit unit)
	{
		unit.Union = this;
		unit.MoveTo(Location);
	}

	protected override void OnUnitRemoved(Unit unit)
	{
		unit.Union = null;
	}

	public void MoveTo(Territory location)
	{
		foreach (var unit in Units)
			unit.MoveTo(location);

		Location = location;
	}

	public override UnitList ToList()
	{
		return new UnitList(Units, Name);
	}

	public override UnitUnion ToUnion(Civilization owner)
	{
		return this;
	}

}
