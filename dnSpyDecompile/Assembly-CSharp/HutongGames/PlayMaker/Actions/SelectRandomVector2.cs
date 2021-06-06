using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EA2 RID: 3746
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Select a Random Vector2 from a Vector2 array.")]
	public class SelectRandomVector2 : FsmStateAction
	{
		// Token: 0x0600A9C9 RID: 43465 RVA: 0x00353740 File Offset: 0x00351940
		public override void Reset()
		{
			this.vector2Array = new FsmVector2[3];
			this.weights = new FsmFloat[]
			{
				1f,
				1f,
				1f
			};
			this.storeVector2 = null;
		}

		// Token: 0x0600A9CA RID: 43466 RVA: 0x00353793 File Offset: 0x00351993
		public override void OnEnter()
		{
			this.DoSelectRandomColor();
			base.Finish();
		}

		// Token: 0x0600A9CB RID: 43467 RVA: 0x003537A4 File Offset: 0x003519A4
		private void DoSelectRandomColor()
		{
			if (this.vector2Array == null)
			{
				return;
			}
			if (this.vector2Array.Length == 0)
			{
				return;
			}
			if (this.storeVector2 == null)
			{
				return;
			}
			int randomWeightedIndex = ActionHelpers.GetRandomWeightedIndex(this.weights);
			if (randomWeightedIndex != -1)
			{
				this.storeVector2.Value = this.vector2Array[randomWeightedIndex].Value;
			}
		}

		// Token: 0x0400906D RID: 36973
		[Tooltip("The array of Vectors and respective weights")]
		[CompoundArray("Vectors", "Vector", "Weight")]
		public FsmVector2[] vector2Array;

		// Token: 0x0400906E RID: 36974
		[HasFloatSlider(0f, 1f)]
		public FsmFloat[] weights;

		// Token: 0x0400906F RID: 36975
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The picked vector2")]
		public FsmVector2 storeVector2;
	}
}
