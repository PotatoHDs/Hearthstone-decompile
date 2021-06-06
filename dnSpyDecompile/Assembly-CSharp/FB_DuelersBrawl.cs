using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200059B RID: 1435
public class FB_DuelersBrawl : MissionEntity
{
	// Token: 0x06004F9A RID: 20378 RVA: 0x0010C851 File Offset: 0x0010AA51
	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.HANDLE_COIN,
				false
			}
		};
	}

	// Token: 0x06004F9B RID: 20379 RVA: 0x0010C860 File Offset: 0x0010AA60
	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	// Token: 0x06004F9C RID: 20380 RVA: 0x001A23BF File Offset: 0x001A05BF
	public FB_DuelersBrawl()
	{
		this.m_gameOptions.AddOptions(FB_DuelersBrawl.s_booleanOptions, FB_DuelersBrawl.s_stringOptions);
	}

	// Token: 0x06004F9D RID: 20381 RVA: 0x001A1DB5 File Offset: 0x0019FFB5
	public override void PreloadAssets()
	{
		base.PreloadSound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
	}

	// Token: 0x06004F9E RID: 20382 RVA: 0x000052EC File Offset: 0x000034EC
	public override bool ShouldDoAlternateMulliganIntro()
	{
		return true;
	}

	// Token: 0x06004F9F RID: 20383 RVA: 0x001A23DC File Offset: 0x001A05DC
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (missionEvent == 10)
		{
			if (GameState.Get().IsFriendlySidePlayerTurn())
			{
				TurnStartManager.Get().BeginListeningForTurnEvents(false);
			}
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(false);
		}
		else
		{
			Vector3 popUpPos = default(Vector3);
			if (GameState.Get().GetFriendlySidePlayer() != GameState.Get().GetCurrentPlayer())
			{
				popUpPos.z = (UniversalInputManager.UsePhoneUI ? 27f : 18f);
			}
			else
			{
				popUpPos.z = (UniversalInputManager.UsePhoneUI ? -18f : -12f);
				yield return new WaitForSeconds(3f);
			}
			yield return this.ShowPopup(GameStrings.Get(FB_DuelersBrawl.popupMsgs[missionEvent].Message), FB_DuelersBrawl.popupMsgs[missionEvent].Delay, popUpPos);
			popUpPos = default(Vector3);
		}
		yield break;
	}

	// Token: 0x06004FA0 RID: 20384 RVA: 0x001A23F2 File Offset: 0x001A05F2
	private IEnumerator ShowPopup(string stringID, float popupDuration, Vector3 popUpPos)
	{
		this.m_popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 1.4f, GameStrings.Get(stringID), false, NotificationManager.PopupTextType.FANCY);
		NotificationManager.Get().DestroyNotification(this.m_popup, popupDuration);
		yield return new WaitForSeconds(1f);
		yield break;
	}

	// Token: 0x040045BA RID: 17850
	private static Map<GameEntityOption, bool> s_booleanOptions = FB_DuelersBrawl.InitBooleanOptions();

	// Token: 0x040045BB RID: 17851
	private static Map<GameEntityOption, string> s_stringOptions = FB_DuelersBrawl.InitStringOptions();

	// Token: 0x040045BC RID: 17852
	private Notification m_popup;

	// Token: 0x040045BD RID: 17853
	private static readonly Dictionary<int, FB_DuelersBrawl.PopupMessage> popupMsgs = new Dictionary<int, FB_DuelersBrawl.PopupMessage>
	{
		{
			1,
			new FB_DuelersBrawl.PopupMessage
			{
				Message = "TB_DUELERSBRAWL_TAKE_PACES",
				Delay = 6f
			}
		},
		{
			2,
			new FB_DuelersBrawl.PopupMessage
			{
				Message = "TB_DUELERSBRAWL_SUDDEN_DEATH",
				Delay = 6f
			}
		}
	};

	// Token: 0x02001F69 RID: 8041
	public struct PopupMessage
	{
		// Token: 0x0400D8FD RID: 55549
		public string Message;

		// Token: 0x0400D8FE RID: 55550
		public float Delay;
	}
}
