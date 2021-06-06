using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BC1 RID: 3009
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Get a Random item from an Array.")]
	public class ArrayGetRandom : FsmStateAction
	{
		// Token: 0x06009C74 RID: 40052 RVA: 0x00325603 File Offset: 0x00323803
		public override void Reset()
		{
			this.array = null;
			this.storeValue = null;
			this.index = null;
			this.everyFrame = false;
			this.noRepeat = false;
		}

		// Token: 0x06009C75 RID: 40053 RVA: 0x0032562D File Offset: 0x0032382D
		public override void OnEnter()
		{
			this.DoGetRandomValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009C76 RID: 40054 RVA: 0x00325643 File Offset: 0x00323843
		public override void OnUpdate()
		{
			this.DoGetRandomValue();
		}

		// Token: 0x06009C77 RID: 40055 RVA: 0x0032564C File Offset: 0x0032384C
		private void DoGetRandomValue()
		{
			if (this.storeValue.IsNone)
			{
				return;
			}
			if (!this.noRepeat.Value || this.array.Length == 1)
			{
				this.randomIndex = UnityEngine.Random.Range(0, this.array.Length);
			}
			else
			{
				do
				{
					this.randomIndex = UnityEngine.Random.Range(0, this.array.Length);
				}
				while (this.randomIndex == this.lastIndex);
				this.lastIndex = this.randomIndex;
			}
			this.index.Value = this.randomIndex;
			this.storeValue.SetValue(this.array.Get(this.index.Value));
		}

		// Token: 0x040081EC RID: 33260
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array to use.")]
		public FsmArray array;

		// Token: 0x040081ED RID: 33261
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the value in a variable.")]
		[MatchElementType("array")]
		public FsmVar storeValue;

		// Token: 0x040081EE RID: 33262
		[Tooltip("The index of the value in the array.")]
		[UIHint(UIHint.Variable)]
		public FsmInt index;

		// Token: 0x040081EF RID: 33263
		[Tooltip("Don't get the same item twice in a row.")]
		public FsmBool noRepeat;

		// Token: 0x040081F0 RID: 33264
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		// Token: 0x040081F1 RID: 33265
		private int randomIndex;

		// Token: 0x040081F2 RID: 33266
		private int lastIndex = -1;
	}
}
