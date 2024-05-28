namespace UniversalCard;

public class Person
{
	public string FirstName { get; }
	public string LastName { get; }
	public Sex Sex { get; }
	public DateTime DateOfBirth { get; }

	public Person(string firstName, string lastName, Sex sex, DateTime dateOfBirth)
	{
		FirstName = firstName;
		LastName = lastName;
		Sex = sex;
		DateOfBirth = dateOfBirth;
	}

	public string GetFullName()
	{
		return $"{FirstName} {LastName}";
	}

	public int GetAge()
	{
		int age = DateTime.Now.Year - DateOfBirth.Year;
		
		if (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear)
			age--;

		return age;
	}
}

