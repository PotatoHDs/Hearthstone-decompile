using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BC5 RID: 3013
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Set the value at an index. Index must be between 0 and the number of items -1. First item is index 0.")]
	public class ArraySet : FsmStateAction
	{
		// Token: 0x06009C82 RID: 40066 RVA: 0x00325813 File Offset: 0x00323A13
		public override void Reset()
		{
			this.array = null;
			this.index = null;
			this.value = null;
			this.everyFrame = false;
			this.indexOutOfRange = null;
		}

		// Token: 0x06009C83 RID: 40067 RVA: 0x00325838 File Offset: 0x00323A38
		public override void OnEnter()
		{
			this.DoGetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009C84 RID: 40068 RVA: 0x0032584E File Offset: 0x00323A4E
		public override void OnUpdate()
		{
			this.DoGetValue();
		}

		// Token: 0x06009C85 RID: 40069 RVA: 0x00325858 File Offset: 0x00323A58
		private void DoGetValue()
		{
			if (this.array.IsNone)
			{
				return;
			}
			if (this.index.Value >= 0 && this.index.Value < this.array.Length)
			{
				this.value.UpdateValue();
				this.array.Set(this.index.Value, this.value.GetValue());
				return;
			}
			base.Fsm.Event(this.indexOutOfRange);
		}

		// Token: 0x040081FA RID: 33274
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to use.")]
		public FsmArray array;

		// Token: 0x040081FB RID: 33275
		[Tooltip("The index into the array.")]
		public FsmInt index;

		// Token: 0x040081FC RID: 33276
		[RequiredField]
		[MatchElementType("array")]
		[Tooltip("Set the value of the array at the specified index.")]
		public FsmVar value;

		// Token: 0x040081FD RID: 33277
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		// Token: 0x040081FE RID: 33278
		[ActionSection("Events")]
		[Tooltip("The event to trigger if the index is out of range")]
		public FsmEvent indexOutOfRange;
	}
}
