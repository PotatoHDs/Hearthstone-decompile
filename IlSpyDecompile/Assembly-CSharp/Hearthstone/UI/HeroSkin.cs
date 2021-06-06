using UnityEngine;

namespace Hearthstone.UI
{
	[AddComponentMenu("")]
	[WidgetBehaviorDescription(Path = "Hearthstone/Hero Skin", UniqueWithinCategory = "asset")]
	public class HeroSkin : Card
	{
		[Tooltip("This will show or hide the name in the hero")]
		[SerializeField]
		private bool m_showHeroName;

		[Tooltip("This will show or hide the class icon in the hero")]
		[SerializeField]
		private bool m_showHeroClass;

		[Overridable]
		public bool ShowHeroName
		{
			get
			{
				return m_showHeroName;
			}
			set
			{
				if (value != m_showHeroName)
				{
					m_showHeroName = value;
					UpdateActor();
				}
			}
		}

		[Overridable]
		public bool ShowHeroClass
		{
			get
			{
				return m_showHeroClass;
			}
			set
			{
				if (value != m_showHeroClass)
				{
					m_showHeroClass = value;
					UpdateActor();
				}
			}
		}

		protected override GameObject LoadActorByActorAssetType(TAG_CARDTYPE cardType, TAG_PREMIUM premium)
		{
			GameObject result = null;
			switch (m_zone)
			{
			case TAG_ZONE.HAND:
				result = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHeroSkinOrHandActor(cardType, premium), AssetLoadingOptions.IgnorePrefabPosition);
				break;
			case TAG_ZONE.PLAY:
				result = AssetLoader.Get().InstantiatePrefab(ActorNames.GetPlayActor(cardType, premium), AssetLoadingOptions.IgnorePrefabPosition);
				break;
			default:
				Debug.LogWarningFormat("CustomWidgetBehavior:HeroSkin - Zone {0} not supported.", m_zone);
				break;
			}
			return result;
		}

		protected override void UpdateActor()
		{
			if (m_cardActor == null)
			{
				return;
			}
			CollectionHeroSkin component = m_cardActor.GetComponent<CollectionHeroSkin>();
			if (component != null)
			{
				if (m_showHeroClass)
				{
					EntityDef entityDef = DefLoader.Get().GetEntityDef(m_displayedCardId);
					if (entityDef != null)
					{
						component.SetClass(entityDef.GetClass());
					}
				}
				component.m_classIcon.transform.parent.gameObject.SetActive(m_showHeroClass);
				component.ShowName = m_showHeroName;
			}
			if (m_useShadow != m_isShowingShadow)
			{
				if (component != null)
				{
					component.ShowShadow(m_useShadow);
				}
				else
				{
					m_cardActor.ContactShadow(m_useShadow);
				}
				m_isShowingShadow = m_useShadow;
			}
			SetUpCustomEffect();
		}
	}
}
