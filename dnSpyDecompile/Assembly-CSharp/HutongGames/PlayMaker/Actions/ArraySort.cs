using System;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BC7 RID: 3015
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Sort items in an Array.")]
	public class ArraySort : FsmStateAction
	{
		// Token: 0x06009C8A RID: 40074 RVA: 0x003259C6 File Offset: 0x00323BC6
		public override void Reset()
		{
			this.array = null;
		}

		// Token: 0x06009C8B RID: 40075 RVA: 0x003259D0 File Offset: 0x00323BD0
		public override void OnEnter()
		{
			List<object> list = new List<object>(this.array.Values);
			list.Sort();
			this.array.Values = list.ToArray();
			base.Finish();
		}

		// Token: 0x04008202 RID: 33282
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array to sort.")]
		public FsmArray array;
	}
}
