using System.Collections;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace LINQ_to_objects;

public class Tenant : IEnumerable<Registration>, IXmlSerializable
{
	private readonly List<Registration> _registrations = [];

	public Tenant()
	{
		Name = string.Empty;
		Middlename = string.Empty;
		Lastname = string.Empty;
	}

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

	public string Name { get; internal set; }
	public string Lastname { get; internal set; }
	public string Middlename { get; internal set; }
	public int FamilyMembersCount { get; set; }
	public int ChildrenCount { get; set; }
	public bool Debt { get; set; }

	public override string ToString()
	{
		return $"{Lastname} {Name} {Middlename}\tFamily: {FamilyMembersCount}[m] {ChildrenCount}[c]\tDebt: {Debt}\tRegistrations: {_registrations.Count}";
	}

	public void Add(Registration registration)
	{
		_registrations.Add(registration);
	}

	public void Remove(Registration registration)
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

	public XmlSchema? GetSchema()
	{
		return null;
	}

	public void ReadXml(XmlReader reader)
	{
		reader.ReadStartElement();
		while (reader.NodeType != XmlNodeType.EndElement)
		{
			switch (reader.Name)
			{
				case "Name":
					Name = reader.ReadElementContentAsString();
					break;
				case "Middlename":
					Middlename = reader.ReadElementContentAsString();
					break;
				case "Lastname":
					Lastname = reader.ReadElementContentAsString();
					break;
				case "FamilyMembersCount":
					FamilyMembersCount = reader.ReadElementContentAsInt();
					break;
				case "ChildrenCount":
					ChildrenCount = reader.ReadElementContentAsInt();
					break;
				case "Debt":
					Debt = reader.ReadElementContentAsBoolean();
					break;
				case "Registrations":
					reader.ReadStartElement();
					while (reader.NodeType != XmlNodeType.EndElement)
					{
						Registration registration = new();
						registration.ReadXml(reader);
						_registrations.Add(registration);
					}
					reader.ReadEndElement();
					break;
				default:
					reader.Read();
					break;
			}
		}
		reader.ReadEndElement();
	}

	public void WriteXml(XmlWriter writer)
	{
		writer.WriteAttributeString("Namespace", GetType().Namespace);
		writer.WriteAttributeString("Type", GetType().Name);
		writer.WriteElementString("Name", Name);
		writer.WriteElementString("Middlename", Middlename);
		writer.WriteElementString("Lastname", Lastname);
		writer.WriteElementString("FamilyMembersCount", FamilyMembersCount.ToString());
		writer.WriteElementString("ChildrenCount", ChildrenCount.ToString());

		writer.WriteStartElement("Debt");
		writer.WriteValue(Debt);
		writer.WriteEndElement();

		if (_registrations.Count != 0)
		{
			writer.WriteStartElement("Registrations");
			foreach (var item in _registrations)
			{
				writer.WriteStartElement("Registration");
				item.WriteXml(writer);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}
	}
}
