using System;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F8A RID: 3978
	[ActionCategory("Pegasus")]
	[Tooltip("Get the GameObjects for each Card's Actor in current choice in left-to-right order. Requires the Spell to be extended from CustomChoiceSpell.")]
	public class SpellGetChoiceCardActorsAction : SpellAction
	{
		// Token: 0x0600ADC0 RID: 44480 RVA: 0x003622A3 File Offset: 0x003604A3
		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_SpellObject);
		}

		// Token: 0x0600ADC1 RID: 44481 RVA: 0x003622B6 File Offset: 0x003604B6
		public override void Reset()
		{
			this.m_SpellObject = null;
			this.m_ChoiceCardActorGameObjects = null;
		}

		// Token: 0x0600ADC2 RID: 44482 RVA: 0x003622C8 File Offset: 0x003604C8
		public override void OnEnter()
		{
			base.OnEnter();
			Spell spell = base.GetSpell();
			if (spell == null)
			{
				global::Log.Spells.PrintError("{0}.OnEnter(): Unable to find Spell.", new object[]
				{
					this
				});
				base.Finish();
				return;
			}
			CustomChoiceSpell customChoiceSpell = spell as CustomChoiceSpell;
			if (customChoiceSpell == null)
			{
				global::Log.Spells.PrintError("{0}.OnEnter(): Spell {1} is not extended from CustomChoiceSpell.", new object[]
				{
					this,
					spell
				});
				base.Finish();
				return;
			}
			if (customChoiceSpell.GetChoiceState() == null)
			{
				global::Log.Spells.PrintError("{0}.OnEnter(): Spell {1} does not have a valid ChoiceState.", new object[]
				{
					this,
					spell
				});
				base.Finish();
				return;
			}
			if (!this.m_ChoiceCardActorGameObjects.IsNone)
			{
				List<Card> cards = customChoiceSpell.GetChoiceState().m_cards;
				GameObject[] array = new GameObject[cards.Count];
				for (int i = 0; i < cards.Count; i++)
				{
					Actor actor = cards[i].GetActor();
					if (actor == null)
					{
						global::Log.Spells.PrintError("{0}.OnEnter(): Choice card {1} doesn't have an actor!", new object[]
						{
							this,
							cards[i]
						});
						base.Finish();
						return;
					}
					array[i] = actor.gameObject;
				}
				FsmArray choiceCardActorGameObjects = this.m_ChoiceCardActorGameObjects;
				object[] values = array;
				choiceCardActorGameObjects.Values = values;
			}
			base.Finish();
		}

		// Token: 0x0400949B RID: 38043
		public FsmOwnerDefault m_SpellObject;

		// Token: 0x0400949C RID: 38044
		[ArrayEditor(VariableType.GameObject, "", 0, 0, 65536)]
		public FsmArray m_ChoiceCardActorGameObjects;
	}
}
