using System.ComponentModel;

public enum SpellStateType
{
	[Description("None")]
	NONE,
	[Description("Birth")]
	BIRTH,
	[Description("Idle")]
	IDLE,
	[Description("Action")]
	ACTION,
	[Description("Cancel")]
	CANCEL,
	[Description("Death")]
	DEATH
}
