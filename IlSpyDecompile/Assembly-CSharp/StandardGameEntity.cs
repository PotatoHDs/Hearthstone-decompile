public class StandardGameEntity : GameEntity
{
	public override void OnTagChanged(TagDelta change)
	{
		switch (change.tag)
		{
		case 198:
			if (change.newValue == 6)
			{
				if (GameState.Get().IsMulliganManagerActive())
				{
					GameState.Get().SetMulliganBusy(busy: true);
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
			break;
		case 19:
			if (change.newValue == 4)
			{
				MulliganManager.Get().BeginMulligan();
			}
			break;
		}
		base.OnTagChanged(change);
	}
}
