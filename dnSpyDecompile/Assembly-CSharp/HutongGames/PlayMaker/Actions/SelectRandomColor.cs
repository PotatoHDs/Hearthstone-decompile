using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D99 RID: 3481
	[ActionCategory(ActionCategory.Color)]
	[Tooltip("Select a random Color from an array of Colors.")]
	public class SelectRandomColor : FsmStateAction
	{
		// Token: 0x0600A504 RID: 42244 RVA: 0x00345520 File Offset: 0x00343720
		public override void Reset()
		{
			this.colors = new FsmColor[3];
			this.weights = new FsmFloat[]
			{
				1f,
				1f,
				1f
			};
			this.storeColor = null;
		}

		// Token: 0x0600A505 RID: 42245 RVA: 0x00345573 File Offset: 0x00343773
		public override void OnEnter()
		{
			this.DoSelectRandomColor();
			base.Finish();
		}

		// Token: 0x0600A506 RID: 42246 RVA: 0x00345584 File Offset: 0x00343784
		private void DoSelectRandomColor()
		{
			if (this.colors == null)
			{
				return;
			}
			if (this.colors.Length == 0)
			{
				return;
			}
			if (this.storeColor == null)
			{
				return;
			}
			int randomWeightedIndex = ActionHelpers.GetRandomWeightedIndex(this.weights);
			if (randomWeightedIndex != -1)
			{
				this.storeColor.Value = this.colors[randomWeightedIndex].Value;
			}
		}

		// Token: 0x04008B9E RID: 35742
		[CompoundArray("Colors", "Color", "Weight")]
		public FsmColor[] colors;

		// Token: 0x04008B9F RID: 35743
		[HasFloatSlider(0f, 1f)]
		public FsmFloat[] weights;

		// Token: 0x04008BA0 RID: 35744
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmColor storeColor;
	}
}
