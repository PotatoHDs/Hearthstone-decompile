using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BBC RID: 3004
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Check if an Array contains a value. Optionally get its index.")]
	public class ArrayContains : FsmStateAction
	{
		// Token: 0x06009C58 RID: 40024 RVA: 0x00324FE5 File Offset: 0x003231E5
		public override void Reset()
		{
			this.array = null;
			this.value = null;
			this.index = null;
			this.isContained = null;
			this.isContainedEvent = null;
			this.isNotContainedEvent = null;
		}

		// Token: 0x06009C59 RID: 40025 RVA: 0x00325011 File Offset: 0x00323211
		public override void OnEnter()
		{
			this.DoCheckContainsValue();
			base.Finish();
		}

		// Token: 0x06009C5A RID: 40026 RVA: 0x00325020 File Offset: 0x00323220
		private void DoCheckContainsValue()
		{
			this.value.UpdateValue();
			int num;
			if (this.value.GetValue() == null || this.value.GetValue().Equals(null))
			{
				num = Array.FindIndex<object>(this.array.Values, (object x) => x == null || x.Equals(null));
			}
			else
			{
				num = Array.IndexOf<object>(this.array.Values, this.value.GetValue());
			}
			bool flag = num != -1;
			this.isContained.Value = flag;
			this.index.Value = num;
			if (flag)
			{
				base.Fsm.Event(this.isContainedEvent);
				return;
			}
			base.Fsm.Event(this.isNotContainedEvent);
		}

		// Token: 0x040081D0 RID: 33232
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to use.")]
		public FsmArray array;

		// Token: 0x040081D1 RID: 33233
		[RequiredField]
		[MatchElementType("array")]
		[Tooltip("The value to check against in the array.")]
		public FsmVar value;

		// Token: 0x040081D2 RID: 33234
		[ActionSection("Result")]
		[Tooltip("The index of the value in the array.")]
		[UIHint(UIHint.Variable)]
		public FsmInt index;

		// Token: 0x040081D3 RID: 33235
		[Tooltip("Store in a bool whether it contains that element or not (described below)")]
		[UIHint(UIHint.Variable)]
		public FsmBool isContained;

		// Token: 0x040081D4 RID: 33236
		[Tooltip("Event sent if the array contains that element (described below)")]
		public FsmEvent isContainedEvent;

		// Token: 0x040081D5 RID: 33237
		[Tooltip("Event sent if the array does not contains that element (described below)")]
		public FsmEvent isNotContainedEvent;
	}
}
