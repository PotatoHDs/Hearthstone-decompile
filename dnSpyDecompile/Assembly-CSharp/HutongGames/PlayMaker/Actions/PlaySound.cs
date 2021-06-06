using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D34 RID: 3380
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Plays an Audio Clip at a position defined by a Game Object or Vector3. If a position is defined, it takes priority over the game object. This action doesn't require an Audio Source component, but offers less control than Audio actions.")]
	public class PlaySound : FsmStateAction
	{
		// Token: 0x0600A2F7 RID: 41719 RVA: 0x0033E314 File Offset: 0x0033C514
		public override void Reset()
		{
			this.gameObject = null;
			this.position = new FsmVector3
			{
				UseVariable = true
			};
			this.clip = null;
			this.volume = 1f;
		}

		// Token: 0x0600A2F8 RID: 41720 RVA: 0x0033E346 File Offset: 0x0033C546
		public override void OnEnter()
		{
			this.DoPlaySound();
			base.Finish();
		}

		// Token: 0x0600A2F9 RID: 41721 RVA: 0x0033E354 File Offset: 0x0033C554
		private void DoPlaySound()
		{
			AudioClip x = this.clip.Value as AudioClip;
			if (x == null)
			{
				base.LogWarning("Missing Audio Clip!");
				return;
			}
			if (!this.position.IsNone)
			{
				AudioSource.PlayClipAtPoint(x, this.position.Value, this.volume.Value);
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			AudioSource.PlayClipAtPoint(x, ownerDefaultTarget.transform.position, this.volume.Value);
		}

		// Token: 0x04008956 RID: 35158
		public FsmOwnerDefault gameObject;

		// Token: 0x04008957 RID: 35159
		public FsmVector3 position;

		// Token: 0x04008958 RID: 35160
		[RequiredField]
		[Title("Audio Clip")]
		[ObjectType(typeof(AudioClip))]
		public FsmObject clip;

		// Token: 0x04008959 RID: 35161
		[HasFloatSlider(0f, 1f)]
		public FsmFloat volume = 1f;
	}
}
