using System.Numerics;
using System.Text;

namespace UniversalCard;

public static class TestData
{
	private static Random _random = new(_seed);
	private static int _seed = 0;

	public static int Seed
	{
		get
		{
			return _seed;
		}

		set
		{
			_seed = value;
			_random = new(_seed);
		}
	}

	private static readonly string[] _firstNames =
["Андрій", "Олександр", "Максим", "Іван", "Дмитро",
	"Тетяна", "Олена", "Юлія", "Катерина", "Наталія",
	"Ігор", "Сергій", "Віталій", "Олексій", "Євген",
	"Анна", "Вікторія", "Ірина", "Оксана", "Світлана"];

	private static readonly string[] _lastNames =
	["Шевченко", "Петренко", "Іващенко", "Коваленко", "Григоренко",
	"Бойко", "Левченко", "Кравченко", "Пономаренко", "Смирнов",
	"Зайченко", "Степаненко", "Гончаренко", "Онищенко", "Ткаченко",
	"Мельник", "Яковенко", "Франчук", "Осадчук", "Василенко"];

	public static UniversalCard GetUniversalCard()
	{
		Person person = GetPerson();
		
		Passport passport = GetPassport(person);
		BankCard bankCard = GetBankCard(person);
		DriverLicense driverLicense = GetDriverLicense(person);
		StudentID studentID = GetStudentID(person);
		InsurancePolicy insurancePolicy = GetInsurancePolicy();
		
		UniversalCard universal = new(bankCard, passport, studentID, insurancePolicy, driverLicense);
		
		return universal;
	}
	
	public static Person GetPerson()
	{
		string name = _firstNames[_random.Next(0, _firstNames.Length)];
		string lastName = _lastNames[_random.Next(0, _lastNames.Length)];

		return new Person(name, lastName, (Sex)_random.Next(0, 2), GetDateTime(new DateTime(1900, 1, 1), DateTime.Now));
	}

	public static Passport GetPassport(Person holder)
	{
		DateTime issued = GetDateTime(new DateTime(2010, 1, 1), DateTime.Now);

		Passport passport = new(GetNumber(9), GetNumber(13), "UKR", issued, issued + new TimeSpan(3600, 0, 0, 0), holder);
		
		return passport;
	}
	
	public static BankCard GetBankCard(Person holder)
	{
		DateTime issued = GetDateTime(new DateTime(2015, 1, 1), DateTime.Now);
		
		BankCard bankcard = new("Privat Bank", GetNumber(4*4), GetNumber(3), issued + TimeSpan.FromDays(1800), holder);
		
		return bankcard;
	}
	
	public static DriverLicense GetDriverLicense(Person holder)
	{
		DateTime issued = GetDateTime(new DateTime(2000, 1, 1), DateTime.Now);
		
		DriverLicense driverlicense = new(GetNumber(8), "UKR", issued, issued + TimeSpan.FromDays(10000), holder);
		
		return driverlicense;
	}
	
	public static StudentID GetStudentID(Person holder)
	{
		DateTime issued = GetDateTime(new DateTime(2018, 1, 1), DateTime.Now);
		
		StudentID studentID = new(GetNumber(8), "KPI", "FIOT", issued, issued + TimeSpan.FromDays(1460), holder);
		
		return studentID;
	}
	
	public static InsurancePolicy GetInsurancePolicy()
	{
		DateTime issued = GetDateTime(new DateTime(2000, 1,1), DateTime.Now);
		
		InsurancePolicy insurancepolicy = new(GetNumber(8), "ProtectCorp", issued, issued + TimeSpan.FromDays(3600));
		
		return insurancepolicy;
	}

	private static DateTime GetDateTime(DateTime lowerBound, DateTime upperBound)
	{
		if (upperBound < lowerBound)
			throw new ArgumentException("Upper bound must be greater than or equal to lower bound.");

		TimeSpan timeSpan = upperBound - lowerBound;
		long ticks = (long)(timeSpan.Ticks * _random.NextDouble());
		return lowerBound.AddTicks(ticks);
	}

	private static string GetNumber(int length)
	{
		StringBuilder builder = new(length);

		for (int i = 0; i < length; i++)
		{
			builder.Append(_random.Next(0, 10));
		}

		return builder.ToString();
	}
}
