using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DAC RID: 3500
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Sets the Volume of the Audio Clip played by the AudioSource component on a Game Object.")]
	public class SetAudioVolume : ComponentAction<AudioSource>
	{
		// Token: 0x0600A553 RID: 42323 RVA: 0x0034669B File Offset: 0x0034489B
		public override void Reset()
		{
			this.gameObject = null;
			this.volume = 1f;
			this.everyFrame = false;
		}

		// Token: 0x0600A554 RID: 42324 RVA: 0x003466BB File Offset: 0x003448BB
		public override void OnEnter()
		{
			this.DoSetAudioVolume();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A555 RID: 42325 RVA: 0x003466D1 File Offset: 0x003448D1
		public override void OnUpdate()
		{
			this.DoSetAudioVolume();
		}

		// Token: 0x0600A556 RID: 42326 RVA: 0x003466DC File Offset: 0x003448DC
		private void DoSetAudioVolume()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget) && !this.volume.IsNone)
			{
				base.audio.volume = this.volume.Value;
			}
		}

		// Token: 0x04008BE3 RID: 35811
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008BE4 RID: 35812
		[HasFloatSlider(0f, 1f)]
		public FsmFloat volume;

		// Token: 0x04008BE5 RID: 35813
		public bool everyFrame;
	}
}
