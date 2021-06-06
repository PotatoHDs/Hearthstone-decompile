using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BB9 RID: 3001
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Add values to an array.")]
	public class ArrayAddRange : FsmStateAction
	{
		// Token: 0x06009C4C RID: 40012 RVA: 0x00324DD3 File Offset: 0x00322FD3
		public override void Reset()
		{
			this.array = null;
			this.variables = new FsmVar[2];
		}

		// Token: 0x06009C4D RID: 40013 RVA: 0x00324DE8 File Offset: 0x00322FE8
		public override void OnEnter()
		{
			this.DoAddRange();
			base.Finish();
		}

		// Token: 0x06009C4E RID: 40014 RVA: 0x00324DF8 File Offset: 0x00322FF8
		private void DoAddRange()
		{
			int num = this.variables.Length;
			if (num > 0)
			{
				this.array.Resize(this.array.Length + num);
				foreach (FsmVar fsmVar in this.variables)
				{
					fsmVar.UpdateValue();
					this.array.Set(this.array.Length - num, fsmVar.GetValue());
					num--;
				}
			}
		}

		// Token: 0x040081C6 RID: 33222
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to use.")]
		public FsmArray array;

		// Token: 0x040081C7 RID: 33223
		[RequiredField]
		[MatchElementType("array")]
		[Tooltip("The variables to add.")]
		public FsmVar[] variables;
	}
}
