using System.Text.Json;
using System.Text.Json.Nodes;

namespace JsonSerialization;

public interface IJsonSerializable
{
	public abstract static IJsonSerializable? Read(JsonElement element);
	public JsonNode Write();
}
