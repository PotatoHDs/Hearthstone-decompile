using System;
using System.Linq;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using Hearthstone.Core;
using Hearthstone.DataModels;
using UnityEngine;
using UnityEngine.Rendering;

namespace Hearthstone.UI
{
	// Token: 0x02000FDB RID: 4059
	[AddComponentMenu("")]
	[WidgetBehaviorDescription(Path = "Hearthstone/Card", UniqueWithinCategory = "asset")]
	public class Card : CustomWidgetBehavior
	{
		// Token: 0x140000A6 RID: 166
		// (add) Token: 0x0600B0A8 RID: 45224 RVA: 0x00369104 File Offset: 0x00367304
		// (remove) Token: 0x0600B0A9 RID: 45225 RVA: 0x0036913C File Offset: 0x0036733C
		private event Card.OnCardActorLoadedDelegate OnCardActorLoaded;

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x0600B0AB RID: 45227 RVA: 0x0036917A File Offset: 0x0036737A
		// (set) Token: 0x0600B0AA RID: 45226 RVA: 0x00369171 File Offset: 0x00367371
		[Overridable]
		public TAG_PREMIUM Premium
		{
			get
			{
				return this.m_displayedPremiumTag;
			}
			set
			{
				this.m_displayedPremiumTag = value;
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x0600B0AC RID: 45228 RVA: 0x00369182 File Offset: 0x00367382
		// (set) Token: 0x0600B0AD RID: 45229 RVA: 0x0036918A File Offset: 0x0036738A
		[Overridable]
		public bool ShowShadow
		{
			get
			{
				return this.m_useShadow;
			}
			set
			{
				if (this.m_useShadow != value)
				{
					this.m_useShadow = value;
					this.UpdateActor();
				}
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x0600B0AE RID: 45230 RVA: 0x003691A2 File Offset: 0x003673A2
		// (set) Token: 0x0600B0AF RID: 45231 RVA: 0x003691AA File Offset: 0x003673AA
		[Overridable]
		public bool ShowCustomEffect
		{
			get
			{
				return this.m_showCustomEffect;
			}
			set
			{
				if (this.m_showCustomEffect != value)
				{
					this.m_showCustomEffect = value;
					this.UpdateActor();
				}
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x0600B0B0 RID: 45232 RVA: 0x003691C2 File Offset: 0x003673C2
		// (set) Token: 0x0600B0B1 RID: 45233 RVA: 0x003691CC File Offset: 0x003673CC
		[Overridable]
		public Material CustomEffectMaterial
		{
			get
			{
				return this.m_customEffectMaterial;
			}
			set
			{
				if (value != this.m_customEffectMaterial)
				{
					this.m_customEffectMaterial = value;
					this.m_showCustomEffect = true;
					if (this.m_isShowingCustomEffect && this.m_cardActor != null)
					{
						this.m_isShowingCustomEffect = false;
						this.m_cardActor.DisableMissingCardEffect();
					}
					this.UpdateActor();
				}
			}
		}

		// Token: 0x0600B0B2 RID: 45234 RVA: 0x00369224 File Offset: 0x00367424
		protected virtual void UpdateActor()
		{
			if (this.m_cardActor == null)
			{
				this.m_isShowingShadow = this.m_useShadow;
				this.m_isShowingCustomEffect = this.m_showCustomEffect;
				return;
			}
			this.m_cardActor.ContactShadow(this.m_useShadow);
			bool flag = this.m_useShadow && !this.m_cardActor.HasContactShadowObject();
			ProjectedShadow component = this.m_cardActor.GetComponent<ProjectedShadow>();
			if (component != null)
			{
				if (flag)
				{
					component.m_enabledAlongsideRealtimeShadows = true;
					this.m_cardActor.GetComponentsInChildren<MeshRenderer>().ForEach(delegate(MeshRenderer r)
					{
						r.shadowCastingMode = ShadowCastingMode.Off;
					});
					component.EnableShadow();
				}
				else
				{
					component.DisableShadow();
				}
			}
			this.m_isShowingShadow = this.m_useShadow;
			this.SetUpCustomEffect();
		}

		// Token: 0x0600B0B3 RID: 45235 RVA: 0x003692F4 File Offset: 0x003674F4
		protected void SetUpCustomEffect()
		{
			if (this.m_cardActor == null)
			{
				return;
			}
			if (!Application.IsPlaying(this))
			{
				return;
			}
			if (this.m_showCustomEffect && !this.m_isShowingCustomEffect)
			{
				if (this.m_customEffectMaterial != null)
				{
					this.m_cardActor.SetMissingCardMaterial(this.m_customEffectMaterial);
				}
				this.m_isShowingCustomEffect = true;
				this.m_cardActor.MissingCardEffect(false);
			}
			if (!this.m_showCustomEffect && this.m_isShowingCustomEffect)
			{
				this.m_isShowingCustomEffect = false;
				this.m_cardActor.DisableMissingCardEffect();
			}
			if (this.m_overrideCustomMaterialRenderQueue && this.m_showCustomEffect && !this.m_isOverriddingRenderQueues)
			{
				this.m_isOverriddingRenderQueues = this.ApplyRenderQueues(false);
				return;
			}
			if (!this.m_showCustomEffect)
			{
				this.m_isOverriddingRenderQueues = false;
				this.ApplyRenderQueues(true);
			}
		}

		// Token: 0x0600B0B4 RID: 45236 RVA: 0x003693BC File Offset: 0x003675BC
		private bool ApplyRenderQueues(bool reset = false)
		{
			if (!this.m_overrideCustomMaterialRenderQueue)
			{
				return false;
			}
			this.m_cardActor.SetMissingCardRenderQueue(reset, this.GetRenderQueue(Card.RenderObject.CustomMaterial));
			this.m_cardActor.MoveShadowToMissingCard(reset, this.GetRenderQueue(Card.RenderObject.Shadow));
			ActorStateMgr actorStateMgr = this.m_cardActor.GetActorStateMgr();
			return actorStateMgr != null && actorStateMgr.SetStateRenderQueue(reset, this.GetRenderQueue(Card.RenderObject.Highlight));
		}

		// Token: 0x0600B0B5 RID: 45237 RVA: 0x0036941E File Offset: 0x0036761E
		private int GetRenderQueue(Card.RenderObject renderObject)
		{
			return this.m_baseCustomMaterialRenderQueue + Array.IndexOf<Card.RenderObject>(this.RENDER_OBJECT_ORDER, renderObject);
		}

		// Token: 0x0600B0B6 RID: 45238 RVA: 0x00369434 File Offset: 0x00367634
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.m_displayedCardId = null;
			this.m_displayedPremiumTag = TAG_PREMIUM.NORMAL;
			IJobDependency[] dependencies;
			HearthstoneServices.InitializeDynamicServicesIfEditor(out dependencies, new Type[]
			{
				typeof(IAssetLoader),
				typeof(GameDbf),
				typeof(WidgetRunner),
				typeof(IAliasedAssetResolver)
			});
			Processor.QueueJob(HearthstoneJobs.CreateJobFromAction("Card.CreatePreviewableObject", new Action(this.CreatePreviewableObject), JobFlags.StartImmediately, dependencies));
		}

		// Token: 0x0600B0B7 RID: 45239 RVA: 0x003694B5 File Offset: 0x003676B5
		private void CreatePreviewableObject()
		{
			if (DefLoader.Get().GetAllEntityDefs().Count == 0)
			{
				DefLoader.Get().LoadAllEntityDefs();
			}
			base.CreatePreviewableObject(delegate(CustomWidgetBehavior.IPreviewableObject previewable, Action<GameObject> callback)
			{
				this.m_isShowingCustomEffect = false;
				this.m_isShowingShadow = false;
				Card.DesiredDataModelData desiredData = this.GetDesiredData();
				this.m_displayedCardId = desiredData.DesiredCardId;
				this.m_displayedPremiumTag = desiredData.DesiredPremium;
				this.m_displayedAttack = desiredData.DesiredAttack;
				this.m_displayedHealth = desiredData.DesiredHealth;
				this.m_displayedMana = desiredData.DesiredMana;
				this.m_cardActor = null;
				this.m_displayedActorAssetType = this.m_zone;
				this.m_displayedSpellTypes = desiredData.DesiredSpellTypes;
				if (string.IsNullOrEmpty(this.m_displayedCardId) || GameDbf.GetIndex().GetCardRecord(this.m_displayedCardId) == null)
				{
					callback(null);
					return;
				}
				EntityDef entityDef = DefLoader.Get().GetEntityDef(this.m_displayedCardId);
				if (entityDef == null)
				{
					callback(null);
					return;
				}
				GameObject gameObject = this.LoadActorByActorAssetType(entityDef.GetCardType(), desiredData.DesiredPremium);
				if (gameObject == null)
				{
					callback(null);
					return;
				}
				Actor component = gameObject.GetComponent<Actor>();
				this.m_cardActor = component;
				EntityDef entityDef2 = entityDef.Clone();
				component.SetEntityDef(entityDef2);
				component.GetEntityDef().SetTag(GAME_TAG.ATK, this.m_displayedAttack);
				component.GetEntityDef().SetTag(GAME_TAG.HEALTH, this.m_displayedHealth);
				component.GetEntityDef().SetTag(GAME_TAG.COST, this.m_displayedMana);
				component.SetPremium(this.m_displayedPremiumTag);
				this.PlaySpellBirths(component, desiredData.DesiredSpellTypes);
				component.SetUnlit();
				component.ToggleCollider(false);
				component.UpdateAllComponents();
				this.UpdateActor();
				Transform transform = component.transform;
				transform.SetParent(base.transform, false);
				transform.localPosition = Vector3.zero;
				transform.localRotation = Quaternion.identity;
				transform.localScale = Vector3.one;
				callback(component.gameObject);
				if (this.OnCardActorLoaded != null)
				{
					this.OnCardActorLoaded(component);
				}
			}, delegate(CustomWidgetBehavior.IPreviewableObject o)
			{
				Card.DesiredDataModelData desiredData = this.GetDesiredData();
				return this.m_displayedCardId != desiredData.DesiredCardId || this.m_displayedPremiumTag != desiredData.DesiredPremium || this.m_displayedActorAssetType != this.m_zone || this.m_displayedAttack != desiredData.DesiredAttack || this.m_displayedHealth != desiredData.DesiredHealth || this.m_displayedMana != desiredData.DesiredMana || !this.m_displayedSpellTypes.SequenceEqual(desiredData.DesiredSpellTypes) || (this.m_cardActor != null && this.m_useShadow != this.m_isShowingShadow);
			}, null);
		}

		// Token: 0x0600B0B8 RID: 45240 RVA: 0x003694F4 File Offset: 0x003676F4
		protected virtual GameObject LoadActorByActorAssetType(TAG_CARDTYPE cardType, TAG_PREMIUM premium)
		{
			GameObject result = null;
			TAG_ZONE zone = this.m_zone;
			if (zone != TAG_ZONE.PLAY)
			{
				if (zone == TAG_ZONE.HAND)
				{
					result = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(cardType, premium, false), AssetLoadingOptions.IgnorePrefabPosition);
				}
				else
				{
					Debug.LogWarningFormat("CustomWidgetBehavior:Card - Zone {0} not supported.", new object[]
					{
						this.m_zone
					});
				}
			}
			else
			{
				result = AssetLoader.Get().InstantiatePrefab(ActorNames.GetPlayActor(cardType, premium), AssetLoadingOptions.IgnorePrefabPosition);
			}
			return result;
		}

		// Token: 0x0600B0B9 RID: 45241 RVA: 0x00369568 File Offset: 0x00367768
		private Card.DesiredDataModelData GetDesiredData()
		{
			Card.DesiredDataModelData desiredDataModelData = default(Card.DesiredDataModelData);
			CardDataModel dataModel = this.GetDataModel();
			if (!Application.IsPlaying(this) || this.m_golden != Card.PremiumTag.UseDataModel)
			{
				desiredDataModelData.DesiredPremium = ((this.m_golden == Card.PremiumTag.Yes) ? TAG_PREMIUM.GOLDEN : TAG_PREMIUM.NORMAL);
			}
			if (!Application.IsPlaying(this) || !this.m_useCardIdFromDataModel)
			{
				desiredDataModelData.DesiredCardId = this.m_defaultCardId;
			}
			desiredDataModelData.DesiredSpellTypes = new DataModelList<SpellType>();
			if (dataModel != null)
			{
				if (this.m_useCardIdFromDataModel)
				{
					desiredDataModelData.DesiredCardId = dataModel.CardId;
				}
				if (this.m_golden == Card.PremiumTag.UseDataModel)
				{
					desiredDataModelData.DesiredPremium = dataModel.Premium;
				}
				if (this.m_useStatsFromDataModel)
				{
					desiredDataModelData.DesiredAttack = dataModel.Attack;
					desiredDataModelData.DesiredHealth = dataModel.Health;
					desiredDataModelData.DesiredMana = dataModel.Mana;
				}
				desiredDataModelData.DesiredSpellTypes = dataModel.SpellTypes;
			}
			if ((!Application.IsPlaying(this) && dataModel == null) || !this.m_useStatsFromDataModel)
			{
				EntityDef entityDef = DefLoader.Get().GetEntityDef(desiredDataModelData.DesiredCardId);
				if (entityDef == null)
				{
					desiredDataModelData.DesiredAttack = 0;
					desiredDataModelData.DesiredHealth = 0;
					desiredDataModelData.DesiredMana = 0;
				}
				else
				{
					desiredDataModelData.DesiredAttack = entityDef.GetATK();
					desiredDataModelData.DesiredHealth = entityDef.GetHealth();
					desiredDataModelData.DesiredMana = entityDef.GetCost();
				}
			}
			return desiredDataModelData;
		}

		// Token: 0x0600B0BA RID: 45242 RVA: 0x003696A4 File Offset: 0x003678A4
		private void PlaySpellBirths(Actor actor, DataModelList<SpellType> spellTypes)
		{
			foreach (SpellType spellType in spellTypes)
			{
				actor.ActivateSpellBirthState(spellType);
			}
		}

		// Token: 0x0600B0BB RID: 45243 RVA: 0x003696F0 File Offset: 0x003678F0
		public CardDataModel GetDataModel()
		{
			IDataModel dataModel = null;
			base.GetDataModel(27, out dataModel);
			return dataModel as CardDataModel;
		}

		// Token: 0x0600B0BC RID: 45244 RVA: 0x00369710 File Offset: 0x00367910
		public void RegisterCardLoadedListener(Card.OnCardActorLoadedDelegate listener)
		{
			this.OnCardActorLoaded -= listener;
			this.OnCardActorLoaded += listener;
			if (this.m_cardActor != null)
			{
				listener(this.m_cardActor);
			}
		}

		// Token: 0x0600B0BD RID: 45245 RVA: 0x0036973A File Offset: 0x0036793A
		public void UnregisterCardLoadedListener(Card.OnCardActorLoadedDelegate listener)
		{
			this.OnCardActorLoaded -= listener;
		}

		// Token: 0x04009549 RID: 38217
		private readonly Card.RenderObject[] RENDER_OBJECT_ORDER = new Card.RenderObject[]
		{
			Card.RenderObject.Shadow,
			Card.RenderObject.Highlight,
			Card.RenderObject.CustomMaterial
		};

		// Token: 0x0400954A RID: 38218
		[Tooltip("This is the ID of the card displayed by default.")]
		[SerializeField]
		private string m_defaultCardId = "BOT_914h";

		// Token: 0x0400954B RID: 38219
		[Tooltip("If true, it will use the card ID from the 'card' data model whenever bound.")]
		[SerializeField]
		private bool m_useCardIdFromDataModel = true;

		// Token: 0x0400954C RID: 38220
		[Tooltip("If true, it will use the premium tag from the 'card' data model whenever bound.")]
		[SerializeField]
		private Card.PremiumTag m_golden;

		// Token: 0x0400954D RID: 38221
		[Tooltip("If true, this will show the shadow object.")]
		[SerializeField]
		protected bool m_useShadow = true;

		// Token: 0x0400954E RID: 38222
		[Tooltip("Displays the card using the visual treatment it would have in this zone.")]
		[SerializeField]
		protected TAG_ZONE m_zone = TAG_ZONE.HAND;

		// Token: 0x0400954F RID: 38223
		[Tooltip("If true, it will use the Base Render Queue to short the render objects such as the custom material plane, highlight, and shadow.")]
		[SerializeField]
		private bool m_overrideCustomMaterialRenderQueue;

		// Token: 0x04009550 RID: 38224
		[Tooltip("This is the bas render queue used for the render objects such as the custom material plane, highlight, and shadow.")]
		[SerializeField]
		private int m_baseCustomMaterialRenderQueue = -3;

		// Token: 0x04009551 RID: 38225
		[Tooltip("If true, it will use the stat values set in the data model for attack and health. Otherwise, it will use the EntityDef defaults.")]
		[SerializeField]
		private bool m_useStatsFromDataModel;

		// Token: 0x04009552 RID: 38226
		protected string m_displayedCardId;

		// Token: 0x04009553 RID: 38227
		private TAG_PREMIUM m_displayedPremiumTag;

		// Token: 0x04009554 RID: 38228
		private int m_displayedAttack;

		// Token: 0x04009555 RID: 38229
		private int m_displayedHealth;

		// Token: 0x04009556 RID: 38230
		private int m_displayedMana;

		// Token: 0x04009557 RID: 38231
		protected bool m_isShowingShadow;

		// Token: 0x04009558 RID: 38232
		protected Actor m_cardActor;

		// Token: 0x04009559 RID: 38233
		private bool m_showCustomEffect;

		// Token: 0x0400955A RID: 38234
		private Material m_customEffectMaterial;

		// Token: 0x0400955B RID: 38235
		private bool m_isShowingCustomEffect;

		// Token: 0x0400955C RID: 38236
		private bool m_isOverriddingRenderQueues;

		// Token: 0x0400955D RID: 38237
		private TAG_ZONE m_displayedActorAssetType = TAG_ZONE.HAND;

		// Token: 0x0400955E RID: 38238
		private DataModelList<SpellType> m_displayedSpellTypes = new DataModelList<SpellType>();

		// Token: 0x02002812 RID: 10258
		private enum RenderObject
		{
			// Token: 0x0400F871 RID: 63601
			Shadow,
			// Token: 0x0400F872 RID: 63602
			Highlight,
			// Token: 0x0400F873 RID: 63603
			CustomMaterial
		}

		// Token: 0x02002813 RID: 10259
		// (Invoke) Token: 0x06013AF6 RID: 80630
		public delegate void OnCardActorLoadedDelegate(Actor cardActor);

		// Token: 0x02002814 RID: 10260
		private enum PremiumTag
		{
			// Token: 0x0400F875 RID: 63605
			UseDataModel,
			// Token: 0x0400F876 RID: 63606
			No,
			// Token: 0x0400F877 RID: 63607
			Yes
		}

		// Token: 0x02002815 RID: 10261
		private struct DesiredDataModelData
		{
			// Token: 0x0400F878 RID: 63608
			public string DesiredCardId;

			// Token: 0x0400F879 RID: 63609
			public TAG_PREMIUM DesiredPremium;

			// Token: 0x0400F87A RID: 63610
			public int DesiredAttack;

			// Token: 0x0400F87B RID: 63611
			public int DesiredHealth;

			// Token: 0x0400F87C RID: 63612
			public int DesiredMana;

			// Token: 0x0400F87D RID: 63613
			public DataModelList<SpellType> DesiredSpellTypes;
		}
	}
}
