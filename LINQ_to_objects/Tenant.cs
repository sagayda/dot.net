using System.Collections;

namespace LINQ_to_objects;

public class Tenant(string name,
				string lastname,
				string middlename,
				int familyMemberCount,
				int childrenCount,
				bool debt) : IEnumerable<Registration>
{
	private readonly List<Registration> _registrations = [];
	public string Name { get; } = name;
	public string Lastname { get; } = lastname;
	public string Middlename { get; } = middlename;
	public int FamilyMembersCount { get; set; } = familyMemberCount;
	public int ChildrenCount { get; set; } = childrenCount;
	public bool Debt { get; set; } = debt;

	public override string ToString()
	{
		return $"{Lastname} {Name} {Middlename}\tFamily: {FamilyMembersCount}[m] {ChildrenCount}[c]\tDebt: {Debt}\tRegistrations: {_registrations.Count}";
	}
	
	public void AddRegistration (Registration registration)
	{
		_registrations.Add(registration);
	}
	
	public void RemoveRegistration (Registration registration)
	{
		_registrations.Remove(registration);
	}

	public IEnumerator<Registration> GetEnumerator()
	{
		return _registrations.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
