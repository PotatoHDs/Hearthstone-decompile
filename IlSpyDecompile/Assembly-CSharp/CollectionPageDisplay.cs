using System;
using System.Collections;
using System.Collections.Generic;
using PegasusShared;
using UnityEngine;

public class CollectionPageDisplay : CollectiblePageDisplay
{
	public enum HEADER_CLASS
	{
		INVALID,
		SHAMAN,
		PALADIN,
		MAGE,
		DRUID,
		HUNTER,
		ROGUE,
		WARRIOR,
		PRIEST,
		WARLOCK,
		HEROSKINS,
		CARDBACKS,
		DEMONHUNTER,
		COINS
	}

	public GameObject m_favoriteBanner;

	public GameObject m_heroSkinsDecor;

	public GameObject[] m_heroSkinFrames;

	public GameObject m_deckTemplateContainer;

	public GameObject m_noMatchFoundObject;

	public UberText m_noMatchExplanationText;

	public GameObject m_noMatchSetHintObject;

	public GameObject m_noMatchManaHintObject;

	public GameObject m_noMatchCraftingHintObject;

	public Material m_deckTemplatePageMaterial;

	public Color m_standardTitleTextColor;

	public Material m_wildHeaderMaterial;

	public Material m_wildPageMaterial;

	public Color m_wildTextColor;

	public Color m_wildTitleTextColor;

	public Material m_classicHeaderMaterial;

	public Material m_classicPageMaterial;

	public Color m_classicTextColor;

	public Color m_classicTitleTextColor;

	public FormatType m_pageFormatType;

	private bool m_isWild;

	private MassDisenchant m_massDisenchantVisual;

	public override void UpdateCollectionCards(List<CollectionCardActors> actorList, CollectionUtils.ViewMode mode)
	{
		base.UpdateCollectionCards(actorList, mode);
		DetachAndHideMassDisenchantVisual();
		UpdateFavoriteCardBack(mode);
		UpdateFavoriteHeroSkins(mode);
		UpdateFavoriteCoin(mode);
		UpdateHeroSkinNames(mode);
	}

	public void UpdatePageWithMassDisenchant()
	{
		MassDisenchant massDisenchantVisual = GetMassDisenchantVisual();
		if (massDisenchantVisual != null)
		{
			massDisenchantVisual.Show();
		}
	}

	public void UpdateMassDisenchantInfo()
	{
		if (!(m_massDisenchantVisual == null))
		{
			m_massDisenchantVisual.UpdateContents(CollectionManager.Get().GetMassDisenchantCards());
		}
	}

	private void DetachAndHideMassDisenchantVisual()
	{
		if (m_massDisenchantVisual != null)
		{
			m_massDisenchantVisual.Hide();
			m_massDisenchantVisual = null;
		}
	}

	public void UpdateFavoriteHeroSkins(CollectionUtils.ViewMode mode)
	{
		bool flag = mode == CollectionUtils.ViewMode.HERO_SKINS;
		if (m_heroSkinsDecor != null)
		{
			m_heroSkinsDecor.SetActive(flag);
		}
		if (!flag)
		{
			return;
		}
		int num = 0;
		foreach (CollectionCardVisual collectionCardVisual in m_collectionCardVisuals)
		{
			if (collectionCardVisual.IsShown())
			{
				Actor actor = collectionCardVisual.GetActor();
				CollectionHeroSkin component = actor.GetComponent<CollectionHeroSkin>();
				if (component == null)
				{
					continue;
				}
				component.ShowShadow(actor.IsShown());
				EntityDef entityDef = actor.GetEntityDef();
				if (entityDef != null)
				{
					component.SetClass(entityDef.GetClass());
					bool show = false;
					NetCache.CardDefinition favoriteHero = CollectionManager.Get().GetFavoriteHero(entityDef.GetClass());
					if (favoriteHero != null)
					{
						show = CollectionManager.Get().GetBestHeroesIOwn(entityDef.GetClass()).Count > 1 && !string.IsNullOrEmpty(favoriteHero.Name) && favoriteHero.Name == entityDef.GetCardId();
					}
					component.ShowFavoriteBanner(show);
				}
			}
			if (num < m_heroSkinFrames.Length)
			{
				m_heroSkinFrames[num++].SetActive(collectionCardVisual.IsShown());
			}
		}
	}

