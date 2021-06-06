using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BB8 RID: 3000
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Add an item to the end of an Array.")]
	public class ArrayAdd : FsmStateAction
	{
		// Token: 0x06009C48 RID: 40008 RVA: 0x00324D61 File Offset: 0x00322F61
		public override void Reset()
		{
			this.array = null;
			this.value = null;
		}

		// Token: 0x06009C49 RID: 40009 RVA: 0x00324D71 File Offset: 0x00322F71
		public override void OnEnter()
		{
			this.DoAddValue();
			base.Finish();
		}

		// Token: 0x06009C4A RID: 40010 RVA: 0x00324D80 File Offset: 0x00322F80
		private void DoAddValue()
		{
			this.array.Resize(this.array.Length + 1);
			this.value.UpdateValue();
			this.array.Set(this.array.Length - 1, this.value.GetValue());
		}

		// Token: 0x040081C4 RID: 33220
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to use.")]
		public FsmArray array;

		// Token: 0x040081C5 RID: 33221
		[RequiredField]
		[MatchElementType("array")]
		[Tooltip("Item to add.")]
		public FsmVar value;
	}
}
