using System;
using UnityEngine;

// Token: 0x020000ED RID: 237
public class ArcaneDustAmount : MonoBehaviour
{
	// Token: 0x06000DBB RID: 3515 RVA: 0x0004DDF8 File Offset: 0x0004BFF8
	private void Awake()
	{
		ArcaneDustAmount.s_instance = this;
	}

	// Token: 0x06000DBC RID: 3516 RVA: 0x0004DE00 File Offset: 0x0004C000
	private void Start()
	{
		this.UpdateCurrentDustAmount();
	}

	// Token: 0x06000DBD RID: 3517 RVA: 0x0004DE08 File Offset: 0x0004C008
	public static ArcaneDustAmount Get()
	{
		return ArcaneDustAmount.s_instance;
	}

	// Token: 0x06000DBE RID: 3518 RVA: 0x0004DE10 File Offset: 0x0004C010
	public void UpdateCurrentDustAmount()
	{
		long arcaneDustBalance = NetCache.Get().GetArcaneDustBalance();
		this.m_dustCount.Text = arcaneDustBalance.ToString();
	}

	// Token: 0x04000972 RID: 2418
	public UberText m_dustCount;

	// Token: 0x04000973 RID: 2419
	public GameObject m_dustJar;

	// Token: 0x04000974 RID: 2420
	public GameObject m_dustFX;

	// Token: 0x04000975 RID: 2421
	public GameObject m_explodeFX_Common;

	// Token: 0x04000976 RID: 2422
	public GameObject m_explodeFX_Rare;

	// Token: 0x04000977 RID: 2423
	public GameObject m_explodeFX_Epic;

	// Token: 0x04000978 RID: 2424
	public GameObject m_explodeFX_Legendary;

	// Token: 0x04000979 RID: 2425
	private static ArcaneDustAmount s_instance;
}
