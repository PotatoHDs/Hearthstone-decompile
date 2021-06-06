using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005D1 RID: 1489
public class TB13_LethalPuzzles : MissionEntity
{
	// Token: 0x06005186 RID: 20870 RVA: 0x0010C851 File Offset: 0x0010AA51
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

	// Token: 0x06005187 RID: 20871 RVA: 0x0010C860 File Offset: 0x0010AA60
	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	// Token: 0x06005188 RID: 20872 RVA: 0x001ACB73 File Offset: 0x001AAD73
	public TB13_LethalPuzzles()
	{
		this.m_gameOptions.AddOptions(TB13_LethalPuzzles.s_booleanOptions, TB13_LethalPuzzles.s_stringOptions);
	}

	// Token: 0x06005189 RID: 20873 RVA: 0x001A1DB5 File Offset: 0x0019FFB5
	public override void PreloadAssets()
	{
		base.PreloadSound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
	}

	// Token: 0x0600518A RID: 20874 RVA: 0x000052EC File Offset: 0x000034EC
	public override bool ShouldDoAlternateMulliganIntro()
	{
		return true;
	}

	// Token: 0x0600518B RID: 20875 RVA: 0x001ACB9B File Offset: 0x001AAD9B
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (this.m_seen.Contains(missionEvent))
		{
			yield break;
		}
		this.m_seen.Add(missionEvent);
		if (missionEvent == 100)
		{
			NotificationManager.Get().DestroyNotification(this.m_popup, 0f);
		}
		else if (missionEvent == 10)
		{
			NameBanner nameBannerForSide = Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING);
			if (nameBannerForSide != null)
			{
				nameBannerForSide.SetName(GameStrings.Get("TB_LETHAL_NAME"));
			}
		}
		else if (TB13_LethalPuzzles.s_minionMsgs.ContainsKey(missionEvent))
		{
			string textID = TB13_LethalPuzzles.s_minionMsgs[missionEvent];
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
			this.m_popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popupScale, GameStrings.Get(textID), false, NotificationManager.PopupTextType.BASIC);
			base.PlaySound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774", 1f, true, false);
			NotificationManager.Get().DestroyNotification(this.m_popup, popupDuration);
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(2f);
			GameState.Get().SetBusy(false);
			NameBanner nameBannerForSide2 = Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING);
			if (nameBannerForSide2 != null)
			{
				nameBannerForSide2.SetName(GameStrings.Get("TB_LETHAL_NAME"));
			}
			textID = null;
			popUpPos = default(Vector3);
		}
		yield break;
	}

	// Token: 0x04004901 RID: 18689
	private static Map<GameEntityOption, bool> s_booleanOptions = TB13_LethalPuzzles.InitBooleanOptions();

	// Token: 0x04004902 RID: 18690
	private static Map<GameEntityOption, string> s_stringOptions = TB13_LethalPuzzles.InitStringOptions();

	// Token: 0x04004903 RID: 18691
	private Notification m_popup;

	// Token: 0x04004904 RID: 18692
	private HashSet<int> m_seen = new HashSet<int>();

	// Token: 0x04004905 RID: 18693
	private static readonly Dictionary<int, string> s_minionMsgs = new Dictionary<int, string>
	{
		{
			1,
			"TB_LETHALPUZZLES_START"
		},
		{
			2,
			"TB_LETHALPUZZLES_SUCCESS"
		},
		{
			3,
			"TB_LETHALPUZZLES_FAILURE"
		}
	};
}
