using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000028 RID: 40
[CustomEditClass]
public class AdventureBossCoin : PegUIElement
{
	// Token: 0x06000159 RID: 345 RVA: 0x00008574 File Offset: 0x00006774
	public void SetPortraitMaterial(AdventureBossDef bossDef)
	{
		MeshRenderer portraitRenderer = this.m_PortraitRenderer;
		List<Material> list = (portraitRenderer != null) ? portraitRenderer.GetMaterials() : null;
		if (list != null && this.m_PortraitMaterialIndex < list.Count)
		{
			Material material = bossDef.m_CoinPortraitMaterial.GetMaterial();
			list[this.m_PortraitMaterialIndex] = material;
			this.m_PortraitRenderer.SetMaterials(list);
		}
	}

	// Token: 0x0600015A RID: 346 RVA: 0x000085CA File Offset: 0x000067CA
	public void ShowConnector(bool show)
	{
		if (this.m_Connector != null)
		{
			this.m_Connector.SetActive(show);
		}
	}

	// Token: 0x0600015B RID: 347 RVA: 0x000085E8 File Offset: 0x000067E8
	public void Enable(bool flag, bool animate = true)
	{
		base.GetComponent<Collider>().enabled = flag;
		if (this.m_DisabledCollider != null)
		{
			this.m_DisabledCollider.gameObject.SetActive(!flag);
		}
		if (this.m_Enabled == flag)
		{
			return;
		}
		this.m_Enabled = flag;
		if (animate && flag)
		{
			this.ShowCoin(false);
			this.m_CoinStateTable.TriggerState("Flip", true, null);
			return;
		}
		this.ShowCoin(flag);
	}

	// Token: 0x0600015C RID: 348 RVA: 0x0000865C File Offset: 0x0000685C
	public void Select(bool selected)
	{
		UIBHighlight component = base.GetComponent<UIBHighlight>();
		if (component == null)
		{
			return;
		}
		component.AlwaysOver = selected;
		if (selected)
		{
			this.EnableFancyHighlight(false);
		}
	}

	// Token: 0x0600015D RID: 349 RVA: 0x0000868C File Offset: 0x0000688C
	public void HighlightOnce()
	{
		UIBHighlight component = base.GetComponent<UIBHighlight>();
		if (component == null)
		{
			return;
		}
		component.HighlightOnce();
	}

	// Token: 0x0600015E RID: 350 RVA: 0x000086B0 File Offset: 0x000068B0
	public void ShowNewLookGlow()
	{
		this.EnableFancyHighlight(true);
	}

	// Token: 0x0600015F RID: 351 RVA: 0x000086BC File Offset: 0x000068BC
	private void EnableFancyHighlight(bool enable)
	{
		UIBHighlightStateControl component = base.GetComponent<UIBHighlightStateControl>();
		if (component == null)
		{
			return;
		}
		component.Select(enable, false);
	}

	// Token: 0x06000160 RID: 352 RVA: 0x000086E2 File Offset: 0x000068E2
	private void ShowCoin(bool show)
	{
		if (this.m_Coin == null)
		{
			return;
		}
		TransformUtil.SetEulerAngleZ(this.m_Coin, show ? 0f : -180f);
	}

	// Token: 0x06000161 RID: 353 RVA: 0x0000870D File Offset: 0x0000690D
	private void Update()
	{
		if (AdventureBossCoin.neverRun)
		{
			Debug.Log("TEST");
		}
	}

	// Token: 0x040000E5 RID: 229
	private const string s_EventCoinFlip = "Flip";

	// Token: 0x040000E6 RID: 230
	public GameObject m_Coin;

	// Token: 0x040000E7 RID: 231
	public MeshRenderer m_PortraitRenderer;

	// Token: 0x040000E8 RID: 232
	public int m_PortraitMaterialIndex = 1;

	// Token: 0x040000E9 RID: 233
	public GameObject m_Connector;

	// Token: 0x040000EA RID: 234
	public StateEventTable m_CoinStateTable;

	// Token: 0x040000EB RID: 235
	public PegUIElement m_DisabledCollider;

	// Token: 0x040000EC RID: 236
	private bool m_Enabled;

	// Token: 0x040000ED RID: 237
	private static bool neverRun;
}
