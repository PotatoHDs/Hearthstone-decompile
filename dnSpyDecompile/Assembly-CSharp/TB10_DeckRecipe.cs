using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005CE RID: 1486
public class TB10_DeckRecipe : MissionEntity
{
	// Token: 0x06005177 RID: 20855 RVA: 0x001A1DB5 File Offset: 0x0019FFB5
	public override void PreloadAssets()
	{
		base.PreloadSound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774");
	}

	// Token: 0x06005178 RID: 20856 RVA: 0x001AC4E2 File Offset: 0x001AA6E2
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (GameState.Get().GetFriendlySidePlayer() != GameState.Get().GetCurrentPlayer())
		{
			yield return null;
		}
		if (this.seen.Contains(missionEvent))
		{
			yield break;
		}
		this.seen.Add(missionEvent);
		this.doPopup = false;
		this.doLeftArrow = false;
		this.doUpArrow = false;
		this.doDownArrow = false;
		if (missionEvent == 11)
		{
			NotificationManager.Get().DestroyNotification(this.DeckRecipePopup, 0f);
			this.doPopup = false;
		}
		else if (missionEvent > 900)
		{
			this.doPopup = true;
			this.textID = TB10_DeckRecipe.popupMsgs[missionEvent].Message;
			this.popupDuration = TB10_DeckRecipe.popupMsgs[missionEvent].Delay;
			this.popUpPos.x = 0f;
			this.popUpPos.z = 10f;
			UniversalInputManager.UsePhoneUI;
		}
		if (this.doPopup)
		{
			yield return new WaitForSeconds(this.delayTime);
			this.DeckRecipePopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popupScale, GameStrings.Get(this.textID), false, NotificationManager.PopupTextType.BASIC);
			if (this.doLeftArrow)
			{
				this.DeckRecipePopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Left);
			}
			if (this.doUpArrow)
			{
				this.DeckRecipePopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Up);
			}
			if (this.doDownArrow)
			{
				this.DeckRecipePopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Down);
			}
			base.PlaySound("tutorial_mission_hero_coin_mouse_away.prefab:6266be3ca0b50a645915b9ea0a59d774", 1f, true, false);
			NotificationManager.Get().DestroyNotification(this.DeckRecipePopup, this.popupDuration);
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(this.popupDuration);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x040048F0 RID: 18672
	private Notification DeckRecipePopup;

	// Token: 0x040048F1 RID: 18673
	private Vector3 popUpPos;

	// Token: 0x040048F2 RID: 18674
	private string textID;

	// Token: 0x040048F3 RID: 18675
	private bool doPopup;

	// Token: 0x040048F4 RID: 18676
	private bool doLeftArrow;

	// Token: 0x040048F5 RID: 18677
	private bool doUpArrow;

	// Token: 0x040048F6 RID: 18678
	private bool doDownArrow;

	// Token: 0x040048F7 RID: 18679
	private float delayTime = 2.5f;

	// Token: 0x040048F8 RID: 18680
	private float popupDuration = 7f;

	// Token: 0x040048F9 RID: 18681
	private float popupScale = 2.5f;

	// Token: 0x040048FA RID: 18682
	private HashSet<int> seen = new HashSet<int>();

	// Token: 0x040048FB RID: 18683
	private static readonly Dictionary<int, TB10_DeckRecipe.RecipeMessage> popupMsgs = new Dictionary<int, TB10_DeckRecipe.RecipeMessage>
	{
		{
			939,
			new TB10_DeckRecipe.RecipeMessage
			{
				Message = "TB_DECKRECIPE_OG_DRUID",
				Delay = 7f
			}
		},
		{
			946,
			new TB10_DeckRecipe.RecipeMessage
			{
				Message = "TB_DECKRECIPE_OG_HUNTER",
				Delay = 7f
			}
		},
		{
			947,
			new TB10_DeckRecipe.RecipeMessage
			{
				Message = "TB_DECKRECIPE_OG_MAGE",
				Delay = 7f
			}
		},
		{
			938,
			new TB10_DeckRecipe.RecipeMessage
			{
				Message = "TB_DECKRECIPE_OG_PALADIN",
				Delay = 7f
			}
		},
		{
			945,
			new TB10_DeckRecipe.RecipeMessage
			{
				Message = "TB_DECKRECIPE_OG_PRIEST",
				Delay = 7f
			}
		},
		{
			944,
			new TB10_DeckRecipe.RecipeMessage
			{
				Message = "TB_DECKRECIPE_OG_ROGUE",
				Delay = 7f
			}
		},
		{
			937,
			new TB10_DeckRecipe.RecipeMessage
			{
				Message = "TB_DECKRECIPE_OG_SHAMAN",
				Delay = 7f
			}
		},
		{
			940,
			new TB10_DeckRecipe.RecipeMessage
			{
				Message = "TB_DECKRECIPE_OG_WARLOCK",
				Delay = 7f
			}
		},
		{
			936,
			new TB10_DeckRecipe.RecipeMessage
			{
				Message = "TB_DECKRECIPE_OG_WARRIOR",
				Delay = 7f
			}
		},
		{
			1125,
			new TB10_DeckRecipe.RecipeMessage
			{
				Message = "TB_DECKRECIPE_MSG_DRUID",
				Delay = 2.5f
			}
		},
		{
			1130,
			new TB10_DeckRecipe.RecipeMessage
			{
				Message = "TB_DECKRECIPE_MSG_HUNTER",
				Delay = 2.5f
			}
		},
		{
			1131,
			new TB10_DeckRecipe.RecipeMessage
			{
				Message = "TB_DECKRECIPE_MSG_MAGE",
				Delay = 2.5f
			}
		},
		{
			1124,
			new TB10_DeckRecipe.RecipeMessage
			{
				Message = "TB_DECKRECIPE_MSG_PALADIN",
				Delay = 2.5f
			}
		},
		{
			1129,
			new TB10_DeckRecipe.RecipeMessage
			{
				Message = "TB_DECKRECIPE_MSG_PRIEST",
				Delay = 2.5f
			}
		},
		{
			1128,
			new TB10_DeckRecipe.RecipeMessage
			{
				Message = "TB_DECKRECIPE_MSG_ROGUE",
				Delay = 2.5f
			}
		},
		{
			1123,
			new TB10_DeckRecipe.RecipeMessage
			{
				Message = "TB_DECKRECIPE_MSG_SHAMAN",
				Delay = 2.5f
			}
		},
		{
			1126,
			new TB10_DeckRecipe.RecipeMessage
			{
				Message = "TB_DECKRECIPE_MSG_WARLOCK",
				Delay = 2.5f
			}
		},
		{
			1122,
			new TB10_DeckRecipe.RecipeMessage
			{
				Message = "TB_DECKRECIPE_MSG_WARRIOR",
				Delay = 2.5f
			}
		}
	};

	// Token: 0x02001FF4 RID: 8180
	public struct RecipeMessage
	{
		// Token: 0x0400DB64 RID: 56164
		public string Message;

		// Token: 0x0400DB65 RID: 56165
		public float Delay;
	}
}
