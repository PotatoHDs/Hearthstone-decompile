using System.Collections.Generic;
using UnityEngine;

public abstract class CollectiblePageDisplay : BookPageDisplay
{
	public GameObject m_cardStartPositionEightCards;

	public UberText m_pageCountText;

	public UberText m_pageNameText;

	public GameObject m_pageFlavorHeader;

	public GameObject m_basePage;

	public Material m_headerMaterial;

	public Material m_pageMaterial;

	public Color m_textColor;

	protected List<CollectionCardVisual> m_collectionCardVisuals = new List<CollectionCardVisual>();

	protected Material m_basePageMaterial;

	public override bool IsLoaded()
	{
		return true;
	}

	public static int GetMaxCardsPerPage()
	{
		CollectionUtils.CollectionPageLayoutSettings.Variables currentPageLayoutSettings = CollectionManager.Get().GetCollectibleDisplay().GetCurrentPageLayoutSettings();
		return currentPageLayoutSettings.m_ColumnCount * currentPageLayoutSettings.m_RowCount;
	}

	public static int GetMaxCardsPerPage(CollectionUtils.ViewMode viewMode)
	{
		CollectionUtils.CollectionPageLayoutSettings.Variables pageLayoutSettings = CollectionManager.Get().GetCollectibleDisplay().GetPageLayoutSettings(viewMode);
		return pageLayoutSettings.m_ColumnCount * pageLayoutSettings.m_RowCount;
	}

	public CollectionCardVisual GetCardVisual(string cardID, TAG_PREMIUM premium)
	{
		foreach (CollectionCardVisual collectionCardVisual in m_collectionCardVisuals)
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

	public override void Show()
	{
		base.Show();
		MassDisenchant massDisenchant = MassDisenchant.Get();
		if (massDisenchant != null && massDisenchant.IsShown())
		{
			return;
		}
		for (int i = 0; i < m_collectionCardVisuals.Count; i++)
		{
			CollectionCardVisual collectionCardVisual = GetCollectionCardVisual(i);
			if (collectionCardVisual.GetActor() != null)
			{
				collectionCardVisual.Show();
			}
		}
	}

	public override void Hide()
	{
		base.Hide();
		MarkAllShownCardsSeen();
		for (int i = 0; i < m_collectionCardVisuals.Count; i++)
		{
			GetCollectionCardVisual(i).Hide();
		}
	}

	public void MarkAllShownCardsSeen()
	{
		for (int i = 0; i < m_collectionCardVisuals.Count; i++)
		{
			CollectionCardVisual collectionCardVisual = GetCollectionCardVisual(i);
			if (collectionCardVisual.IsShown())
			{
				CollectionManager.Get().MarkAllInstancesAsSeen(collectionCardVisual.CardId, collectionCardVisual.Premium);
			}
		}
	}

	public virtual void UpdateCollectionCards(List<CollectionCardActors> actorList, CollectionUtils.ViewMode mode)
	{
		Log.CollectionManager.Print("mode={0}", mode);
		int i;
		for (i = 0; i < actorList.Count && i < GetMaxCardsPerPage(); i++)
		{
			CollectionCardVisual collectionCardVisual = GetCollectionCardVisual(i);
			collectionCardVisual.SetActors(actorList[i], mode);
			collectionCardVisual.Show();
			if (mode == CollectionUtils.ViewMode.HERO_SKINS)
			{
				collectionCardVisual.SetHeroSkinBoxCollider();
			}
			else
			{
				collectionCardVisual.SetDefaultBoxCollider();
			}
		}
		for (int j = i; j < m_collectionCardVisuals.Count; j++)
		{
			CollectionCardVisual collectionCardVisual2 = GetCollectionCardVisual(j);
			collectionCardVisual2.Hide();
			collectionCardVisual2.SetActors(null);
		}
		UpdateCurrentPageCardLocks();
	}

	public void UpdateBasePage()
	{
		if (m_basePageMaterial != null && m_basePage != null)
		{
			m_basePage.GetComponent<MeshRenderer>().SetMaterial(m_basePageMaterial);
		}
	}

	public abstract void ShowNoMatchesFound(bool show, CollectionManager.FindCardsResult findResults = null, bool showHints = true);

	public virtual void UpdateCurrentPageCardLocks(bool playSound = false)
	{
		if (CollectionDeckTray.Get().GetCurrentContentType() == DeckTray.DeckContentTypes.Cards)
		{
			return;
		}
		foreach (CollectionCardVisual collectionCardVisual in m_collectionCardVisuals)
		{
			if (!collectionCardVisual.IsShown())
			{
				continue;
			}
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

	public void SetPageCountText(string text)
	{
		if (m_pageCountText != null)
		{
			m_pageCountText.Text = text;
		}
	}

	public void ActivatePageCountText(bool active)
	{
		if (m_pageCountText != null)
		{
			m_pageCountText.gameObject.SetActive(active);
		}
	}

	protected CollectionCardVisual GetCollectionCardVisual(int index)
	{
		CollectionUtils.CollectionPageLayoutSettings.Variables currentPageLayoutSettings = CollectionManager.Get().GetCollectibleDisplay().GetCurrentPageLayoutSettings();
		float columnSpacing = currentPageLayoutSettings.m_ColumnSpacing;
		int columnCount = currentPageLayoutSettings.m_ColumnCount;
		float num = columnSpacing * (float)(columnCount - 1);
		float scale = currentPageLayoutSettings.m_Scale;
		float rowSpacing = currentPageLayoutSettings.m_RowSpacing;
		Vector3 position = m_cardStartPositionEightCards.transform.localPosition + currentPageLayoutSettings.m_Offset;
		int num2 = index / columnCount;
		position.x += (float)(index % columnCount) * columnSpacing - num * 0.5f;
		position.z -= rowSpacing * (float)num2;
		CollectionCardVisual collectionCardVisual;
		if (index == m_collectionCardVisuals.Count)
		{
			collectionCardVisual = (CollectionCardVisual)GameUtils.Instantiate(CollectionManager.Get().GetCollectibleDisplay().GetCardVisualPrefab(), base.gameObject);
			m_collectionCardVisuals.Insert(index, collectionCardVisual);
		}
		else
		{
			collectionCardVisual = m_collectionCardVisuals[index];
		}
		collectionCardVisual.SetCMRow(num2);
		collectionCardVisual.transform.localScale = new Vector3(scale, scale, scale);
		collectionCardVisual.transform.position = base.transform.TransformPoint(position);
		return collectionCardVisual;
	}

	protected void SetPageNameText(string className)
	{
		if (m_pageNameText != null)
		{
			m_pageNameText.Text = className;
		}
	}

	public static void SetPageFlavorTextures(GameObject header, Vector2 offset)
	{
		if (!(header == null))
		{
			header.GetComponent<Renderer>().GetMaterial().SetTextureOffset("_MainTex", offset);
			if (header != null)
			{
				header.SetActive(value: true);
			}
		}
	}
}
