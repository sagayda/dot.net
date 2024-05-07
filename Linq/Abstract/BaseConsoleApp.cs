namespace LINQ_to_objects;

public abstract class BaseConsoleApp
{
	private const string EXIT_CODE = "ext";
	private Predicate<string> IsActionPredicate => (str) => str == EXIT_CODE || _actions.ContainsKey(str);

	private bool _initState;
	private Dictionary<string, string> _actionsDesc;
	private Dictionary<string, Action> _actions;

	protected abstract string Info { get; }

	public BaseConsoleApp()
	{
		_actions = new()
		{
			{ "hlp", ShowMenu },
			{ "clr", Console.Clear}
		};

		_actionsDesc = new()
		{
			{$"{EXIT_CODE}", "Exit"},
			{"hlp", "Help"},
			{"clr", "Clear console"}
		};
	}

	public void Start()
	{
		if (_initState == false)
			throw new InvalidOperationException();

		ShowMenu();

		string code;

		while (true)
		{
			System.Console.WriteLine();
			
			if(Info.Length != 0)
				LogInfo(Info);
			
			code = ReadAnswer("Action", IsActionPredicate);

			if (code == EXIT_CODE)
			{
				LogAction("Exiting...");
				break;
			}

			RunAction(code);
		}
	}

	protected void Init(Dictionary<string, Action> actions, Dictionary<string, string> actionsDesc)
	{
		foreach (var action in actions)
		{
			_actions.Add(action.Key, action.Value);
		}

		foreach (var item in actionsDesc)
		{
			_actionsDesc.Add(item.Key, item.Value);
		}

		_initState = true;
	}

	protected void InitN(string[] codes, string[] descs, Action[] actions)
	{
		if (codes.Length != descs.Length || codes.Length != actions.Length)
			throw new ArgumentException("Arrays must be the same length");

		int length = codes.Length;

		for (int i = 0; i < length; i++)
		{
			_actions.Add(codes[i], actions[i]);
		}

		for (int i = 0; i < length; i++)
		{
			_actionsDesc.Add(codes[i], descs[i]);
		}

		_initState = true;
	}

	protected string ReadAnswer(string question)
	{
		System.Console.Write($"{question}: ".Italic());
		Console.ForegroundColor = ConsoleColor.Cyan;
		var r = System.Console.ReadLine();
		Console.ForegroundColor = ConsoleColor.White;
		return r is null ? string.Empty : r;
	}

	protected string ReadAnswer(string question, Predicate<string> acceptionPredicate)
	{
		System.Console.WriteLine();
		Console.CursorTop--;
		
		System.Console.Write($"{question}: ".Italic());
		var (Left, Top) = Console.GetCursorPosition();

		string res = string.Empty;

		Console.ForegroundColor = ConsoleColor.Cyan;
		do
		{
			Console.SetCursorPosition(Left, Top);
			System.Console.Write(new string(' ', res.Length));
			Console.SetCursorPosition(Left, Top);
			res = Console.ReadLine() ?? string.Empty;
		} while (acceptionPredicate(res) == false);
		Console.ForegroundColor = ConsoleColor.White;

		return res;
	}

	protected int ReadVariant(string[] variants, string? question = null)
	{
		ArgumentNullException.ThrowIfNull(variants);

		if (variants.Length <= 1)
			throw new ArgumentException("Invalid variants count", nameof(variants));

		if (question is not null)
		{
			System.Console.WriteLine(question);
		}

		Console.CursorVisible = false;

		int left = Console.CursorLeft;

		System.Console.WriteLine(new string('\n', variants.Length - 1));
		int top = Console.CursorTop - variants.Length;

		int current = 0;
		bool selected = false;

		while (selected == false)
		{
			Console.CursorTop = top;

			for (int i = 0; i < variants.Length; i++)
			{
				Console.CursorLeft = left;

				if (i == current)
				{
					System.Console.WriteLine(variants[i].Bold().Underline());
					continue;
				}

				System.Console.WriteLine(variants[i].Faint());

			}

			var key = Console.ReadKey();

			switch (key.Key)
			{
				case ConsoleKey.UpArrow:
					current = current == 0 ? variants.Length - 1 : --current;
					break;
				case ConsoleKey.DownArrow:
					current = current == variants.Length - 1 ? 0 : ++current;
					break;
				case ConsoleKey.Enter:
					selected = true;
					break;
				default:
					break;
			}
		}

		Console.CursorVisible = true;
		return current;
	}

	protected int ReadVariantHorizontaly(string[] variants, string? question)
	{
		if (question is not null)
		{
			System.Console.Write(question);
		}

		ArgumentNullException.ThrowIfNull(variants);

		if (variants.Length <= 1)
			throw new ArgumentException("Invalid variants count", nameof(variants));

		Console.CursorVisible = false;
		int left = Console.CursorLeft;

		int current = 0;
		bool selected = false;
		string seperator = "    ";

		while (selected == false)
		{
			Console.CursorLeft = left;

			for (int i = 0; i < variants.Length; i++)
			{
				System.Console.Write(seperator);

				if (i == current)
				{
					System.Console.Write(variants[i].Bold().Underline());
					continue;
				}

				System.Console.Write(variants[i].Faint());
			}

			var key = Console.ReadKey();

			switch (key.Key)
			{
				case ConsoleKey.LeftArrow:
					current = current == 0 ? variants.Length - 1 : --current;
					break;
				case ConsoleKey.RightArrow:
					current = current == variants.Length - 1 ? 0 : ++current;
					break;
				case ConsoleKey.Enter:
					selected = true;
					break;
				default:
					break;
			}
		}

		System.Console.WriteLine();
		Console.CursorVisible = true;

		return current;
	}

	protected void LogError(string message)
	{
		System.Console.WriteLine($"Error: {message}".Red().Bold());
	}

	protected void LogSucces(string message)
	{
		System.Console.WriteLine(message.Green());
	}

	protected void LogTitle(string message)
	{
		Console.ForegroundColor = ConsoleColor.DarkYellow;
		System.Console.WriteLine();
		Console.WriteLine(new string('=', message.Length + 4));
		Console.CursorLeft = 2;
		System.Console.WriteLine(message);
		Console.WriteLine(new string('=', message.Length + 4));
		Console.ForegroundColor = ConsoleColor.White;
	}

	protected void LogAction(string message)
	{
		System.Console.WriteLine(message.Yellow().Italic());
	}

	protected void LogInfo(string message)
	{
		System.Console.WriteLine(message.Faint());
	}

	protected void LogSequence<T>(string message, IEnumerable<T> values)
	{
		LogInfo(message);
		
		int i =0;
		foreach (var item in values)
		{
			System.Console.Write($"#{i}: ".Blue());
			LogInfo(item?.ToString() ?? "Null");
			i++;
		}
	}
	
	private void ShowMenu()
	{
		string format = "{0,-12}{1}";

		System.Console.WriteLine();
		int i = 0;
		foreach (var item in _actionsDesc)
		{
			System.Console.WriteLine(format, item.Key, item.Value);

			if (i == 2)
				System.Console.WriteLine();

			i++;
		}
	}

	private void RunAction(string code)
	{
		_actions[code].Invoke();
	}

}

public abstract class BaseConsoleApp<T> : BaseConsoleApp
{
	public abstract T GetResult();
}