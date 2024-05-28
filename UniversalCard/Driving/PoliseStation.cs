namespace UniversalCard;

public static class PoliseStation
{
	public static void Visit(IDriverLicense license)
	{
		if(license.IsValid())
		{
			System.Console.WriteLine("Ваше водійське посвідчення актуальне!");
		}
		else
		{
			System.Console.WriteLine("Ваше водійське посвідчення не актуальне!");
		}
	}
}
