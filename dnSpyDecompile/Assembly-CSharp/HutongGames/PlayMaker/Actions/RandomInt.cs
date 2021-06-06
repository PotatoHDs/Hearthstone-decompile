using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D4D RID: 3405
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Sets an Integer Variable to a random value between Min/Max.")]
	public class RandomInt : FsmStateAction
	{
		// Token: 0x0600A389 RID: 41865 RVA: 0x0033F56A File Offset: 0x0033D76A
		public override void Reset()
		{
			this.min = 0;
			this.max = 100;
			this.storeResult = null;
			this.inclusiveMax = false;
			this.noRepeat = true;
		}

		// Token: 0x0600A38A RID: 41866 RVA: 0x0033F59F File Offset: 0x0033D79F
		public override void OnEnter()
		{
			this.PickRandom();
			base.Finish();
		}

		// Token: 0x0600A38B RID: 41867 RVA: 0x0033F5B0 File Offset: 0x0033D7B0
		private void PickRandom()
		{
			if (this.noRepeat.Value && this.max.Value != this.min.Value && !this.inclusiveMax && Mathf.Abs(this.max.Value - this.min.Value) > 1)
			{
				do
				{
					this.randomIndex = (this.inclusiveMax ? UnityEngine.Random.Range(this.min.Value, this.max.Value + 1) : UnityEngine.Random.Range(this.min.Value, this.max.Value));
				}
				while (this.randomIndex == this.lastIndex);
				this.lastIndex = this.randomIndex;
				this.storeResult.Value = this.randomIndex;
				return;
			}
			this.randomIndex = (this.inclusiveMax ? UnityEngine.Random.Range(this.min.Value, this.max.Value + 1) : UnityEngine.Random.Range(this.min.Value, this.max.Value));
			this.storeResult.Value = this.randomIndex;
		}

		// Token: 0x040089B5 RID: 35253
		[RequiredField]
		[Tooltip("Minimum value for the random number.")]
		public FsmInt min;

		// Token: 0x040089B6 RID: 35254
		[RequiredField]
		[Tooltip("Maximim value for the random number.")]
		public FsmInt max;

		// Token: 0x040089B7 RID: 35255
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in an Integer variable.")]
		public FsmInt storeResult;

		// Token: 0x040089B8 RID: 35256
		[Tooltip("Should the Max value be included in the possible results?")]
		public bool inclusiveMax;

		// Token: 0x040089B9 RID: 35257
		[Tooltip("Don't repeat the same value twice.")]
		public FsmBool noRepeat;

		// Token: 0x040089BA RID: 35258
		private int randomIndex;

		// Token: 0x040089BB RID: 35259
		private int lastIndex = -1;
	}
}
