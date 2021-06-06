using System;

// Token: 0x020008FC RID: 2300
public static class OptionUtils
{
	// Token: 0x06007FBB RID: 32699 RVA: 0x00297EE4 File Offset: 0x002960E4
	public static Option GetOptionFromString(string optionName)
	{
		if (string.IsNullOrEmpty(optionName))
		{
			return Option.INVALID;
		}
		object obj = Enum.Parse(typeof(Option), optionName, true);
		if (obj == null)
		{
			return Option.INVALID;
		}
		return (Option)obj;
	}
}
