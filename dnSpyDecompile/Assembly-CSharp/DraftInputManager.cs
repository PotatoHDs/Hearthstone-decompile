using System;
using System.Collections.Generic;
using Hearthstone;
using UnityEngine;

// Token: 0x020002B8 RID: 696
public class DraftInputManager : MonoBehaviour
{
	// Token: 0x06002475 RID: 9333 RVA: 0x000B7615 File Offset: 0x000B5815
	private void Awake()
	{
		DraftInputManager.s_instance = this;
	}

	// Token: 0x06002476 RID: 9334 RVA: 0x000B761D File Offset: 0x000B581D
	private void OnDestroy()
	{
		DraftInputManager.s_instance = null;
	}

	// Token: 0x06002477 RID: 9335 RVA: 0x000B7625 File Offset: 0x000B5825
	public static DraftInputManager Get()
	{
		return DraftInputManager.s_instance;
	}

	// Token: 0x06002478 RID: 9336 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Unload()
	{
	}

	// Token: 0x06002479 RID: 9337 RVA: 0x000B762C File Offset: 0x000B582C
	public bool HandleKeyboardInput()
	{
		DraftDisplay draftDisplay = DraftDisplay.Get();
		if (draftDisplay == null)
		{
			return false;
		}
		bool flag = draftDisplay.IsInHeroSelectMode();
		if (InputCollection.GetKeyUp(KeyCode.Escape) && flag)
		{
			draftDisplay.DoHeroCancelAnimation();
			return true;
		}
		CollectionDeck draftDeck = DraftManager.Get().GetDraftDeck();
		if (draftDisplay.GetDraftMode() == DraftDisplay.DraftMode.ACTIVE_DRAFT_DECK && InputCollection.GetKeyDown(KeyCode.C) && (InputCollection.GetKey(KeyCode.LeftCommand) || InputCollection.GetKey(KeyCode.LeftControl)))
		{
			ClipboardUtils.CopyToClipboard(draftDeck.GetShareableDeck().Serialize(true));
			UIStatus.Get().AddInfo(GameStrings.Get("GLUE_COLLECTION_DECK_COPIED_TOAST"));
		}
		if (!HearthstoneApplication.IsInternal())
		{
			return false;
		}
		List<DraftCardVisual> cardVisuals = DraftDisplay.Get().GetCardVisuals();
		if (cardVisuals == null)
		{
			return false;
		}
		if (cardVisuals.Count == 0)
		{
			return false;
		}
		int num = -1;
		if (InputCollection.GetKeyUp(KeyCode.Alpha1))
		{
			num = 0;
		}
		else if (InputCollection.GetKeyUp(KeyCode.Alpha2))
		{
			num = 1;
		}
		else if (InputCollection.GetKeyUp(KeyCode.Alpha3))
		{
			num = 2;
		}
		if (num == -1)
		{
			return false;
		}
		if (cardVisuals.Count < num + 1)
		{
			return false;
		}
		if (flag && this.m_selectedIndex == num)
		{
			draftDisplay.ClickConfirmButton();
			this.m_selectedIndex = -1;
			return true;
		}
		DraftCardVisual draftCardVisual = cardVisuals[num];
		if (draftCardVisual == null)
		{
			return false;
		}
		if (flag)
		{
			draftDisplay.DoHeroCancelAnimation();
		}
		this.m_selectedIndex = num;
		draftCardVisual.ChooseThisCard();
		return true;
	}

	// Token: 0x04001466 RID: 5222
	private static DraftInputManager s_instance;

	// Token: 0x04001467 RID: 5223
	private int m_selectedIndex = -1;
}