	public void UpdateHeroSkinNames(CollectionUtils.ViewMode mode)
	{
		if (mode == CollectionUtils.ViewMode.HERO_SKINS)
		{
			StartCoroutine(WaitThenUpdateHeroSkinNames(mode));
		}
	}

	private IEnumerator WaitThenUpdateHeroSkinNames(CollectionUtils.ViewMode mode)
	{
		yield return null;
		foreach (CollectionCardVisual collectionCardVisual in m_collectionCardVisuals)
		{
			if (collectionCardVisual.IsShown())
			{
				CollectionHeroSkin component = collectionCardVisual.GetActor().GetComponent<CollectionHeroSkin>();
				if (!(component == null))
				{
					component.ShowCollectionManagerText();
				}
			}
		}
	}

	public void UpdateFavoriteCardBack(CollectionUtils.ViewMode mode)
	{
		if (mode != CollectionUtils.ViewMode.CARD_BACKS)
		{
			return;
		}
		int favoriteCardBack = CardBackManager.Get().GetCardBacks().FavoriteCardBack;
		foreach (CollectionCardVisual collectionCardVisual in m_collectionCardVisuals)
		{
			if (collectionCardVisual.IsShown())
			{
				CollectionCardBack component = collectionCardVisual.GetActor().GetComponent<CollectionCardBack>();
				if (!(component == null))
				{
					component.ShowFavoriteBanner(favoriteCardBack == component.GetCardBackId());
				}
			}
		}
	}

	public void UpdateFavoriteCoin(CollectionUtils.ViewMode mode)
	{
		FavoriteBanner component = m_favoriteBanner.GetComponent<FavoriteBanner>();
		if (mode != CollectionUtils.ViewMode.COINS)
		{
			component.SetActive(enable: false);
			return;
		}
		string favoriteCoinCardId = CoinManager.Get().GetFavoriteCoinCardId();
		if (favoriteCoinCardId == null)
		{
			return;
		}
		component.SetActive(enable: true);
		foreach (CollectionCardVisual collectionCardVisual in m_collectionCardVisuals)
		{
			if (collectionCardVisual.IsShown() && collectionCardVisual.CardId == favoriteCoinCardId)
			{
				Actor actor = collectionCardVisual.GetActor();
				component.PinToActor(actor);
				break;
			}
		}
	}

	public void UpdateDeckTemplateHeader(GameObject deckTemplateHeader, FormatType pageFormatType)
	{
		if (!(deckTemplateHeader == null) && !(deckTemplateHeader.GetComponent<Renderer>() == null))
		{
			Material headerMaterial = GetHeaderMaterial(pageFormatType, null);
			deckTemplateHeader.GetComponent<Renderer>().SetMaterial(headerMaterial);
		}
	}

	public void UpdateDeckTemplatePage(Component deckTemplatePicker)
	{
		HideHeroSkinsDecor();
		if (!(deckTemplatePicker != null) || !(m_deckTemplateContainer != null))
		{
			return;
		}
		foreach (CollectionCardVisual collectionCardVisual in m_collectionCardVisuals)
		{
			collectionCardVisual.Hide();
			collectionCardVisual.SetActors(null);
		}
		if (m_basePage != null)
		{
			MeshRenderer component = m_basePage.GetComponent<MeshRenderer>();
			m_basePageMaterial = component.GetMaterial();
			component.SetMaterial(m_deckTemplatePageMaterial);
		}
		GameUtils.SetParent(deckTemplatePicker, m_deckTemplateContainer);
		GameUtils.ResetTransform(deckTemplatePicker);
	}

