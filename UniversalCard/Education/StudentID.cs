namespace UniversalCard;

public class StudentID : IStudentID
{
	public string Number { get; }
	public string University { get; }
	public string Faculty { get; }
	public DateTime IssueDate { get; }
	public DateTime ExpiryDate { get; }
	public Person Holder { get; }

	public StudentID(string number, string university, string faculty, DateTime issueDate, DateTime expiryDate, Person holder)
	{
		Number = number;
		University = university;
		Faculty = faculty;
		IssueDate = issueDate;
		ExpiryDate = expiryDate;
		Holder = holder;
	}

	public bool IsValid()
	{
		return DateTime.Now < ExpiryDate;
	}
}

