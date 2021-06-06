using System;
using UnityEngine;

// Token: 0x020000DD RID: 221
public class CardBackSummon : MonoBehaviour
{
	// Token: 0x06000D0F RID: 3343 RVA: 0x0004B89F File Offset: 0x00049A9F
	private void Start()
	{
		this.UpdateEffect();
	}

	// Token: 0x06000D10 RID: 3344 RVA: 0x0004B8A7 File Offset: 0x00049AA7
	public void UpdateEffect()
	{
		this.UpdateEchoTexture();
	}

	// Token: 0x06000D11 RID: 3345 RVA: 0x0004B8B0 File Offset: 0x00049AB0
	private void UpdateEchoTexture()
	{
		if (this.m_CardBackManager == null)
		{
			this.m_CardBackManager = CardBackManager.Get();
			if (this.m_CardBackManager == null)
			{
				Debug.LogError("CardBackSummonIn failed to get CardBackManager!");
				base.enabled = false;
			}
		}
		if (this.m_Actor == null)
		{
			this.m_Actor = SceneUtils.FindComponentInParents<Actor>(base.gameObject);
			if (this.m_Actor == null)
			{
				Debug.LogError("CardBackSummonIn failed to get Actor!");
			}
		}
		Texture texture = base.GetComponent<Renderer>().GetMaterial().mainTexture;
		if (this.m_CardBackManager.IsActorFriendly(this.m_Actor))
		{
			CardBack friendlyCardBack = this.m_CardBackManager.GetFriendlyCardBack();
			if (friendlyCardBack != null)
			{
				texture = friendlyCardBack.m_HiddenCardEchoTexture;
			}
		}
		else
		{
			CardBack opponentCardBack = this.m_CardBackManager.GetOpponentCardBack();
			if (opponentCardBack != null)
			{
				texture = opponentCardBack.m_HiddenCardEchoTexture;
			}
		}
		if (texture != null)
		{
			base.GetComponent<Renderer>().GetMaterial().mainTexture = texture;
		}
	}

	// Token: 0x040008F9 RID: 2297
	private CardBackManager m_CardBackManager;

	// Token: 0x040008FA RID: 2298
	private Actor m_Actor;
}
