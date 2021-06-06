using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BBB RID: 3003
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Tests if 2 Array Variables have the same values.")]
	public class ArrayCompare : FsmStateAction
	{
		// Token: 0x06009C53 RID: 40019 RVA: 0x00324EFB File Offset: 0x003230FB
		public override void Reset()
		{
			this.array1 = null;
			this.array2 = null;
			this.SequenceEqual = null;
			this.SequenceNotEqual = null;
		}

		// Token: 0x06009C54 RID: 40020 RVA: 0x00324F19 File Offset: 0x00323119
		public override void OnEnter()
		{
			this.DoSequenceEqual();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009C55 RID: 40021 RVA: 0x00324F30 File Offset: 0x00323130
		private void DoSequenceEqual()
		{
			if (this.array1.Values == null || this.array2.Values == null)
			{
				return;
			}
			this.storeResult.Value = this.TestSequenceEqual(this.array1.Values, this.array2.Values);
			base.Fsm.Event(this.storeResult.Value ? this.SequenceEqual : this.SequenceNotEqual);
		}

		// Token: 0x06009C56 RID: 40022 RVA: 0x00324FA8 File Offset: 0x003231A8
		private bool TestSequenceEqual(object[] _array1, object[] _array2)
		{
			if (_array1.Length != _array2.Length)
			{
				return false;
			}
			for (int i = 0; i < this.array1.Length; i++)
			{
				if (!_array1[i].Equals(_array2[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040081CA RID: 33226
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The first Array Variable to test.")]
		public FsmArray array1;

		// Token: 0x040081CB RID: 33227
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The second Array Variable to test.")]
		public FsmArray array2;

		// Token: 0x040081CC RID: 33228
		[Tooltip("Event to send if the 2 arrays have the same values.")]
		public FsmEvent SequenceEqual;

		// Token: 0x040081CD RID: 33229
		[Tooltip("Event to send if the 2 arrays have different values.")]
		public FsmEvent SequenceNotEqual;

		// Token: 0x040081CE RID: 33230
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a Bool variable.")]
		public FsmBool storeResult;

		// Token: 0x040081CF RID: 33231
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
