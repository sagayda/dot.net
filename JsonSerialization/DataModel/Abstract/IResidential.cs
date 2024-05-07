using System.Text.Json.Serialization;

namespace DataModel.Abstract;

public interface IResidential
{
	public float TotalArea { get; }
	public float EffectiveArea { get; }
	public int RoomsCount { get; }
	public int FloorsCount { get; }
}
