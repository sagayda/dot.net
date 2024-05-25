namespace Civilization;

public class OrcRace : Race
{
	public override string Name => "Orc";

	public override UnitStats UnitStats { get; }

	public override RaceStats Stats { get; }

	public OrcRace()
	{
		UnitStats = new()
		{
			Health = 250,
			Speed = 60,
			Stamina = 100,
			Strength = 100,
			Precision = 10,
			WorkEfficiency = 40,
		};
		
		Stats = new()
		{
			ReproductionRate = 40,
		};
	}
}
