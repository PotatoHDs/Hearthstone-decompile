using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002F2 RID: 754
public class FiresideGatheringSign : MonoBehaviour
{
	// Token: 0x14000028 RID: 40
	// (add) Token: 0x06002829 RID: 10281 RVA: 0x000CA200 File Offset: 0x000C8400
	// (remove) Token: 0x0600282A RID: 10282 RVA: 0x000CA238 File Offset: 0x000C8438
	public event FiresideGatheringSign.OnDestroyCallback OnDestroyEvent;

	// Token: 0x0600282B RID: 10283 RVA: 0x000CA26D File Offset: 0x000C846D
	public void SetSignShield(FiresideGatheringSignShield shield)
	{
		this.m_shield = shield;
	}

	// Token: 0x0600282C RID: 10284 RVA: 0x000CA276 File Offset: 0x000C8476
	public void SetSignShadowEnabled(bool enabled)
	{
		this.m_shield.m_ShieldShadow.SetActive(enabled);
	}

	// Token: 0x0600282D RID: 10285 RVA: 0x000CA289 File Offset: 0x000C8489
	public MeshRenderer GetShieldMeshRenderer()
	{
		return this.m_shield.m_ShieldMeshRenderer;
	}

	// Token: 0x0600282E RID: 10286 RVA: 0x000CA296 File Offset: 0x000C8496
	private void OnDestroy()
	{
		if (this.OnDestroyEvent != null)
		{
			this.OnDestroyEvent();
		}
	}

	// Token: 0x0600282F RID: 10287 RVA: 0x000CA2AB File Offset: 0x000C84AB
	public void RegisterSignSocketAnimationCompleteListener(Action listener)
	{
		if (!this.m_signSocketAnimationCompleteListeners.Contains(listener))
		{
			this.m_signSocketAnimationCompleteListeners.Add(listener);
		}
	}

	// Token: 0x06002830 RID: 10288 RVA: 0x000CA2C7 File Offset: 0x000C84C7
	public void UnregisterSignSocketAnimationCompleteListener(Action listener)
	{
		if (this.m_signSocketAnimationCompleteListeners.Contains(listener))
		{
			this.m_signSocketAnimationCompleteListeners.Remove(listener);
		}
	}

	// Token: 0x06002831 RID: 10289 RVA: 0x000CA2E4 File Offset: 0x000C84E4
	public void FireSignSocketAnimationCompleteListener()
	{
		foreach (Action action in this.m_signSocketAnimationCompleteListeners)
		{
			action();
		}
	}

	// Token: 0x040016CD RID: 5837
	public GameObject m_fxMotes;

	// Token: 0x040016CE RID: 5838
	public Transform m_shieldContainer;

	// Token: 0x040016D0 RID: 5840
	private FiresideGatheringSignShield m_shield;

	// Token: 0x040016D1 RID: 5841
	private List<Action> m_signSocketAnimationCompleteListeners = new List<Action>();

	// Token: 0x02001622 RID: 5666
	// (Invoke) Token: 0x0600E2FD RID: 58109
	public delegate void OnDestroyCallback();
}
