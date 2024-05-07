namespace LINQ_to_objects;

public static class StringExtensions
{
	#region Colors
	public static string White(this string str)
	{
		return "\u001b[38;5;15m" + str + "\u001b[39m";
	}

	public static string Green(this string str)
	{
		return "\u001b[38;5;40m" + str + "\u001b[39m";
	}

	public static string Red(this string str)
	{
		return "\u001b[38;5;9m" + str + "\u001b[39m";
	}

	public static string Yellow(this string str)
	{
		return "\u001b[38;5;11m" + str + "\u001b[39m";
	}

	public static string Gray(this string str)
	{
		return "\u001b[38;5;7m" + str + "\u001b[39m";
	}
	
	public static string Blue(this string str)
	{
		return "\u001b[38;5;27m" + str + "\u001b[39m";
	}
	#endregion

	#region  Styles
	public static string Bold(this string str)
	{
		return "\u001b[1m" + str + "\u001b[22m";
	}

	public static string Faint(this string str)
	{
		return "\u001b[2m" + str + "\u001b[22m";
	}

	public static string Italic(this string str)
	{
		return "\u001b[3m" + str + "\u001b[23m";
	}

	public static string Underline(this string str)
	{
		return "\u001b[4m" + str + "\u001b[24m";
	}

	public static string Blinking(this string str)
	{
		return "\u001b[5m" + str + "\u001b[25m";
	}

	public static string Inverse(this string str)
	{
		return "\u001b[7m" + str + "\u001b[27m";
	}

	public static string Strikethrough(this string str)
	{
		return "\u001b[9m" + str + "\u001b[29m";
	}
	#endregion
}