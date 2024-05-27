namespace Civilization;


public class Singleton<T> where T : class
{
	private static T? _instance = null;

	public static T Instance
	{
		get
		{
			if (_instance != null)
			{
				return _instance;
			}
			else
			{
				throw new InvalidOperationException("Singleton instanse is not initialized");
			}
		}

		set
		{
			if(_instance is not null && value != _instance)
				throw new InvalidOperationException("Singleton instanse is already initialized");
			
			_instance = value;
		}
	}
}