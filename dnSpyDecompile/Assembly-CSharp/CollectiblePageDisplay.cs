using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E9 RID: 233
public abstract class CollectiblePageDisplay : BookPageDisplay
{
	// Token: 0x06000D99 RID: 3481 RVA: 0x000052EC File Offset: 0x000034EC
	public override bool IsLoaded()
	{
		return true;
	}

	// Token: 0x06000D9A RID: 3482 RVA: 0x0004D2A8 File Offset: 0x0004B4A8
	public static int GetMaxCardsPerPage()
	{
		CollectionUtils.CollectionPageLayoutSettings.Variables currentPageLayoutSettings = CollectionManager.Get().GetCollectibleDisplay().GetCurrentPageLayoutSettings();
		return currentPageLayoutSettings.m_ColumnCount * currentPageLayoutSettings.m_RowCount;
	}

	// Token: 0x06000D9B RID: 3483 RVA: 0x0004D2D4 File Offset: 0x0004B4D4
	public static int GetMaxCardsPerPage(CollectionUtils.ViewMode viewMode)
	{
		CollectionUtils.CollectionPageLayoutSettings.Variables pageLayoutSettings = CollectionManager.Get().GetCollectibleDisplay().GetPageLayoutSettings(viewMode);
		return pageLayoutSettings.m_ColumnCount * pageLayoutSettings.m_RowCount;
	}

	// Token: 0x06000D9C RID: 3484 RVA: 0x0004D300 File Offset: 0x0004B500
	public CollectionCardVisual GetCardVisual(string cardID, TAG_PREMIUM premium)
	{
		foreach (CollectionCardVisual collectionCardVisual in this.m_collectionCardVisuals)
		{
			if (collectionCardVisual.IsShown() && collectionCardVisual.GetVisualType() == CollectionUtils.ViewMode.CARDS)
			{
				Actor actor = collectionCardVisual.GetActor();
				if (actor.GetEntityDef().GetCardId().Equals(cardID) && actor.GetPremium() == premium)
				{
					return collectionCardVisual;
				}
			}
		}
		return null;
	}

	// Token: 0x06000D9D RID: 3485 RVA: 0x0004D388 File Offset: 0x0004B588
	public override void Show()
	{
		base.Show();
		MassDisenchant massDisenchant = MassDisenchant.Get();
		if (massDisenchant != null && massDisenchant.IsShown())
		{
			return;
		}
		for (int i = 0; i < this.m_collectionCardVisuals.Count; i++)
		{
			CollectionCardVisual collectionCardVisual = this.GetCollectionCardVisual(i);
			if (collectionCardVisual.GetActor() != null)
			{
				collectionCardVisual.Show();
			}
		}
	}

	// Token: 0x06000D9E RID: 3486 RVA: 0x0004D3E8 File Offset: 0x0004B5E8
	public override void Hide()
	{
		base.Hide();
		this.MarkAllShownCardsSeen();
		for (int i = 0; i < this.m_collectionCardVisuals.Count; i++)
		{
			this.GetCollectionCardVisual(i).Hide();
		}
	}

	// Token: 0x06000D9F RID: 3487 RVA: 0x0004D424 File Offset: 0x0004B624
	public void MarkAllShownCardsSeen()
	{
		for (int i = 0; i < this.m_collectionCardVisuals.Count; i++)
		{
			CollectionCardVisual collectionCardVisual = this.GetCollectionCardVisual(i);
			if (collectionCardVisual.IsShown())
			{
				CollectionManager.Get().MarkAllInstancesAsSeen(collectionCardVisual.CardId, collectionCardVisual.Premium);
			}
		}
	}

	// Token: 0x06000DA0 RID: 3488 RVA: 0x0004D470 File Offset: 0x0004B670
	public virtual void UpdateCollectionCards(List<CollectionCardActors> actorList, CollectionUtils.ViewMode mode)
	{
		Log.CollectionManager.Print("mode={0}", new object[]
		{
			mode
		});
		int num = 0;
		while (num < actorList.Count && num < CollectiblePageDisplay.GetMaxCardsPerPage())
		{
			CollectionCardVisual collectionCardVisual = this.GetCollectionCardVisual(num);
			collectionCardVisual.SetActors(actorList[num], mode);
			collectionCardVisual.Show();
			if (mode == CollectionUtils.ViewMode.HERO_SKINS)
			{
				collectionCardVisual.SetHeroSkinBoxCollider();
			}
			else
			{
				collectionCardVisual.SetDefaultBoxCollider();
			}
			num++;
		}
		for (int i = num; i < this.m_collectionCardVisuals.Count; i++)
		{
			CollectionCardVisual collectionCardVisual2 = this.GetCollectionCardVisual(i);
			collectionCardVisual2.Hide();
			collectionCardVisual2.SetActors(null, CollectionUtils.ViewMode.CARDS);
		}
		this.UpdateCurrentPageCardLocks(false);
	}

	// Token: 0x06000DA1 RID: 3489 RVA: 0x0004D513 File Offset: 0x0004B713
	public void UpdateBasePage()
	{
		if (this.m_basePageMaterial != null && this.m_basePage != null)
		{
			this.m_basePage.GetComponent<MeshRenderer>().SetMaterial(this.m_basePageMaterial);
		}
	}

	// Token: 0x06000DA2 RID: 3490
	public abstract void ShowNoMatchesFound(bool show, CollectionManager.FindCardsResult findResults = null, bool showHints = true);

