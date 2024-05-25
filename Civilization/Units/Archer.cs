namespace Civilization;

public class Archer : Unit
{
	private readonly static UnitStats _rawStats = new()
	{
		Health = 50,
		Speed = 50,
		Stamina = 0,
		Strength = 0,
		Precision = 100,
		WorkEfficiency = -100,
	};

	public override string Name => "Archer";

	public Archer(Civilization owner) : base(owner, _rawStats, UnitType.Warrior)
	{

	}

	public Archer(Civilization owner, Territory location) : base(owner, _rawStats, UnitType.Warrior, location)
	{

	}

	protected override int GetDamage()
	{
		return Stats.Precision;
	}
}
