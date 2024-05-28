namespace UniversalCard;

public static class CarAccident
{
	public static void Visit(IInsurancePolicy insurance)
	{
		if(insurance.IsValid())
		{
			System.Console.WriteLine($"Після аварії ваше авто відремонтувало страхове агенстство {insurance.Provider}");
		}
		else
		{
			System.Console.WriteLine("Вам прийшлось ремонтувати авто власними коштами");
		}
	}
}
