using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DA9 RID: 3497
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Sets the Audio Clip played by the AudioSource component on a Game Object.")]
	public class SetAudioClip : ComponentAction<AudioSource>
	{
		// Token: 0x0600A548 RID: 42312 RVA: 0x00346554 File Offset: 0x00344754
		public override void Reset()
		{
			this.gameObject = null;
			this.audioClip = null;
		}

		// Token: 0x0600A549 RID: 42313 RVA: 0x00346564 File Offset: 0x00344764
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				base.audio.clip = (this.audioClip.Value as AudioClip);
			}
			base.Finish();
		}

		// Token: 0x04008BDC RID: 35804
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		[Tooltip("The GameObject with the AudioSource component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008BDD RID: 35805
		[ObjectType(typeof(AudioClip))]
		[Tooltip("The AudioClip to set.")]
		public FsmObject audioClip;
	}
}
