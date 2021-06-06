using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BBA RID: 3002
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Sets all items in an Array to their default value: 0, empty string, false, or null depending on their type. Optionally defines a reset value to use.")]
	public class ArrayClear : FsmStateAction
	{
		// Token: 0x06009C50 RID: 40016 RVA: 0x00324E6B File Offset: 0x0032306B
		public override void Reset()
		{
			this.array = null;
			this.resetValue = new FsmVar
			{
				useVariable = true
			};
		}

		// Token: 0x06009C51 RID: 40017 RVA: 0x00324E88 File Offset: 0x00323088
		public override void OnEnter()
		{
			int length = this.array.Length;
			this.array.Reset();
			this.array.Resize(length);
			if (!this.resetValue.IsNone)
			{
				this.resetValue.UpdateValue();
				object value = this.resetValue.GetValue();
				for (int i = 0; i < length; i++)
				{
					this.array.Set(i, value);
				}
			}
			base.Finish();
		}

		// Token: 0x040081C8 RID: 33224
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to clear.")]
		public FsmArray array;

		// Token: 0x040081C9 RID: 33225
		[MatchElementType("array")]
		[Tooltip("Optional reset value. Leave as None for default value.")]
		public FsmVar resetValue;
	}
}
