using System.Diagnostics;

namespace Civilization;

public abstract class Unit : ITickable
{
	public abstract string Name { get; }
	public Civilization Owner { get; private set; }
	public Race Race { get; }
	public UnitStats Stats { get; }
	public UnitType Type { get; }
	public Territory Location { get; set; }
	public UnitUnion? Union { get; set; }

	public int Health { get; private set; }
	public int Stamina { get; private set; }

	public Unit(Civilization owner, UnitStats stats, UnitType type)
	{
		Owner = owner;
		Stats = stats;
		Type = type;

		Race = owner.Race;
		Location = owner.Base;
	}

	public Unit(Civilization owner, UnitStats stats, UnitType type, Territory location)
	{
		Owner = owner;
		Stats = stats;
		Type = type;
		Location = location;

		Race = owner.Race;
	}

	protected abstract int GetDamage();

	public void SetOwner(Civilization civilization)
	{
		Owner = civilization;
	}

	public void Attack(Unit target)
	{
		if (Health <= 0)
			return;

		if (Stamina < 10)
			return;

		Stamina -= 10;
		target.Health -= GetDamage();
	}
	
	public void MoveTo(Territory territory)
	{
		Location.RemoveUnit(this);
		Location = territory;
		territory.AddUnit(this);
	}

	public virtual void Tick()
	{
		if (Health <= 0)
		{
			Debug.WriteLine("Tick for dead unit");
			return;
		}

		Health += Stats.Health / 100;
		Stamina += Stats.Stamina / 10;
	}
}
