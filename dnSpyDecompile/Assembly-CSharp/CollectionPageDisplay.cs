using System;
using System.Collections;
using System.Collections.Generic;
using PegasusShared;
using UnityEngine;

// Token: 0x02000114 RID: 276
public class CollectionPageDisplay : CollectiblePageDisplay
{
	// Token: 0x060011DC RID: 4572 RVA: 0x00065DBB File Offset: 0x00063FBB
	public override void UpdateCollectionCards(List<CollectionCardActors> actorList, CollectionUtils.ViewMode mode)
	{
		base.UpdateCollectionCards(actorList, mode);
		this.DetachAndHideMassDisenchantVisual();
		this.UpdateFavoriteCardBack(mode);
		this.UpdateFavoriteHeroSkins(mode);
		this.UpdateFavoriteCoin(mode);
		this.UpdateHeroSkinNames(mode);
	}

	// Token: 0x060011DD RID: 4573 RVA: 0x00065DE8 File Offset: 0x00063FE8
	public void UpdatePageWithMassDisenchant()
	{
		MassDisenchant massDisenchantVisual = this.GetMassDisenchantVisual();
		if (massDisenchantVisual != null)
		{
			massDisenchantVisual.Show();
		}
	}

	// Token: 0x060011DE RID: 4574 RVA: 0x00065E0B File Offset: 0x0006400B
	public void UpdateMassDisenchantInfo()
	{
		if (this.m_massDisenchantVisual == null)
		{
			return;
		}
		this.m_massDisenchantVisual.UpdateContents(CollectionManager.Get().GetMassDisenchantCards());
	}

	// Token: 0x060011DF RID: 4575 RVA: 0x00065E31 File Offset: 0x00064031
	private void DetachAndHideMassDisenchantVisual()
	{
		if (this.m_massDisenchantVisual != null)
		{
			this.m_massDisenchantVisual.Hide();
			this.m_massDisenchantVisual = null;
		}
	}

