using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D51 RID: 3409
	[ActionCategory(ActionCategory.Rect)]
	[Tooltip("Tests if a point is inside a rectangle.")]
	public class RectContains : FsmStateAction
	{
		// Token: 0x0600A39B RID: 41883 RVA: 0x0033FDA8 File Offset: 0x0033DFA8
		public override void Reset()
		{
			this.rectangle = new FsmRect
			{
				UseVariable = true
			};
			this.point = new FsmVector3
			{
				UseVariable = true
			};
			this.x = new FsmFloat
			{
				UseVariable = true
			};
			this.y = new FsmFloat
			{
				UseVariable = true
			};
			this.storeResult = null;
			this.trueEvent = null;
			this.falseEvent = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A39C RID: 41884 RVA: 0x0033FE19 File Offset: 0x0033E019
		public override void OnEnter()
		{
			this.DoRectContains();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A39D RID: 41885 RVA: 0x0033FE2F File Offset: 0x0033E02F
		public override void OnUpdate()
		{
			this.DoRectContains();
		}

		// Token: 0x0600A39E RID: 41886 RVA: 0x0033FE38 File Offset: 0x0033E038
		private void DoRectContains()
		{
			if (this.rectangle.IsNone)
			{
				return;
			}
			Vector3 value = this.point.Value;
			if (!this.x.IsNone)
			{
				value.x = this.x.Value;
			}
			if (!this.y.IsNone)
			{
				value.y = this.y.Value;
			}
			bool flag = this.rectangle.Value.Contains(value);
			this.storeResult.Value = flag;
			base.Fsm.Event(flag ? this.trueEvent : this.falseEvent);
		}

		// Token: 0x040089E6 RID: 35302
		[RequiredField]
		[Tooltip("Rectangle to test.")]
		public FsmRect rectangle;

		// Token: 0x040089E7 RID: 35303
		[Tooltip("Point to test.")]
		public FsmVector3 point;

		// Token: 0x040089E8 RID: 35304
		[Tooltip("Specify/override X value.")]
		public FsmFloat x;

		// Token: 0x040089E9 RID: 35305
		[Tooltip("Specify/override Y value.")]
		public FsmFloat y;

		// Token: 0x040089EA RID: 35306
		[Tooltip("Event to send if the Point is inside the Rectangle.")]
		public FsmEvent trueEvent;

		// Token: 0x040089EB RID: 35307
		[Tooltip("Event to send if the Point is outside the Rectangle.")]
		public FsmEvent falseEvent;

		// Token: 0x040089EC RID: 35308
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a variable.")]
		public FsmBool storeResult;

		// Token: 0x040089ED RID: 35309
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
