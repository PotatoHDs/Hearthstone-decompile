using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D9B RID: 3483
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Select a Random String from an array of Strings.")]
	public class SelectRandomString : FsmStateAction
	{
		// Token: 0x0600A50C RID: 42252 RVA: 0x00345690 File Offset: 0x00343890
		public override void Reset()
		{
			this.strings = new FsmString[3];
			this.weights = new FsmFloat[]
			{
				1f,
				1f,
				1f
			};
			this.storeString = null;
		}

		// Token: 0x0600A50D RID: 42253 RVA: 0x003456E3 File Offset: 0x003438E3
		public override void OnEnter()
		{
			this.DoSelectRandomString();
			base.Finish();
		}

		// Token: 0x0600A50E RID: 42254 RVA: 0x003456F4 File Offset: 0x003438F4
		private void DoSelectRandomString()
		{
			if (this.strings == null)
			{
				return;
			}
			if (this.strings.Length == 0)
			{
				return;
			}
			if (this.storeString == null)
			{
				return;
			}
			int randomWeightedIndex = ActionHelpers.GetRandomWeightedIndex(this.weights);
			if (randomWeightedIndex != -1)
			{
				this.storeString.Value = this.strings[randomWeightedIndex].Value;
			}
		}

		// Token: 0x04008BA4 RID: 35748
		[CompoundArray("Strings", "String", "Weight")]
		public FsmString[] strings;

		// Token: 0x04008BA5 RID: 35749
		[HasFloatSlider(0f, 1f)]
		public FsmFloat[] weights;

		// Token: 0x04008BA6 RID: 35750
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString storeString;
	}
}
