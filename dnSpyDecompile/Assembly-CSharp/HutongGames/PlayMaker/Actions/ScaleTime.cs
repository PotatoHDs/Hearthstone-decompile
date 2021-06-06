using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D7B RID: 3451
	[ActionCategory(ActionCategory.Time)]
	[Tooltip("Scales time: 1 = normal, 0.5 = half speed, 2 = double speed.")]
	public class ScaleTime : FsmStateAction
	{
		// Token: 0x0600A478 RID: 42104 RVA: 0x00343117 File Offset: 0x00341317
		public override void Reset()
		{
			this.timeScale = 1f;
			this.adjustFixedDeltaTime = true;
			this.everyFrame = false;
		}

		// Token: 0x0600A479 RID: 42105 RVA: 0x0034313C File Offset: 0x0034133C
		public override void OnEnter()
		{
			this.DoTimeScale();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A47A RID: 42106 RVA: 0x00343152 File Offset: 0x00341352
		public override void OnUpdate()
		{
			this.DoTimeScale();
		}

		// Token: 0x0600A47B RID: 42107 RVA: 0x0034315A File Offset: 0x0034135A
		private void DoTimeScale()
		{
			Time.timeScale = this.timeScale.Value;
			if (this.adjustFixedDeltaTime.Value)
			{
				Time.fixedDeltaTime = 0.02f * Time.timeScale;
			}
		}

		// Token: 0x04008AD3 RID: 35539
		[RequiredField]
		[HasFloatSlider(0f, 4f)]
		[Tooltip("Scales time: 1 = normal, 0.5 = half speed, 2 = double speed.")]
		public FsmFloat timeScale;

		// Token: 0x04008AD4 RID: 35540
		[Tooltip("Adjust the fixed physics time step to match the time scale.")]
		public FsmBool adjustFixedDeltaTime;

		// Token: 0x04008AD5 RID: 35541
		[Tooltip("Repeat every frame. Useful when animating the value.")]
		public bool everyFrame;
	}
}
