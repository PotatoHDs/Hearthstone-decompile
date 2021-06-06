using System;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BC4 RID: 3012
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Reverse the order of items in an Array.")]
	public class ArrayReverse : FsmStateAction
	{
		// Token: 0x06009C7F RID: 40063 RVA: 0x003257CE File Offset: 0x003239CE
		public override void Reset()
		{
			this.array = null;
		}

		// Token: 0x06009C80 RID: 40064 RVA: 0x003257D8 File Offset: 0x003239D8
		public override void OnEnter()
		{
			List<object> list = new List<object>(this.array.Values);
			list.Reverse();
			this.array.Values = list.ToArray();
			base.Finish();
		}

		// Token: 0x040081F9 RID: 33273
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array to reverse.")]
		public FsmArray array;
	}
}
