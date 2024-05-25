namespace Civilization;

public class HumanCivilization : Civilization
{
	private readonly static CivilizationStats _rawStats = new()
	{
		Economy = 40,
		Industry = 60,
		Aggression = 50,
	};

	public override string Name => "Humans";

	public HumanCivilization(Territory territory) : base(_rawStats, new HumanRace(), territory)
	{
		
	}
}
