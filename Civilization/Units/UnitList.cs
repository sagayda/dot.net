using System.Collections;

namespace Civilization;

public class UnitList : IUnitList
{
	protected HashSet<Unit> Units { get; } = [];

	public string Name { get; set; } = "Unnamed";

	public UnitList()
	{

	}

	public UnitList(IEnumerable<Unit> units)
	{
		Units = units.ToHashSet();

		foreach (var item in units)
			OnUnitAdded(item);
	}

	public UnitList(string name)
	{
		Name = name;
	}

	public UnitList(IEnumerable<Unit> units, string name)
	{
		Name = name;
		Units = units.ToHashSet();

		foreach (var item in units)
			OnUnitAdded(item);
	}

	protected virtual void OnUnitAdded(Unit unit)
	{

	}

	protected virtual void OnUnitRemoved(Unit unit)
	{

	}

	public IEnumerator<Unit> GetEnumerator()
	{
		return Units.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
	
	public void Clear()
	{
		Units.Clear();
	}

	public void Add(Unit unit)
	{
		Units.Add(unit);
		OnUnitAdded(unit);
	}

	public void Add(IEnumerable<Unit> units)
	{
		foreach (var unit in units)
			Add(unit);
	}

	public void Remove(Unit unit)
	{
		Units.Remove(unit);
		OnUnitRemoved(unit);
	}

	public void Remove(IEnumerable<Unit> units)
	{
		foreach (var unit in units)
			Remove(unit);
	}

	public void RemoveAt(Index index)
	{
		Remove(Units.ElementAt(index));
	}

	public UnitStats GetGroupStats()
	{
		UnitStats stats = new();

		foreach (var unit in Units)
			stats += unit.Stats;

		return stats;
	}

	public IUnitEnumerable Select(UnitType type)
	{
		return new UnitListPart(Units.Where(u => u.Type == type), this);
	}

	public IUnitEnumerable Select(int count)
	{
		return new UnitListPart(Units.Take(count), this);
	}

	public IUnitEnumerable Select(Territory location)
	{
		return new UnitListPart(Units.Where(u => u.Location == location), this);
	}

	public IUnitEnumerable Select(Predicate<Unit> predicate)
	{
		return new UnitListPart(Units.Where(u => predicate(u)), this);
	}

	public virtual UnitList ToList()
	{
		return this;
	}

	public virtual UnitUnion ToUnion(Civilization owner)
	{
		return new UnitUnion(this, owner);
	}

	public virtual UnitListPart AsPart()
	{
		return new UnitListPart(Units, this); ;
	}
}

// public class UnitGroup : IEnumerable<Unit>
// {
// 	protected HashSet<Unit> Units { get; } = [];

// 	public string Name { get; }
// 	public int Count => Units.Count;

// 	public UnitGroup()
// 	{
// 		Name = "Unnamed";
// 	}

// 	public UnitGroup(IEnumerable<Unit> units)
// 	{
// 		Name = "Unnamed";
// 		Add(units);
// 	}

// 	public UnitGroup(string name)
// 	{
// 		Name = name;
// 	}

// 	protected virtual void OnUnitAdded(Unit unit)
// 	{

// 	}

// 	protected virtual void OnUnitRemoved(Unit unit)
// 	{

// 	}

// 	public UnitStats GetGroupStats()
// 	{
// 		UnitStats stats = new();

// 		foreach (var unit in Units)
// 			stats += unit.Stats;

// 		return stats;
// 	}

// 	public IEnumerator<Unit> GetEnumerator()
// 	{
// 		return Units.GetEnumerator();
// 	}

// 	IEnumerator IEnumerable.GetEnumerator()
// 	{
// 		return GetEnumerator();
// 	}

// 	public void Add(Unit unit)
// 	{
// 		Units.Add(unit);

// 		OnUnitAdded(unit);
// 	}

// 	public void Add(IEnumerable<Unit> units)
// 	{
// 		foreach (var unit in units)
// 			Add(unit);
// 	}

// 	public void Remove(Unit unit)
// 	{
// 		Units.Remove(unit);
// 		OnUnitRemoved(unit);
// 	}

// 	public void Remove(IEnumerable<Unit> units)
// 	{
// 		foreach (var unit in units)
// 			Remove(unit);
// 	}

// 	public void RemoveAt(Index index)
// 	{
// 		var toRemove = Units.ElementAt(index);

// 		Units.Remove(toRemove);
// 		OnUnitRemoved(toRemove);
// 	}

// 	public UnitGroup Select(UnitType type)
// 	{
// 		return new(Units.Where(u => u.Type == type));
// 	}

// 	public UnitGroup Select(int count)
// 	{
// 		return new(Units.Take(count));
// 	}

// 	public UnitGroup Select(Territory location)
// 	{
// 		return new(Units.Where(u => u.Location == location));
// 	}

// 	public UnitGroup Select(Predicate<Unit> predicate)
// 	{
// 		return new(Units.Where(u => predicate(u)));
// 	}

// 	public UnitUnion ToUnion(Territory location)
// 	{
// 		UnitUnion union = new(location)
// 		{
// 			Units
// 		};

// 		return union;
// 	}

// 	public UnitUnion ToUnion()
// 	{
// 		UnitUnion union = new()
// 		{
// 			Units
// 		};

// 		return union;
// 	}
// }
