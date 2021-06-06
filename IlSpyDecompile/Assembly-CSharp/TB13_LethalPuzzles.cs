using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TB13_LethalPuzzles : MissionEntity
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static Map<GameEntityOption, string> s_stringOptions = InitStringOptions();

	private Notification m_popup;

	private HashSet<int> m_seen = new HashSet<int>();

	private static readonly Dictionary<int, string> s_minionMsgs = new Dictionary<int, string>
	{
		{ 1, "TB_LETHALPUZZLES_START" },
		{ 2, "TB_LETHALPUZZLES_SUCCESS" },
		{ 3, "TB_LETHALPUZZLES_FAILURE" }
	};

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

	public TB13_LethalPuzzles()
	{
		m_gameOptions.AddOptions(s_booleanOptions, s_stringOptions);
	}

	public override void PreloadAssets()
	{
		PreloadSound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
	}

	public override bool ShouldDoAlternateMulliganIntro()
	{
		return true;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		if (m_seen.Contains(missionEvent))
		{
			yield break;
		}
		m_seen.Add(missionEvent);
		switch (missionEvent)
		{
		case 100:
			NotificationManager.Get().DestroyNotification(m_popup, 0f);
			yield break;
		case 10:
		{
			NameBanner nameBannerForSide = Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING);
			if (nameBannerForSide != null)
			{
				nameBannerForSide.SetName(GameStrings.Get("TB_LETHAL_NAME"));
			}
			yield break;
		}
		}
		if (s_minionMsgs.ContainsKey(missionEvent))
		{
			string textID = s_minionMsgs[missionEvent];
			float seconds = 0f;
			float popupDuration = 2.5f;
			float popupScale = 2.5f;
			Vector3 popUpPos = new Vector3(0f, 0f, 4f);
			if (missionEvent == 1)
			{
				seconds = 5f;
				popupDuration = 5f;
			}
			yield return new WaitForSeconds(seconds);
			m_popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popupScale, GameStrings.Get(textID), convertLegacyPosition: false);
			PlaySound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
			NotificationManager.Get().DestroyNotification(m_popup, popupDuration);
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(busy: false);
			NameBanner nameBannerForSide2 = Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING);
			if (nameBannerForSide2 != null)
			{
				nameBannerForSide2.SetName(GameStrings.Get("TB_LETHAL_NAME"));
			}
		}
	}
}