	// Token: 0x06000DA3 RID: 3491 RVA: 0x0004D548 File Offset: 0x0004B748
	public virtual void UpdateCurrentPageCardLocks(bool playSound = false)
	{
		if (CollectionDeckTray.Get().GetCurrentContentType() != DeckTray.DeckContentTypes.Cards)
		{
			foreach (CollectionCardVisual collectionCardVisual in this.m_collectionCardVisuals)
			{
				if (collectionCardVisual.IsShown())
				{
					if (collectionCardVisual.GetVisualType() == CollectionUtils.ViewMode.CARDS)
					{
						Actor actor = collectionCardVisual.GetActor();
						string cardId = actor.GetEntityDef().GetCardId();
						TAG_PREMIUM premium = actor.GetPremium();
						CollectibleCard card = CollectionManager.Get().GetCard(cardId, premium);
						if (!GameUtils.IsCardGameplayEventActive(cardId) && card.OwnedCount > 0)
						{
							collectionCardVisual.ShowLock(CollectionCardVisual.LockType.NOT_PLAYABLE, GameStrings.Get("GLUE_COLLECTION_LOCK_CARD_NOT_PLAYABLE"), playSound);
							continue;
						}
					}
					collectionCardVisual.ShowLock(CollectionCardVisual.LockType.NONE);
				}
			}
			return;
		}
	}

	// Token: 0x06000DA4 RID: 3492 RVA: 0x0004D608 File Offset: 0x0004B808
	public void SetPageCountText(string text)
	{
		if (this.m_pageCountText != null)
		{
			this.m_pageCountText.Text = text;
		}
	}

	// Token: 0x06000DA5 RID: 3493 RVA: 0x0004D624 File Offset: 0x0004B824
	public void ActivatePageCountText(bool active)
	{
		if (this.m_pageCountText != null)
		{
			this.m_pageCountText.gameObject.SetActive(active);
		}
	}

	// Token: 0x06000DA6 RID: 3494 RVA: 0x0004D648 File Offset: 0x0004B848
	protected CollectionCardVisual GetCollectionCardVisual(int index)
	{
		CollectionUtils.CollectionPageLayoutSettings.Variables currentPageLayoutSettings = CollectionManager.Get().GetCollectibleDisplay().GetCurrentPageLayoutSettings();
		float columnSpacing = currentPageLayoutSettings.m_ColumnSpacing;
		int columnCount = currentPageLayoutSettings.m_ColumnCount;
		float num = columnSpacing * (float)(columnCount - 1);
		float scale = currentPageLayoutSettings.m_Scale;
		float rowSpacing = currentPageLayoutSettings.m_RowSpacing;
		Vector3 position = this.m_cardStartPositionEightCards.transform.localPosition + currentPageLayoutSettings.m_Offset;
		int num2 = index / columnCount;
		position.x += (float)(index % columnCount) * columnSpacing - num * 0.5f;
		position.z -= rowSpacing * (float)num2;
		CollectionCardVisual collectionCardVisual;
		if (index == this.m_collectionCardVisuals.Count)
		{
			collectionCardVisual = (CollectionCardVisual)GameUtils.Instantiate(CollectionManager.Get().GetCollectibleDisplay().GetCardVisualPrefab(), base.gameObject, false);
			this.m_collectionCardVisuals.Insert(index, collectionCardVisual);
		}
		else
		{
			collectionCardVisual = this.m_collectionCardVisuals[index];
		}
		collectionCardVisual.SetCMRow(num2);
		collectionCardVisual.transform.localScale = new Vector3(scale, scale, scale);
		collectionCardVisual.transform.position = base.transform.TransformPoint(position);
		return collectionCardVisual;
	}

	// Token: 0x06000DA7 RID: 3495 RVA: 0x0004D75F File Offset: 0x0004B95F
	protected void SetPageNameText(string className)
	{
		if (this.m_pageNameText != null)
		{
			this.m_pageNameText.Text = className;
		}
	}

	// Token: 0x06000DA8 RID: 3496 RVA: 0x0004D77B File Offset: 0x0004B97B
	public static void SetPageFlavorTextures(GameObject header, Vector2 offset)
	{
		if (header == null)
		{
			return;
		}
		header.GetComponent<Renderer>().GetMaterial().SetTextureOffset("_MainTex", offset);
		if (header != null)
		{
			header.SetActive(true);
		}
	}

	// Token: 0x0400094B RID: 2379
	public GameObject m_cardStartPositionEightCards;

	// Token: 0x0400094C RID: 2380
	public UberText m_pageCountText;

	// Token: 0x0400094D RID: 2381
	public UberText m_pageNameText;

	// Token: 0x0400094E RID: 2382
	public GameObject m_pageFlavorHeader;

	// Token: 0x0400094F RID: 2383
	public GameObject m_basePage;

	// Token: 0x04000950 RID: 2384
	public Material m_headerMaterial;

	// Token: 0x04000951 RID: 2385
	public Material m_pageMaterial;

	// Token: 0x04000952 RID: 2386
	public Color m_textColor;

	// Token: 0x04000953 RID: 2387
	protected List<CollectionCardVisual> m_collectionCardVisuals = new List<CollectionCardVisual>();

	// Token: 0x04000954 RID: 2388
	protected Material m_basePageMaterial;
}
