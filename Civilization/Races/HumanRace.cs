namespace Civilization;

public class HumanRace : Race
{
	public override string Name => "Human";

	public override UnitStats UnitStats { get; }

	public override RaceStats Stats { get; }
	
	public HumanRace()
	{
		UnitStats = new()
		{
			Health = 100,
			Speed = 50,
			Stamina = 50,
			Strength = 50,
			Precision = 70,
			WorkEfficiency = 70,
		};
		
		Stats = new()
		{
			ReproductionRate = 0.7f,
		};
	}
}
