using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D52 RID: 3410
	[ActionCategory(ActionCategory.Rect)]
	[Tooltip("Tests if 2 Rects overlap.")]
	public class RectOverlaps : FsmStateAction
	{
		// Token: 0x0600A3A0 RID: 41888 RVA: 0x0033FEDC File Offset: 0x0033E0DC
		public override void Reset()
		{
			this.rect1 = new FsmRect
			{
				UseVariable = true
			};
			this.rect2 = new FsmRect
			{
				UseVariable = true
			};
			this.storeResult = null;
			this.trueEvent = null;
			this.falseEvent = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A3A1 RID: 41889 RVA: 0x0033FF29 File Offset: 0x0033E129
		public override void OnEnter()
		{
			this.DoRectOverlap();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A3A2 RID: 41890 RVA: 0x0033FF3F File Offset: 0x0033E13F
		public override void OnUpdate()
		{
			this.DoRectOverlap();
		}

		// Token: 0x0600A3A3 RID: 41891 RVA: 0x0033FF48 File Offset: 0x0033E148
		private void DoRectOverlap()
		{
			if (this.rect1.IsNone || this.rect2.IsNone)
			{
				return;
			}
			bool flag = RectOverlaps.Intersect(this.rect1.Value, this.rect2.Value);
			this.storeResult.Value = flag;
			base.Fsm.Event(flag ? this.trueEvent : this.falseEvent);
		}

		// Token: 0x0600A3A4 RID: 41892 RVA: 0x0033FFB4 File Offset: 0x0033E1B4
		public static bool Intersect(Rect a, Rect b)
		{
			RectOverlaps.FlipNegative(ref a);
			RectOverlaps.FlipNegative(ref b);
			bool flag = a.xMin < b.xMax;
			bool flag2 = a.xMax > b.xMin;
			bool flag3 = a.yMin < b.yMax;
			bool flag4 = a.yMax > b.yMin;
			return flag && flag2 && flag3 && flag4;
		}

		// Token: 0x0600A3A5 RID: 41893 RVA: 0x00340018 File Offset: 0x0033E218
		public static void FlipNegative(ref Rect r)
		{
			if (r.width < 0f)
			{
				r.x -= (r.width *= -1f);
			}
			if (r.height < 0f)
			{
				r.y -= (r.height *= -1f);
			}
		}

		// Token: 0x040089EE RID: 35310
		[RequiredField]
		[Tooltip("First Rectangle.")]
		public FsmRect rect1;

		// Token: 0x040089EF RID: 35311
		[RequiredField]
		[Tooltip("Second Rectangle.")]
		public FsmRect rect2;

		// Token: 0x040089F0 RID: 35312
		[Tooltip("Event to send if the Rects overlap.")]
		public FsmEvent trueEvent;

		// Token: 0x040089F1 RID: 35313
		[Tooltip("Event to send if the Rects do not overlap.")]
		public FsmEvent falseEvent;

		// Token: 0x040089F2 RID: 35314
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a variable.")]
		public FsmBool storeResult;

		// Token: 0x040089F3 RID: 35315
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
