using System.Collections;

namespace Civilization;

public class UnitListPart : IUnitEnumerable
{
	public IUnitList Parent { get; }

	protected IEnumerable<Unit> Units { get; private set; }

	public UnitListPart(IEnumerable<Unit> units, IUnitList parent)
	{
		Parent = parent;
		Units = units;
	}

	public UnitStats GetGroupStats()
	{
		UnitStats stats = new();

		foreach (var unit in Units)
			stats += unit.Stats;

		return stats;
	}

	public IEnumerator<Unit> GetEnumerator()
	{
		return Units.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public IUnitEnumerable Select(UnitType type)
	{
		return new UnitListPart(Units.Where(u => u.Type == type), Parent);
	}

	public IUnitEnumerable Select(int count)
	{
		return new UnitListPart(Units.Take(count), Parent);
	}

	public IUnitEnumerable Select(Territory location)
	{
		return new UnitListPart(Units.Where(u => u.Location == location), Parent);
	}

	public IUnitEnumerable Select(Predicate<Unit> predicate)
	{
		return new UnitListPart(Units.Where(u => predicate(u)), Parent);
	}

	public UnitListPart Crop()
	{
		Units = Units.ToList();
		
		foreach (var selected in Units)
			Parent.Remove(selected);
		
		return this;
	}

	public UnitList ToList()
	{
		return new UnitList(Units);
	}

	public UnitUnion ToUnion(Civilization owner)
	{
		return new UnitUnion(this, owner);
	}

	public UnitListPart AsPart()
	{
		return this;
	}
}