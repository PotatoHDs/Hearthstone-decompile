using bgs;
using Blizzard.Telemetry.Standard.Network;

public static class TelemetryUtil
{
	public const string GameConnectIdentifier = "GAME";

	public const string AuroraConnectIdentifier = "AURORA";

	public static Disconnect.Reason GetReasonFromBnetError(BattleNetErrors error)
	{
		switch (error)
		{
		case BattleNetErrors.ERROR_OK:
			return Disconnect.Reason.LOCAL;
		case BattleNetErrors.ERROR_TIMED_OUT:
		case BattleNetErrors.ERROR_LOGON_MODULE_TIMEOUT:
		case BattleNetErrors.ERROR_LOGON_WEB_VERIFY_TIMEOUT:
		case BattleNetErrors.ERROR_RPC_REQUEST_TIMED_OUT:
		case BattleNetErrors.ERROR_RPC_CONNECTION_TIMED_OUT:
			return Disconnect.Reason.TIMEOUT;
		default:
			return Disconnect.Reason.REMOTE;
		}
	}
}
