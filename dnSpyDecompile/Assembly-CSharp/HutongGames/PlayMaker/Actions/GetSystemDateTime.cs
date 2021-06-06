using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C8C RID: 3212
	[ActionCategory(ActionCategory.Time)]
	[Tooltip("Gets system date and time info and stores it in a string variable. An optional format string gives you a lot of control over the formatting (see online docs for format syntax).")]
	public class GetSystemDateTime : FsmStateAction
	{
		// Token: 0x06009FF8 RID: 40952 RVA: 0x0032FA34 File Offset: 0x0032DC34
		public override void Reset()
		{
			this.storeString = null;
			this.format = "MM/dd/yyyy HH:mm";
		}

		// Token: 0x06009FF9 RID: 40953 RVA: 0x0032FA50 File Offset: 0x0032DC50
		public override void OnEnter()
		{
			this.storeString.Value = DateTime.Now.ToString(this.format.Value);
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009FFA RID: 40954 RVA: 0x0032FA90 File Offset: 0x0032DC90
		public override void OnUpdate()
		{
			this.storeString.Value = DateTime.Now.ToString(this.format.Value);
		}

		// Token: 0x0400857B RID: 34171
		[UIHint(UIHint.Variable)]
		[Tooltip("Store System DateTime as a string.")]
		public FsmString storeString;

		// Token: 0x0400857C RID: 34172
		[Tooltip("Optional format string. E.g., MM/dd/yyyy HH:mm")]
		public FsmString format;

		// Token: 0x0400857D RID: 34173
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
