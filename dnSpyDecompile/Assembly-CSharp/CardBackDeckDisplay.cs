using System;
using Hearthstone;
using UnityEngine;

// Token: 0x020000D7 RID: 215
public class CardBackDeckDisplay : MonoBehaviour
{
	// Token: 0x06000C98 RID: 3224 RVA: 0x000496EC File Offset: 0x000478EC
	private void Start()
	{
		this.m_CardBackManager = CardBackManager.Get();
		if (this.m_CardBackManager == null)
		{
			if (HearthstoneApplication.Get() != null)
			{
				Debug.LogError("Failed to get CardBackManager!");
			}
			base.enabled = false;
		}
		else
		{
			this.m_CardBackManager.RegisterUpdateCardbacksListener(new CardBackManager.UpdateCardbacksCallback(this.UpdateDeckCardBacks));
		}
		this.UpdateDeckCardBacks();
	}

	// Token: 0x06000C99 RID: 3225 RVA: 0x0004974A File Offset: 0x0004794A
	private void OnDestroy()
	{
		if (CardBackManager.Get() != null)
		{
			CardBackManager.Get().UnregisterUpdateCardbacksListener(new CardBackManager.UpdateCardbacksCallback(this.UpdateDeckCardBacks));
		}
	}

	// Token: 0x06000C9A RID: 3226 RVA: 0x0004976C File Offset: 0x0004796C
	public void UpdateDeckCardBacks()
	{
		if (this.m_CardBackManager == null)
		{
			return;
		}
		CardBackManager.CardBackSlot slot = this.m_FriendlyDeck ? CardBackManager.CardBackSlot.FRIENDLY : CardBackManager.CardBackSlot.OPPONENT;
		this.m_CardBackManager.UpdateDeck(base.gameObject, slot);
	}

	// Token: 0x040008CC RID: 2252
	public bool m_FriendlyDeck = true;

	// Token: 0x040008CD RID: 2253
	private CardBackManager m_CardBackManager;
}