	public override void ShowNoMatchesFound(bool show, CollectionManager.FindCardsResult findResults = null, bool showHints = true)
	{
		m_noMatchFoundObject.SetActive(show);
		m_noMatchCraftingHintObject.SetActive(value: false);
		m_noMatchSetHintObject.SetActive(value: false);
		m_noMatchManaHintObject.SetActive(value: false);
		string key = "GLUE_COLLECTION_NO_RESULTS";
		if (show && showHints && findResults != null)
		{
			if (findResults.m_resultsWithoutManaFilterExist)
			{
				m_noMatchManaHintObject.SetActive(value: true);
				key = "GLUE_COLLECTION_NO_RESULTS_IN_SELECTED_COST";
			}
			else if (findResults.m_resultsWithoutSetFilterExist)
			{
				m_noMatchSetHintObject.SetActive(value: true);
				key = "GLUE_COLLECTION_NO_RESULTS_IN_CURRENT_SET";
			}
			else if (findResults.m_resultsUnownedExist)
			{
				m_noMatchCraftingHintObject.SetActive(value: true);
				key = "GLUE_COLLECTION_NO_RESULTS_BUT_CRAFTABLE";
			}
			else if (findResults.m_resultsInWildExist)
			{
				key = "GLUE_COLLECTION_NO_RESULTS_IN_STANDARD";
			}
		}
		m_noMatchExplanationText.Text = GameStrings.Get(key);
	}

	public void HideHeroSkinsDecor()
	{
		if (m_heroSkinsDecor != null)
		{
			m_heroSkinsDecor.SetActive(value: false);
		}
	}

	public override void UpdateCurrentPageCardLocks(bool playSound = false)
	{
		base.UpdateCurrentPageCardLocks(playSound);
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		foreach (CollectionCardVisual collectionCardVisual in m_collectionCardVisuals)
		{
			if (!collectionCardVisual.IsShown() || collectionCardVisual.GetVisualType() != 0)
			{
				collectionCardVisual.ShowLock(CollectionCardVisual.LockType.NONE);
				continue;
			}
			Actor actor = collectionCardVisual.GetActor();
			string cardId = actor.GetEntityDef().GetCardId();
			TAG_PREMIUM premium = actor.GetPremium();
			CollectibleCard card = CollectionManager.Get().GetCard(cardId, premium);
			if (card.OwnedCount <= 0)
			{
				collectionCardVisual.ShowLock(CollectionCardVisual.LockType.NONE);
				continue;
			}
			CollectionCardVisual.LockType lockType = CollectionCardVisual.LockType.NONE;
			DeckRuleset deckRuleset = CollectionManager.Get().GetDeckRuleset();
			if (deckRuleset == null || deckRuleset.CanAddToDeck(actor.GetEntityDef(), premium, editedDeck, out List<RuleInvalidReason> reasons, out List<DeckRule> brokenRules))
			{
				collectionCardVisual.ShowLock(CollectionCardVisual.LockType.NONE, null, playSound);
				continue;
			}
			string displayError = reasons[0].DisplayError;
			lockType = ((brokenRules[0].Type == DeckRule.RuleType.IS_CARD_PLAYABLE) ? CollectionCardVisual.LockType.NOT_PLAYABLE : ((brokenRules[0].Type == DeckRule.RuleType.PLAYER_OWNS_EACH_COPY) ? CollectionCardVisual.LockType.NO_MORE_INSTANCES : ((brokenRules[0].Type == DeckRule.RuleType.COUNT_COPIES_OF_EACH_CARD) ? CollectionCardVisual.LockType.MAX_COPIES_IN_DECK : CollectionCardVisual.LockType.BANNED)));
			if (brokenRules.Count > 1)
			{
				int num = brokenRules.FindIndex((DeckRule r) => r.Type == DeckRule.RuleType.PLAYER_OWNS_EACH_COPY);
				if (num >= 0)
				{
					int cardCountAllMatchingSlots = editedDeck.GetCardCountAllMatchingSlots(cardId, TAG_PREMIUM.DIAMOND);
					int cardCountAllMatchingSlots2 = editedDeck.GetCardCountAllMatchingSlots(cardId, TAG_PREMIUM.GOLDEN);
					int cardCountAllMatchingSlots3 = editedDeck.GetCardCountAllMatchingSlots(cardId, TAG_PREMIUM.NORMAL);
					int ownedCount = card.OwnedCount;
					bool flag = false;
					switch (premium)
					{
					case TAG_PREMIUM.DIAMOND:
						if (cardCountAllMatchingSlots2 + cardCountAllMatchingSlots3 > 0)
						{
							flag = true;
						}
						break;
					case TAG_PREMIUM.GOLDEN:
						if (cardCountAllMatchingSlots + cardCountAllMatchingSlots3 > 0)
						{
							flag = true;
						}
						break;
					case TAG_PREMIUM.NORMAL:
						if (cardCountAllMatchingSlots3 > ownedCount)
						{
							flag = true;
						}
						break;
					}
					if (flag)
					{
						lockType = CollectionCardVisual.LockType.NO_MORE_INSTANCES;
						displayError = reasons[num].DisplayError;
					}
				}
			}
			collectionCardVisual.ShowLock(lockType, displayError, playSound);
		}
	}

