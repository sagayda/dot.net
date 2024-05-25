namespace Civilization;

public class Swordsman : Unit
{
	private readonly static UnitStats _rawStats = new()
	{
		Health = 200,
		Speed = -20,
		Stamina = -20,
		Strength = 100,
		Precision = -100,
		WorkEfficiency = -100,
	};

	public override string Name => "Swordsman";

	public Swordsman(Civilization owner) : base(owner, _rawStats, UnitType.Warrior)
	{

	}

	public Swordsman(Civilization owner, Territory location) : base(owner, _rawStats, UnitType.Warrior, location)
	{

	}


	protected override int GetDamage()
	{
		return Stats.Strength;
	}
}
