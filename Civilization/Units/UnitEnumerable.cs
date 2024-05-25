using System.Collections;

namespace Civilization;

public abstract class UnitEnumerable : IEnumerable<Unit>
{
	public EventHandler<UnitEnumerableEventArgs>? UnitRemoved;
	public EventHandler<UnitEnumerableEventArgs>? UnitAdded;

	protected HashSet<Unit> Units { get; } = [];

	public int Count => Units.Count;

	public UnitEnumerable()
	{

	}

	public UnitEnumerable(IEnumerable<Unit> units)
	{
		Add(units);
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

	public void Add(Unit unit)
	{
		Units.Add(unit);
		UnitAdded?.Invoke(this, new(unit));
	}

	public void Add(IEnumerable<Unit> units)
	{
		foreach (var unit in units)
			Add(unit);
	}

	public void Remove(Unit unit)
	{
		Units.Remove(unit);
		UnitRemoved?.Invoke(this, new(unit));
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

	public UnitSubgroup Select(UnitType type)
	{
		return new(Units.Where(u => u.Type == type), this);
	}

	public UnitSubgroup Select(int count)
	{
		return new(Units.Take(count), this);
	}

	public UnitSubgroup Select(Territory location)
	{
		return new(Units.Where(u => u.Location == location), this);
	}

	public UnitSubgroup Select(Predicate<Unit> predicate)
	{
		return new(Units.Where(u => predicate(u)), this);
	}

	public UnitUnion ToUnion(Territory location)
	{
		UnitUnion union = new(location)
		{
			Units
		};

		return union;
	}

	public UnitUnion ToUnion()
	{
		UnitUnion union = new()
		{
			Units
		};

		return union;
	}
}

public class UnitEnumerableEventArgs(Unit unit) : EventArgs
{
	public Unit Unit { get; set; } = unit;
}