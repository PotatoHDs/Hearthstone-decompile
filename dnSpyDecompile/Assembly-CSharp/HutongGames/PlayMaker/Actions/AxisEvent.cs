using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BD5 RID: 3029
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Sends events based on the direction of Input Axis (Left/Right/Up/Down...).")]
	public class AxisEvent : FsmStateAction
	{
		// Token: 0x06009CBB RID: 40123 RVA: 0x0032658C File Offset: 0x0032478C
		public override void Reset()
		{
			this.horizontalAxis = "Horizontal";
			this.verticalAxis = "Vertical";
			this.leftEvent = null;
			this.rightEvent = null;
			this.upEvent = null;
			this.downEvent = null;
			this.anyDirection = null;
			this.noDirection = null;
		}

		// Token: 0x06009CBC RID: 40124 RVA: 0x003265E4 File Offset: 0x003247E4
		public override void OnUpdate()
		{
			float num = (this.horizontalAxis.Value != "") ? Input.GetAxis(this.horizontalAxis.Value) : 0f;
			float num2 = (this.verticalAxis.Value != "") ? Input.GetAxis(this.verticalAxis.Value) : 0f;
			if ((num * num + num2 * num2).Equals(0f))
			{
				if (this.noDirection != null)
				{
					base.Fsm.Event(this.noDirection);
				}
				return;
			}
			float num3 = Mathf.Atan2(num2, num) * 57.29578f + 45f;
			if (num3 < 0f)
			{
				num3 += 360f;
			}
			int num4 = (int)(num3 / 90f);
			if (num4 == 0 && this.rightEvent != null)
			{
				base.Fsm.Event(this.rightEvent);
				return;
			}
			if (num4 == 1 && this.upEvent != null)
			{
				base.Fsm.Event(this.upEvent);
				return;
			}
			if (num4 == 2 && this.leftEvent != null)
			{
				base.Fsm.Event(this.leftEvent);
				return;
			}
			if (num4 == 3 && this.downEvent != null)
			{
				base.Fsm.Event(this.downEvent);
				return;
			}
			if (this.anyDirection != null)
			{
				base.Fsm.Event(this.anyDirection);
			}
		}

		// Token: 0x04008230 RID: 33328
		[Tooltip("Horizontal axis as defined in the Input Manager")]
		public FsmString horizontalAxis;

		// Token: 0x04008231 RID: 33329
		[Tooltip("Vertical axis as defined in the Input Manager")]
		public FsmString verticalAxis;

		// Token: 0x04008232 RID: 33330
		[Tooltip("Event to send if input is to the left.")]
		public FsmEvent leftEvent;

		// Token: 0x04008233 RID: 33331
		[Tooltip("Event to send if input is to the right.")]
		public FsmEvent rightEvent;

		// Token: 0x04008234 RID: 33332
		[Tooltip("Event to send if input is to the up.")]
		public FsmEvent upEvent;

		// Token: 0x04008235 RID: 33333
		[Tooltip("Event to send if input is to the down.")]
		public FsmEvent downEvent;

		// Token: 0x04008236 RID: 33334
		[Tooltip("Event to send if input is in any direction.")]
		public FsmEvent anyDirection;

		// Token: 0x04008237 RID: 33335
		[Tooltip("Event to send if no axis input (centered).")]
		public FsmEvent noDirection;
	}
}
