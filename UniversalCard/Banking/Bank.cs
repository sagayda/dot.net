namespace UniversalCard;

public static class Bank
{
	public static void Visit(IBankCard card)
	{	
		
		System.Console.WriteLine($"Банк {card.Bank}");
		if(card.IsValid() == false)
		{
			System.Console.WriteLine($"Карта з платіжними даними '{card.GetPaymentDetails()}' не дійсна!");
		}
		else
		{
			System.Console.WriteLine($"Успішно виконано переказ за карти '{card.GetPaymentDetails()}'");
		}
	}
}
