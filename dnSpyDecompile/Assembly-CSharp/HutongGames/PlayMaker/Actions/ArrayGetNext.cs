using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BC0 RID: 3008
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Each time this action is called it gets the next item from a Array. \nThis lets you quickly loop through all the items of an array to perform actions on them.")]
	public class ArrayGetNext : FsmStateAction
	{
		// Token: 0x06009C70 RID: 40048 RVA: 0x00325421 File Offset: 0x00323621
		public override void Reset()
		{
			this.array = null;
			this.startIndex = null;
			this.endIndex = null;
			this.currentIndex = null;
			this.loopEvent = null;
			this.finishedEvent = null;
			this.resetFlag = null;
			this.result = null;
		}

		// Token: 0x06009C71 RID: 40049 RVA: 0x0032545C File Offset: 0x0032365C
		public override void OnEnter()
		{
			if (this.nextItemIndex == 0 && this.startIndex.Value > 0)
			{
				this.nextItemIndex = this.startIndex.Value;
			}
			if (this.resetFlag.Value)
			{
				this.nextItemIndex = this.startIndex.Value;
				this.resetFlag.Value = false;
			}
			this.DoGetNextItem();
			base.Finish();
		}

		// Token: 0x06009C72 RID: 40050 RVA: 0x003254C8 File Offset: 0x003236C8
		private void DoGetNextItem()
		{
			if (this.nextItemIndex >= this.array.Length)
			{
				this.nextItemIndex = 0;
				this.currentIndex.Value = this.array.Length - 1;
				base.Fsm.Event(this.finishedEvent);
				return;
			}
			this.result.SetValue(this.array.Get(this.nextItemIndex));
			if (this.nextItemIndex >= this.array.Length)
			{
				this.nextItemIndex = 0;
				this.currentIndex.Value = this.array.Length - 1;
				base.Fsm.Event(this.finishedEvent);
				return;
			}
			if (this.endIndex.Value > 0 && this.nextItemIndex >= this.endIndex.Value)
			{
				this.nextItemIndex = 0;
				this.currentIndex.Value = this.endIndex.Value;
				base.Fsm.Event(this.finishedEvent);
				return;
			}
			this.nextItemIndex++;
			this.currentIndex.Value = this.nextItemIndex - 1;
			if (this.loopEvent != null)
			{
				base.Fsm.Event(this.loopEvent);
			}
		}

		// Token: 0x040081E3 RID: 33251
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to use.")]
		public FsmArray array;

		// Token: 0x040081E4 RID: 33252
		[Tooltip("From where to start iteration, leave as 0 to start from the beginning")]
		public FsmInt startIndex;

		// Token: 0x040081E5 RID: 33253
		[Tooltip("When to end iteration, leave as 0 to iterate until the end")]
		public FsmInt endIndex;

		// Token: 0x040081E6 RID: 33254
		[Tooltip("Event to send to get the next item.")]
		public FsmEvent loopEvent;

		// Token: 0x040081E7 RID: 33255
		[Tooltip("If you want to reset the iteration, raise this flag to true when you enter the state, it will indicate you want to start from the beginning again")]
		[UIHint(UIHint.Variable)]
		public FsmBool resetFlag;

		// Token: 0x040081E8 RID: 33256
		[Tooltip("Event to send when there are no more items.")]
		public FsmEvent finishedEvent;

		// Token: 0x040081E9 RID: 33257
		[ActionSection("Result")]
		[MatchElementType("array")]
		[UIHint(UIHint.Variable)]
		public FsmVar result;

		// Token: 0x040081EA RID: 33258
		[UIHint(UIHint.Variable)]
		public FsmInt currentIndex;

		// Token: 0x040081EB RID: 33259
		private int nextItemIndex;
	}
}
