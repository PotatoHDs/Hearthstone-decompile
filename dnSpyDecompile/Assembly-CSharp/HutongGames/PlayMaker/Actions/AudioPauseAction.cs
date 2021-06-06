using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F19 RID: 3865
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Pauses the Audio Source of a Game Object.")]
	public class AudioPauseAction : FsmStateAction
	{
		// Token: 0x0600ABE3 RID: 44003 RVA: 0x0035A745 File Offset: 0x00358945
		public override void Reset()
		{
			this.m_GameObject = null;
		}

		// Token: 0x0600ABE4 RID: 44004 RVA: 0x0035A750 File Offset: 0x00358950
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget != null)
			{
				AudioSource component = ownerDefaultTarget.GetComponent<AudioSource>();
				if (component != null)
				{
					SoundManager.Get().Pause(component);
				}
			}
			base.Finish();
		}

		// Token: 0x040092A4 RID: 37540
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault m_GameObject;
	}
}
