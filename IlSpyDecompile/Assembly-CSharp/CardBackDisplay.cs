using System.Collections;
using UnityEngine;

public class CardBackDisplay : MonoBehaviour
{
	public Actor m_Actor;

	public GameObject m_Shadow;

	public CardBackManager.CardBackSlot m_CardBackSlot = CardBackManager.CardBackSlot.FRIENDLY;

	private CardBackManager m_CardBackManager;

	private void Start()
	{
		m_CardBackManager = CardBackManager.Get();
		if (m_CardBackManager == null)
		{
			Debug.LogError("Failed to get CardBackManager!");
			base.enabled = false;
		}
		else
		{
			m_CardBackManager.RegisterUpdateCardbacksListener(UpdateCardBack);
		}
		UpdateCardBack();
	}

	private void OnDestroy()
	{
		if (CardBackManager.Get() != null)
		{
			CardBackManager.Get().UnregisterUpdateCardbacksListener(UpdateCardBack);
		}
	}

	public void UpdateCardBack()
	{
		if (m_CardBackManager != null && base.gameObject.activeInHierarchy)
		{
			StartCoroutine(SetCardBackDisplay());
		}
	}

	public void SetCardBack(CardBackManager.CardBackSlot slot)
	{
		if (m_CardBackManager == null)
		{
			m_CardBackManager = CardBackManager.Get();
		}
		m_CardBackManager.UpdateCardBack(base.gameObject, slot);
	}

	public void EnableShadow(bool enabled)
	{
		if (m_Shadow != null)
		{
			m_Shadow.SetActive(enabled);
		}
	}

	private IEnumerator SetCardBackDisplay()
	{
		if (m_Actor == null)
		{
			m_CardBackManager.UpdateCardBack(base.gameObject, m_CardBackSlot);
		}
		else
		{
			if (m_Actor.GetCardbackUpdateIgnore())
			{
				yield break;
			}
			if (SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY)
			{
				while (m_Actor.GetEntity() == null)
				{
					yield return null;
				}
			}
			m_CardBackManager.UpdateCardBack(base.gameObject, m_Actor.GetCardBackSlot());
			m_Actor.SeedMaterialEffects();
		}
	}
}
