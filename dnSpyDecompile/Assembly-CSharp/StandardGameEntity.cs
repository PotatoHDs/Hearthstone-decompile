using System;

// Token: 0x02000598 RID: 1432
public class StandardGameEntity : GameEntity
{
	// Token: 0x06004F8C RID: 20364 RVA: 0x001A1B68 File Offset: 0x0019FD68
	public override void OnTagChanged(TagDelta change)
	{
		GAME_TAG tag = (GAME_TAG)change.tag;
		if (tag != GAME_TAG.STEP)
		{
			if (tag == GAME_TAG.NEXT_STEP)
			{
				if (change.newValue == 6)
				{
					if (GameState.Get().IsMulliganManagerActive())
					{
						GameState.Get().SetMulliganBusy(true);
					}
				}
				else if (change.oldValue == 9 && change.newValue == 10)
				{
					GameState.Get().GetTimeTracker().ResetAccruedLostTime();
					if (GameState.Get().IsLocalSidePlayerTurn())
					{
						TurnStartManager.Get().BeginPlayingTurnEvents();
					}
				}
			}
		}
		else if (change.newValue == 4)
		{
			MulliganManager.Get().BeginMulligan();
		}
		base.OnTagChanged(change);
	}
}