	public void SetIsWild(bool isWild)
	{
		if (isWild != m_isWild)
		{
			m_isWild = isWild;
			if (m_pageFlavorHeader != null)
			{
				m_pageFlavorHeader.GetComponent<Renderer>().SetMaterial(isWild ? m_wildHeaderMaterial : m_headerMaterial);
			}
			if (m_pageCountText != null)
			{
				m_pageCountText.TextColor = ((CollectionManager.Get().GetThemeShowing() == FormatType.FT_WILD) ? m_wildTextColor : m_textColor);
			}
			m_basePageRenderer.SetMaterial(isWild ? m_wildPageMaterial : m_pageMaterial);
		}
	}

	public void SetPageType(FormatType inputFormatType)
	{
		if (inputFormatType == m_pageFormatType)
		{
			return;
		}
		m_pageFormatType = inputFormatType;
		if (m_pageFlavorHeader != null)
		{
			Material headerMaterial = GetHeaderMaterial(inputFormatType, null);
			if (headerMaterial != null)
			{
				m_pageFlavorHeader.GetComponent<Renderer>().SetMaterial(headerMaterial);
			}
		}
		if (m_pageCountText != null)
		{
			m_pageCountText.TextColor = GetTextColor(inputFormatType, m_textColor);
		}
		Material pageMaterial = GetPageMaterial(inputFormatType, null);
		if (pageMaterial != null)
		{
			m_basePageRenderer.SetMaterial(pageMaterial);
		}
	}

	public void SetPageTextColor()
	{
		if (m_pageNameText != null)
		{
			m_pageNameText.TextColor = GetTitleTextColor(CollectionManager.Get().GetThemeShowing(), m_textColor);
		}
	}

	public void SetClass(TAG_CLASS? classTag)
	{
		if (!classTag.HasValue)
		{
			SetPageNameText("");
			if (m_pageFlavorHeader != null)
			{
				m_pageFlavorHeader.SetActive(value: false);
			}
		}
		else
		{
			TAG_CLASS value = classTag.Value;
			SetPageNameText(GameStrings.GetClassName(value));
			SetPageFlavorTextures(m_pageFlavorHeader, TagClassToHeaderClass(value));
		}
	}

	public void SetHeroSkins()
	{
		SetPageNameText(GameStrings.Get("GLUE_COLLECTION_MANAGER_HERO_SKINS_TITLE"));
		SetPageFlavorTextures(m_pageFlavorHeader, HEADER_CLASS.HEROSKINS);
	}

	public void SetCardBacks()
	{
		SetPageNameText(GameStrings.Get("GLUE_COLLECTION_MANAGER_CARD_BACKS_TITLE"));
		SetPageFlavorTextures(m_pageFlavorHeader, HEADER_CLASS.CARDBACKS);
	}

	public void SetCoins()
	{
		SetPageNameText(GameStrings.Get("GLUE_COLLECTION_MANAGER_COIN_TITLE"));
		SetPageFlavorTextures(m_pageFlavorHeader, HEADER_CLASS.COINS);
	}

	public void SetDeckTemplates()
	{
		SetPageNameText(string.Empty);
		if (m_pageFlavorHeader != null)
		{
			m_pageFlavorHeader.SetActive(value: false);
		}
	}

