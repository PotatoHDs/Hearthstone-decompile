using System;
using UnityEngine;

// Token: 0x0200012A RID: 298
public class FavoriteBanner : MonoBehaviour
{
	// Token: 0x060013BE RID: 5054 RVA: 0x00071930 File Offset: 0x0006FB30
	private void Start()
	{
		Vector3 position = this.m_favoriteBanner.transform.parent.position;
		this.m_worldOffset = this.m_favoriteBanner.transform.position - position;
	}

	// Token: 0x060013BF RID: 5055 RVA: 0x00071970 File Offset: 0x0006FB70
	public void PinToActor(Actor actor)
	{
		if (this.m_favoriteBanner == null)
		{
			return;
		}
		this.m_favoriteBanner.transform.position = this.m_worldOffset + actor.transform.position;
		this.m_favoriteBanner.SetActive(true);
	}

	// Token: 0x060013C0 RID: 5056 RVA: 0x000719BE File Offset: 0x0006FBBE
	public void SetActive(bool enable)
	{
		this.m_favoriteBanner.SetActive(enable);
	}

	// Token: 0x04000CEB RID: 3307
	public GameObject m_favoriteBanner;

	// Token: 0x04000CEC RID: 3308
	private Vector3 m_worldOffset;
}
