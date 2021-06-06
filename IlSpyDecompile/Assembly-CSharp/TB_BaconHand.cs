using System.Collections;
using UnityEngine;

public class TB_BaconHand : MissionEntity
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static Map<GameEntityOption, string> s_stringOptions = InitStringOptions();

	private const int MISSION_EVENT_SHOP = 1;

	private const int MISSION_EVENT_COMBAT = 2;

	private Notification m_popup;

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.HANDLE_COIN,
			false
		} };
	}

	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	public TB_BaconHand()
	{
		m_gameOptions.AddOptions(s_booleanOptions, s_stringOptions);
		HistoryManager.Get().DisableHistory();
	}

	public override bool ShouldDoAlternateMulliganIntro()
	{
		return true;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		switch (missionEvent)
		{
		case 1:
			yield return ShowPopup("Shop");
			break;
		case 2:
			yield return ShowPopup("Combat");
			break;
		}
	}

	private IEnumerator ShowPopup(string text)
	{
		float seconds = 0f;
		float popupDuration = 1.5f;
		float popupScale = 2f;
		Vector3 popUpPos = new Vector3(0f, 0f, 4f);
		yield return new WaitForSeconds(seconds);
		m_popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popupScale, text, convertLegacyPosition: false);
		PlaySound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
		NotificationManager.Get().DestroyNotification(m_popup, popupDuration);
		DoBlur();
		yield return new WaitForSeconds(popupDuration);
		EndBlur();
	}

	private void DoBlur()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.SetBlurBrightness(1f);
		fullScreenFXMgr.SetBlurDesaturation(0f);
		fullScreenFXMgr.Vignette();
		fullScreenFXMgr.Blur(1f, 0.4f, iTween.EaseType.easeOutCirc);
	}

	public void EndBlur()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.StopVignette();
		fullScreenFXMgr.StopBlur();
	}
}
