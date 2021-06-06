using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Tells the game that a Spell is finished, allowing the game to progress.")]
	public class SpellFinishAction : SpellAction
	{
		public FsmOwnerDefault m_SpellObject;

		public FsmFloat m_Delay = 0f;

		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(m_SpellObject);
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (m_spell == null)
			{
				Debug.LogError($"{this}.OnEnter() - FAILED to find Spell component on Owner \"{base.Owner}\"");
				return;
			}
			if (m_Delay.Value > 0f)
			{
				m_spell.StartCoroutine(DelaySpellFinished());
			}
			else
			{
				m_spell.OnSpellFinished();
			}
			Finish();
		}

		private IEnumerator DelaySpellFinished()
		{
			yield return new WaitForSeconds(m_Delay.Value);
			m_spell.OnSpellFinished();
		}
	}
}
