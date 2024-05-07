using System.Xml.Linq;
using System.Xml.Serialization;

namespace LINQ_to_objects;

public class Pair<T, K>
{
	public T First { get; private set; }
	public K Second { get; private set; }

	public Pair(T first, K second)
	{
		First = first;
		Second = second;
	}

	public override string ToString()
	{
		return $"{typeof(T).Name.Faint()}:\n{First}\n{typeof(K).Name.Faint()}:\n{Second}";
	}
}

public class XElementPair : Pair<XElement, XElement>
{
	public XElementPair(XElement first, XElement second) : base(new XElement(first), new XElement(second))
	{

	}

	public bool TryDeserialize<T, K>(out Pair<T?, K?>? result, bool forceRename = false)
	{
		result = null;

		if (forceRename)
		{
			First.Name = typeof(T).Name;
			Second.Name = typeof(K).Name;
		}

		using var readerFirst = First.CreateReader();
		XmlSerializer serializer = new(typeof(T));

		T? first;
		K? second;

		try
		{
			first = (T?)serializer.Deserialize(readerFirst);
		}
		catch (System.Exception)
		{
			return false;
		}

		using var readerSecond = Second.CreateReader();
		serializer = new(typeof(K));

		try
		{
			second = (K?)serializer.Deserialize(readerSecond);
		}
		catch (System.Exception)
		{
			return false;
		}

		result = new(first, second);
		return true;
	}
}
