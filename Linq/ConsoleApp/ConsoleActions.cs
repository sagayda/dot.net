using System.Text.RegularExpressions;

namespace LINQ_to_objects;

public partial class ConsoleActions
{
	private const int _levelPadding = 5;
	private int _left;
	private int _top;

	public int Left
	{
		get
		{
			return _left;
		}
		private set
		{
			_left = value;
			Console.CursorLeft = _left;
		}
	}

	public int Top
	{
		get
		{
			return _top;
		}
		set
		{
			_top = value;
			Console.CursorTop = _top;
		}
	}

	public void NextLine()
	{
		Top++;
	}

	public void Print(string message)
	{
		System.Console.WriteLine(message.White());
	}

	public void PrintIEumerable<T>(IEnumerable<T>? values)
	{
		if (values is null)
		{
			LogInfo($"Sequence is null");
			return;
		}

		if (values.Any() == false)
		{
			LogInfo($"Sequence contains no elements");
			return;
		}

		int i = 0;

		LogInfo($"Type: {typeof(T).Name}\tElements: {values.Count()}");
		System.Console.WriteLine();
		foreach (var item in values)
		{
			System.Console.WriteLine($"#{i}:");
			System.Console.WriteLine(item);
			i++;
		}
		System.Console.WriteLine();
	}

	public void LogSucces(string message)
	{
		System.Console.WriteLine(message.Green().Bold());
	}

	public void LogError(string message)
	{
		System.Console.WriteLine(message.Red().Bold());
	}

	public void LogAlert(string message)
	{
		System.Console.WriteLine(message.Yellow().Bold());
	}

	public void LogAction(string message)
	{
		System.Console.WriteLine(message.Yellow().Italic());
	}

	public void LogInfo(string message)
	{
		System.Console.WriteLine(message.Gray());
	}

	public int SelectOption(string[] options)
	{
		Console.CursorVisible = false;

		System.Console.WriteLine(new string('\n', options.Length - 1));

		int top = Console.CursorTop - options.Length;
		// int left = _level;
		int left = Left;

		int current = 0;
		bool selected = false;

		while (selected == false)
		{
			Console.CursorTop = top;

			for (int i = 0; i < options.Length; i++)
			{
				Console.CursorLeft = left;

				if (i == current)
				{
					System.Console.WriteLine(options[i].Bold().Underline());
					continue;
				}

				System.Console.WriteLine(options[i].Faint());

			}

			var key = Console.ReadKey();

			switch (key.Key)
			{
				case ConsoleKey.UpArrow:
					current = current == 0 ? options.Length - 1 : --current;
					break;
				case ConsoleKey.DownArrow:
					current = current == options.Length - 1 ? 0 : ++current;
					break;
				case ConsoleKey.Enter:
					// Console.CursorVisible = true;
					// return current;
					selected = true;
					break;
				default:
					break;
			}
		}

		Console.CursorTop = top;
		for (int i = 0; i < options.Length; i++)
		{
			Console.CursorLeft = left;
			System.Console.WriteLine(new string(' ', options[i].Length));
		}

		Console.SetCursorPosition(left, top);
		LogAction($"Running '{options[current]}'...");
		return current;
	}

	public string Read(string message)
	{
		System.Console.Write((message + ": ").Faint());
		Console.ForegroundColor = ConsoleColor.Cyan;
		var res = Console.ReadLine();
		Console.ForegroundColor = ConsoleColor.White;

		return res is null ? string.Empty : res;
	}

	public IEnumerable<string> ReadSequence(string message)
	{
		System.Console.WriteLine();

		// Console.SetCursorPosition(_level, Console.CursorTop - 1);
		System.Console.Write((message + ": ").Faint());

		(int left, int top) = Console.GetCursorPosition();

		while (true)
		{
			Console.SetCursorPosition(left, top);
			System.Console.Write(new string(' ', Console.WindowWidth - left));
			Console.SetCursorPosition(left, top);
			Console.ForegroundColor = ConsoleColor.Cyan;
			var res = Console.ReadLine();
			Console.ForegroundColor = ConsoleColor.White;

			yield return res is null ? string.Empty : res;
		}
	}

	public int ReadInt(string message)
	{
		// Console.CursorLeft = _level;
		System.Console.Write((message + ": ").Faint());

		int left = Console.CursorLeft;
		int num;

		Console.ForegroundColor = ConsoleColor.Cyan;
		while (true)
		{
			if (int.TryParse(Console.ReadLine(), out num))
				break;

			Console.CursorTop--;
			Console.CursorLeft = left;
			System.Console.Write(new string(' ', Console.WindowWidth - left));
			Console.CursorLeft = left;
		}
		Console.ForegroundColor = ConsoleColor.White;

		return num;
	}

	public void IncreaseLevel()
	{
		// if (_level + _levelPadding >= Console.WindowWidth)
		// 	return;

		// _level += _levelPadding;
		// System.Console.WriteLine();
	}

	public void DecreaseLevel()
	{
		// if (_level - _levelPadding < 0)
		// 	return;

		// _level -= _levelPadding;
		// System.Console.WriteLine();
	}

	public partial class BufferArea
	{
		public (int Left, int Top) Start { get; }
		public (int Left, int Top) End { get; }
		
		private List<BufferArea> _subAreas = [];

		public BufferArea(int startLeft, int startTop, int endLeft, int endTop)
		{
			Start = (startLeft, startTop);
			End = (endLeft, endTop);
		}

		public BufferArea(int endLeft, int endTop)
		{
			Start = Console.GetCursorPosition();
			End = (endLeft, endTop);
		}
		
		public void AddSubArea(int startLeft, int startTop, int endLeft, int endTop)
		{
			if(startLeft < Start.Left || startTop < Start.Top || endLeft > End.Left || endTop > End.Top)
			{
				throw new ArgumentException("Error crateing subarea");
			}
			
			
		}
		
		public void Print(string str, int left, int top)
		{
			int width = End.Left - Start.Left;
			
			var strings = str.Split('\n').SelectMany(s => MyRegex().Split(s));
			
			foreach (var item in strings)
			{
				Console.CursorLeft = Start.Left;
				System.Console.WriteLine(item);
			}
		}

        [GeneratedRegex(@".{10}", RegexOptions.IgnoreCase | RegexOptions.Multiline, "ru-RU")]
        private static partial Regex MyRegex();
    }
}

