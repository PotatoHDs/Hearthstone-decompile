using System.ComponentModel;

public enum ApplicationMode
{
	INVALID,
	[Description("Internal")]
	INTERNAL,
	[Description("Public")]
	PUBLIC
}
