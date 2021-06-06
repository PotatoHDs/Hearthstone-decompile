using System;
using bgs;
using Blizzard.Telemetry.Standard.Network;

// Token: 0x02000930 RID: 2352
public static class TelemetryUtil
{
	// Token: 0x060081DD RID: 33245 RVA: 0x002A4244 File Offset: 0x002A2444
	public static Disconnect.Reason GetReasonFromBnetError(BattleNetErrors error)
	{
		if (error <= BattleNetErrors.ERROR_TIMED_OUT)
		{
			if (error == BattleNetErrors.ERROR_OK)
			{
				return Disconnect.Reason.LOCAL;
			}
			if (error != BattleNetErrors.ERROR_TIMED_OUT)
			{
				return Disconnect.Reason.REMOTE;
			}
		}
		else if (error != BattleNetErrors.ERROR_LOGON_MODULE_TIMEOUT && error != BattleNetErrors.ERROR_LOGON_WEB_VERIFY_TIMEOUT && error - BattleNetErrors.ERROR_RPC_REQUEST_TIMED_OUT > 1U)
		{
			return Disconnect.Reason.REMOTE;
		}
		return Disconnect.Reason.TIMEOUT;
	}

	// Token: 0x04006CF2 RID: 27890
	public const string GameConnectIdentifier = "GAME";

	// Token: 0x04006CF3 RID: 27891
	public const string AuroraConnectIdentifier = "AURORA";
}
