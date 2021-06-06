using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005D6 RID: 1494
public class TB_Juggernaut : MissionEntity
{
	// Token: 0x0600519E RID: 20894 RVA: 0x001A1DB5 File Offset: 0x0019FFB5
	public override void PreloadAssets()
	{
		base.PreloadSound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
	}

	// Token: 0x0600519F RID: 20895 RVA: 0x001ACDD6 File Offset: 0x001AAFD6
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		this.doPopup = false;
		if (missionEvent == 1)
		{
			this.doPopup = true;
			this.popupScale = 1.85f;
			this.text = GameStrings.Get("FB_JUGGERNAUT_CHOOSE_OPPONENT");
			this.popupDuration = 3f;
			this.popupDelay = 3f;
			this.popUpPos.x = 0f;
			this.popUpPos.z = 51f;
		}
		else
		{
			this.doPopup = true;
			this.AIHeroClass = missionEvent / 100;
			this.HumanHeroClass = missionEvent - 100 * this.AIHeroClass;
			this.text = string.Concat(new string[]
			{
				GameStrings.Get("FB_JUGGERNAUT_FIRSTLINE"),
				"\n",
				GameStrings.Get(TB_Juggernaut.minionMsgs[this.HumanHeroClass]),
				" beats ",
				GameStrings.Get(TB_Juggernaut.minionMsgs[this.AIHeroClass])
			});
			this.popUpPos.x = 0f;
			this.popUpPos.z = 10f;
		}
		if (this.doPopup)
		{
			yield return new WaitForSeconds(this.popupDelay);
			this.m_popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popupScale, this.text, false, NotificationManager.PopupTextType.BASIC);
			NotificationManager.Get().DestroyNotification(this.m_popup, this.popupDuration);
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(4f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x04004923 RID: 18723
	private Notification m_popup;

	// Token: 0x04004924 RID: 18724
	private Vector3 popUpPos;

	// Token: 0x04004925 RID: 18725
	private string text;

	// Token: 0x04004926 RID: 18726
	private bool doPopup;

	// Token: 0x04004927 RID: 18727
	private int HumanHeroClass;

	// Token: 0x04004928 RID: 18728
	private int AIHeroClass;

	// Token: 0x04004929 RID: 18729
	private float popupDuration = 7f;

	// Token: 0x0400492A RID: 18730
	private float popupScale = 2.5f;

	// Token: 0x0400492B RID: 18731
	private float popupDelay;

	// Token: 0x0400492C RID: 18732
	private static readonly Dictionary<int, string> minionMsgs = new Dictionary<int, string>
	{
		{
			0,
			"FB_JUGGERNAUT_UNKNOWN"
		},
		{
			1,
			"FB_JUGGERNAUT_UNKNOWN"
		},
		{
			2,
			"FB_JUGGERNAUT_DRUID"
		},
		{
			3,
			"FB_JUGGERNAUT_HUNTER"
		},
		{
			4,
			"FB_JUGGERNAUT_MAGE"
		},
		{
			5,
			"FB_JUGGERNAUT_PALADIN"
		},
		{
			6,
			"FB_JUGGERNAUT_PRIEST"
		},
		{
			7,
			"FB_JUGGERNAUT_ROGUE"
		},
		{
			8,
			"FB_JUGGERNAUT_SHAMAN"
		},
		{
			9,
			"FB_JUGGERNAUT_WARLOCK"
		},
		{
			10,
			"FB_JUGGERNAUT_WARRIOR"
		}
	};
}
