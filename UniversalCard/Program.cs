using UniversalCard;

internal class Program
{
	private static void Main(string[] args)
	{
		var card = TestData.GetUniversalCard();
		GoEverywhere(card);

		System.Console.WriteLine();
		System.Console.WriteLine();

		TestData.Seed = 1;
		card = TestData.GetUniversalCard();
		GoEverywhere(card);

		System.Console.WriteLine();
		System.Console.WriteLine();

		TestData.Seed = 2;
		card = TestData.GetUniversalCard();
		GoEverywhere(card);
	}

	private static void GoEverywhere(UniversalCard.UniversalCard card)
	{
		Bank.Visit(card);
		PoliseStation.Visit(card);
		University.Visit(card);
		Shop.Visit(card);
		CarAccident.Visit(card);
	}
}