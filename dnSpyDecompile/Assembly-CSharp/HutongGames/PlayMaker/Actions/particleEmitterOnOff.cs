using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F5C RID: 3932
	[ActionCategory("Pegasus")]
	[Tooltip("Turns a Particle Emitter on and off with optional delay.")]
	public class particleEmitterOnOff : FsmStateAction
	{
		// Token: 0x0600ACFE RID: 44286 RVA: 0x0035F82C File Offset: 0x0035DA2C
		public override void Reset()
		{
			this.gameObject = null;
			this.emitOnOff = false;
			this.delay = 0f;
			this.finishEvent = null;
			this.realTime = false;
		}

		// Token: 0x0600ACFF RID: 44287 RVA: 0x0035F85F File Offset: 0x0035DA5F
		public override void OnEnter()
		{
			if (this.delay.Value <= 0f)
			{
				base.Finish();
				return;
			}
			this.startTime = Time.realtimeSinceStartup;
			this.timer = 0f;
		}

		// Token: 0x0600AD00 RID: 44288 RVA: 0x0035F890 File Offset: 0x0035DA90
		public override void OnUpdate()
		{
			if (this.realTime)
			{
				this.timer = Time.realtimeSinceStartup - this.startTime;
			}
			else
			{
				this.timer += Time.deltaTime;
			}
			if (this.timer > this.delay.Value)
			{
				base.Finish();
				if (this.finishEvent != null)
				{
					base.Fsm.Event(this.finishEvent);
				}
			}
		}

		// Token: 0x040093DD RID: 37853
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x040093DE RID: 37854
		[Tooltip("Set to True to turn it on and False to turn it off.")]
		public FsmBool emitOnOff;

		// Token: 0x040093DF RID: 37855
		[Tooltip("If 0 it just acts like a switch. Values cause it to Toggle value after delay time (sec).")]
		public FsmFloat delay;

		// Token: 0x040093E0 RID: 37856
		public FsmEvent finishEvent;

		// Token: 0x040093E1 RID: 37857
		public bool realTime;

		// Token: 0x040093E2 RID: 37858
		private float startTime;

		// Token: 0x040093E3 RID: 37859
		private float timer;
	}
}
