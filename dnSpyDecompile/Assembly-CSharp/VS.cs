using System;
using UnityEngine;

// Token: 0x02000353 RID: 851
public class VS : MonoBehaviour
{
	// Token: 0x06003176 RID: 12662 RVA: 0x000FDDEC File Offset: 0x000FBFEC
	private void Start()
	{
		this.SetDefaults();
	}

	// Token: 0x06003177 RID: 12663 RVA: 0x000FDDEC File Offset: 0x000FBFEC
	private void OnDestroy()
	{
		this.SetDefaults();
	}

	// Token: 0x06003178 RID: 12664 RVA: 0x000FDDF4 File Offset: 0x000FBFF4
	private void SetDefaults()
	{
		this.ActivateShadow(false);
	}

	// Token: 0x06003179 RID: 12665 RVA: 0x000FDDFD File Offset: 0x000FBFFD
	public void ActivateShadow(bool active = true)
	{
		this.m_shadow.SetActive(active);
	}

	// Token: 0x0600317A RID: 12666 RVA: 0x000FDE0B File Offset: 0x000FC00B
	public void ActivateAnimation(bool active = true)
	{
		base.gameObject.GetComponentInChildren<Animation>().enabled = active;
	}

	// Token: 0x04001B88 RID: 7048
	public GameObject m_shadow;
}
