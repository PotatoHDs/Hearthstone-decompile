using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BE5 RID: 3045
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Builds a String from other Strings.")]
	public class BuildString : FsmStateAction
	{
		// Token: 0x06009CFD RID: 40189 RVA: 0x0032710E File Offset: 0x0032530E
		public override void Reset()
		{
			this.stringParts = new FsmString[3];
			this.separator = null;
			this.addToEnd = true;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009CFE RID: 40190 RVA: 0x0032713D File Offset: 0x0032533D
		public override void OnEnter()
		{
			this.DoBuildString();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009CFF RID: 40191 RVA: 0x00327153 File Offset: 0x00325353
		public override void OnUpdate()
		{
			this.DoBuildString();
		}

		// Token: 0x06009D00 RID: 40192 RVA: 0x0032715C File Offset: 0x0032535C
		private void DoBuildString()
		{
			if (this.storeResult == null)
			{
				return;
			}
			this.result = "";
			for (int i = 0; i < this.stringParts.Length - 1; i++)
			{
				this.result += this.stringParts[i];
				this.result += this.separator.Value;
			}
			this.result += this.stringParts[this.stringParts.Length - 1];
			if (this.addToEnd.Value)
			{
				this.result += this.separator.Value;
			}
			this.storeResult.Value = this.result;
		}

		// Token: 0x04008273 RID: 33395
		[RequiredField]
		[Tooltip("Array of Strings to combine.")]
		public FsmString[] stringParts;

		// Token: 0x04008274 RID: 33396
		[Tooltip("Separator to insert between each String. E.g. space character.")]
		public FsmString separator;

		// Token: 0x04008275 RID: 33397
		[Tooltip("Add Separator to end of built string.")]
		public FsmBool addToEnd;

		// Token: 0x04008276 RID: 33398
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the final String in a variable.")]
		public FsmString storeResult;

		// Token: 0x04008277 RID: 33399
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		// Token: 0x04008278 RID: 33400
		private string result;
	}
}
