using System;
using Blizzard.T5.Jobs;
using Hearthstone.Core;
using Hearthstone.DataModels;
using UnityEngine;

namespace Hearthstone.UI
{
	[AddComponentMenu("")]
	[WidgetBehaviorDescription(Path = "Hearthstone/CardTile", UniqueWithinCategory = "asset")]
	public class CardTile : CustomWidgetBehavior
	{
		private const string DEFAULT_CARD_ID = "EX1_009";

		public GameObject m_legendaryVfx;

		private CollectionDeckTileActor m_actor;

		private string m_displayedCardId;

		private TAG_PREMIUM m_displayedPremium;

		private int m_displayedCount;

		private bool m_displayedSelected;

		protected override void OnInitialize()
		{
			base.OnInitialize();
			HearthstoneServices.InitializeDynamicServicesIfEditor(out var serviceDependencies, typeof(IAssetLoader), typeof(GameDbf), typeof(WidgetRunner), typeof(IAliasedAssetResolver));
			Processor.QueueJob(HearthstoneJobs.CreateJobFromAction("CardTile.CreatePreviewableObject", CreatePreviewableObject, JobFlags.StartImmediately, serviceDependencies));
		}

		private CardTileDataModel GetDesiredData()
		{
			CardTileDataModel cardTileDataModel = null;
			if (!GetDataModel(262, out var dataModel))
			{
				cardTileDataModel = new CardTileDataModel
				{
					CardId = "EX1_009",
					Count = 1
				};
			}
			else
			{
				cardTileDataModel = dataModel as CardTileDataModel;
				if (string.IsNullOrEmpty(cardTileDataModel.CardId))
				{
					cardTileDataModel.CardId = "EX1_009";
				}
			}
			return cardTileDataModel;
		}

		private void CreatePreviewableObject()
		{
			if (DefLoader.Get().GetAllEntityDefs().Count == 0)
			{
				DefLoader.Get().LoadAllEntityDefs();
			}
			CreatePreviewableObject(delegate(IPreviewableObject previewable, Action<GameObject> callback)
			{
				CardTileDataModel desiredData2 = GetDesiredData();
				string cardId = desiredData2.CardId;
				EntityDef entityDef = DefLoader.Get().GetEntityDef(cardId);
				if (entityDef != null)
				{
					entityDef = entityDef.Clone();
					GameObject gameObject = AssetLoader.Get().InstantiatePrefab("DeckCardBar.prefab:c2bab6eea6c3a3a4d90dcd7572075291", AssetLoadingOptions.IgnorePrefabPosition);
					m_actor = gameObject.GetComponent<CollectionDeckTileActor>();
					if (!Application.isPlaying)
					{
						m_actor.Awake();
					}
					m_actor.SetPremium(desiredData2.Premium);
					m_actor.SetEntityDef(entityDef);
					m_actor.SetGhosted(CollectionDeckTileActor.GhostedState.NONE);
					bool flag = entityDef.GetRarity() == TAG_RARITY.LEGENDARY;
					m_actor.UpdateDeckCardProperties(flag, isMultiCard: false, desiredData2.Count, useSliderAnimations: false);
					m_legendaryVfx.SetActive(flag);
					using (DefLoader.DisposableCardDef disposableCardDef = DefLoader.Get().GetCardDef(cardId))
					{
						m_actor.SetCardDef(disposableCardDef);
						m_actor.UpdateAllComponents();
						m_actor.UpdateMaterial(disposableCardDef.CardDef.GetDeckCardBarPortrait());
					}
					m_actor.m_highlight.SetActive(desiredData2.Selected);
					m_actor.UpdateGhostTileEffect();
					m_displayedCardId = cardId;
					m_displayedPremium = desiredData2.Premium;
					m_displayedCount = desiredData2.Count;
					m_displayedSelected = desiredData2.Selected;
					m_actor.transform.SetParent(base.transform, worldPositionStays: false);
					m_actor.transform.localPosition = Vector3.zero;
					callback(m_actor.gameObject);
				}
			}, delegate
			{
				CardTileDataModel desiredData = GetDesiredData();
				return desiredData.CardId != m_displayedCardId || desiredData.Premium != m_displayedPremium || desiredData.Count != m_displayedCount || desiredData.Selected != m_displayedSelected;
			});
		}
	}
}
