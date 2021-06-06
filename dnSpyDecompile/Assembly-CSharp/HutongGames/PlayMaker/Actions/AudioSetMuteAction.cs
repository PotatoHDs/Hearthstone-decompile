using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F1F RID: 3871
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Mute/unmute the Audio Source on a Game Object.")]
	public class AudioSetMuteAction : FsmStateAction
	{
		// Token: 0x0600AC04 RID: 44036 RVA: 0x0035B143 File Offset: 0x00359343
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_Mute = false;
		}

		// Token: 0x0600AC05 RID: 44037 RVA: 0x0035B158 File Offset: 0x00359358
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget != null)
			{
				AudioSource component = ownerDefaultTarget.GetComponent<AudioSource>();
				if (component != null)
				{
					component.mute = this.m_Mute.Value;
				}
			}
			base.Finish();
		}

		// Token: 0x040092D4 RID: 37588
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x040092D5 RID: 37589
		public FsmBool m_Mute;
	}
}
