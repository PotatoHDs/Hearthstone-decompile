using System;
using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F86 RID: 3974
	[ActionCategory("Pegasus")]
	[Tooltip("Tells the game that a Spell is finished, allowing the game to progress.")]
	public class SpellEventAction : SpellAction
	{
		// Token: 0x0600ADAF RID: 44463 RVA: 0x00361FD5 File Offset: 0x003601D5
		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_SpellObject);
		}

		// Token: 0x0600ADB0 RID: 44464 RVA: 0x00361FE8 File Offset: 0x003601E8
		public override void Reset()
		{
			this.m_SpellObject = null;
			this.m_Delay = 0f;
			this.m_EventName = "";
			this.m_EventData = null;
		}

		// Token: 0x0600ADB1 RID: 44465 RVA: 0x00362018 File Offset: 0x00360218
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_spell == null)
			{
				Debug.LogErrorFormat("{0}.OnEnter() - FAILED to find Spell component on Owner \"{1}\"", new object[]
				{
					this,
					base.Owner
				});
				return;
			}
			if (this.m_Delay.Value > 0f)
			{
				this.m_spell.StartCoroutine(this.DelaySpellEvent());
			}
			else
			{
				this.m_spell.OnSpellEvent(this.m_EventName.Value, this.m_EventData.Value);
			}
			base.Finish();
		}

		// Token: 0x0600ADB2 RID: 44466 RVA: 0x003620A4 File Offset: 0x003602A4
		private IEnumerator DelaySpellEvent()
		{
			yield return new WaitForSeconds(this.m_Delay.Value);
			this.m_spell.OnSpellEvent(this.m_EventName.Value, this.m_EventData.Value);
			yield break;
		}

		// Token: 0x0400948F RID: 38031
		public FsmOwnerDefault m_SpellObject;

		// Token: 0x04009490 RID: 38032
		public FsmFloat m_Delay = 0f;

		// Token: 0x04009491 RID: 38033
		public FsmString m_EventName = "";

		// Token: 0x04009492 RID: 38034
		public FsmObject m_EventData;
	}
}
