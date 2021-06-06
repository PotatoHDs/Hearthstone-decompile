using System;
using Assets;
using UnityEngine;

// Token: 0x0200066D RID: 1645
public class DeckReward : Reward
{
	// Token: 0x06005C65 RID: 23653 RVA: 0x001E04E1 File Offset: 0x001DE6E1
	protected override void InitData()
	{
		base.SetData(new DeckRewardData(0, 0), false);
	}

	// Token: 0x06005C66 RID: 23654 RVA: 0x001E04F4 File Offset: 0x001DE6F4
	protected override void ShowReward(bool updateCacheValues)
	{
		if (!(base.Data is DeckRewardData))
		{
			Debug.LogWarning(string.Format("SimpleReward.ShowReward() - Data {0} is not SimpleRewardData", base.Data));
			return;
		}
		Vector3 localScale = this.m_root.transform.localScale;
		this.m_root.SetActive(true);
		this.m_root.transform.localScale = Vector3.zero;
		iTween.ScaleTo(this.m_root, iTween.Hash(new object[]
		{
			"scale",
			localScale,
			"time",
			0.5f,
			"easetype",
			iTween.EaseType.easeOutElastic
		}));
	}

	// Token: 0x06005C67 RID: 23655 RVA: 0x001DDEDB File Offset: 0x001DC0DB
	protected override void HideReward()
	{
		base.HideReward();
		this.m_root.SetActive(false);
	}

	// Token: 0x06005C68 RID: 23656 RVA: 0x001E05A4 File Offset: 0x001DE7A4
	protected override void OnDataSet(bool updateVisuals)
	{
		if (updateVisuals)
		{
			DeckRewardData deckRewardData = base.Data as DeckRewardData;
			if (deckRewardData != null)
			{
				this.DeckId = deckRewardData.DeckId;
				this.ClassId = deckRewardData.ClassId;
				if (GameUtils.DeckIncludesRotatedCards(this.DeckId))
				{
					this.deckFrameStandard.SetActive(false);
					this.deckMeshWild.SetMaterial(this.GetClassMaterial());
				}
				else
				{
					this.deckFrameWild.SetActive(false);
					this.deckMeshStandard.SetMaterial(this.GetClassMaterial());
				}
				this.deckNameWild.Text = (this.deckNameStandard.Text = this.GetDeckName());
			}
		}
	}

	// Token: 0x06005C69 RID: 23657 RVA: 0x001E064C File Offset: 0x001DE84C
	private Material GetClassMaterial()
	{
		this.ReleaseCardDef();
		string heroCardId = CollectionManager.GetHeroCardId((TAG_CLASS)this.ClassId, CardHero.HeroType.VANILLA);
		this.m_heroCardDef = DefLoader.Get().GetCardDef(heroCardId, null);
		return this.m_heroCardDef.CardDef.GetCustomDeckPortrait();
	}

	// Token: 0x06005C6A RID: 23658 RVA: 0x001E068E File Offset: 0x001DE88E
	private string GetDeckName()
	{
		return GameDbf.Deck.GetRecord(this.DeckId).Name;
	}

	// Token: 0x06005C6B RID: 23659 RVA: 0x001E06AA File Offset: 0x001DE8AA
	protected override void OnDestroy()
	{
		this.ReleaseCardDef();
		base.OnDestroy();
	}

	// Token: 0x06005C6C RID: 23660 RVA: 0x001E06B8 File Offset: 0x001DE8B8
	private void ReleaseCardDef()
	{
		DefLoader.DisposableCardDef heroCardDef = this.m_heroCardDef;
		if (heroCardDef != null)
		{
			heroCardDef.Dispose();
		}
		this.m_heroCardDef = null;
	}

	// Token: 0x04004E8B RID: 20107
	protected int DeckId;

	// Token: 0x04004E8C RID: 20108
	protected int ClassId;

	// Token: 0x04004E8D RID: 20109
	private DefLoader.DisposableCardDef m_heroCardDef;

	// Token: 0x04004E8E RID: 20110
	public UberText deckNameWild;

	// Token: 0x04004E8F RID: 20111
	public UberText deckNameStandard;

	// Token: 0x04004E90 RID: 20112
	public GameObject deckFrameWild;

	// Token: 0x04004E91 RID: 20113
	public GameObject deckFrameStandard;

	// Token: 0x04004E92 RID: 20114
	public MeshRenderer deckMeshWild;

	// Token: 0x04004E93 RID: 20115
	public MeshRenderer deckMeshStandard;
}
