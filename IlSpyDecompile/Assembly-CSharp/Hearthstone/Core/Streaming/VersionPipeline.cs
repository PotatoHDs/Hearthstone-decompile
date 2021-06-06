using System.ComponentModel;

namespace Hearthstone.Core.Streaming
{
	public enum VersionPipeline
	{
		[Description("Unknown")]
		UNKNOWN,
		[Description("Dev")]
		DEV,
		[Description("External")]
		EXTERNAL,
		[Description("RC")]
		RC,
		[Description("Live")]
		LIVE
	}
}
