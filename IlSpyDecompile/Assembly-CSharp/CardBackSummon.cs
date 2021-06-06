using UnityEngine;

public class CardBackSummon : MonoBehaviour
{
	private CardBackManager m_CardBackManager;

	private Actor m_Actor;

	private void Start()
	{
		UpdateEffect();
	}

	public void UpdateEffect()
	{
		UpdateEchoTexture();
	}

	private void UpdateEchoTexture()
	{
		if (m_CardBackManager == null)
		{
			m_CardBackManager = CardBackManager.Get();
			if (m_CardBackManager == null)
			{
				Debug.LogError("CardBackSummonIn failed to get CardBackManager!");
				base.enabled = false;
			}
		}
		if (m_Actor == null)
		{
			m_Actor = SceneUtils.FindComponentInParents<Actor>(base.gameObject);
			if (m_Actor == null)
			{
				Debug.LogError("CardBackSummonIn failed to get Actor!");
			}
		}
		Texture texture = GetComponent<Renderer>().GetMaterial().mainTexture;
		if (m_CardBackManager.IsActorFriendly(m_Actor))
		{
			CardBack friendlyCardBack = m_CardBackManager.GetFriendlyCardBack();
			if (friendlyCardBack != null)
			{
				texture = friendlyCardBack.m_HiddenCardEchoTexture;
			}
		}
		else
		{
			CardBack opponentCardBack = m_CardBackManager.GetOpponentCardBack();
			if (opponentCardBack != null)
			{
				texture = opponentCardBack.m_HiddenCardEchoTexture;
			}
		}
		if (texture != null)
		{
			GetComponent<Renderer>().GetMaterial().mainTexture = texture;
		}
	}
}
