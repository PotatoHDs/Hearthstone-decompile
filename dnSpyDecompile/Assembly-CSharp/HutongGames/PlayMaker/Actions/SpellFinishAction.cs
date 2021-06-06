using System;
using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F87 RID: 3975
	[ActionCategory("Pegasus")]
	[Tooltip("Tells the game that a Spell is finished, allowing the game to progress.")]
	public class SpellFinishAction : SpellAction
	{
		// Token: 0x0600ADB4 RID: 44468 RVA: 0x003620DB File Offset: 0x003602DB
		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_SpellObject);
		}

		// Token: 0x0600ADB5 RID: 44469 RVA: 0x003620F0 File Offset: 0x003602F0
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_spell == null)
			{
				Debug.LogError(string.Format("{0}.OnEnter() - FAILED to find Spell component on Owner \"{1}\"", this, base.Owner));
				return;
			}
			if (this.m_Delay.Value > 0f)
			{
				this.m_spell.StartCoroutine(this.DelaySpellFinished());
			}
			else
			{
				this.m_spell.OnSpellFinished();
			}
			base.Finish();
		}

		// Token: 0x0600ADB6 RID: 44470 RVA: 0x0036215F File Offset: 0x0036035F
		private IEnumerator DelaySpellFinished()
		{
			yield return new WaitForSeconds(this.m_Delay.Value);
			this.m_spell.OnSpellFinished();
			yield break;
		}

		// Token: 0x04009493 RID: 38035
		public FsmOwnerDefault m_SpellObject;

		// Token: 0x04009494 RID: 38036
		public FsmFloat m_Delay = 0f;
	}
}