	public void SetMassDisenchant()
	{
		SetPageNameText(string.Empty);
		if (m_heroSkinsDecor != null)
		{
			m_heroSkinsDecor.SetActive(value: false);
		}
		if (m_pageFlavorHeader != null)
		{
			m_pageFlavorHeader.SetActive(value: false);
		}
		if (m_favoriteBanner != null)
		{
			m_favoriteBanner.GetComponent<FavoriteBanner>().SetActive(enable: false);
		}
	}

	public TAG_CLASS? GetFirstCardClass()
	{
		if (m_collectionCardVisuals.Count == 0)
		{
			return null;
		}
		CollectionCardVisual collectionCardVisual = m_collectionCardVisuals[0];
		if (!collectionCardVisual.IsShown())
		{
			return null;
		}
		Actor actor = collectionCardVisual.GetActor();
		if (!actor.IsShown())
		{
			return null;
		}
		return actor.GetEntityDef()?.GetClass();
	}

	private MassDisenchant GetMassDisenchantVisual()
	{
		if (MassDisenchant.Get() == null)
		{
			return null;
		}
		m_massDisenchantVisual = MassDisenchant.Get();
		GameUtils.SetParent(m_massDisenchantVisual, base.gameObject);
		return m_massDisenchantVisual;
	}

	private Material GetHeaderMaterial(FormatType formatType, Material defaultMaterial)
	{
		if (!new Map<FormatType, Material>
		{
			{
				FormatType.FT_STANDARD,
				m_headerMaterial
			},
			{
				FormatType.FT_WILD,
				m_wildHeaderMaterial
			},
			{
				FormatType.FT_CLASSIC,
				m_classicHeaderMaterial
			}
		}.TryGetValue(formatType, out var value))
		{
			return defaultMaterial;
		}
		return value;
	}

	private Material GetPageMaterial(FormatType formatType, Material defaultMaterial)
	{
		if (!new Map<FormatType, Material>
		{
			{
				FormatType.FT_STANDARD,
				m_pageMaterial
			},
			{
				FormatType.FT_WILD,
				m_wildPageMaterial
			},
			{
				FormatType.FT_CLASSIC,
				m_classicPageMaterial
			}
		}.TryGetValue(formatType, out var value))
		{
			return defaultMaterial;
		}
		return value;
	}

	private Color GetTextColor(FormatType formatType, Color defaultColor)
	{
		if (!new Map<FormatType, Color>
		{
			{
				FormatType.FT_STANDARD,
				m_textColor
			},
			{
				FormatType.FT_WILD,
				m_wildTextColor
			},
			{
				FormatType.FT_CLASSIC,
				m_classicTextColor
			}
		}.TryGetValue(formatType, out var value))
		{
			return defaultColor;
		}
		return value;
	}

	private Color GetTitleTextColor(FormatType formatType, Color defaultColor)
	{
		if (!new Map<FormatType, Color>
		{
			{
				FormatType.FT_STANDARD,
				m_standardTitleTextColor
			},
			{
				FormatType.FT_WILD,
				m_wildTitleTextColor
			},
			{
				FormatType.FT_CLASSIC,
				m_classicTitleTextColor
			}
		}.TryGetValue(formatType, out var value))
		{
			return defaultColor;
		}
		return value;
	}

	public static HEADER_CLASS TagClassToHeaderClass(TAG_CLASS classTag)
	{
		string value = classTag.ToString();
		if (Enum.IsDefined(typeof(HEADER_CLASS), value))
		{
			return (HEADER_CLASS)Enum.Parse(typeof(HEADER_CLASS), value);
		}
		return HEADER_CLASS.INVALID;
	}

	public static void SetPageFlavorTextures(GameObject header, HEADER_CLASS headerClass)
	{
		if (!(header == null))
		{
			float x = (((float)headerClass < 8f) ? 0f : 0.5f);
			float y = (0f - (float)headerClass) / 8f;
			CollectiblePageDisplay.SetPageFlavorTextures(header, new UnityEngine.Vector2(x, y));
		}
	}
}
