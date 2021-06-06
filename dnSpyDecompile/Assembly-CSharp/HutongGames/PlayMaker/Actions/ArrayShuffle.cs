using System;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BC6 RID: 3014
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Shuffle values in an array. Optionally set a start index and range to shuffle only part of the array.")]
	public class ArrayShuffle : FsmStateAction
	{
		// Token: 0x06009C87 RID: 40071 RVA: 0x003258D7 File Offset: 0x00323AD7
		public override void Reset()
		{
			this.array = null;
			this.startIndex = new FsmInt
			{
				UseVariable = true
			};
			this.shufflingRange = new FsmInt
			{
				UseVariable = true
			};
		}

		// Token: 0x06009C88 RID: 40072 RVA: 0x00325904 File Offset: 0x00323B04
		public override void OnEnter()
		{
			List<object> list = new List<object>(this.array.Values);
			int num = 0;
			int num2 = list.Count - 1;
			if (this.startIndex.Value > 0)
			{
				num = Mathf.Min(this.startIndex.Value, num2);
			}
			if (this.shufflingRange.Value > 0)
			{
				num2 = Mathf.Min(list.Count - 1, num + this.shufflingRange.Value);
			}
			for (int i = num2; i > num; i--)
			{
				int index = UnityEngine.Random.Range(num, i + 1);
				object value = list[i];
				list[i] = list[index];
				list[index] = value;
			}
			this.array.Values = list.ToArray();
			base.Finish();
		}

		// Token: 0x040081FF RID: 33279
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array to shuffle.")]
		public FsmArray array;

		// Token: 0x04008200 RID: 33280
		[Tooltip("Optional start Index for the shuffling. Leave it to none or 0 for no effect")]
		public FsmInt startIndex;

		// Token: 0x04008201 RID: 33281
		[Tooltip("Optional range for the shuffling, starting at the start index if greater than 0. Leave it to none or 0 for no effect, it will shuffle the whole array")]
		public FsmInt shufflingRange;
	}
}
