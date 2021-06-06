using System;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02000FDE RID: 4062
	[AddComponentMenu("")]
	[WidgetBehaviorDescription(Path = "Hearthstone/Hero Skin", UniqueWithinCategory = "asset")]
	public class HeroSkin : Card
	{
		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x0600B0D1 RID: 45265 RVA: 0x00369F7E File Offset: 0x0036817E
		// (set) Token: 0x0600B0D2 RID: 45266 RVA: 0x00369F86 File Offset: 0x00368186
		[Overridable]
		public bool ShowHeroName
		{
			get
			{
				return this.m_showHeroName;
			}
			set
			{
				if (value != this.m_showHeroName)
				{
					this.m_showHeroName = value;
					this.UpdateActor();
				}
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x0600B0D3 RID: 45267 RVA: 0x00369F9E File Offset: 0x0036819E
		// (set) Token: 0x0600B0D4 RID: 45268 RVA: 0x00369FA6 File Offset: 0x003681A6
		[Overridable]
		public bool ShowHeroClass
		{
			get
			{
				return this.m_showHeroClass;
			}
			set
			{
				if (value != this.m_showHeroClass)
				{
					this.m_showHeroClass = value;
					this.UpdateActor();
				}
			}
		}

		// Token: 0x0600B0D5 RID: 45269 RVA: 0x00369FC0 File Offset: 0x003681C0
		protected override GameObject LoadActorByActorAssetType(TAG_CARDTYPE cardType, TAG_PREMIUM premium)
		{
			GameObject result = null;
			TAG_ZONE zone = this.m_zone;
			if (zone != TAG_ZONE.PLAY)
			{
				if (zone == TAG_ZONE.HAND)
				{
					result = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHeroSkinOrHandActor(cardType, premium), AssetLoadingOptions.IgnorePrefabPosition);
				}
				else
				{
					Debug.LogWarningFormat("CustomWidgetBehavior:HeroSkin - Zone {0} not supported.", new object[]
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

		// Token: 0x0600B0D6 RID: 45270 RVA: 0x0036A034 File Offset: 0x00368234
		protected override void UpdateActor()
		{
			if (this.m_cardActor == null)
			{
				return;
			}
			CollectionHeroSkin component = this.m_cardActor.GetComponent<CollectionHeroSkin>();
			if (component != null)
			{
				if (this.m_showHeroClass)
				{
					EntityDef entityDef = DefLoader.Get().GetEntityDef(this.m_displayedCardId);
					if (entityDef != null)
					{
						component.SetClass(entityDef.GetClass());
					}
				}
				component.m_classIcon.transform.parent.gameObject.SetActive(this.m_showHeroClass);
				component.ShowName = this.m_showHeroName;
			}
			if (this.m_useShadow != this.m_isShowingShadow)
			{
				if (component != null)
				{
					component.ShowShadow(this.m_useShadow);
				}
				else
				{
					this.m_cardActor.ContactShadow(this.m_useShadow);
				}
				this.m_isShowingShadow = this.m_useShadow;
			}
			base.SetUpCustomEffect();
		}

		// Token: 0x0400956D RID: 38253
		[Tooltip("This will show or hide the name in the hero")]
		[SerializeField]
		private bool m_showHeroName;

		// Token: 0x0400956E RID: 38254
		[Tooltip("This will show or hide the class icon in the hero")]
		[SerializeField]
		private bool m_showHeroClass;
	}
}
