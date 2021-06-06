using System;
using UnityEngine;

// Token: 0x02000A18 RID: 2584
public class CardCrafting_WepSetParent : MonoBehaviour
{
	// Token: 0x06008B64 RID: 35684 RVA: 0x002C92AF File Offset: 0x002C74AF
	private void Start()
	{
		if (!this.m_Parent)
		{
			Debug.LogError("Animation Event Set Parent is null!");
			base.enabled = false;
		}
	}

	// Token: 0x06008B65 RID: 35685 RVA: 0x002C92CF File Offset: 0x002C74CF
	public void SetParentManaGem()
	{
		if (this.m_Parent)
		{
			this.m_ManaGem.transform.parent = this.m_Parent.transform;
		}
	}

	// Token: 0x06008B66 RID: 35686 RVA: 0x002C92F9 File Offset: 0x002C74F9
	public void SetParentPortrait()
	{
		if (this.m_Parent)
		{
			this.m_Portrait.transform.parent = this.m_Parent.transform;
		}
	}

	// Token: 0x06008B67 RID: 35687 RVA: 0x002C9323 File Offset: 0x002C7523
	public void SetParentNameBanner()
	{
		if (this.m_Parent)
		{
			this.m_NameBanner.transform.parent = this.m_Parent.transform;
		}
	}

	// Token: 0x06008B68 RID: 35688 RVA: 0x002C934D File Offset: 0x002C754D
	public void SetParentRarityGem()
	{
		if (this.m_Parent)
		{
			this.m_RarityGem.transform.parent = this.m_Parent.transform;
		}
	}

	// Token: 0x06008B69 RID: 35689 RVA: 0x002C9377 File Offset: 0x002C7577
	public void SetParentDiscription()
	{
		if (this.m_Parent)
		{
			this.m_Discription.transform.parent = this.m_Parent.transform;
		}
	}

	// Token: 0x06008B6A RID: 35690 RVA: 0x002C93A1 File Offset: 0x002C75A1
	public void SetParentSwords()
	{
		if (this.m_Parent)
		{
			this.m_Swords.transform.parent = this.m_Parent.transform;
		}
	}

	// Token: 0x06008B6B RID: 35691 RVA: 0x002C93CB File Offset: 0x002C75CB
	public void SetParentShield()
	{
		if (this.m_Parent)
		{
			this.m_Shield.transform.parent = this.m_Parent.transform;
		}
	}

	// Token: 0x06008B6C RID: 35692 RVA: 0x002C93F8 File Offset: 0x002C75F8
	public void SetBackToOrgParent()
	{
		if (this.m_OrgParent)
		{
			this.m_ManaGem.transform.parent = this.m_OrgParent;
		}
		this.m_Portrait.transform.parent = this.m_OrgParent;
		this.m_NameBanner.transform.parent = this.m_OrgParent;
		this.m_RarityGem.transform.parent = this.m_OrgParent;
		this.m_Discription.transform.parent = this.m_OrgParent;
		this.m_Swords.transform.parent = this.m_OrgParent;
		this.m_Shield.transform.parent = this.m_OrgParent;
	}

	// Token: 0x040073E3 RID: 29667
	public GameObject m_Parent;

	// Token: 0x040073E4 RID: 29668
	public Transform m_OrgParent;

	// Token: 0x040073E5 RID: 29669
	public GameObject m_ManaGem;

	// Token: 0x040073E6 RID: 29670
	public GameObject m_Portrait;

	// Token: 0x040073E7 RID: 29671
	public GameObject m_NameBanner;

	// Token: 0x040073E8 RID: 29672
	public GameObject m_RarityGem;

	// Token: 0x040073E9 RID: 29673
	public GameObject m_Discription;

	// Token: 0x040073EA RID: 29674
	public GameObject m_Swords;

	// Token: 0x040073EB RID: 29675
	public GameObject m_Shield;
}
