using System;
using System.Collections.Generic;
using Blizzard.T5.AssetManager;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x020006BB RID: 1723
public class ShopDeckPouchDisplay : MonoBehaviour
{
	// Token: 0x0600609A RID: 24730 RVA: 0x001F77CD File Offset: 0x001F59CD
	public void OnDestroy()
	{
		this.SafelyDisposeTempPortrait();
		AssetHandle.SafeDispose<Material>(ref this.m_portraitMaterialHandle);
		AssetHandle.SafeDispose<Texture>(ref this.m_portraitTextureHandle);
	}

	// Token: 0x0600609B RID: 24731 RVA: 0x001F77EB File Offset: 0x001F59EB
	private void SafelyDisposeTempPortrait()
	{
		if (this.m_temporaryPortraitMaterial != null)
		{
			UnityEngine.Object.Destroy(this.m_temporaryPortraitMaterial);
			this.m_temporaryPortraitMaterial = null;
		}
	}

	// Token: 0x0600609C RID: 24732 RVA: 0x001F780D File Offset: 0x001F5A0D
	public void SetRewardItem()
	{
		if (this.deckWidget == null)
		{
			Log.Store.PrintWarning("[ShopDeckPouchDisplay.SetRewardItem] DeckWidget reference is null!", Array.Empty<object>());
			return;
		}
		this.deckWidget.RegisterDoneChangingStatesListener(new Action<object>(this.SetupRewardOnWidgetReady), null, true, false);
	}

	// Token: 0x0600609D RID: 24733 RVA: 0x001F784C File Offset: 0x001F5A4C
	private void SetupRewardOnWidgetReady(object _)
	{
		this.deckWidget.RemoveDoneChangingStatesListener(new Action<object>(this.SetupRewardOnWidgetReady));
		this.SetupRewardFromWidget();
	}

	// Token: 0x0600609E RID: 24734 RVA: 0x001F786C File Offset: 0x001F5A6C
	private void SetupRewardFromWidget()
	{
		this.m_rewardItemDataModel = this.deckWidget.GetDataModel<RewardItemDataModel>();
		RewardItemDataModel rewardItemDataModel = this.m_rewardItemDataModel;
		if (rewardItemDataModel != null && rewardItemDataModel.ItemType == RewardItemType.SELLABLE_DECK)
		{
			int itemId = this.m_rewardItemDataModel.ItemId;
			SellableDeckDbfRecord record = GameDbf.SellableDeck.GetRecord(itemId);
			if (record == null)
			{
				Log.Store.PrintWarning("[ShopDeckPouchDisplay.SetRewardItem] Failed to find sellable deck DB record {0}!", new object[]
				{
					itemId
				});
				return;
			}
			if (record.DeckTemplateRecord == null || record.DeckTemplateRecord.DeckRecord == null)
			{
				Log.Store.PrintWarning("[ShopDeckPouchDisplay.SetRewardItem] The DB record {0} does NOT have a deck template with a valid deck record!", new object[]
				{
					record.ID
				});
				return;
			}
			this.SetDeckPouchData(this.deckWidget, record.DeckTemplateRecord);
		}
	}

	// Token: 0x0600609F RID: 24735 RVA: 0x001F792C File Offset: 0x001F5B2C
	public void SetDeckPouchData(Widget widget, DeckTemplateDbfRecord record)
	{
		if (widget == null)
		{
			Log.Store.PrintWarning("[ShopDeckPouchDisplay.SetDeckPouchData] Deck widget is null!", Array.Empty<object>());
			return;
		}
		DeckPouchDataModel dataModel = this.CreateDeckPouchDataModelFromDeckTemplate(record);
		widget.BindDataModel(dataModel, false);
	}

