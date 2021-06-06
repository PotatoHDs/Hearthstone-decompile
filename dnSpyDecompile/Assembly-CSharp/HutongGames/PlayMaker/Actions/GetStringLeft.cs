using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C88 RID: 3208
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Gets the Left n characters from a String Variable.")]
	public class GetStringLeft : FsmStateAction
	{
		// Token: 0x06009FE4 RID: 40932 RVA: 0x0032F7E3 File Offset: 0x0032D9E3
		public override void Reset()
		{
			this.stringVariable = null;
			this.charCount = 0;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009FE5 RID: 40933 RVA: 0x0032F806 File Offset: 0x0032DA06
		public override void OnEnter()
		{
			this.DoGetStringLeft();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009FE6 RID: 40934 RVA: 0x0032F81C File Offset: 0x0032DA1C
		public override void OnUpdate()
		{
			this.DoGetStringLeft();
		}

		// Token: 0x06009FE7 RID: 40935 RVA: 0x0032F824 File Offset: 0x0032DA24
		private void DoGetStringLeft()
		{
			if (this.stringVariable.IsNone)
			{
				return;
			}
			if (this.storeResult.IsNone)
			{
				return;
			}
			this.storeResult.Value = this.stringVariable.Value.Substring(0, Mathf.Clamp(this.charCount.Value, 0, this.stringVariable.Value.Length));
		}

		// Token: 0x0400856B RID: 34155
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString stringVariable;

		// Token: 0x0400856C RID: 34156
		[Tooltip("Number of characters to get.")]
		public FsmInt charCount;

		// Token: 0x0400856D RID: 34157
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString storeResult;

		// Token: 0x0400856E RID: 34158
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
