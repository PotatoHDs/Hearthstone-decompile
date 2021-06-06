using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BBF RID: 3007
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Get a value at an index. Index must be between 0 and the number of items -1. First item is index 0.")]
	public class ArrayGet : FsmStateAction
	{
		// Token: 0x06009C6B RID: 40043 RVA: 0x0032535C File Offset: 0x0032355C
		public override void Reset()
		{
			this.array = null;
			this.index = null;
			this.everyFrame = false;
			this.storeValue = null;
			this.indexOutOfRange = null;
		}

		// Token: 0x06009C6C RID: 40044 RVA: 0x00325381 File Offset: 0x00323581
		public override void OnEnter()
		{
			this.DoGetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009C6D RID: 40045 RVA: 0x00325397 File Offset: 0x00323597
		public override void OnUpdate()
		{
			this.DoGetValue();
		}

		// Token: 0x06009C6E RID: 40046 RVA: 0x003253A0 File Offset: 0x003235A0
		private void DoGetValue()
		{
			if (this.array.IsNone || this.storeValue.IsNone)
			{
				return;
			}
			if (this.index.Value >= 0 && this.index.Value < this.array.Length)
			{
				this.storeValue.SetValue(this.array.Get(this.index.Value));
				return;
			}
			base.Fsm.Event(this.indexOutOfRange);
		}

		// Token: 0x040081DE RID: 33246
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to use.")]
		public FsmArray array;

		// Token: 0x040081DF RID: 33247
		[Tooltip("The index into the array.")]
		public FsmInt index;

		// Token: 0x040081E0 RID: 33248
		[RequiredField]
		[MatchElementType("array")]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the value in a variable.")]
		public FsmVar storeValue;

		// Token: 0x040081E1 RID: 33249
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		// Token: 0x040081E2 RID: 33250
		[ActionSection("Events")]
		[Tooltip("The event to trigger if the index is out of range")]
		public FsmEvent indexOutOfRange;
	}
}
