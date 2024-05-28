namespace UniversalCard;

public static class Shop
{
	public static void Visit(IPassport passport)
	{
		if(passport.IsValid() == false)
		{
			System.Console.WriteLine("Вам не продали алкоголь, адже термін дії вашого паспотру вичерпано!");
		}
		else if(passport.GetHashCode() < 18)
		{
			System.Console.WriteLine("Вам не продали алкоголь, адже вам менше 18 років!");
		}
		else
		{
			System.Console.WriteLine("Ви успішно придбали алкоголь!");
		}
	}
}
