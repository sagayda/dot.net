namespace Civilization;

public class Worker : Unit
{
	private readonly static UnitStats _rawStats = new()
	{
		Health = 0,
		Speed = 10,
		Stamina = 0,
		Strength = -20,
		Precision = -20,
		WorkEfficiency = 100,
	};


	public override string Name => "Worker";

	public Worker(Civilization owner) : base(owner, _rawStats, UnitType.Civilian)
	{

	}

	public Worker(Civilization owner, Territory location) : base(owner, _rawStats, UnitType.Civilian, location)
	{

	}

	protected override int GetDamage()
	{
		return Stats.Strength / 2;
	}
}
