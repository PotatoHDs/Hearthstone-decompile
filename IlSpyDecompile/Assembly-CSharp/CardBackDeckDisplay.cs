using Hearthstone;
using UnityEngine;

public class CardBackDeckDisplay : MonoBehaviour
{
	public bool m_FriendlyDeck = true;

	private CardBackManager m_CardBackManager;

	private void Start()
	{
		m_CardBackManager = CardBackManager.Get();
		if (m_CardBackManager == null)
		{
			if (HearthstoneApplication.Get() != null)
			{
				Debug.LogError("Failed to get CardBackManager!");
			}
			base.enabled = false;
		}
		else
		{
			m_CardBackManager.RegisterUpdateCardbacksListener(UpdateDeckCardBacks);
		}
		UpdateDeckCardBacks();
	}

	private void OnDestroy()
	{
		if (CardBackManager.Get() != null)
		{
			CardBackManager.Get().UnregisterUpdateCardbacksListener(UpdateDeckCardBacks);
		}
	}

	public void UpdateDeckCardBacks()
	{
		if (m_CardBackManager != null)
		{
			CardBackManager.CardBackSlot slot = (m_FriendlyDeck ? CardBackManager.CardBackSlot.FRIENDLY : CardBackManager.CardBackSlot.OPPONENT);
			m_CardBackManager.UpdateDeck(base.gameObject, slot);
		}
	}
}
