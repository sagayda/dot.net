using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Unicode;
using DataModel;
using DataModel.Abstract;

namespace JsonSerialization;

public class TestSerialization
{
	private IEnumerable<Building> _buildings;
	private IEnumerable<Tenant> _tenants;
	private float _volume = 0.5f;

	private readonly JsonSerializerOptions _options = new()
	{
		WriteIndented = true,
		Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
	};

	public TestSerialization()
	{
		(_buildings, _tenants) = TestData.GetTestData(_volume);

		_options = new()
		{
			WriteIndented = true,
			Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
		};
	}

	public void Select()
	{
		string menu = "e - Exit\nv - Set test data volume\ng - Generate test data\n1 - Serialize\n2 - Deserialize\n3 - Serialize using JsonSerializer\n4 - Deserialize using JsonSerializer";

		while (true)
		{
			Console.Clear();
			System.Console.WriteLine(menu);
			System.Console.Write("\nOption: ");
			switch (Console.ReadLine())
			{
				case "e":
					return;
				case "v":
					ReadVolume();
					break;
				case "g":
					GenerateTestData();
					break;
				case "1":
					Serialize();
					System.Console.WriteLine("\nPress any key to continue...");
					System.Console.ReadLine();
					break;
				case "2":
					Deserialize();
					System.Console.WriteLine("\nPress any key to continue...");
					System.Console.ReadLine();
					break;
				case "3":
					SerializeViaSerializer();
					System.Console.WriteLine("\nPress any key to continue...");
					System.Console.ReadLine();
					break;
				case "4":
					DeserializeViaSerializer();
					System.Console.WriteLine("\nPress any key to continue...");
					System.Console.ReadLine();
					break;
				default:
					break;
			}
		}
	}

	public void Serialize()
	{
		LogSerialization(_buildings.ToList(), "Buildings");
		LogSerialization(_tenants.ToList(), "Tenants");
	}

	public void Deserialize()
	{
		LogDeserialization<Building>("Buildings");
		LogDeserialization<Tenant>("Tenants");
	}

	public void SerializeViaSerializer()
	{
		LogSerializationViaSerializer(_buildings.ToList(), "Buildings");
		LogSerializationViaSerializer(_tenants.ToList(), "Tenants");
	}

	public void DeserializeViaSerializer()
	{
		LogDeserializationViaSerializer<Building>("Buildings");
		LogDeserializationViaSerializer<Tenant>("Tenants");
	}

	private void LogDeserializationViaSerializer<T>(string name)
	{
		System.Console.WriteLine($"\nDeserialize: \u001b[1m{name}\n\u001b[0m");
		using var bReader = File.OpenRead($"{name}.json");

		try
		{
			List<T>? values = JsonSerializer.Deserialize<List<T>>(bReader, _options);

			foreach (var item in values ?? [])
			{
				System.Console.WriteLine(item);
			}
		}
		catch (System.Exception e)
		{
			System.Console.WriteLine($"\u001b[31m{e.Message}\u001b[0m");
		}
	}

	private void LogSerializationViaSerializer<T>(List<T> values, string name)
	{
		System.Console.WriteLine($"\nSerialize: \u001b[1m{name}\n\u001b[0m");

		using var stream = File.CreateText($"{name}.json");

		try
		{
			stream.Write(JsonSerializer.Serialize(values, _options));
			System.Console.WriteLine($"Successfully serialized items");
		}
		catch (System.Exception e)
		{
			System.Console.WriteLine($"\u001b[31m{e.Message}\u001b[0m");
		}
	}

	private void LogDeserialization<T>(string name) where T : IJsonSerializable
	{
		System.Console.WriteLine($"\nDeserialize: \u001b[1m{name}\n\u001b[0m");
		using var reader = File.OpenRead($"{name}.json");
		JsonArray? values = JsonArray.Parse(reader)?.AsArray();

		foreach (var item in values ?? [])
		{
			try
			{
				System.Console.WriteLine(T.Read(JsonSerializer.SerializeToElement(item)));
			}
			catch (System.Exception e)
			{
				System.Console.WriteLine($"\u001b[31m{e.Message}\u001b[0m");
			}
		}
	}

	private void LogSerialization<T>(List<T> values, string name) where T : IJsonSerializable
	{
		System.Console.WriteLine($"\nSerialize: \u001b[1m{name}\n\u001b[0m");

		JsonArray jsonValues = [];

		int failed = 0;

		foreach (var item in values)
		{
			try
			{
				jsonValues.Add(item.Write());
			}
			catch (System.Exception)
			{
				failed++;
			}
		}

		using var stream = File.CreateText($"{name}.json");
		stream.Write(JsonSerializer.Serialize(jsonValues, _options));

		System.Console.WriteLine($"Successfully serialized {jsonValues.Count} items");
		System.Console.WriteLine($"\u001b[31mFailed {failed} items\u001b[0m");
	}

	private void ReadVolume()
	{
		string answer = string.Empty;
		float volume;

		while (float.TryParse(answer, out volume) == false || volume < 0.2f || volume > 5f)
		{
			System.Console.Write("Volume: ");
			answer = System.Console.ReadLine() ?? string.Empty;
		}

		_volume = volume;
	}

	private void GenerateTestData()
	{
		Random random = new();
		TestData.Seed = random.Next();

		(_buildings, _tenants) = TestData.GetTestData(_volume);
	}
}
