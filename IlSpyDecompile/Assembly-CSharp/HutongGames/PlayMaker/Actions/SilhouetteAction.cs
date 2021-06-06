using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	public abstract class SilhouetteAction<T> : FsmStateAction where T : NamedVariable
	{
		[Flags]
		private enum CardAttributes
		{
			GOLDEN = 0x1,
			TAUNT = 0x2,
			LEGENDARY = 0x4,
			DIAMOND = 0x8
		}

		[RequiredField]
		public T m_Normal;

		[RequiredField]
		public T m_Golden;

		[RequiredField]
		public T m_Diamond;

		[RequiredField]
		public T m_Taunt;

		[RequiredField]
		public T m_Legendary;

		[RequiredField]
		public T m_LegendaryTaunt;

		[RequiredField]
		public T m_GoldenTaunt;

		[RequiredField]
		public T m_GoldenLegendary;

		[RequiredField]
		public T m_GoldenLegendaryTaunt;

		[RequiredField]
		public T m_DiamondTaunt;

		[RequiredField]
		public T m_DiamondLegendary;

		[RequiredField]
		public T m_DiamondLegendaryTaunt;

		[RequiredField]
		[UIHint(UIHint.FsmGameObject)]
		[Tooltip("Output GameObject with a Texture component which will have its sharedTexture swapped for one of the Textures above.")]
		public FsmGameObject m_Minion;

		protected abstract void AssignAsset(T asset);

		public override void OnEnter()
		{
			Actor actor = GetActor();
			if (!actor)
			{
				Debug.LogWarningFormat("Failed to find Actor in SilhouetteAction: {0}", SceneUtils.FindComponentInThisOrParents<Card>(base.Owner));
				Finish();
				return;
			}
			switch (GetCardAttributes(actor))
			{
			case CardAttributes.TAUNT | CardAttributes.LEGENDARY | CardAttributes.DIAMOND:
				AssignAsset(m_DiamondLegendaryTaunt);
				break;
			case CardAttributes.TAUNT | CardAttributes.DIAMOND:
				AssignAsset(m_DiamondTaunt);
				break;
			case CardAttributes.LEGENDARY | CardAttributes.DIAMOND:
				AssignAsset(m_DiamondLegendary);
				break;
			case CardAttributes.GOLDEN | CardAttributes.TAUNT | CardAttributes.LEGENDARY:
				AssignAsset(m_GoldenLegendaryTaunt);
				break;
			case CardAttributes.GOLDEN | CardAttributes.TAUNT:
				AssignAsset(m_GoldenTaunt);
				break;
			case CardAttributes.GOLDEN | CardAttributes.LEGENDARY:
				AssignAsset(m_GoldenLegendary);
				break;
			case CardAttributes.TAUNT | CardAttributes.LEGENDARY:
				AssignAsset(m_LegendaryTaunt);
				break;
			case CardAttributes.DIAMOND:
				AssignAsset(m_Diamond);
				break;
			case CardAttributes.LEGENDARY:
				AssignAsset(m_Legendary);
				break;
			case CardAttributes.TAUNT:
				AssignAsset(m_Taunt);
				break;
			case CardAttributes.GOLDEN:
				AssignAsset(m_Golden);
				break;
			default:
				AssignAsset(m_Normal);
				break;
			}
			Finish();
		}

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

		private CardAttributes GetCardAttributes(Actor actor)
		{
			CardAttributes cardAttributes = (CardAttributes)0;
			if (actor.GetRarity() == TAG_RARITY.LEGENDARY)
			{
				cardAttributes |= CardAttributes.LEGENDARY;
			}
			if ((bool)actor.GetSpellIfLoaded(SpellType.TAUNT) || (bool)actor.GetSpellIfLoaded(SpellType.TAUNT_STEALTH) || (bool)actor.GetSpellIfLoaded(SpellType.TAUNT_PREMIUM) || (bool)actor.GetSpellIfLoaded(SpellType.TAUNT_PREMIUM_STEALTH) || (bool)actor.GetSpellIfLoaded(SpellType.TAUNT_DIAMOND) || (bool)actor.GetSpellIfLoaded(SpellType.TAUNT_DIAMOND_STEALTH))
			{
				cardAttributes |= CardAttributes.TAUNT;
			}
			if (actor.GetPremium() == TAG_PREMIUM.GOLDEN)
			{
				cardAttributes |= CardAttributes.GOLDEN;
			}
			else if (actor.GetPremium() == TAG_PREMIUM.DIAMOND)
			{
				cardAttributes |= CardAttributes.DIAMOND;
			}
			return cardAttributes;
		}
	}
}
