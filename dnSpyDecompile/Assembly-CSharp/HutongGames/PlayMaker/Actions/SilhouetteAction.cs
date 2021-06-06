using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EC7 RID: 3783
	public abstract class SilhouetteAction<T> : FsmStateAction where T : NamedVariable
	{
		// Token: 0x0600AA69 RID: 43625
		protected abstract void AssignAsset(T asset);

		// Token: 0x0600AA6A RID: 43626 RVA: 0x00355628 File Offset: 0x00353828
		public override void OnEnter()
		{
			Actor actor = this.GetActor();
			if (!actor)
			{
				Debug.LogWarningFormat("Failed to find Actor in SilhouetteAction: {0}", new object[]
				{
					SceneUtils.FindComponentInThisOrParents<Card>(base.Owner)
				});
				base.Finish();
				return;
			}
			switch (this.GetCardAttributes(actor))
			{
			case SilhouetteAction<T>.CardAttributes.GOLDEN:
				this.AssignAsset(this.m_Golden);
				goto IL_12D;
			case SilhouetteAction<T>.CardAttributes.TAUNT:
				this.AssignAsset(this.m_Taunt);
				goto IL_12D;
			case SilhouetteAction<T>.CardAttributes.GOLDEN | SilhouetteAction<T>.CardAttributes.TAUNT:
				this.AssignAsset(this.m_GoldenTaunt);
				goto IL_12D;
			case SilhouetteAction<T>.CardAttributes.LEGENDARY:
				this.AssignAsset(this.m_Legendary);
				goto IL_12D;
			case SilhouetteAction<T>.CardAttributes.GOLDEN | SilhouetteAction<T>.CardAttributes.LEGENDARY:
				this.AssignAsset(this.m_GoldenLegendary);
				goto IL_12D;
			case SilhouetteAction<T>.CardAttributes.TAUNT | SilhouetteAction<T>.CardAttributes.LEGENDARY:
				this.AssignAsset(this.m_LegendaryTaunt);
				goto IL_12D;
			case SilhouetteAction<T>.CardAttributes.GOLDEN | SilhouetteAction<T>.CardAttributes.TAUNT | SilhouetteAction<T>.CardAttributes.LEGENDARY:
				this.AssignAsset(this.m_GoldenLegendaryTaunt);
				goto IL_12D;
			case SilhouetteAction<T>.CardAttributes.DIAMOND:
				this.AssignAsset(this.m_Diamond);
				goto IL_12D;
			case SilhouetteAction<T>.CardAttributes.TAUNT | SilhouetteAction<T>.CardAttributes.DIAMOND:
				this.AssignAsset(this.m_DiamondTaunt);
				goto IL_12D;
			case SilhouetteAction<T>.CardAttributes.LEGENDARY | SilhouetteAction<T>.CardAttributes.DIAMOND:
				this.AssignAsset(this.m_DiamondLegendary);
				goto IL_12D;
			case SilhouetteAction<T>.CardAttributes.TAUNT | SilhouetteAction<T>.CardAttributes.LEGENDARY | SilhouetteAction<T>.CardAttributes.DIAMOND:
				this.AssignAsset(this.m_DiamondLegendaryTaunt);
				goto IL_12D;
			}
			this.AssignAsset(this.m_Normal);
			IL_12D:
			base.Finish();
		}

		// Token: 0x0600AA6B RID: 43627 RVA: 0x00355768 File Offset: 0x00353968
		private Actor GetActor()
		{
			Actor actor = SceneUtils.FindComponentInThisOrParents<Actor>(base.Owner);
			if (actor == null)
			{
				Card card = SceneUtils.FindComponentInThisOrParents<Card>(base.Owner);
				if (!(card != null))
				{
					return null;
				}
				actor = card.GetActor();
			}
			return actor;
		}

		// Token: 0x0600AA6C RID: 43628 RVA: 0x003557AC File Offset: 0x003539AC
		private SilhouetteAction<T>.CardAttributes GetCardAttributes(Actor actor)
		{
			SilhouetteAction<T>.CardAttributes cardAttributes = (SilhouetteAction<T>.CardAttributes)0;
			if (actor.GetRarity() == TAG_RARITY.LEGENDARY)
			{
				cardAttributes |= SilhouetteAction<T>.CardAttributes.LEGENDARY;
			}
			if (actor.GetSpellIfLoaded(SpellType.TAUNT) || actor.GetSpellIfLoaded(SpellType.TAUNT_STEALTH) || actor.GetSpellIfLoaded(SpellType.TAUNT_PREMIUM) || actor.GetSpellIfLoaded(SpellType.TAUNT_PREMIUM_STEALTH) || actor.GetSpellIfLoaded(SpellType.TAUNT_DIAMOND) || actor.GetSpellIfLoaded(SpellType.TAUNT_DIAMOND_STEALTH))
			{
				cardAttributes |= SilhouetteAction<T>.CardAttributes.TAUNT;
			}
			if (actor.GetPremium() == TAG_PREMIUM.GOLDEN)
			{
				cardAttributes |= SilhouetteAction<T>.CardAttributes.GOLDEN;
			}
			else if (actor.GetPremium() == TAG_PREMIUM.DIAMOND)
			{
				cardAttributes |= SilhouetteAction<T>.CardAttributes.DIAMOND;
			}
			return cardAttributes;
		}

		// Token: 0x04009105 RID: 37125
		[RequiredField]
		public T m_Normal;

		// Token: 0x04009106 RID: 37126
		[RequiredField]
		public T m_Golden;

		// Token: 0x04009107 RID: 37127
		[RequiredField]
		public T m_Diamond;

		// Token: 0x04009108 RID: 37128
		[RequiredField]
		public T m_Taunt;

		// Token: 0x04009109 RID: 37129
		[RequiredField]
		public T m_Legendary;

		// Token: 0x0400910A RID: 37130
		[RequiredField]
		public T m_LegendaryTaunt;

		// Token: 0x0400910B RID: 37131
		[RequiredField]
		public T m_GoldenTaunt;

		// Token: 0x0400910C RID: 37132
		[RequiredField]
		public T m_GoldenLegendary;

		// Token: 0x0400910D RID: 37133
		[RequiredField]
		public T m_GoldenLegendaryTaunt;

		// Token: 0x0400910E RID: 37134
		[RequiredField]
		public T m_DiamondTaunt;

		// Token: 0x0400910F RID: 37135
		[RequiredField]
		public T m_DiamondLegendary;

		// Token: 0x04009110 RID: 37136
		[RequiredField]
		public T m_DiamondLegendaryTaunt;

		// Token: 0x04009111 RID: 37137
		[RequiredField]
		[UIHint(UIHint.FsmGameObject)]
		[Tooltip("Output GameObject with a Texture component which will have its sharedTexture swapped for one of the Textures above.")]
		public FsmGameObject m_Minion;

		// Token: 0x020027B4 RID: 10164
		[Flags]
		private enum CardAttributes
		{
			// Token: 0x0400F564 RID: 62820
			GOLDEN = 1,
			// Token: 0x0400F565 RID: 62821
			TAUNT = 2,
			// Token: 0x0400F566 RID: 62822
			LEGENDARY = 4,
			// Token: 0x0400F567 RID: 62823
			DIAMOND = 8
		}
	}
}
