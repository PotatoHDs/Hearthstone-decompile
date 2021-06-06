using System;
using System.Collections;
using UnityEngine;

// Token: 0x020005A3 RID: 1443
public class TB_BaconHand : MissionEntity
{
	// Token: 0x06004FC8 RID: 20424 RVA: 0x0010C851 File Offset: 0x0010AA51
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

	// Token: 0x06004FC9 RID: 20425 RVA: 0x0010C860 File Offset: 0x0010AA60
	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	// Token: 0x06004FCA RID: 20426 RVA: 0x001A2DB5 File Offset: 0x001A0FB5
	public TB_BaconHand()
	{
		this.m_gameOptions.AddOptions(TB_BaconHand.s_booleanOptions, TB_BaconHand.s_stringOptions);
		HistoryManager.Get().DisableHistory();
	}

	// Token: 0x06004FCB RID: 20427 RVA: 0x000052EC File Offset: 0x000034EC
	public override bool ShouldDoAlternateMulliganIntro()
	{
		return true;
	}

	// Token: 0x06004FCC RID: 20428 RVA: 0x001A2DDC File Offset: 0x001A0FDC
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 1)
		{
			yield return this.ShowPopup("Shop");
		}
		else if (missionEvent == 2)
		{
			yield return this.ShowPopup("Combat");
		}
		yield break;
	}

	// Token: 0x06004FCD RID: 20429 RVA: 0x001A2DF2 File Offset: 0x001A0FF2
	private IEnumerator ShowPopup(string text)
	{
		float seconds = 0f;
		float popupDuration = 1.5f;
		float popupScale = 2f;
		Vector3 popUpPos = new Vector3(0f, 0f, 4f);
		yield return new WaitForSeconds(seconds);
		this.m_popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popupScale, text, false, NotificationManager.PopupTextType.BASIC);
		base.PlaySound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774", 1f, true, false);
		NotificationManager.Get().DestroyNotification(this.m_popup, popupDuration);
		this.DoBlur();
		yield return new WaitForSeconds(popupDuration);
		this.EndBlur();
		yield break;
	}

	// Token: 0x06004FCE RID: 20430 RVA: 0x0006BF0E File Offset: 0x0006A10E
	private void DoBlur()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.SetBlurBrightness(1f);
		fullScreenFXMgr.SetBlurDesaturation(0f);
		fullScreenFXMgr.Vignette();
		fullScreenFXMgr.Blur(1f, 0.4f, iTween.EaseType.easeOutCirc, null);
	}

	// Token: 0x06004FCF RID: 20431 RVA: 0x001A2E08 File Offset: 0x001A1008
	public void EndBlur()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.StopVignette();
		fullScreenFXMgr.StopBlur();
	}

	// Token: 0x040045DC RID: 17884
	private static Map<GameEntityOption, bool> s_booleanOptions = TB_BaconHand.InitBooleanOptions();

	// Token: 0x040045DD RID: 17885
	private static Map<GameEntityOption, string> s_stringOptions = TB_BaconHand.InitStringOptions();

	// Token: 0x040045DE RID: 17886
	private const int MISSION_EVENT_SHOP = 1;

	// Token: 0x040045DF RID: 17887
	private const int MISSION_EVENT_COMBAT = 2;

	// Token: 0x040045E0 RID: 17888
	private Notification m_popup;
}
