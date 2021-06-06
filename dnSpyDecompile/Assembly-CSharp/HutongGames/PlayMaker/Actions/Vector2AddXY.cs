using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EA6 RID: 3750
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Adds a XY values to Vector2 Variable.")]
	public class Vector2AddXY : FsmStateAction
	{
		// Token: 0x0600A9DB RID: 43483 RVA: 0x003539DC File Offset: 0x00351BDC
		public override void Reset()
		{
			this.vector2Variable = null;
			this.addX = 0f;
			this.addY = 0f;
			this.everyFrame = false;
			this.perSecond = false;
		}

		// Token: 0x0600A9DC RID: 43484 RVA: 0x00353A13 File Offset: 0x00351C13
		public override void OnEnter()
		{
			this.DoVector2AddXYZ();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A9DD RID: 43485 RVA: 0x00353A29 File Offset: 0x00351C29
		public override void OnUpdate()
		{
			this.DoVector2AddXYZ();
		}

		// Token: 0x0600A9DE RID: 43486 RVA: 0x00353A34 File Offset: 0x00351C34
		private void DoVector2AddXYZ()
		{
			Vector2 vector = new Vector2(this.addX.Value, this.addY.Value);
			if (this.perSecond)
			{
				this.vector2Variable.Value += vector * Time.deltaTime;
				return;
			}
			this.vector2Variable.Value += vector;
		}

		// Token: 0x0400907C RID: 36988
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The vector2 target")]
		public FsmVector2 vector2Variable;

		// Token: 0x0400907D RID: 36989
		[Tooltip("The x component to add")]
		public FsmFloat addX;

		// Token: 0x0400907E RID: 36990
		[Tooltip("The y component to add")]
		public FsmFloat addY;

		// Token: 0x0400907F RID: 36991
		[Tooltip("Repeat every frame")]
		public bool everyFrame;

		// Token: 0x04009080 RID: 36992
		[Tooltip("Add the value on a per second bases.")]
		public bool perSecond;
	}
}
