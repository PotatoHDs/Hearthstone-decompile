using System;
using Blizzard.T5.Jobs;
using Hearthstone.Core;
using Hearthstone.DataModels;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02000FDD RID: 4061
	[AddComponentMenu("")]
	[WidgetBehaviorDescription(Path = "Hearthstone/CardTile", UniqueWithinCategory = "asset")]
	public class CardTile : CustomWidgetBehavior
	{
		// Token: 0x0600B0CB RID: 45259 RVA: 0x00369C74 File Offset: 0x00367E74
		protected override void OnInitialize()
		{
			base.OnInitialize();
			IJobDependency[] dependencies;
			HearthstoneServices.InitializeDynamicServicesIfEditor(out dependencies, new Type[]
			{
				typeof(IAssetLoader),
				typeof(GameDbf),
				typeof(WidgetRunner),
				typeof(IAliasedAssetResolver)
			});
			Processor.QueueJob(HearthstoneJobs.CreateJobFromAction("CardTile.CreatePreviewableObject", new Action(this.CreatePreviewableObject), JobFlags.StartImmediately, dependencies));
		}

		// Token: 0x0600B0CC RID: 45260 RVA: 0x00369CE8 File Offset: 0x00367EE8
		private CardTileDataModel GetDesiredData()
		{
			IDataModel dataModel;
			CardTileDataModel cardTileDataModel;
			if (!base.GetDataModel(262, out dataModel))
			{
				cardTileDataModel = new CardTileDataModel
				{
					CardId = "EX1_009",
					Count = 1
				};
			}
			else
			{
				cardTileDataModel = (dataModel as CardTileDataModel);
				if (string.IsNullOrEmpty(cardTileDataModel.CardId))
				{
					cardTileDataModel.CardId = "EX1_009";
				}
			}
			return cardTileDataModel;
		}

		// Token: 0x0600B0CD RID: 45261 RVA: 0x00369D40 File Offset: 0x00367F40
		private void CreatePreviewableObject()
		{
			if (DefLoader.Get().GetAllEntityDefs().Count == 0)
			{
				DefLoader.Get().LoadAllEntityDefs();
			}
			base.CreatePreviewableObject(delegate(CustomWidgetBehavior.IPreviewableObject previewable, Action<GameObject> callback)
			{
				CardTileDataModel desiredData = this.GetDesiredData();
				string cardId = desiredData.CardId;
				EntityDef entityDef = DefLoader.Get().GetEntityDef(cardId);
				if (entityDef == null)
				{
					return;
				}
				entityDef = entityDef.Clone();
				GameObject gameObject = AssetLoader.Get().InstantiatePrefab("DeckCardBar.prefab:c2bab6eea6c3a3a4d90dcd7572075291", AssetLoadingOptions.IgnorePrefabPosition);
				this.m_actor = gameObject.GetComponent<CollectionDeckTileActor>();
				if (!Application.isPlaying)
				{
					this.m_actor.Awake();
				}
				this.m_actor.SetPremium(desiredData.Premium);
				this.m_actor.SetEntityDef(entityDef);
				this.m_actor.SetGhosted(CollectionDeckTileActor.GhostedState.NONE);
				bool flag = entityDef.GetRarity() == TAG_RARITY.LEGENDARY;
				this.m_actor.UpdateDeckCardProperties(flag, false, desiredData.Count, false);
				this.m_legendaryVfx.SetActive(flag);
				using (DefLoader.DisposableCardDef cardDef = DefLoader.Get().GetCardDef(cardId, null))
				{
					this.m_actor.SetCardDef(cardDef);
					this.m_actor.UpdateAllComponents();
					this.m_actor.UpdateMaterial(cardDef.CardDef.GetDeckCardBarPortrait());
				}
				this.m_actor.m_highlight.SetActive(desiredData.Selected);
				this.m_actor.UpdateGhostTileEffect();
				this.m_displayedCardId = cardId;
				this.m_displayedPremium = desiredData.Premium;
				this.m_displayedCount = desiredData.Count;
				this.m_displayedSelected = desiredData.Selected;
				this.m_actor.transform.SetParent(base.transform, false);
				this.m_actor.transform.localPosition = Vector3.zero;
				callback(this.m_actor.gameObject);
			}, delegate(CustomWidgetBehavior.IPreviewableObject o)
			{
				CardTileDataModel desiredData = this.GetDesiredData();
				return desiredData.CardId != this.m_displayedCardId || desiredData.Premium != this.m_displayedPremium || desiredData.Count != this.m_displayedCount || desiredData.Selected != this.m_displayedSelected;
			}, null);
		}

		// Token: 0x04009566 RID: 38246
		private const string DEFAULT_CARD_ID = "EX1_009";

		// Token: 0x04009567 RID: 38247
		public GameObject m_legendaryVfx;

		// Token: 0x04009568 RID: 38248
		private CollectionDeckTileActor m_actor;

		// Token: 0x04009569 RID: 38249
		private string m_displayedCardId;

		// Token: 0x0400956A RID: 38250
		private TAG_PREMIUM m_displayedPremium;

		// Token: 0x0400956B RID: 38251
		private int m_displayedCount;

		// Token: 0x0400956C RID: 38252
		private bool m_displayedSelected;
	}
}
