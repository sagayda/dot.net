namespace UniversalCard;

public interface IStudentID
{
	public string University { get; }
	public string Faculty { get; }
	public bool IsValid();

}
