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
	[AddComponentMenu("")]
	[WidgetBehaviorDescription(Path = "Hearthstone/Card", UniqueWithinCategory = "asset")]
	public class Card : CustomWidgetBehavior
	{
		private enum RenderObject
		{
			Shadow,
			Highlight,
			CustomMaterial
		}

		public delegate void OnCardActorLoadedDelegate(Actor cardActor);

		private enum PremiumTag
		{
			UseDataModel,
			No,
			Yes
		}

		private struct DesiredDataModelData
		{
			public string DesiredCardId;

			public TAG_PREMIUM DesiredPremium;

			public int DesiredAttack;

			public int DesiredHealth;

			public int DesiredMana;

			public DataModelList<SpellType> DesiredSpellTypes;
		}

		private readonly RenderObject[] RENDER_OBJECT_ORDER = new RenderObject[3]
		{
			RenderObject.Shadow,
			RenderObject.Highlight,
			RenderObject.CustomMaterial
		};

		[Tooltip("This is the ID of the card displayed by default.")]
		[SerializeField]
		private string m_defaultCardId = "BOT_914h";

		[Tooltip("If true, it will use the card ID from the 'card' data model whenever bound.")]
		[SerializeField]
		private bool m_useCardIdFromDataModel = true;

		[Tooltip("If true, it will use the premium tag from the 'card' data model whenever bound.")]
		[SerializeField]
		private PremiumTag m_golden;

		[Tooltip("If true, this will show the shadow object.")]
		[SerializeField]
		protected bool m_useShadow = true;

		[Tooltip("Displays the card using the visual treatment it would have in this zone.")]
		[SerializeField]
		protected TAG_ZONE m_zone = TAG_ZONE.HAND;

		[Tooltip("If true, it will use the Base Render Queue to short the render objects such as the custom material plane, highlight, and shadow.")]
		[SerializeField]
		private bool m_overrideCustomMaterialRenderQueue;

		[Tooltip("This is the bas render queue used for the render objects such as the custom material plane, highlight, and shadow.")]
		[SerializeField]
		private int m_baseCustomMaterialRenderQueue = -3;

		[Tooltip("If true, it will use the stat values set in the data model for attack and health. Otherwise, it will use the EntityDef defaults.")]
		[SerializeField]
		private bool m_useStatsFromDataModel;

		protected string m_displayedCardId;

		private TAG_PREMIUM m_displayedPremiumTag;

		private int m_displayedAttack;

		private int m_displayedHealth;

		private int m_displayedMana;

		protected bool m_isShowingShadow;

		protected Actor m_cardActor;

		private bool m_showCustomEffect;

		private Material m_customEffectMaterial;

		private bool m_isShowingCustomEffect;

		private bool m_isOverriddingRenderQueues;

		private TAG_ZONE m_displayedActorAssetType = TAG_ZONE.HAND;

		private DataModelList<SpellType> m_displayedSpellTypes = new DataModelList<SpellType>();

		[Overridable]
		public TAG_PREMIUM Premium
		{
			get
			{
				return m_displayedPremiumTag;
			}
			set
			{
				m_displayedPremiumTag = value;
			}
		}

		[Overridable]
		public bool ShowShadow
		{
			get
			{
				return m_useShadow;
			}
			set
			{
				if (m_useShadow != value)
				{
					m_useShadow = value;
					UpdateActor();
				}
			}
		}

		[Overridable]
		public bool ShowCustomEffect
		{
			get
			{
				return m_showCustomEffect;
			}
			set
			{
				if (m_showCustomEffect != value)
				{
					m_showCustomEffect = value;
					UpdateActor();
				}
			}
		}

		[Overridable]
		public Material CustomEffectMaterial
		{
			get
			{
				return m_customEffectMaterial;
			}
			set
			{
				if (value != m_customEffectMaterial)
				{
					m_customEffectMaterial = value;
					m_showCustomEffect = true;
					if (m_isShowingCustomEffect && m_cardActor != null)
					{
						m_isShowingCustomEffect = false;
						m_cardActor.DisableMissingCardEffect();
					}
					UpdateActor();
				}
			}
		}

		private event OnCardActorLoadedDelegate OnCardActorLoaded;

		protected virtual void UpdateActor()
		{
			if (m_cardActor == null)
			{
				m_isShowingShadow = m_useShadow;
				m_isShowingCustomEffect = m_showCustomEffect;
				return;
			}
			m_cardActor.ContactShadow(m_useShadow);
			bool flag = m_useShadow && !m_cardActor.HasContactShadowObject();
			ProjectedShadow component = m_cardActor.GetComponent<ProjectedShadow>();
			if (component != null)
			{
				if (flag)
				{
					component.m_enabledAlongsideRealtimeShadows = true;
					m_cardActor.GetComponentsInChildren<MeshRenderer>().ForEach(delegate(MeshRenderer r)
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
			m_isShowingShadow = m_useShadow;
			SetUpCustomEffect();
		}

		protected void SetUpCustomEffect()
		{
			if (m_cardActor == null || !Application.IsPlaying(this))
			{
				return;
			}
			if (m_showCustomEffect && !m_isShowingCustomEffect)
			{
				if (m_customEffectMaterial != null)
				{
					m_cardActor.SetMissingCardMaterial(m_customEffectMaterial);
				}
				m_isShowingCustomEffect = true;
				m_cardActor.MissingCardEffect(refreshOnFocus: false);
			}
			if (!m_showCustomEffect && m_isShowingCustomEffect)
			{
				m_isShowingCustomEffect = false;
				m_cardActor.DisableMissingCardEffect();
			}
			if (m_overrideCustomMaterialRenderQueue && m_showCustomEffect && !m_isOverriddingRenderQueues)
			{
				m_isOverriddingRenderQueues = ApplyRenderQueues();
			}
			else if (!m_showCustomEffect)
			{
				m_isOverriddingRenderQueues = false;
				ApplyRenderQueues(reset: true);
			}
		}

		private bool ApplyRenderQueues(bool reset = false)
		{
			if (!m_overrideCustomMaterialRenderQueue)
			{
				return false;
			}
			m_cardActor.SetMissingCardRenderQueue(reset, GetRenderQueue(RenderObject.CustomMaterial));
			m_cardActor.MoveShadowToMissingCard(reset, GetRenderQueue(RenderObject.Shadow));
			ActorStateMgr actorStateMgr = m_cardActor.GetActorStateMgr();
			if (actorStateMgr != null)
			{
				return actorStateMgr.SetStateRenderQueue(reset, GetRenderQueue(RenderObject.Highlight));
			}
			return false;
		}

		private int GetRenderQueue(RenderObject renderObject)
		{
			return m_baseCustomMaterialRenderQueue + Array.IndexOf(RENDER_OBJECT_ORDER, renderObject);
		}

		protected override void OnInitialize()
		{
			base.OnInitialize();
			m_displayedCardId = null;
			m_displayedPremiumTag = TAG_PREMIUM.NORMAL;
			HearthstoneServices.InitializeDynamicServicesIfEditor(out var serviceDependencies, typeof(IAssetLoader), typeof(GameDbf), typeof(WidgetRunner), typeof(IAliasedAssetResolver));
			Processor.QueueJob(HearthstoneJobs.CreateJobFromAction("Card.CreatePreviewableObject", CreatePreviewableObject, JobFlags.StartImmediately, serviceDependencies));
		}

		private void CreatePreviewableObject()
		{
			if (DefLoader.Get().GetAllEntityDefs().Count == 0)
			{
				DefLoader.Get().LoadAllEntityDefs();
			}
			CreatePreviewableObject(delegate(IPreviewableObject previewable, Action<GameObject> callback)
			{
				m_isShowingCustomEffect = false;
				m_isShowingShadow = false;
				DesiredDataModelData desiredData2 = GetDesiredData();
				m_displayedCardId = desiredData2.DesiredCardId;
				m_displayedPremiumTag = desiredData2.DesiredPremium;
				m_displayedAttack = desiredData2.DesiredAttack;
				m_displayedHealth = desiredData2.DesiredHealth;
				m_displayedMana = desiredData2.DesiredMana;
				m_cardActor = null;
				m_displayedActorAssetType = m_zone;
				m_displayedSpellTypes = desiredData2.DesiredSpellTypes;
				if (string.IsNullOrEmpty(m_displayedCardId) || GameDbf.GetIndex().GetCardRecord(m_displayedCardId) == null)
				{
					callback(null);
				}
				else
				{
					EntityDef entityDef = DefLoader.Get().GetEntityDef(m_displayedCardId);
					if (entityDef == null)
					{
						callback(null);
					}
					else
					{
						GameObject gameObject = LoadActorByActorAssetType(entityDef.GetCardType(), desiredData2.DesiredPremium);
						if (gameObject == null)
						{
							callback(null);
						}
						else
						{
							Actor actor = (m_cardActor = gameObject.GetComponent<Actor>());
							EntityDef entityDef2 = entityDef.Clone();
							actor.SetEntityDef(entityDef2);
							actor.GetEntityDef().SetTag(GAME_TAG.ATK, m_displayedAttack);
							actor.GetEntityDef().SetTag(GAME_TAG.HEALTH, m_displayedHealth);
							actor.GetEntityDef().SetTag(GAME_TAG.COST, m_displayedMana);
							actor.SetPremium(m_displayedPremiumTag);
							PlaySpellBirths(actor, desiredData2.DesiredSpellTypes);
							actor.SetUnlit();
							actor.ToggleCollider(enabled: false);
							actor.UpdateAllComponents();
							UpdateActor();
							Transform obj = actor.transform;
							obj.SetParent(base.transform, worldPositionStays: false);
							obj.localPosition = Vector3.zero;
							obj.localRotation = Quaternion.identity;
							obj.localScale = Vector3.one;
							callback(actor.gameObject);
							if (this.OnCardActorLoaded != null)
							{
								this.OnCardActorLoaded(actor);
							}
						}
					}
				}
			}, delegate
			{
				DesiredDataModelData desiredData = GetDesiredData();
				return m_displayedCardId != desiredData.DesiredCardId || m_displayedPremiumTag != desiredData.DesiredPremium || m_displayedActorAssetType != m_zone || m_displayedAttack != desiredData.DesiredAttack || m_displayedHealth != desiredData.DesiredHealth || m_displayedMana != desiredData.DesiredMana || !m_displayedSpellTypes.SequenceEqual(desiredData.DesiredSpellTypes) || (m_cardActor != null && m_useShadow != m_isShowingShadow);
			});
		}

		protected virtual GameObject LoadActorByActorAssetType(TAG_CARDTYPE cardType, TAG_PREMIUM premium)
		{
			GameObject result = null;
			switch (m_zone)
			{
			case TAG_ZONE.HAND:
				result = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(cardType, premium), AssetLoadingOptions.IgnorePrefabPosition);
				break;
			case TAG_ZONE.PLAY:
				result = AssetLoader.Get().InstantiatePrefab(ActorNames.GetPlayActor(cardType, premium), AssetLoadingOptions.IgnorePrefabPosition);
				break;
			default:
				Debug.LogWarningFormat("CustomWidgetBehavior:Card - Zone {0} not supported.", m_zone);
				break;
			}
			return result;
		}

		private DesiredDataModelData GetDesiredData()
		{
			DesiredDataModelData result = default(DesiredDataModelData);
			CardDataModel dataModel = GetDataModel();
			if (!Application.IsPlaying(this) || m_golden != 0)
			{
				result.DesiredPremium = ((m_golden == PremiumTag.Yes) ? TAG_PREMIUM.GOLDEN : TAG_PREMIUM.NORMAL);
			}
			if (!Application.IsPlaying(this) || !m_useCardIdFromDataModel)
			{
				result.DesiredCardId = m_defaultCardId;
			}
			result.DesiredSpellTypes = new DataModelList<SpellType>();
			if (dataModel != null)
			{
				if (m_useCardIdFromDataModel)
				{
					result.DesiredCardId = dataModel.CardId;
				}
				if (m_golden == PremiumTag.UseDataModel)
				{
					result.DesiredPremium = dataModel.Premium;
				}
				if (m_useStatsFromDataModel)
				{
					result.DesiredAttack = dataModel.Attack;
					result.DesiredHealth = dataModel.Health;
					result.DesiredMana = dataModel.Mana;
				}
				result.DesiredSpellTypes = dataModel.SpellTypes;
			}
			if ((!Application.IsPlaying(this) && dataModel == null) || !m_useStatsFromDataModel)
			{
				EntityDef entityDef = DefLoader.Get().GetEntityDef(result.DesiredCardId);
				if (entityDef == null)
				{
					result.DesiredAttack = 0;
					result.DesiredHealth = 0;
					result.DesiredMana = 0;
				}
				else
				{
					result.DesiredAttack = entityDef.GetATK();
					result.DesiredHealth = entityDef.GetHealth();
					result.DesiredMana = entityDef.GetCost();
				}
			}
			return result;
		}

		private void PlaySpellBirths(Actor actor, DataModelList<SpellType> spellTypes)
		{
			foreach (SpellType spellType in spellTypes)
			{
				actor.ActivateSpellBirthState(spellType);
			}
		}

		public CardDataModel GetDataModel()
		{
			IDataModel dataModel = null;
			GetDataModel(27, out dataModel);
			return dataModel as CardDataModel;
		}

		public void RegisterCardLoadedListener(OnCardActorLoadedDelegate listener)
		{
			OnCardActorLoaded -= listener;
			OnCardActorLoaded += listener;
			if (m_cardActor != null)
			{
				listener(m_cardActor);
			}
		}

		public void UnregisterCardLoadedListener(OnCardActorLoadedDelegate listener)
		{
			OnCardActorLoaded -= listener;
		}
	}
}
