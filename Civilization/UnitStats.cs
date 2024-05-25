namespace Civilization;

public readonly struct UnitStats
{
	public readonly int Health { get; init; }
	public readonly int Speed { get; init; }
	public readonly int Stamina { get; init; }
	public readonly int Strength { get; init; }
	public readonly int Precision { get; init; }
	public readonly int WorkEfficiency { get; init; }

	public static UnitStats operator +(UnitStats a, UnitStats b)
	{
		return new UnitStats()
		{
			Health = a.Health + b.Health,
			Speed = a.Speed + b.Speed,
			Stamina = a.Stamina + b.Stamina,
			Strength = a.Strength + b.Strength,
			Precision = a.Precision + b.Precision,
			WorkEfficiency = a.WorkEfficiency + b.WorkEfficiency,
		};
	}
}
