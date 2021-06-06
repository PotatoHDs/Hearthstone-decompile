using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002B9 RID: 697
public class DraftManaCurve : MonoBehaviour
{
	// Token: 0x0600247C RID: 9340 RVA: 0x000B777B File Offset: 0x000B597B
	private void Awake()
	{
		this.ResetBars();
	}

	// Token: 0x0600247D RID: 9341 RVA: 0x000B7784 File Offset: 0x000B5984
	public void UpdateBars()
	{
		int num = 0;
		foreach (int num2 in this.m_manaCosts)
		{
			if (num2 > num)
			{
				num = num2;
			}
		}
		if (num < 10)
		{
			num = 10;
		}
		for (int i = 0; i < this.m_bars.Count; i++)
		{
			this.m_bars[i].m_maxValue = (float)num;
			this.m_bars[i].AnimateBar((float)this.m_manaCosts[i]);
		}
	}

	// Token: 0x0600247E RID: 9342 RVA: 0x000B7828 File Offset: 0x000B5A28
	public void AddCardOfCost(int cost)
	{
		if (this.m_manaCosts == null)
		{
			return;
		}
		cost = Mathf.Clamp(cost, 0, this.m_manaCosts.Count - 1);
		List<int> manaCosts = this.m_manaCosts;
		int index = cost;
		int num = manaCosts[index];
		manaCosts[index] = num + 1;
		this.UpdateBars();
	}

	// Token: 0x0600247F RID: 9343 RVA: 0x000B7874 File Offset: 0x000B5A74
	public void ResetBars()
	{
		this.m_manaCosts = new List<int>();
		for (int i = 0; i < this.m_bars.Count; i++)
		{
			this.m_manaCosts.Add(0);
		}
		this.UpdateBars();
	}

	// Token: 0x06002480 RID: 9344 RVA: 0x000B78B4 File Offset: 0x000B5AB4
	public void AddCardToManaCurve(EntityDef entityDef)
	{
		if (entityDef == null)
		{
			Debug.LogWarning("DraftManaCurve.AddCardToManaCurve() - entityDef is null");
			return;
		}
		this.AddCardOfCost(entityDef.GetCost());
	}

	// Token: 0x04001468 RID: 5224
	public List<ManaCostBar> m_bars;

	// Token: 0x04001469 RID: 5225
	private List<int> m_manaCosts;

	// Token: 0x0400146A RID: 5226
	private const int MAX_CARDS = 10;

	// Token: 0x0400146B RID: 5227
	private const float SIZE_PER_CARD = 0.1f;
}
