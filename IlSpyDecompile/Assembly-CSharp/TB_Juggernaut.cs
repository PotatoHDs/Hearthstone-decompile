using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TB_Juggernaut : MissionEntity
{
	private Notification m_popup;

	private Vector3 popUpPos;

	private string text;

	private bool doPopup;

	private int HumanHeroClass;

	private int AIHeroClass;

	private float popupDuration = 7f;

	private float popupScale = 2.5f;

	private float popupDelay;

	private static readonly Dictionary<int, string> minionMsgs = new Dictionary<int, string>
	{
		{ 0, "FB_JUGGERNAUT_UNKNOWN" },
		{ 1, "FB_JUGGERNAUT_UNKNOWN" },
		{ 2, "FB_JUGGERNAUT_DRUID" },
		{ 3, "FB_JUGGERNAUT_HUNTER" },
		{ 4, "FB_JUGGERNAUT_MAGE" },
		{ 5, "FB_JUGGERNAUT_PALADIN" },
		{ 6, "FB_JUGGERNAUT_PRIEST" },
		{ 7, "FB_JUGGERNAUT_ROGUE" },
		{ 8, "FB_JUGGERNAUT_SHAMAN" },
		{ 9, "FB_JUGGERNAUT_WARLOCK" },
		{ 10, "FB_JUGGERNAUT_WARRIOR" }
	};

	public override void PreloadAssets()
	{
		PreloadSound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		doPopup = false;
		if (missionEvent == 1)
		{
			doPopup = true;
			popupScale = 1.85f;
			text = GameStrings.Get("FB_JUGGERNAUT_CHOOSE_OPPONENT");
			popupDuration = 3f;
			popupDelay = 3f;
			popUpPos.x = 0f;
			popUpPos.z = 51f;
		}
		else
		{
			doPopup = true;
			AIHeroClass = missionEvent / 100;
			HumanHeroClass = missionEvent - 100 * AIHeroClass;
			text = GameStrings.Get("FB_JUGGERNAUT_FIRSTLINE") + "\n" + GameStrings.Get(minionMsgs[HumanHeroClass]) + " beats " + GameStrings.Get(minionMsgs[AIHeroClass]);
			popUpPos.x = 0f;
			popUpPos.z = 10f;
		}
		if (doPopup)
		{
			yield return new WaitForSeconds(popupDelay);
			m_popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popupScale, text, convertLegacyPosition: false);
			NotificationManager.Get().DestroyNotification(m_popup, popupDuration);
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(4f);
			GameState.Get().SetBusy(busy: false);
		}
	}
}
