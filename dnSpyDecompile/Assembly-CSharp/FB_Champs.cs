using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200059A RID: 1434
public class FB_Champs : MissionEntity
{
	// Token: 0x06004F94 RID: 20372 RVA: 0x001A1DB5 File Offset: 0x0019FFB5
	public override void PreloadAssets()
	{
		base.PreloadSound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
	}

	// Token: 0x06004F95 RID: 20373 RVA: 0x001A1DC4 File Offset: 0x0019FFC4
	public override string GetNameBannerOverride(Player.Side playerSide)
	{
		int tag = GameState.Get().GetPlayerBySide(playerSide).GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
		return FB_Champs.popupMsgs[tag].Champion;
	}

	// Token: 0x06004F96 RID: 20374 RVA: 0x001A1DF3 File Offset: 0x0019FFF3
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		this.doPopup = true;
		if (missionEvent == 10000)
		{
			this.doPopup = false;
		}
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
		if (this.doPopup)
		{
			yield return this.ShowPopup(GameStrings.Get(FB_Champs.popupMsgs[missionEvent].Message), FB_Champs.popupMsgs[missionEvent].Delay, popUpPos);
		}
		yield break;
	}

	// Token: 0x06004F97 RID: 20375 RVA: 0x001A1E09 File Offset: 0x001A0009
	private IEnumerator ShowPopup(string stringID, float popupDuration, Vector3 popUpPos)
	{
		this.m_popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * 1.4f, GameStrings.Get(stringID), false, NotificationManager.PopupTextType.FANCY);
		NotificationManager.Get().DestroyNotification(this.m_popup, popupDuration);
		GameState.Get().SetBusy(true);
		yield return new WaitForSeconds(5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x040045B7 RID: 17847
	private Notification m_popup;

	// Token: 0x040045B8 RID: 17848
	public bool doPopup;

	// Token: 0x040045B9 RID: 17849
	private static readonly Dictionary<int, FB_Champs.PopupMessage> popupMsgs = new Dictionary<int, FB_Champs.PopupMessage>
	{
		{
			1235,
			new FB_Champs.PopupMessage
			{
				Message = "FB_CHAMPS_PAVEL_DRUID",
				Delay = 6f,
				Champion = "Pavel"
			}
		},
		{
			1236,
			new FB_Champs.PopupMessage
			{
				Message = "FB_CHAMPS_PAVEL_MAGE",
				Delay = 6f,
				Champion = "Pavel"
			}
		},
		{
			1237,
			new FB_Champs.PopupMessage
			{
				Message = "FB_CHAMPS_PAVEL_SHAMAN",
				Delay = 6f,
				Champion = "Pavel"
			}
		},
		{
			1238,
			new FB_Champs.PopupMessage
			{
				Message = "FB_CHAMPS_PAVEL_ROGUE",
				Delay = 6f,
				Champion = "Pavel"
			}
		},
		{
			1239,
			new FB_Champs.PopupMessage
			{
				Message = "FB_CHAMPS_PAVEL_WARRIOR",
				Delay = 6f,
				Champion = "Pavel"
			}
		},
		{
			1671,
			new FB_Champs.PopupMessage
			{
				Message = "FB_CHAMPS_OSTKAKA_ROGUE",
				Delay = 6f,
				Champion = "Ostkaka"
			}
		},
		{
			1672,
			new FB_Champs.PopupMessage
			{
				Message = "FB_CHAMPS_OSTKAKA_WARRIOR",
				Delay = 6f,
				Champion = "Ostkaka"
			}
		},
		{
			1673,
			new FB_Champs.PopupMessage
			{
				Message = "FB_CHAMPS_OSTKAKA_MAGE",
				Delay = 6f,
				Champion = "Ostkaka"
			}
		},
		{
			1675,
			new FB_Champs.PopupMessage
			{
				Message = "FB_CHAMPS_FIREBAT_ROGUE",
				Delay = 6f,
				Champion = "Firebat"
			}
		},
		{
			1676,
			new FB_Champs.PopupMessage
			{
				Message = "FB_CHAMPS_FIREBAT_HUNTER",
				Delay = 6f,
				Champion = "Firebat"
			}
		},
		{
			1678,
			new FB_Champs.PopupMessage
			{
				Message = "FB_CHAMPS_FIREBAT_DRUID",
				Delay = 6f,
				Champion = "Firebat"
			}
		},
		{
			1679,
			new FB_Champs.PopupMessage
			{
				Message = "FB_CHAMPS_FIREBAT_WARLOCK",
				Delay = 6f,
				Champion = "Firebat"
			}
		},
		{
			2173,
			new FB_Champs.PopupMessage
			{
				Message = "FB_CHAMPS_TOM60229_WARLOCK",
				Delay = 6f,
				Champion = "tom60229"
			}
		},
		{
			2174,
			new FB_Champs.PopupMessage
			{
				Message = "FB_CHAMPS_TOM60229_DRUID",
				Delay = 6f,
				Champion = "tom60229"
			}
		},
		{
			2175,
			new FB_Champs.PopupMessage
			{
				Message = "FB_CHAMPS_TOM60229_ROGUE",
				Delay = 6f,
				Champion = "tom60229"
			}
		},
		{
			2176,
			new FB_Champs.PopupMessage
			{
				Message = "FB_CHAMPS_TOM60229_PRIEST",
				Delay = 6f,
				Champion = "tom60229"
			}
		},
		{
			2838,
			new FB_Champs.PopupMessage
			{
				Message = "FB_CHAMPS_VKLIOOON_SHAMAN",
				Delay = 6f,
				Champion = "VKLiooon"
			}
		},
		{
			2839,
			new FB_Champs.PopupMessage
			{
				Message = "FB_CHAMPS_VKLIOOON_HUNTER",
				Delay = 6f,
				Champion = "VKLiooon"
			}
		},
		{
			2840,
			new FB_Champs.PopupMessage
			{
				Message = "FB_CHAMPS_VKLIOOON_PRIEST",
				Delay = 6f,
				Champion = "VKLiooon"
			}
		},
		{
			2841,
			new FB_Champs.PopupMessage
			{
				Message = "FB_CHAMPS_VKLIOOON_DRUID",
				Delay = 6f,
				Champion = "VKLiooon"
			}
		},
		{
			2842,
			new FB_Champs.PopupMessage
			{
				Message = "FB_CHAMPS_HUNTERACE_SHAMAN",
				Delay = 6f,
				Champion = "Hunterace"
			}
		},
		{
			2843,
			new FB_Champs.PopupMessage
			{
				Message = "FB_CHAMPS_HUNTERACE_ROGUE",
				Delay = 6f,
				Champion = "Hunterace"
			}
		},
		{
			2844,
			new FB_Champs.PopupMessage
			{
				Message = "FB_CHAMPS_HUNTERACE_MAGE",
				Delay = 6f,
				Champion = "Hunterace"
			}
		},
		{
			2845,
			new FB_Champs.PopupMessage
			{
				Message = "FB_CHAMPS_HUNTERACE_WARRIOR",
				Delay = 6f,
				Champion = "Hunterace"
			}
		},
		{
			2847,
			new FB_Champs.PopupMessage
			{
				Message = "FB_CHAMPS_MERC14",
				Delay = 6f,
				Champion = "Mercenaries 14"
			}
		}
	};

	// Token: 0x02001F66 RID: 8038
	public struct PopupMessage
	{
		// Token: 0x0400D8EF RID: 55535
		public string Message;

		// Token: 0x0400D8F0 RID: 55536
		public float Delay;

		// Token: 0x0400D8F1 RID: 55537
		public string Champion;
	}
}
