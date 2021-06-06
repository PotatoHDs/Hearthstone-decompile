using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Tells the game that a Spell is finished, allowing the game to progress.")]
	public class SpellEventAction : SpellAction
	{
		public FsmOwnerDefault m_SpellObject;

		public FsmFloat m_Delay = 0f;

		public FsmString m_EventName = "";

		public FsmObject m_EventData;

		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(m_SpellObject);
		}

		public override void Reset()
		{
			m_SpellObject = null;
			m_Delay = 0f;
			m_EventName = "";
			m_EventData = null;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (m_spell == null)
			{
				Debug.LogErrorFormat("{0}.OnEnter() - FAILED to find Spell component on Owner \"{1}\"", this, base.Owner);
				return;
			}
			if (m_Delay.Value > 0f)
			{
				m_spell.StartCoroutine(DelaySpellEvent());
			}
			else
			{
				m_spell.OnSpellEvent(m_EventName.Value, m_EventData.Value);
			}
			Finish();
		}

		private IEnumerator DelaySpellEvent()
		{
			yield return new WaitForSeconds(m_Delay.Value);
			m_spell.OnSpellEvent(m_EventName.Value, m_EventData.Value);
		}
	}
}
