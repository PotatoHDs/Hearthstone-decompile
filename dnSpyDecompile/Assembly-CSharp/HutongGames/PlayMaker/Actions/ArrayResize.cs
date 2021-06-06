using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BC3 RID: 3011
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Resize an array.")]
	public class ArrayResize : FsmStateAction
	{
		// Token: 0x06009C7D RID: 40061 RVA: 0x00325764 File Offset: 0x00323964
		public override void OnEnter()
		{
			if (this.newSize.Value >= 0)
			{
				this.array.Resize(this.newSize.Value);
			}
			else
			{
				base.LogError("Size out of range: " + this.newSize.Value);
				base.Fsm.Event(this.sizeOutOfRangeEvent);
			}
			base.Finish();
		}

		// Token: 0x040081F6 RID: 33270
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to resize")]
		public FsmArray array;

		// Token: 0x040081F7 RID: 33271
		[Tooltip("The new size of the array.")]
		public FsmInt newSize;

		// Token: 0x040081F8 RID: 33272
		[Tooltip("The event to trigger if the new size is out of range")]
		public FsmEvent sizeOutOfRangeEvent;
	}
}
