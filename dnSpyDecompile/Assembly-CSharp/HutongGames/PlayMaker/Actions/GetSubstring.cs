using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C8B RID: 3211
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Gets a sub-string from a String Variable.")]
	public class GetSubstring : FsmStateAction
	{
		// Token: 0x06009FF3 RID: 40947 RVA: 0x0032F997 File Offset: 0x0032DB97
		public override void Reset()
		{
			this.stringVariable = null;
			this.startIndex = 0;
			this.length = 1;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009FF4 RID: 40948 RVA: 0x0032F9C6 File Offset: 0x0032DBC6
		public override void OnEnter()
		{
			this.DoGetSubstring();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009FF5 RID: 40949 RVA: 0x0032F9DC File Offset: 0x0032DBDC
		public override void OnUpdate()
		{
			this.DoGetSubstring();
		}

		// Token: 0x06009FF6 RID: 40950 RVA: 0x0032F9E4 File Offset: 0x0032DBE4
		private void DoGetSubstring()
		{
			if (this.stringVariable == null)
			{
				return;
			}
			if (this.storeResult == null)
			{
				return;
			}
			this.storeResult.Value = this.stringVariable.Value.Substring(this.startIndex.Value, this.length.Value);
		}

		// Token: 0x04008576 RID: 34166
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString stringVariable;

		// Token: 0x04008577 RID: 34167
		[RequiredField]
		public FsmInt startIndex;

		// Token: 0x04008578 RID: 34168
		[RequiredField]
		public FsmInt length;

		// Token: 0x04008579 RID: 34169
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString storeResult;

		// Token: 0x0400857A RID: 34170
		public bool everyFrame;
	}
}
