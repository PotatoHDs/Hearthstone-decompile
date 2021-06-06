using System;
using System.ComponentModel;
using UnityEngine;

// Token: 0x0200009D RID: 157
public class MobileChatNotification : MonoBehaviour
{
	// Token: 0x14000010 RID: 16
	// (add) Token: 0x060009CC RID: 2508 RVA: 0x00038448 File Offset: 0x00036648
	// (remove) Token: 0x060009CD RID: 2509 RVA: 0x00038480 File Offset: 0x00036680
	public event MobileChatNotification.NotifiedEvent Notified;

	// Token: 0x060009CE RID: 2510 RVA: 0x000384B5 File Offset: 0x000366B5
	private void OnEnable()
	{
		this.state = MobileChatNotification.State.None;
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x000384C0 File Offset: 0x000366C0
	private void OnDestroy()
	{
		if (GameState.Get() != null && !SpectatorManager.Get().IsSpectatingOrWatching)
		{
			GameState.Get().UnregisterTurnChangedListener(new GameState.TurnChangedCallback(this.OnTurnChanged));
			GameState.Get().UnregisterTurnTimerUpdateListener(new GameState.TurnTimerUpdateCallback(this.OnTurnTimerUpdate));
		}
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x00038510 File Offset: 0x00036710
	private void Update()
	{
		if (GameState.Get() == null || SpectatorManager.Get().IsSpectatingOrWatching)
		{
			this.state = MobileChatNotification.State.None;
			return;
		}
		GameState.Get().RegisterTurnChangedListener(new GameState.TurnChangedCallback(this.OnTurnChanged));
		GameState.Get().RegisterTurnTimerUpdateListener(new GameState.TurnTimerUpdateCallback(this.OnTurnTimerUpdate));
		if (GameState.Get().IsMulliganPhase())
		{
			if (this.state == MobileChatNotification.State.None)
			{
				this.state = MobileChatNotification.State.GameStarted;
				this.FireNotification();
			}
			return;
		}
		if (this.state == MobileChatNotification.State.GameStarted)
		{
			this.state = (GameState.Get().IsFriendlySidePlayerTurn() ? MobileChatNotification.State.YourTurn : MobileChatNotification.State.None);
			this.FireNotification();
		}
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x000385AD File Offset: 0x000367AD
	private string GetStateText(MobileChatNotification.State state)
	{
		if (state == MobileChatNotification.State.None)
		{
			return string.Empty;
		}
		return GameStrings.Get((typeof(MobileChatNotification.State).GetField(state.ToString()).GetCustomAttributes(false)[0] as DescriptionAttribute).Description);
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x000385EB File Offset: 0x000367EB
	private void OnTurnChanged(int oldTurn, int newTurn, object userData)
	{
		if (GameState.Get().IsFriendlySidePlayerTurn())
		{
			this.state = MobileChatNotification.State.YourTurn;
			this.FireNotification();
		}
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x00038606 File Offset: 0x00036806
	private void OnTurnTimerUpdate(TurnTimerUpdate update, object userData)
	{
		if (update.GetSecondsRemaining() > 0f && GameState.Get().IsFriendlySidePlayerTurn())
		{
			this.state = MobileChatNotification.State.TurnCountdown;
			this.FireNotification();
		}
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x0003862E File Offset: 0x0003682E
	private void FireNotification()
	{
		if (this.Notified != null && this.state != MobileChatNotification.State.None)
		{
			this.Notified(this.GetStateText(this.state));
		}
	}

	// Token: 0x04000674 RID: 1652
	private MobileChatNotification.State state;

	// Token: 0x020013A3 RID: 5027
	// (Invoke) Token: 0x0600D819 RID: 55321
	public delegate void NotifiedEvent(string text);

	// Token: 0x020013A4 RID: 5028
	private enum State
	{
		// Token: 0x0400A748 RID: 42824
		None,
		// Token: 0x0400A749 RID: 42825
		[Description("GLOBAL_MOBILECHAT_NOTIFICATION_MULLIGAIN")]
		GameStarted,
		// Token: 0x0400A74A RID: 42826
		[Description("GLOBAL_MOBILECHAT_NOTIFICATION_YOUR_TURN")]
		YourTurn,
		// Token: 0x0400A74B RID: 42827
		[Description("GLOBAL_MOBILECHAT_NOTIFICATION_TURN_COUNTDOWN")]
		TurnCountdown
	}
}
