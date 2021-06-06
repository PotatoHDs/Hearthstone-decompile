using System.ComponentModel;

namespace bgs
{
	public enum LogLevel
	{
		[Description("None")]
		None,
		[Description("Debug")]
		Debug,
		[Description("Info")]
		Info,
		[Description("Warning")]
		Warning,
		[Description("Error")]
		Error,
		[Description("Exception")]
		Exception,
		[Description("Fatal")]
		Fatal
	}
}
