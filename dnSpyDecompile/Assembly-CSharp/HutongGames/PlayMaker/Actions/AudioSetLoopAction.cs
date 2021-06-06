using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F1E RID: 3870
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Sets looping on the AudioSource on a Game Object.")]
	public class AudioSetLoopAction : FsmStateAction
	{
		// Token: 0x0600AC01 RID: 44033 RVA: 0x0035B0DE File Offset: 0x003592DE
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_Loop = false;
		}

		// Token: 0x0600AC02 RID: 44034 RVA: 0x0035B0F4 File Offset: 0x003592F4
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget != null)
			{
				AudioSource component = ownerDefaultTarget.GetComponent<AudioSource>();
				if (component != null)
				{
					component.loop = this.m_Loop.Value;
				}
			}
			base.Finish();
		}

		// Token: 0x040092D2 RID: 37586
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x040092D3 RID: 37587
		public FsmBool m_Loop;
	}
}
