using System;
using System.Collections.Generic;
using System.Linq;

// Token: 0x02000108 RID: 264
public class CollectionDeckSlot
{
	// Token: 0x06000FC1 RID: 4033 RVA: 0x00058260 File Offset: 0x00056460
	public override string ToString()
	{
		return string.Format("[CollectionDeckSlot: Index={0}, PreferredPremium={1}, Count={2}, CardID={3}]", new object[]
		{
			this.Index,
			this.PreferredPremium,
			this.Count,
			this.CardID
		});
	}

	// Token: 0x170000D1 RID: 209
	// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x000582B0 File Offset: 0x000564B0
	// (set) Token: 0x06000FC3 RID: 4035 RVA: 0x000582B8 File Offset: 0x000564B8
	public int Index
	{
		get
		{
			return this.m_index;
		}
		set
		{
			this.m_index = value;
		}
	}

	// Token: 0x170000D2 RID: 210
	// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x000582C4 File Offset: 0x000564C4
	public TAG_PREMIUM PreferredPremium
	{
		get
		{
			TAG_PREMIUM tag_PREMIUM = CollectionManager.Get().GetPreferredPremium();
			if (this.m_count[(int)tag_PREMIUM] > 0)
			{
				return tag_PREMIUM;
			}
			tag_PREMIUM = TAG_PREMIUM.NORMAL;
			for (int i = this.m_count.Count - 1; i > 0; i--)
			{
				if (this.m_count[i] > 0)
				{
					return (TAG_PREMIUM)i;
				}
			}
			return tag_PREMIUM;
		}
	}

	// Token: 0x170000D3 RID: 211
	// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x0005831C File Offset: 0x0005651C
	public TAG_PREMIUM UnPreferredPremium
	{
		get
		{
			TAG_PREMIUM preferredPremium = CollectionManager.Get().GetPreferredPremium();
			for (int i = 0; i < this.m_count.Count; i++)
			{
				if (this.m_count[i] > 0 && i != (int)preferredPremium)
				{
					return (TAG_PREMIUM)i;
				}
			}
			return preferredPremium;
		}
	}

	// Token: 0x170000D4 RID: 212
	// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x00058360 File Offset: 0x00056560
	public int Count
	{
		get
		{
			return this.m_count.Sum();
		}
	}

	// Token: 0x170000D5 RID: 213
	// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x0005836D File Offset: 0x0005656D
	// (set) Token: 0x06000FC8 RID: 4040 RVA: 0x00058375 File Offset: 0x00056575
	public string CardID
	{
		get
		{
			return this.m_cardId;
		}
		set
		{
			this.m_cardId = value;
		}
	}

	// Token: 0x170000D6 RID: 214
	// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x0005837E File Offset: 0x0005657E
	// (set) Token: 0x06000FCA RID: 4042 RVA: 0x00058386 File Offset: 0x00056586
	public bool Owned
	{
		get
		{
			return this.m_owned;
		}
		set
		{
			this.m_owned = value;
		}
	}

	// Token: 0x06000FCB RID: 4043 RVA: 0x0005838F File Offset: 0x0005658F
	public int GetCount(TAG_PREMIUM premium)
	{
		return this.m_count[(int)premium];
	}

	// Token: 0x06000FCC RID: 4044 RVA: 0x0005839D File Offset: 0x0005659D
	public void SetCount(int count, TAG_PREMIUM premium)
	{
		this.m_count[(int)premium] = count;
		if (this.Count > 0)
		{
			return;
		}
		if (this.OnSlotEmptied != null)
		{
			this.OnSlotEmptied(this);
		}
	}

	// Token: 0x06000FCD RID: 4045 RVA: 0x000583CC File Offset: 0x000565CC
	public void RemoveCard(int count, TAG_PREMIUM premium)
	{
		List<int> count2 = this.m_count;
		count2[(int)premium] = count2[(int)premium] - count;
		if (this.Count > 0)
		{
			return;
		}
		if (this.OnSlotEmptied != null)
		{
			this.OnSlotEmptied(this);
		}
	}

	// Token: 0x06000FCE RID: 4046 RVA: 0x00058410 File Offset: 0x00056610
	public void AddCard(int count, TAG_PREMIUM premium)
	{
		List<int> count2 = this.m_count;
		count2[(int)premium] = count2[(int)premium] + count;
	}

	// Token: 0x06000FCF RID: 4047 RVA: 0x00058438 File Offset: 0x00056638
	public void CreateDynamicEntity()
	{
		if (this.m_entityDefOverride == null)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(this.m_cardId);
			this.m_entityDefOverride = entityDef.Clone();
		}
	}

	// Token: 0x06000FD0 RID: 4048 RVA: 0x0005846A File Offset: 0x0005666A
	public EntityDef GetEntityDef()
	{
		if (this.m_entityDefOverride != null)
		{
			return this.m_entityDefOverride;
		}
		return DefLoader.Get().GetEntityDef(this.m_cardId);
	}

	// Token: 0x06000FD1 RID: 4049 RVA: 0x0005848B File Offset: 0x0005668B
	public void CopyFrom(CollectionDeckSlot otherSlot)
	{
		this.Index = otherSlot.Index;
		this.m_count = new List<int>(otherSlot.m_count);
		this.CardID = otherSlot.CardID;
		this.Owned = otherSlot.Owned;
	}

	// Token: 0x04000AA5 RID: 2725
	public CollectionDeckSlot.DelOnSlotEmptied OnSlotEmptied;

	// Token: 0x04000AA6 RID: 2726
	private int m_index;

	// Token: 0x04000AA7 RID: 2727
	private List<int> m_count = new List<int>(new int[EnumUtils.Length<TAG_PREMIUM>()]);

	// Token: 0x04000AA8 RID: 2728
	private string m_cardId;

	// Token: 0x04000AA9 RID: 2729
	private bool m_owned = true;

	// Token: 0x04000AAA RID: 2730
	public EntityDef m_entityDefOverride;

	// Token: 0x02001430 RID: 5168
	// (Invoke) Token: 0x0600D9F9 RID: 55801
	public delegate void DelOnSlotEmptied(CollectionDeckSlot slot);
}
