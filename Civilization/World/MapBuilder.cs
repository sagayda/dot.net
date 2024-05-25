namespace Civilization;

public class MapBuilder
{
	public int Size { get; private set; }

	public float DeadlandsVolume { get; private set; }
	public float ForestVolume { get; private set; }
	public float PlainsVolume { get; private set; }

	public MapBuilder SetSize(int size)
	{
		if (size <= 0)
			throw new ArgumentOutOfRangeException(nameof(size), "Size of map must be greater than 0");

		Size = size;
		return this;
	}

	public MapBuilder SetDeadlandsVolume(float volume)
	{
		ArgumentOutOfRangeException.ThrowIfNegative(volume, "Territory volume must be equals or greater than 0");

		DeadlandsVolume = volume;
		return this;
	}

	public MapBuilder SetForestVolume(float volume)
	{
		ArgumentOutOfRangeException.ThrowIfNegative(volume, "Territory volume must be equals or greater than 0");

		ForestVolume = volume;
		return this;
	}

	public MapBuilder SetPlainsVolume(float volume)
	{
		ArgumentOutOfRangeException.ThrowIfNegative(volume, "Territory volume must be equals or greater than 0");

		PlainsVolume = volume;
		return this;
	}

	public WorldMap Build()
	{
		float overallVolume = DeadlandsVolume + ForestVolume + PlainsVolume;
		float volumePerTerritory = overallVolume / Size;

		int forests = (int)Math.Floor(ForestVolume / volumePerTerritory);
		int plains = (int)Math.Floor(PlainsVolume / volumePerTerritory);
		int deadlands = Size - forests - plains;

		return new WorldMap([.. CreateDeadlands(deadlands), .. CreateForests(forests), .. CreatePlains(plains)]);
	}

	private static IEnumerable<Forest> CreateForests(int count)
	{
		for (int i = 0; i < count; i++)
			yield return new Forest();
	}

	private static IEnumerable<Plains> CreatePlains(int count)
	{
		for (int i = 0; i < count; i++)
			yield return new Plains();
	}

	private static IEnumerable<Deadlands> CreateDeadlands(int count)
	{
		for (int i = 0; i < count; i++)
			yield return new Deadlands();
	}
}
