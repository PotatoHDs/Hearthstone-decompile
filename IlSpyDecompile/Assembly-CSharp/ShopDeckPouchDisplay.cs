using System.Collections.Generic;
using Blizzard.T5.AssetManager;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

public class ShopDeckPouchDisplay : MonoBehaviour
{
	public Widget deckWidget;

	public RewardItemDataModel m_rewardItemDataModel;

	public Material portraitMaterial;

	private Material m_temporaryPortraitMaterial;

	private AssetHandle<Texture> m_portraitTextureHandle;

	private AssetHandle<Material> m_portraitMaterialHandle;

	private int m_currentDisplayCardId;

	public void OnDestroy()
	{
		SafelyDisposeTempPortrait();
		AssetHandle.SafeDispose(ref m_portraitMaterialHandle);
		AssetHandle.SafeDispose(ref m_portraitTextureHandle);
	}

	private void SafelyDisposeTempPortrait()
	{
		if (m_temporaryPortraitMaterial != null)
		{
			Object.Destroy(m_temporaryPortraitMaterial);
			m_temporaryPortraitMaterial = null;
		}
	}

	public void SetRewardItem()
	{
		if (deckWidget == null)
		{
			Log.Store.PrintWarning("[ShopDeckPouchDisplay.SetRewardItem] DeckWidget reference is null!");
		}
		else
		{
			deckWidget.RegisterDoneChangingStatesListener(SetupRewardOnWidgetReady);
		}
	}

	private void SetupRewardOnWidgetReady(object _)
	{
		deckWidget.RemoveDoneChangingStatesListener(SetupRewardOnWidgetReady);
		SetupRewardFromWidget();
	}

	private void SetupRewardFromWidget()
	{
		m_rewardItemDataModel = deckWidget.GetDataModel<RewardItemDataModel>();
		RewardItemDataModel rewardItemDataModel = m_rewardItemDataModel;
		if (rewardItemDataModel != null && rewardItemDataModel.ItemType == RewardItemType.SELLABLE_DECK)
		{
			int itemId = m_rewardItemDataModel.ItemId;
			SellableDeckDbfRecord record = GameDbf.SellableDeck.GetRecord(itemId);
			if (record == null)
			{
				Log.Store.PrintWarning("[ShopDeckPouchDisplay.SetRewardItem] Failed to find sellable deck DB record {0}!", itemId);
			}
			else if (record.DeckTemplateRecord == null || record.DeckTemplateRecord.DeckRecord == null)
			{
				Log.Store.PrintWarning("[ShopDeckPouchDisplay.SetRewardItem] The DB record {0} does NOT have a deck template with a valid deck record!", record.ID);
			}
			else
			{
				SetDeckPouchData(deckWidget, record.DeckTemplateRecord);
			}
		}
	}

	public void SetDeckPouchData(Widget widget, DeckTemplateDbfRecord record)
	{
		if (widget == null)
		{
			Log.Store.PrintWarning("[ShopDeckPouchDisplay.SetDeckPouchData] Deck widget is null!");
			return;
		}
		DeckPouchDataModel dataModel = CreateDeckPouchDataModelFromDeckTemplate(record);
		widget.BindDataModel(dataModel);
	}

	public DeckPouchDataModel CreateDeckPouchDataModelFromDeckTemplate(DeckTemplateDbfRecord record)
	{
		DeckDbfRecord deckDbfRecord = record?.DeckRecord;
		List<DeckCardDbfRecord> list = deckDbfRecord?.Cards;
		if (list == null)
		{
			return new DeckPouchDataModel();
		}
		int[] rarityCounts = GetRarityCounts(list);
		DeckPouchDataModel deckPouchDataModel = new DeckPouchDataModel();
		deckPouchDataModel.Pouch = new AdventureLoadoutOptionDataModel
		{
			Name = deckDbfRecord.Name,
			DisplayTexture = GetPortraitMaterialFromDeckTemplateRecord(record),
			DisplayColor = CollectionPageManager.ColorForClass((TAG_CLASS)record.ClassId)
		};
		deckPouchDataModel.Details = new DeckDetailsDataModel
		{
			Product = new ProductDataModel
			{
				DescriptionHeader = GameStrings.Format("GLUE_COLLECTION_NEW_DECK_DETAIL_HEADER", GameStrings.GetClassName((TAG_CLASS)record.ClassId)),
				Description = GameStrings.Format("GLUE_COLLECTION_NEW_DECK_DETAIL_DESC", rarityCounts[5], rarityCounts[4], rarityCounts[3], rarityCounts[1])
			}
		};
		deckPouchDataModel.Class = (TAG_CLASS)record.ClassId;
		return deckPouchDataModel;
	}

	private Material GetPortraitMaterialFromDeckTemplateRecord(DeckTemplateDbfRecord record)
	{
		if (portraitMaterial != null && record.DisplayCardId != 0)
		{
			using (DefLoader.DisposableCardDef disposableCardDef = DefLoader.Get().GetCardDef(record.DisplayCardId))
			{
				if (m_temporaryPortraitMaterial == null || m_currentDisplayCardId != record.DisplayCardId)
				{
					SafelyDisposeTempPortrait();
					m_temporaryPortraitMaterial = new Material(portraitMaterial);
				}
				AssetHandle.Set(ref m_portraitTextureHandle, disposableCardDef?.CardDef.GetPortraitTextureHandle());
				m_temporaryPortraitMaterial.mainTexture = m_portraitTextureHandle;
				m_currentDisplayCardId = record.DisplayCardId;
			}
			return m_temporaryPortraitMaterial;
		}
		AssetLoader.Get().LoadAsset(ref m_portraitMaterialHandle, record.DisplayTexture);
		return m_portraitMaterialHandle;
	}

	public static int[] GetRarityCounts(List<DeckCardDbfRecord> cards)
	{
		DefLoader defLoader = DefLoader.Get();
		int[] rarityCounts = new int[6];
		cards.ForEach(delegate(DeckCardDbfRecord r)
		{
			rarityCounts[(int)(defLoader.GetEntityDef(r.CardId)?.GetRarity() ?? TAG_RARITY.INVALID)]++;
		});
		return rarityCounts;
	}
}
