using System;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BC8 RID: 3016
	[NoActionTargets]
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Transfer a value from one array to another, basically a copy/cut paste action on steroids.")]
	public class ArrayTransferValue : FsmStateAction
	{
		// Token: 0x06009C8D RID: 40077 RVA: 0x00325A0B File Offset: 0x00323C0B
		public override void Reset()
		{
			this.arraySource = null;
			this.arrayTarget = null;
			this.indexToTransfer = null;
			this.copyType = ArrayTransferValue.ArrayTransferType.Copy;
			this.pasteType = ArrayTransferValue.ArrayPasteType.AsLastItem;
		}

		// Token: 0x06009C8E RID: 40078 RVA: 0x00325A44 File Offset: 0x00323C44
		public override void OnEnter()
		{
			this.DoTransferValue();
			base.Finish();
		}

		// Token: 0x06009C8F RID: 40079 RVA: 0x00325A54 File Offset: 0x00323C54
		private void DoTransferValue()
		{
			if (this.arraySource.IsNone || this.arrayTarget.IsNone)
			{
				return;
			}
			int value = this.indexToTransfer.Value;
			if (value < 0 || value >= this.arraySource.Length)
			{
				base.Fsm.Event(this.indexOutOfRange);
				return;
			}
			object obj = this.arraySource.Values[value];
			if ((ArrayTransferValue.ArrayTransferType)this.copyType.Value == ArrayTransferValue.ArrayTransferType.Cut)
			{
				List<object> list = new List<object>(this.arraySource.Values);
				list.RemoveAt(value);
				this.arraySource.Values = list.ToArray();
			}
			else if ((ArrayTransferValue.ArrayTransferType)this.copyType.Value == ArrayTransferValue.ArrayTransferType.nullify)
			{
				this.arraySource.Values.SetValue(null, value);
			}
			if ((ArrayTransferValue.ArrayPasteType)this.pasteType.Value == ArrayTransferValue.ArrayPasteType.AsFirstItem)
			{
				List<object> list2 = new List<object>(this.arrayTarget.Values);
				list2.Insert(0, obj);
				this.arrayTarget.Values = list2.ToArray();
				return;
			}
			if ((ArrayTransferValue.ArrayPasteType)this.pasteType.Value == ArrayTransferValue.ArrayPasteType.AsLastItem)
			{
				this.arrayTarget.Resize(this.arrayTarget.Length + 1);
				this.arrayTarget.Set(this.arrayTarget.Length - 1, obj);
				return;
			}
			if ((ArrayTransferValue.ArrayPasteType)this.pasteType.Value == ArrayTransferValue.ArrayPasteType.InsertAtSameIndex)
			{
				if (value >= this.arrayTarget.Length)
				{
					base.Fsm.Event(this.indexOutOfRange);
				}
				List<object> list3 = new List<object>(this.arrayTarget.Values);
				list3.Insert(value, obj);
				this.arrayTarget.Values = list3.ToArray();
				return;
			}
			if ((ArrayTransferValue.ArrayPasteType)this.pasteType.Value == ArrayTransferValue.ArrayPasteType.ReplaceAtSameIndex)
			{
				if (value >= this.arrayTarget.Length)
				{
					base.Fsm.Event(this.indexOutOfRange);
					return;
				}
				this.arrayTarget.Set(value, obj);
			}
		}

		// Token: 0x04008203 RID: 33283
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable source.")]
		public FsmArray arraySource;

		// Token: 0x04008204 RID: 33284
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable target.")]
		public FsmArray arrayTarget;

		// Token: 0x04008205 RID: 33285
		[MatchFieldType("array")]
		[Tooltip("The index to transfer.")]
		public FsmInt indexToTransfer;

		// Token: 0x04008206 RID: 33286
		[ActionSection("Transfer Options")]
		[ObjectType(typeof(ArrayTransferValue.ArrayTransferType))]
		public FsmEnum copyType;

		// Token: 0x04008207 RID: 33287
		[ObjectType(typeof(ArrayTransferValue.ArrayPasteType))]
		public FsmEnum pasteType;

		// Token: 0x04008208 RID: 33288
		[ActionSection("Result")]
		[Tooltip("Event sent if this array source does not contains that element (described below)")]
		public FsmEvent indexOutOfRange;

		// Token: 0x02002790 RID: 10128
		public enum ArrayTransferType
		{
			// Token: 0x0400F49D RID: 62621
			Copy,
			// Token: 0x0400F49E RID: 62622
			Cut,
			// Token: 0x0400F49F RID: 62623
			nullify
		}

		// Token: 0x02002791 RID: 10129
		public enum ArrayPasteType
		{
			// Token: 0x0400F4A1 RID: 62625
			AsFirstItem,
			// Token: 0x0400F4A2 RID: 62626
			AsLastItem,
			// Token: 0x0400F4A3 RID: 62627
			InsertAtSameIndex,
			// Token: 0x0400F4A4 RID: 62628
			ReplaceAtSameIndex
		}
	}
}