	// Token: 0x060011E0 RID: 4576 RVA: 0x00065E54 File Offset: 0x00064054
	public void UpdateFavoriteHeroSkins(CollectionUtils.ViewMode mode)
	{
		bool flag = mode == CollectionUtils.ViewMode.HERO_SKINS;
		if (this.m_heroSkinsDecor != null)
		{
			this.m_heroSkinsDecor.SetActive(flag);
		}
		if (!flag)
		{
			return;
		}
		int num = 0;
		foreach (CollectionCardVisual collectionCardVisual in this.m_collectionCardVisuals)
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
						show = (CollectionManager.Get().GetBestHeroesIOwn(entityDef.GetClass()).Count > 1 && !string.IsNullOrEmpty(favoriteHero.Name) && favoriteHero.Name == entityDef.GetCardId());
					}
					component.ShowFavoriteBanner(show);
				}
			}
			if (num < this.m_heroSkinFrames.Length)
			{
				this.m_heroSkinFrames[num++].SetActive(collectionCardVisual.IsShown());
			}
		}
	}

	// Token: 0x060011E1 RID: 4577 RVA: 0x00065FA4 File Offset: 0x000641A4
	public void UpdateHeroSkinNames(CollectionUtils.ViewMode mode)
	{
		if (mode != CollectionUtils.ViewMode.HERO_SKINS)
		{
			return;
		}
		base.StartCoroutine(this.WaitThenUpdateHeroSkinNames(mode));
	}

	// Token: 0x060011E2 RID: 4578 RVA: 0x00065FB9 File Offset: 0x000641B9
	private IEnumerator WaitThenUpdateHeroSkinNames(CollectionUtils.ViewMode mode)
	{
		yield return null;
		using (List<CollectionCardVisual>.Enumerator enumerator = this.m_collectionCardVisuals.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				CollectionCardVisual collectionCardVisual = enumerator.Current;
				if (collectionCardVisual.IsShown())
				{
					CollectionHeroSkin component = collectionCardVisual.GetActor().GetComponent<CollectionHeroSkin>();
					if (!(component == null))
					{
						component.ShowCollectionManagerText();
					}
				}
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x060011E3 RID: 4579 RVA: 0x00065FC8 File Offset: 0x000641C8
	public void UpdateFavoriteCardBack(CollectionUtils.ViewMode mode)
	{
		if (mode != CollectionUtils.ViewMode.CARD_BACKS)
		{
			return;
		}
		int favoriteCardBack = CardBackManager.Get().GetCardBacks().FavoriteCardBack;
		foreach (CollectionCardVisual collectionCardVisual in this.m_collectionCardVisuals)
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

	// Token: 0x060011E4 RID: 4580 RVA: 0x00066058 File Offset: 0x00064258
	public void UpdateFavoriteCoin(CollectionUtils.ViewMode mode)
	{
		FavoriteBanner component = this.m_favoriteBanner.GetComponent<FavoriteBanner>();
		if (mode != CollectionUtils.ViewMode.COINS)
		{
			component.SetActive(false);
			return;
		}
		string favoriteCoinCardId = CoinManager.Get().GetFavoriteCoinCardId();
		if (favoriteCoinCardId == null)
		{
			return;
		}
		component.SetActive(true);
		foreach (CollectionCardVisual collectionCardVisual in this.m_collectionCardVisuals)
		{
			if (collectionCardVisual.IsShown() && collectionCardVisual.CardId == favoriteCoinCardId)
			{
				Actor actor = collectionCardVisual.GetActor();
				component.PinToActor(actor);
				break;
			}
		}
	}

	// Token: 0x060011E5 RID: 4581 RVA: 0x000660FC File Offset: 0x000642FC
	public void UpdateDeckTemplateHeader(GameObject deckTemplateHeader, FormatType pageFormatType)
	{
		if (deckTemplateHeader == null || deckTemplateHeader.GetComponent<Renderer>() == null)
		{
			return;
		}
		Material headerMaterial = this.GetHeaderMaterial(pageFormatType, null);
		deckTemplateHeader.GetComponent<Renderer>().SetMaterial(headerMaterial);
	}

	// Token: 0x060011E6 RID: 4582 RVA: 0x00066138 File Offset: 0x00064338
	public void UpdateDeckTemplatePage(Component deckTemplatePicker)
	{
		this.HideHeroSkinsDecor();
		if (deckTemplatePicker != null && this.m_deckTemplateContainer != null)
		{
			foreach (CollectionCardVisual collectionCardVisual in this.m_collectionCardVisuals)
			{
				collectionCardVisual.Hide();
				collectionCardVisual.SetActors(null, CollectionUtils.ViewMode.CARDS);
			}
			if (this.m_basePage != null)
			{
				MeshRenderer component = this.m_basePage.GetComponent<MeshRenderer>();
				this.m_basePageMaterial = component.GetMaterial();
				component.SetMaterial(this.m_deckTemplatePageMaterial);
			}
			GameUtils.SetParent(deckTemplatePicker, this.m_deckTemplateContainer, false);
			GameUtils.ResetTransform(deckTemplatePicker);
		}
	}

	// Token: 0x060011E7 RID: 4583 RVA: 0x000661F8 File Offset: 0x000643F8
	public override void ShowNoMatchesFound(bool show, CollectionManager.FindCardsResult findResults = null, bool showHints = true)
	{
		this.m_noMatchFoundObject.SetActive(show);
		this.m_noMatchCraftingHintObject.SetActive(false);
		this.m_noMatchSetHintObject.SetActive(false);
		this.m_noMatchManaHintObject.SetActive(false);
		string key = "GLUE_COLLECTION_NO_RESULTS";
		if (show && showHints && findResults != null)
		{
			if (findResults.m_resultsWithoutManaFilterExist)
			{
				this.m_noMatchManaHintObject.SetActive(true);
				key = "GLUE_COLLECTION_NO_RESULTS_IN_SELECTED_COST";
			}
			else if (findResults.m_resultsWithoutSetFilterExist)
			{
				this.m_noMatchSetHintObject.SetActive(true);
				key = "GLUE_COLLECTION_NO_RESULTS_IN_CURRENT_SET";
			}
			else if (findResults.m_resultsUnownedExist)
			{
				this.m_noMatchCraftingHintObject.SetActive(true);
				key = "GLUE_COLLECTION_NO_RESULTS_BUT_CRAFTABLE";
			}
			else if (findResults.m_resultsInWildExist)
			{
				key = "GLUE_COLLECTION_NO_RESULTS_IN_STANDARD";
			}
		}
		this.m_noMatchExplanationText.Text = GameStrings.Get(key);
	}

	// Token: 0x060011E8 RID: 4584 RVA: 0x000662B6 File Offset: 0x000644B6
	public void HideHeroSkinsDecor()
	{
		if (this.m_heroSkinsDecor != null)
		{
			this.m_heroSkinsDecor.SetActive(false);
		}
	}

	// Token: 0x060011E9 RID: 4585 RVA: 0x000662D4 File Offset: 0x000644D4
	public override void UpdateCurrentPageCardLocks(bool playSound = false)
	{
		base.UpdateCurrentPageCardLocks(playSound);
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		foreach (CollectionCardVisual collectionCardVisual in this.m_collectionCardVisuals)
		{
			if (!collectionCardVisual.IsShown() || collectionCardVisual.GetVisualType() != CollectionUtils.ViewMode.CARDS)
			{
				collectionCardVisual.ShowLock(CollectionCardVisual.LockType.NONE);
			}
			else
			{
				Actor actor = collectionCardVisual.GetActor();
				string cardId = actor.GetEntityDef().GetCardId();
				TAG_PREMIUM premium = actor.GetPremium();
				CollectibleCard card = CollectionManager.Get().GetCard(cardId, premium);
				if (card.OwnedCount <= 0)
				{
					collectionCardVisual.ShowLock(CollectionCardVisual.LockType.NONE);
				}
				else
				{
					DeckRuleset deckRuleset = CollectionManager.Get().GetDeckRuleset();
					List<RuleInvalidReason> list;
					List<DeckRule> list2;
					if (deckRuleset == null || deckRuleset.CanAddToDeck(actor.GetEntityDef(), premium, editedDeck, out list, out list2, Array.Empty<DeckRule.RuleType>()))
					{
						collectionCardVisual.ShowLock(CollectionCardVisual.LockType.NONE, null, playSound);
					}
					else
					{
						string displayError = list[0].DisplayError;
						CollectionCardVisual.LockType lockType;
						if (list2[0].Type == DeckRule.RuleType.IS_CARD_PLAYABLE)
						{
							lockType = CollectionCardVisual.LockType.NOT_PLAYABLE;
						}
						else if (list2[0].Type == DeckRule.RuleType.PLAYER_OWNS_EACH_COPY)
						{
							lockType = CollectionCardVisual.LockType.NO_MORE_INSTANCES;
						}
						else if (list2[0].Type == DeckRule.RuleType.COUNT_COPIES_OF_EACH_CARD)
						{
							lockType = CollectionCardVisual.LockType.MAX_COPIES_IN_DECK;
						}
						else
						{
							lockType = CollectionCardVisual.LockType.BANNED;
						}
						if (list2.Count > 1)
						{
							int num = list2.FindIndex((DeckRule r) => r.Type == DeckRule.RuleType.PLAYER_OWNS_EACH_COPY);
							if (num >= 0)
							{
								int cardCountAllMatchingSlots = editedDeck.GetCardCountAllMatchingSlots(cardId, TAG_PREMIUM.DIAMOND);
								int cardCountAllMatchingSlots2 = editedDeck.GetCardCountAllMatchingSlots(cardId, TAG_PREMIUM.GOLDEN);
								int cardCountAllMatchingSlots3 = editedDeck.GetCardCountAllMatchingSlots(cardId, TAG_PREMIUM.NORMAL);
								int ownedCount = card.OwnedCount;
								bool flag = false;
								switch (premium)
								{
								case TAG_PREMIUM.NORMAL:
									if (cardCountAllMatchingSlots3 > ownedCount)
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
								case TAG_PREMIUM.DIAMOND:
									if (cardCountAllMatchingSlots2 + cardCountAllMatchingSlots3 > 0)
									{
										flag = true;
									}
									break;
								}
								if (flag)
								{
									lockType = CollectionCardVisual.LockType.NO_MORE_INSTANCES;
									displayError = list[num].DisplayError;
								}
							}
						}
						collectionCardVisual.ShowLock(lockType, displayError, playSound);
					}
				}
			}
		}
	}

	// Token: 0x060011EA RID: 4586 RVA: 0x000664F4 File Offset: 0x000646F4
	public void SetIsWild(bool isWild)
	{
		if (isWild == this.m_isWild)
		{
			return;
		}
		this.m_isWild = isWild;
		if (this.m_pageFlavorHeader != null)
		{
			this.m_pageFlavorHeader.GetComponent<Renderer>().SetMaterial(isWild ? this.m_wildHeaderMaterial : this.m_headerMaterial);
		}
		if (this.m_pageCountText != null)
		{
			this.m_pageCountText.TextColor = ((CollectionManager.Get().GetThemeShowing(null) == FormatType.FT_WILD) ? this.m_wildTextColor : this.m_textColor);
		}
		this.m_basePageRenderer.SetMaterial(isWild ? this.m_wildPageMaterial : this.m_pageMaterial);
	}

	// Token: 0x060011EB RID: 4587 RVA: 0x00066594 File Offset: 0x00064794
	public void SetPageType(FormatType inputFormatType)
	{
		if (inputFormatType == this.m_pageFormatType)
		{
			return;
		}
		this.m_pageFormatType = inputFormatType;
		if (this.m_pageFlavorHeader != null)
		{
			Material headerMaterial = this.GetHeaderMaterial(inputFormatType, null);
			if (headerMaterial != null)
			{
				this.m_pageFlavorHeader.GetComponent<Renderer>().SetMaterial(headerMaterial);
			}
		}
		if (this.m_pageCountText != null)
		{
			this.m_pageCountText.TextColor = this.GetTextColor(inputFormatType, this.m_textColor);
		}
		Material pageMaterial = this.GetPageMaterial(inputFormatType, null);
		if (pageMaterial != null)
		{
			this.m_basePageRenderer.SetMaterial(pageMaterial);
		}
	}

	// Token: 0x060011EC RID: 4588 RVA: 0x00066627 File Offset: 0x00064827
	public void SetPageTextColor()
	{
		if (this.m_pageNameText != null)
		{
			this.m_pageNameText.TextColor = this.GetTitleTextColor(CollectionManager.Get().GetThemeShowing(null), this.m_textColor);
		}
	}

	// Token: 0x060011ED RID: 4589 RVA: 0x0006665C File Offset: 0x0006485C
	public void SetClass(TAG_CLASS? classTag)
	{
		if (classTag == null)
		{
			base.SetPageNameText("");
			if (this.m_pageFlavorHeader != null)
			{
				this.m_pageFlavorHeader.SetActive(false);
			}
			return;
		}
		TAG_CLASS value = classTag.Value;
		base.SetPageNameText(GameStrings.GetClassName(value));
		CollectionPageDisplay.SetPageFlavorTextures(this.m_pageFlavorHeader, CollectionPageDisplay.TagClassToHeaderClass(value));
	}

	// Token: 0x060011EE RID: 4590 RVA: 0x000666BD File Offset: 0x000648BD
	public void SetHeroSkins()
	{
		base.SetPageNameText(GameStrings.Get("GLUE_COLLECTION_MANAGER_HERO_SKINS_TITLE"));
		CollectionPageDisplay.SetPageFlavorTextures(this.m_pageFlavorHeader, CollectionPageDisplay.HEADER_CLASS.HEROSKINS);
	}

	// Token: 0x060011EF RID: 4591 RVA: 0x000666DC File Offset: 0x000648DC
	public void SetCardBacks()
	{
		base.SetPageNameText(GameStrings.Get("GLUE_COLLECTION_MANAGER_CARD_BACKS_TITLE"));
		CollectionPageDisplay.SetPageFlavorTextures(this.m_pageFlavorHeader, CollectionPageDisplay.HEADER_CLASS.CARDBACKS);
	}

	// Token: 0x060011F0 RID: 4592 RVA: 0x000666FB File Offset: 0x000648FB
	public void SetCoins()
	{
		base.SetPageNameText(GameStrings.Get("GLUE_COLLECTION_MANAGER_COIN_TITLE"));
		CollectionPageDisplay.SetPageFlavorTextures(this.m_pageFlavorHeader, CollectionPageDisplay.HEADER_CLASS.COINS);
	}

	// Token: 0x060011F1 RID: 4593 RVA: 0x0006671A File Offset: 0x0006491A
	public void SetDeckTemplates()
	{
		base.SetPageNameText(string.Empty);
		if (this.m_pageFlavorHeader != null)
		{
			this.m_pageFlavorHeader.SetActive(false);
		}
	}

	// Token: 0x060011F2 RID: 4594 RVA: 0x00066744 File Offset: 0x00064944
	public void SetMassDisenchant()
	{
		base.SetPageNameText(string.Empty);
		if (this.m_heroSkinsDecor != null)
		{
			this.m_heroSkinsDecor.SetActive(false);
		}
		if (this.m_pageFlavorHeader != null)
		{
			this.m_pageFlavorHeader.SetActive(false);
		}
		if (this.m_favoriteBanner != null)
		{
			this.m_favoriteBanner.GetComponent<FavoriteBanner>().SetActive(false);
		}
	}

	// Token: 0x060011F3 RID: 4595 RVA: 0x000667B0 File Offset: 0x000649B0
	public TAG_CLASS? GetFirstCardClass()
	{
		if (this.m_collectionCardVisuals.Count == 0)
		{
			return null;
		}
		CollectionCardVisual collectionCardVisual = this.m_collectionCardVisuals[0];
		if (!collectionCardVisual.IsShown())
		{
			return null;
		}
		Actor actor = collectionCardVisual.GetActor();
		if (!actor.IsShown())
		{
			return null;
		}
		EntityDef entityDef = actor.GetEntityDef();
		if (entityDef == null)
		{
			return null;
		}
		return new TAG_CLASS?(entityDef.GetClass());
	}

	// Token: 0x060011F4 RID: 4596 RVA: 0x0006682B File Offset: 0x00064A2B
	private MassDisenchant GetMassDisenchantVisual()
	{
		if (MassDisenchant.Get() == null)
		{
			return null;
		}
		this.m_massDisenchantVisual = MassDisenchant.Get();
		GameUtils.SetParent(this.m_massDisenchantVisual, base.gameObject, false);
		return this.m_massDisenchantVisual;
	}

	// Token: 0x060011F5 RID: 4597 RVA: 0x00066860 File Offset: 0x00064A60
	private Material GetHeaderMaterial(FormatType formatType, Material defaultMaterial)
	{
		Material result;
		if (!new Map<FormatType, Material>
		{
			{
				FormatType.FT_STANDARD,
				this.m_headerMaterial
			},
			{
				FormatType.FT_WILD,
				this.m_wildHeaderMaterial
			},
			{
				FormatType.FT_CLASSIC,
				this.m_classicHeaderMaterial
			}
		}.TryGetValue(formatType, out result))
		{
			result = defaultMaterial;
		}
		return result;
	}

	// Token: 0x060011F6 RID: 4598 RVA: 0x000668A8 File Offset: 0x00064AA8
	private Material GetPageMaterial(FormatType formatType, Material defaultMaterial)
	{
		Material result;
		if (!new Map<FormatType, Material>
		{
			{
				FormatType.FT_STANDARD,
				this.m_pageMaterial
			},
			{
				FormatType.FT_WILD,
				this.m_wildPageMaterial
			},
			{
				FormatType.FT_CLASSIC,
				this.m_classicPageMaterial
			}
		}.TryGetValue(formatType, out result))
		{
			result = defaultMaterial;
		}
		return result;
	}

	// Token: 0x060011F7 RID: 4599 RVA: 0x000668F0 File Offset: 0x00064AF0
	private Color GetTextColor(FormatType formatType, Color defaultColor)
	{
		Color result;
		if (!new Map<FormatType, Color>
		{
			{
				FormatType.FT_STANDARD,
				this.m_textColor
			},
			{
				FormatType.FT_WILD,
				this.m_wildTextColor
			},
			{
				FormatType.FT_CLASSIC,
				this.m_classicTextColor
			}
		}.TryGetValue(formatType, out result))
		{
			result = defaultColor;
		}
		return result;
	}

	// Token: 0x060011F8 RID: 4600 RVA: 0x00066938 File Offset: 0x00064B38
	private Color GetTitleTextColor(FormatType formatType, Color defaultColor)
	{
		Color result;
		if (!new Map<FormatType, Color>
		{
			{
				FormatType.FT_STANDARD,
				this.m_standardTitleTextColor
			},
			{
				FormatType.FT_WILD,
				this.m_wildTitleTextColor
			},
			{
				FormatType.FT_CLASSIC,
				this.m_classicTitleTextColor
			}
		}.TryGetValue(formatType, out result))
		{
			result = defaultColor;
		}
		return result;
	}

	// Token: 0x060011F9 RID: 4601 RVA: 0x00066980 File Offset: 0x00064B80
	public static CollectionPageDisplay.HEADER_CLASS TagClassToHeaderClass(TAG_CLASS classTag)
	{
		string value = classTag.ToString();
		if (Enum.IsDefined(typeof(CollectionPageDisplay.HEADER_CLASS), value))
		{
			return (CollectionPageDisplay.HEADER_CLASS)Enum.Parse(typeof(CollectionPageDisplay.HEADER_CLASS), value);
		}
		return CollectionPageDisplay.HEADER_CLASS.INVALID;
	}

	// Token: 0x060011FA RID: 4602 RVA: 0x000669C4 File Offset: 0x00064BC4
	public static void SetPageFlavorTextures(GameObject header, CollectionPageDisplay.HEADER_CLASS headerClass)
	{
		if (header == null)
		{
			return;
		}
		float x = ((float)headerClass < 8f) ? 0f : 0.5f;
		float y = -(float)headerClass / 8f;
		CollectiblePageDisplay.SetPageFlavorTextures(header, new UnityEngine.Vector2(x, y));
	}

	// Token: 0x04000B7C RID: 2940
	public GameObject m_favoriteBanner;

	// Token: 0x04000B7D RID: 2941
	public GameObject m_heroSkinsDecor;

	// Token: 0x04000B7E RID: 2942
	public GameObject[] m_heroSkinFrames;

	// Token: 0x04000B7F RID: 2943
	public GameObject m_deckTemplateContainer;

	// Token: 0x04000B80 RID: 2944
	public GameObject m_noMatchFoundObject;

	// Token: 0x04000B81 RID: 2945
	public UberText m_noMatchExplanationText;

	// Token: 0x04000B82 RID: 2946
	public GameObject m_noMatchSetHintObject;

	// Token: 0x04000B83 RID: 2947
	public GameObject m_noMatchManaHintObject;

	// Token: 0x04000B84 RID: 2948
	public GameObject m_noMatchCraftingHintObject;

	// Token: 0x04000B85 RID: 2949
	public Material m_deckTemplatePageMaterial;

	// Token: 0x04000B86 RID: 2950
	public Color m_standardTitleTextColor;

	// Token: 0x04000B87 RID: 2951
	public Material m_wildHeaderMaterial;

	// Token: 0x04000B88 RID: 2952
	public Material m_wildPageMaterial;

	// Token: 0x04000B89 RID: 2953
	public Color m_wildTextColor;

	// Token: 0x04000B8A RID: 2954
	public Color m_wildTitleTextColor;

	// Token: 0x04000B8B RID: 2955
	public Material m_classicHeaderMaterial;

	// Token: 0x04000B8C RID: 2956
	public Material m_classicPageMaterial;

	// Token: 0x04000B8D RID: 2957
	public Color m_classicTextColor;

	// Token: 0x04000B8E RID: 2958
	public Color m_classicTitleTextColor;

	// Token: 0x04000B8F RID: 2959
	public FormatType m_pageFormatType;

	// Token: 0x04000B90 RID: 2960
	private bool m_isWild;

	// Token: 0x04000B91 RID: 2961
	private MassDisenchant m_massDisenchantVisual;

	// Token: 0x0200148E RID: 5262
	public enum HEADER_CLASS
	{
		// Token: 0x0400AA20 RID: 43552
		INVALID,
		// Token: 0x0400AA21 RID: 43553
		SHAMAN,
		// Token: 0x0400AA22 RID: 43554
		PALADIN,
		// Token: 0x0400AA23 RID: 43555
		MAGE,
		// Token: 0x0400AA24 RID: 43556
		DRUID,
		// Token: 0x0400AA25 RID: 43557
		HUNTER,
		// Token: 0x0400AA26 RID: 43558
		ROGUE,
		// Token: 0x0400AA27 RID: 43559
		WARRIOR,
		// Token: 0x0400AA28 RID: 43560
		PRIEST,
		// Token: 0x0400AA29 RID: 43561
		WARLOCK,
		// Token: 0x0400AA2A RID: 43562
		HEROSKINS,
		// Token: 0x0400AA2B RID: 43563
		CARDBACKS,
		// Token: 0x0400AA2C RID: 43564
		DEMONHUNTER,
		// Token: 0x0400AA2D RID: 43565
		COINS
	}
}
