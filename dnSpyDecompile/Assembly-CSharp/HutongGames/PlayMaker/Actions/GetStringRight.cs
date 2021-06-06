using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C8A RID: 3210
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Gets the Right n characters from a String.")]
	public class GetStringRight : FsmStateAction
	{
		// Token: 0x06009FEE RID: 40942 RVA: 0x0032F8EE File Offset: 0x0032DAEE
		public override void Reset()
		{
			this.stringVariable = null;
			this.charCount = 0;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009FEF RID: 40943 RVA: 0x0032F911 File Offset: 0x0032DB11
		public override void OnEnter()
		{
			this.DoGetStringRight();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009FF0 RID: 40944 RVA: 0x0032F927 File Offset: 0x0032DB27
		public override void OnUpdate()
		{
			this.DoGetStringRight();
		}

		// Token: 0x06009FF1 RID: 40945 RVA: 0x0032F930 File Offset: 0x0032DB30
		private void DoGetStringRight()
		{
			if (this.stringVariable.IsNone)
			{
				return;
			}
			if (this.storeResult.IsNone)
			{
				return;
			}
			string value = this.stringVariable.Value;
			int num = Mathf.Clamp(this.charCount.Value, 0, value.Length);
			this.storeResult.Value = value.Substring(value.Length - num, num);
		}

		// Token: 0x04008572 RID: 34162
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString stringVariable;

		// Token: 0x04008573 RID: 34163
		[Tooltip("Number of characters to get.")]
		public FsmInt charCount;

		// Token: 0x04008574 RID: 34164
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString storeResult;

		// Token: 0x04008575 RID: 34165
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
