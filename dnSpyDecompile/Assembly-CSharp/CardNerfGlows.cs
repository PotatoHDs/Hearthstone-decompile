using System;
using System.Collections.Generic;
using Assets;
using UnityEngine;

// Token: 0x02000AFE RID: 2814
public class CardNerfGlows : MonoBehaviour
{
	// Token: 0x060095D8 RID: 38360 RVA: 0x0030873D File Offset: 0x0030693D
	private void Awake()
	{
		this.HideAll();
	}

	// Token: 0x060095D9 RID: 38361 RVA: 0x00308748 File Offset: 0x00306948
	public void SetGlowsForCard(CollectibleCard card, List<CardChangeDbfRecord> cardChanges)
	{
		this.HideAll();
		if (cardChanges != null)
		{
			foreach (CardChangeDbfRecord cardChangeDbfRecord in cardChanges)
			{
				if (cardChangeDbfRecord.ChangeType == CardChange.ChangeType.BUFF || cardChangeDbfRecord.ChangeType == CardChange.ChangeType.NERF)
				{
					Material material = (cardChangeDbfRecord.ChangeType == CardChange.ChangeType.BUFF) ? this.m_buffMaterial : this.m_nerfMaterial;
					int tagId = cardChangeDbfRecord.TagId;
					switch (tagId)
					{
					case 45:
						this.m_health.GetComponent<Renderer>().SetMaterial(material);
						this.m_health.SetActive(true);
						break;
					case 46:
						break;
					case 47:
						this.m_attack.GetComponent<Renderer>().SetMaterial(material);
						this.m_attack.SetActive(true);
						break;
					case 48:
						this.m_manaCost.GetComponent<Renderer>().SetMaterial(material);
						this.m_manaCost.SetActive(true);
						break;
					default:
						if (tagId == 184)
						{
							this.m_cardText.GetComponent<Renderer>().SetMaterial(material);
							this.m_cardText.SetActive(true);
						}
						break;
					}
				}
			}
		}
	}

	// Token: 0x060095DA RID: 38362 RVA: 0x00308874 File Offset: 0x00306A74
	private void HideAll()
	{
		foreach (object obj in base.transform)
		{
			((Transform)obj).gameObject.SetActive(false);
		}
	}

	// Token: 0x04007D94 RID: 32148
	public Material m_buffMaterial;

	// Token: 0x04007D95 RID: 32149
	public Material m_nerfMaterial;

	// Token: 0x04007D96 RID: 32150
	public GameObject m_attack;

	// Token: 0x04007D97 RID: 32151
	public GameObject m_health;

	// Token: 0x04007D98 RID: 32152
	public GameObject m_manaCost;

	// Token: 0x04007D99 RID: 32153
	public GameObject m_rarityGem;

	// Token: 0x04007D9A RID: 32154
	public GameObject m_art;

	// Token: 0x04007D9B RID: 32155
	public GameObject m_cardText;

	// Token: 0x04007D9C RID: 32156
	public GameObject m_cardName;

	// Token: 0x04007D9D RID: 32157
	public GameObject m_race;
}
