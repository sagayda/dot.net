using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Civilization;

public abstract class Territory : ITickable
{
	private readonly List<Unit> _units = [];
	private readonly List<Building> _building = [];

	public abstract string Name { get; }
	public TerritoryStats Stats { get; protected set; }
	public ReadOnlyCollection<Unit> Units => _units.AsReadOnly();
	public ReadOnlyCollection<Building> Buildings => _building.AsReadOnly();
	public Civilization? Owner { get; private set; }
	
	public void Capture(Civilization civilization)
	{
		if(Owner == civilization)
			Debug.WriteLine($"Territory {Name} was recaptured by {civilization.Name}");
		
		Owner = civilization;
	}
	
	public void AddBuilding(Building building)
	{
		if(building.Location != this)
			throw new ArgumentException();
		
		_building.Add(building);
	}
	
	public void AddUnit(Unit unit)
	{
		_units.Add(unit);
	}
	
	public void RemoveUnit(Unit unit)
	{
		_units.Remove(unit);
	}

	public virtual void Tick()
	{
		
	}
}
