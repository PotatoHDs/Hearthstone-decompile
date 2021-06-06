using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000D8 RID: 216
public class CardBackDisplay : MonoBehaviour
{
	// Token: 0x06000C9C RID: 3228 RVA: 0x000497B0 File Offset: 0x000479B0
	private void Start()
	{
		this.m_CardBackManager = CardBackManager.Get();
		if (this.m_CardBackManager == null)
		{
			Debug.LogError("Failed to get CardBackManager!");
			base.enabled = false;
		}
		else
		{
			this.m_CardBackManager.RegisterUpdateCardbacksListener(new CardBackManager.UpdateCardbacksCallback(this.UpdateCardBack));
		}
		this.UpdateCardBack();
	}

	// Token: 0x06000C9D RID: 3229 RVA: 0x00049801 File Offset: 0x00047A01
	private void OnDestroy()
	{
		if (CardBackManager.Get() != null)
		{
			CardBackManager.Get().UnregisterUpdateCardbacksListener(new CardBackManager.UpdateCardbacksCallback(this.UpdateCardBack));
		}
	}

	// Token: 0x06000C9E RID: 3230 RVA: 0x00049821 File Offset: 0x00047A21
	public void UpdateCardBack()
	{
		if (this.m_CardBackManager == null)
		{
			return;
		}
		if (base.gameObject.activeInHierarchy)
		{
			base.StartCoroutine(this.SetCardBackDisplay());
		}
	}

	// Token: 0x06000C9F RID: 3231 RVA: 0x00049846 File Offset: 0x00047A46
	public void SetCardBack(CardBackManager.CardBackSlot slot)
	{
		if (this.m_CardBackManager == null)
		{
			this.m_CardBackManager = CardBackManager.Get();
		}
		this.m_CardBackManager.UpdateCardBack(base.gameObject, slot);
	}

	// Token: 0x06000CA0 RID: 3232 RVA: 0x0004986D File Offset: 0x00047A6D
	public void EnableShadow(bool enabled)
	{
		if (this.m_Shadow != null)
		{
			this.m_Shadow.SetActive(enabled);
		}
	}

	// Token: 0x06000CA1 RID: 3233 RVA: 0x00049889 File Offset: 0x00047A89
	private IEnumerator SetCardBackDisplay()
	{
		if (this.m_Actor == null)
		{
			this.m_CardBackManager.UpdateCardBack(base.gameObject, this.m_CardBackSlot);
			yield break;
		}
		if (this.m_Actor.GetCardbackUpdateIgnore())
		{
			yield break;
		}
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY)
		{
			while (this.m_Actor.GetEntity() == null)
			{
				yield return null;
			}
		}
		this.m_CardBackManager.UpdateCardBack(base.gameObject, this.m_Actor.GetCardBackSlot());
		this.m_Actor.SeedMaterialEffects();
		yield break;
	}

	// Token: 0x040008CE RID: 2254
	public Actor m_Actor;

	// Token: 0x040008CF RID: 2255
	public GameObject m_Shadow;

	// Token: 0x040008D0 RID: 2256
	public CardBackManager.CardBackSlot m_CardBackSlot = CardBackManager.CardBackSlot.FRIENDLY;

	// Token: 0x040008D1 RID: 2257
	private CardBackManager m_CardBackManager;
}
