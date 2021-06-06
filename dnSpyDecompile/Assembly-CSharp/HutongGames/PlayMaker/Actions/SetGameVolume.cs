using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DCB RID: 3531
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Sets the global sound volume.")]
	public class SetGameVolume : FsmStateAction
	{
		// Token: 0x0600A5E7 RID: 42471 RVA: 0x00348242 File Offset: 0x00346442
		public override void Reset()
		{
			this.volume = 1f;
			this.everyFrame = false;
		}

		// Token: 0x0600A5E8 RID: 42472 RVA: 0x0034825B File Offset: 0x0034645B
		public override void OnEnter()
		{
			AudioListener.volume = this.volume.Value;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A5E9 RID: 42473 RVA: 0x0034827B File Offset: 0x0034647B
		public override void OnUpdate()
		{
			AudioListener.volume = this.volume.Value;
		}

		// Token: 0x04008C96 RID: 35990
		[RequiredField]
		[HasFloatSlider(0f, 1f)]
		public FsmFloat volume;

		// Token: 0x04008C97 RID: 35991
		public bool everyFrame;
	}
}
