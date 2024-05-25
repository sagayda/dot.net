namespace Civilization;


public class Singleton<T> where T : class
{
	private static T? instance = null;

	public static T Instance
	{
		get
		{
			if (instance != null)
			{
				return instance;
			}
			else
			{
				throw new InvalidOperationException("Singleton instanse is not initialized");
			}
		}

		set
		{
			if(instance is not null && value != instance)
				throw new InvalidOperationException("Singleton instanse is already initialized");
			
			instance = value;
		}
	}
}