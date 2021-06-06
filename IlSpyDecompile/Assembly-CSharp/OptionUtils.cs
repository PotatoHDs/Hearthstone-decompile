using System;

public static class OptionUtils
{
	public static Option GetOptionFromString(string optionName)
	{
		if (string.IsNullOrEmpty(optionName))
		{
			return Option.INVALID;
		}
		object obj = Enum.Parse(typeof(Option), optionName, ignoreCase: true);
		if (obj == null)
		{
			return Option.INVALID;
		}
		return (Option)obj;
	}
}
