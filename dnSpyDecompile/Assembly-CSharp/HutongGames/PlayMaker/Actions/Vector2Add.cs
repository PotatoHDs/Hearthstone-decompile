using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EA5 RID: 3749
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Adds a value to Vector2 Variable.")]
	public class Vector2Add : FsmStateAction
	{
		// Token: 0x0600A9D6 RID: 43478 RVA: 0x00353929 File Offset: 0x00351B29
		public override void Reset()
		{
			this.vector2Variable = null;
			this.addVector = new FsmVector2
			{
				UseVariable = true
			};
			this.everyFrame = false;
			this.perSecond = false;
		}

		// Token: 0x0600A9D7 RID: 43479 RVA: 0x00353952 File Offset: 0x00351B52
		public override void OnEnter()
		{
			this.DoVector2Add();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A9D8 RID: 43480 RVA: 0x00353968 File Offset: 0x00351B68
		public override void OnUpdate()
		{
			this.DoVector2Add();
		}

		// Token: 0x0600A9D9 RID: 43481 RVA: 0x00353970 File Offset: 0x00351B70
		private void DoVector2Add()
		{
			if (this.perSecond)
			{
				this.vector2Variable.Value = this.vector2Variable.Value + this.addVector.Value * Time.deltaTime;
				return;
			}
			this.vector2Variable.Value = this.vector2Variable.Value + this.addVector.Value;
		}

		// Token: 0x04009078 RID: 36984
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The vector2 target")]
		public FsmVector2 vector2Variable;

		// Token: 0x04009079 RID: 36985
		[RequiredField]
		[Tooltip("The vector2 to add")]
		public FsmVector2 addVector;

		// Token: 0x0400907A RID: 36986
		[Tooltip("Repeat every frame")]
		public bool everyFrame;

		// Token: 0x0400907B RID: 36987
		[Tooltip("Add the value on a per second bases.")]
		public bool perSecond;
	}
}
