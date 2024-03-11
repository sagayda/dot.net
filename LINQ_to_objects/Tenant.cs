using System.Collections;

namespace LINQ_to_objects;

public class Tenant
{
	public Tenant(string name,
					string lastname,
					string middlename,
					int familyMemberCount,
					int childrenCount,
					bool debt)
	{
		Name = name;
		Lastname = lastname;
		Middlename = middlename;
		FamilyMembersCount = familyMemberCount;
		ChildrenCount = childrenCount;
		Debt = debt;
	}
	
	public string Name {get;}
	public string Lastname {get; }
	public string Middlename {get; }
	public int FamilyMembersCount {get; set;}
	public int ChildrenCount {get; set;}
	public bool Debt {get; set;} = false;
	public Address? Address {get; private set;}
	public DateOnly? RegistrationDate {get; private set; }

	public override string ToString()
	{
		return $"{Lastname} {Name} {Middlename}\tFamily: {FamilyMembersCount}[m] {ChildrenCount}[c]\tDebt: {Debt}\tAddress: {Address}\tRegistration date: {RegistrationDate}";
	}
	
	public void SetRegistration (Address address, DateOnly date)
	{
		Address = address;
		RegistrationDate = date;
	}
}
