using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F26 RID: 3878
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Stops an Audio Source on a Game Object.")]
	public class AudioStopAction : FsmStateAction
	{
		// Token: 0x0600AC23 RID: 44067 RVA: 0x0035B555 File Offset: 0x00359755
		public override void Reset()
		{
			this.m_GameObject = null;
		}

		// Token: 0x0600AC24 RID: 44068 RVA: 0x0035B560 File Offset: 0x00359760
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget != null)
			{
				AudioSource component = ownerDefaultTarget.GetComponent<AudioSource>();
				if (component != null)
				{
					SoundManager.Get().Stop(component);
				}
			}
			base.Finish();
		}

		// Token: 0x040092E9 RID: 37609
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault m_GameObject;
	}
}