	// Token: 0x060060A0 RID: 24736 RVA: 0x001F7968 File Offset: 0x001F5B68
	public DeckPouchDataModel CreateDeckPouchDataModelFromDeckTemplate(DeckTemplateDbfRecord record)
	{
		DeckDbfRecord deckDbfRecord = (record != null) ? record.DeckRecord : null;
		List<DeckCardDbfRecord> list = (deckDbfRecord != null) ? deckDbfRecord.Cards : null;
		if (list == null)
		{
			return new DeckPouchDataModel();
		}
		int[] rarityCounts = ShopDeckPouchDisplay.GetRarityCounts(list);
		return new DeckPouchDataModel
		{
			Pouch = new AdventureLoadoutOptionDataModel
			{
				Name = deckDbfRecord.Name,
				DisplayTexture = this.GetPortraitMaterialFromDeckTemplateRecord(record),
				DisplayColor = CollectionPageManager.ColorForClass((TAG_CLASS)record.ClassId)
			},
			Details = new DeckDetailsDataModel
			{
				Product = new ProductDataModel
				{
					DescriptionHeader = GameStrings.Format("GLUE_COLLECTION_NEW_DECK_DETAIL_HEADER", new object[]
					{
						GameStrings.GetClassName((TAG_CLASS)record.ClassId)
					}),
					Description = GameStrings.Format("GLUE_COLLECTION_NEW_DECK_DETAIL_DESC", new object[]
					{
						rarityCounts[5],
						rarityCounts[4],
						rarityCounts[3],
						rarityCounts[1]
					})
				}
			},
			Class = (TAG_CLASS)record.ClassId
		};
	}

	// Token: 0x060060A1 RID: 24737 RVA: 0x001F7A74 File Offset: 0x001F5C74
	private Material GetPortraitMaterialFromDeckTemplateRecord(DeckTemplateDbfRecord record)
	{
		if (this.portraitMaterial != null && record.DisplayCardId != 0)
		{
			using (DefLoader.DisposableCardDef cardDef = DefLoader.Get().GetCardDef(record.DisplayCardId))
			{
				if (this.m_temporaryPortraitMaterial == null || this.m_currentDisplayCardId != record.DisplayCardId)
				{
					this.SafelyDisposeTempPortrait();
					this.m_temporaryPortraitMaterial = new Material(this.portraitMaterial);
				}
				AssetHandle.Set<Texture>(ref this.m_portraitTextureHandle, (cardDef != null) ? cardDef.CardDef.GetPortraitTextureHandle() : null);
				this.m_temporaryPortraitMaterial.mainTexture = this.m_portraitTextureHandle;
				this.m_currentDisplayCardId = record.DisplayCardId;
			}
			return this.m_temporaryPortraitMaterial;
		}
		AssetLoader.Get().LoadAsset<Material>(ref this.m_portraitMaterialHandle, record.DisplayTexture, AssetLoadingOptions.None);
		return this.m_portraitMaterialHandle;
	}

	// Token: 0x060060A2 RID: 24738 RVA: 0x001F7B6C File Offset: 0x001F5D6C
	public static int[] GetRarityCounts(List<DeckCardDbfRecord> cards)
	{
		DefLoader defLoader = DefLoader.Get();
		int[] rarityCounts = new int[6];
		cards.ForEach(delegate(DeckCardDbfRecord r)
		{
			int[] rarityCounts = rarityCounts;
			EntityDef entityDef = defLoader.GetEntityDef(r.CardId, true);
			rarityCounts[(int)((entityDef != null) ? entityDef.GetRarity() : TAG_RARITY.INVALID)]++;
		});
		return rarityCounts;
	}

	// Token: 0x040050D5 RID: 20693
	public Widget deckWidget;

	// Token: 0x040050D6 RID: 20694
	public RewardItemDataModel m_rewardItemDataModel;

	// Token: 0x040050D7 RID: 20695
	public Material portraitMaterial;

	// Token: 0x040050D8 RID: 20696
	private Material m_temporaryPortraitMaterial;

	// Token: 0x040050D9 RID: 20697
	private AssetHandle<Texture> m_portraitTextureHandle;

	// Token: 0x040050DA RID: 20698
	private AssetHandle<Material> m_portraitMaterialHandle;

	// Token: 0x040050DB RID: 20699
	private int m_currentDisplayCardId;
}
