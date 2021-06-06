using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D19 RID: 3353
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Sets the gravity vector, or individual axis.")]
	public class SetGravity2d : FsmStateAction
	{
		// Token: 0x0600A279 RID: 41593 RVA: 0x0033C61A File Offset: 0x0033A81A
		public override void Reset()
		{
			this.vector = null;
			this.x = new FsmFloat
			{
				UseVariable = true
			};
			this.y = new FsmFloat
			{
				UseVariable = true
			};
			this.everyFrame = false;
		}

		// Token: 0x0600A27A RID: 41594 RVA: 0x0033C64E File Offset: 0x0033A84E
		public override void OnEnter()
		{
			this.DoSetGravity();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A27B RID: 41595 RVA: 0x0033C664 File Offset: 0x0033A864
		public override void OnUpdate()
		{
			this.DoSetGravity();
		}

		// Token: 0x0600A27C RID: 41596 RVA: 0x0033C66C File Offset: 0x0033A86C
		private void DoSetGravity()
		{
			Vector2 value = this.vector.Value;
			if (!this.x.IsNone)
			{
				value.x = this.x.Value;
			}
			if (!this.y.IsNone)
			{
				value.y = this.y.Value;
			}
			Physics2D.gravity = value;
		}

		// Token: 0x040088E2 RID: 35042
		[Tooltip("Gravity as Vector2.")]
		public FsmVector2 vector;

		// Token: 0x040088E3 RID: 35043
		[Tooltip("Override the x value of the gravity")]
		public FsmFloat x;

		// Token: 0x040088E4 RID: 35044
		[Tooltip("Override the y value of the gravity")]
		public FsmFloat y;

		// Token: 0x040088E5 RID: 35045
		[Tooltip("Repeat every frame")]
		public bool everyFrame;
	}
}
