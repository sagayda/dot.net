namespace UniversalCard;

public class University
{
	public static void Visit(IStudentID id)
	{
		if(id.IsValid())
		{
			System.Console.WriteLine($"Вас пропустив охоронець {id.University} {id.Faculty}");
		}
		else
		{
			System.Console.WriteLine($"Вас не пропустили до університету {id.University} {id.Faculty}: студентський квиток не дійсний!");
		}
	}
}
