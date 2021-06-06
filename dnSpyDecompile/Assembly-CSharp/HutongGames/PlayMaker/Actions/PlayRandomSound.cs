using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D33 RID: 3379
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Plays a Random Audio Clip at a position defined by a Game Object or a Vector3. If a position is defined, it takes priority over the game object. You can set the relative weight of the clips to control how often they are selected.")]
	public class PlayRandomSound : FsmStateAction
	{
		// Token: 0x0600A2F3 RID: 41715 RVA: 0x0033E160 File Offset: 0x0033C360
		public override void Reset()
		{
			this.gameObject = null;
			this.position = new FsmVector3
			{
				UseVariable = true
			};
			this.audioClips = new FsmObject[3];
			this.weights = new FsmFloat[]
			{
				1f,
				1f,
				1f
			};
			this.volume = 1f;
			this.noRepeat = false;
		}

		// Token: 0x0600A2F4 RID: 41716 RVA: 0x0033E1E1 File Offset: 0x0033C3E1
		public override void OnEnter()
		{
			this.DoPlayRandomClip();
			base.Finish();
		}

		// Token: 0x0600A2F5 RID: 41717 RVA: 0x0033E1F0 File Offset: 0x0033C3F0
		private void DoPlayRandomClip()
		{
			if (this.audioClips.Length == 0)
			{
				return;
			}
			if (!this.noRepeat.Value || this.weights.Length == 1)
			{
				this.randomIndex = ActionHelpers.GetRandomWeightedIndex(this.weights);
			}
			else
			{
				do
				{
					this.randomIndex = ActionHelpers.GetRandomWeightedIndex(this.weights);
				}
				while (this.randomIndex == this.lastIndex && this.randomIndex != -1);
				this.lastIndex = this.randomIndex;
			}
			if (this.randomIndex != -1)
			{
				AudioClip audioClip = this.audioClips[this.randomIndex].Value as AudioClip;
				if (audioClip != null)
				{
					if (!this.position.IsNone)
					{
						AudioSource.PlayClipAtPoint(audioClip, this.position.Value, this.volume.Value);
						return;
					}
					GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
					if (ownerDefaultTarget == null)
					{
						return;
					}
					AudioSource.PlayClipAtPoint(audioClip, ownerDefaultTarget.transform.position, this.volume.Value);
				}
			}
		}

		// Token: 0x0400894E RID: 35150
		[Tooltip("The GameObject to play the sound.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400894F RID: 35151
		[Tooltip("Use world position instead of GameObject.")]
		public FsmVector3 position;

		// Token: 0x04008950 RID: 35152
		[CompoundArray("Audio Clips", "Audio Clip", "Weight")]
		[ObjectType(typeof(AudioClip))]
		public FsmObject[] audioClips;

		// Token: 0x04008951 RID: 35153
		[HasFloatSlider(0f, 1f)]
		public FsmFloat[] weights;

		// Token: 0x04008952 RID: 35154
		[HasFloatSlider(0f, 1f)]
		public FsmFloat volume = 1f;

		// Token: 0x04008953 RID: 35155
		[Tooltip("Don't play the same sound twice in a row")]
		public FsmBool noRepeat;

		// Token: 0x04008954 RID: 35156
		private int randomIndex;

		// Token: 0x04008955 RID: 35157
		private int lastIndex = -1;
	}
}
