using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D9C RID: 3484
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Select a Random Vector3 from a Vector3 array.")]
	public class SelectRandomVector3 : FsmStateAction
	{
		// Token: 0x0600A510 RID: 42256 RVA: 0x00345748 File Offset: 0x00343948
		public override void Reset()
		{
			this.vector3Array = new FsmVector3[3];
			this.weights = new FsmFloat[]
			{
				1f,
				1f,
				1f
			};
			this.storeVector3 = null;
		}

		// Token: 0x0600A511 RID: 42257 RVA: 0x0034579B File Offset: 0x0034399B
		public override void OnEnter()
		{
			this.DoSelectRandomColor();
			base.Finish();
		}

		// Token: 0x0600A512 RID: 42258 RVA: 0x003457AC File Offset: 0x003439AC
		private void DoSelectRandomColor()
		{
			if (this.vector3Array == null)
			{
				return;
			}
			if (this.vector3Array.Length == 0)
			{
				return;
			}
			if (this.storeVector3 == null)
			{
				return;
			}
			int randomWeightedIndex = ActionHelpers.GetRandomWeightedIndex(this.weights);
			if (randomWeightedIndex != -1)
			{
				this.storeVector3.Value = this.vector3Array[randomWeightedIndex].Value;
			}
		}

		// Token: 0x04008BA7 RID: 35751
		[CompoundArray("Vectors", "Vector", "Weight")]
		public FsmVector3[] vector3Array;

		// Token: 0x04008BA8 RID: 35752
		[HasFloatSlider(0f, 1f)]
		public FsmFloat[] weights;

		// Token: 0x04008BA9 RID: 35753
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 storeVector3;
	}
}
