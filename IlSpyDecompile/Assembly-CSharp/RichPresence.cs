using System;
using Assets;

public class RichPresence
{
	public static readonly FourCC STATUS_STREAMID = new FourCC("stat");

	public static readonly FourCC TUTORIAL_STREAMID = new FourCC("tut");

	public static readonly FourCC SCENARIOS_STREAMID = new FourCC("scen");

	public const uint FIELD_INDEX_START = 458752u;

	public static readonly Map<Type, FourCC> s_streamIds = new Map<Type, FourCC>
	{
		{
			typeof(Global.PresenceStatus),
			STATUS_STREAMID
		},
		{
			typeof(PresenceTutorial),
			TUTORIAL_STREAMID
		},
		{
			typeof(ScenarioDbId),
			SCENARIOS_STREAMID
		}
	};
}
