using System.Collections.Generic;
using Hearthstone;
using UnityEngine;

public class DraftInputManager : MonoBehaviour
{
	private static DraftInputManager s_instance;

	private int m_selectedIndex = -1;

	private void Awake()
	{
		s_instance = this;
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	public static DraftInputManager Get()
	{
		return s_instance;
	}

	public void Unload()
	{
	}

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
			ClipboardUtils.CopyToClipboard(draftDeck.GetShareableDeck().Serialize());
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
		if (flag && m_selectedIndex == num)
		{
			draftDisplay.ClickConfirmButton();
			m_selectedIndex = -1;
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
		m_selectedIndex = num;
		draftCardVisual.ChooseThisCard();
		return true;
	}
}
