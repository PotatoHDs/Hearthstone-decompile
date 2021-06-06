using System;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BBD RID: 3005
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Delete the item at an index. Index must be between 0 and the number of items -1. First item is index 0.")]
	public class ArrayDeleteAt : FsmStateAction
	{
		// Token: 0x06009C5C RID: 40028 RVA: 0x003250EE File Offset: 0x003232EE
		public override void Reset()
		{
			this.array = null;
			this.index = null;
			this.indexOutOfRangeEvent = null;
		}

		// Token: 0x06009C5D RID: 40029 RVA: 0x00325105 File Offset: 0x00323305
		public override void OnEnter()
		{
			this.DoDeleteAt();
			base.Finish();
		}

		// Token: 0x06009C5E RID: 40030 RVA: 0x00325114 File Offset: 0x00323314
		private void DoDeleteAt()
		{
			if (this.index.Value >= 0 && this.index.Value < this.array.Length)
			{
				List<object> list = new List<object>(this.array.Values);
				list.RemoveAt(this.index.Value);
				this.array.Values = list.ToArray();
				return;
			}
			base.Fsm.Event(this.indexOutOfRangeEvent);
		}

		// Token: 0x040081D6 RID: 33238
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to use.")]
		public FsmArray array;

		// Token: 0x040081D7 RID: 33239
		[Tooltip("The index into the array.")]
		public FsmInt index;

		// Token: 0x040081D8 RID: 33240
		[ActionSection("Result")]
		[Tooltip("The event to trigger if the index is out of range")]
		public FsmEvent indexOutOfRangeEvent;
	}
}
