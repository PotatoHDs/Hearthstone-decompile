using System;
using Assets;

// Token: 0x0200077A RID: 1914
public class RichPresence
{
	// Token: 0x04005780 RID: 22400
	public static readonly FourCC STATUS_STREAMID = new FourCC("stat");

	// Token: 0x04005781 RID: 22401
	public static readonly FourCC TUTORIAL_STREAMID = new FourCC("tut");

	// Token: 0x04005782 RID: 22402
	public static readonly FourCC SCENARIOS_STREAMID = new FourCC("scen");

	// Token: 0x04005783 RID: 22403
	public const uint FIELD_INDEX_START = 458752U;

	// Token: 0x04005784 RID: 22404
	public static readonly Map<Type, FourCC> s_streamIds = new Map<Type, FourCC>
	{
		{
			typeof(Global.PresenceStatus),
			RichPresence.STATUS_STREAMID
		},
		{
			typeof(PresenceTutorial),
			RichPresence.TUTORIAL_STREAMID
		},
		{
			typeof(ScenarioDbId),
			RichPresence.SCENARIOS_STREAMID
		}
	};
}
