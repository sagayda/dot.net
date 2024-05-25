namespace Civilization;

public abstract class Race
{
	public abstract string Name { get; }
	public abstract UnitStats UnitStats { get; }
	public abstract RaceStats Stats { get; }
}