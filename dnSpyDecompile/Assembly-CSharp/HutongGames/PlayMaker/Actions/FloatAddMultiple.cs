using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C2B RID: 3115
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Adds multiple float variables to float variable.")]
	public class FloatAddMultiple : FsmStateAction
	{
		// Token: 0x06009E44 RID: 40516 RVA: 0x0032B192 File Offset: 0x00329392
		public override void Reset()
		{
			this.floatVariables = null;
			this.addTo = null;
			this.everyFrame = false;
		}

		// Token: 0x06009E45 RID: 40517 RVA: 0x0032B1A9 File Offset: 0x003293A9
		public override void OnEnter()
		{
			this.DoFloatAdd();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009E46 RID: 40518 RVA: 0x0032B1BF File Offset: 0x003293BF
		public override void OnUpdate()
		{
			this.DoFloatAdd();
		}

		// Token: 0x06009E47 RID: 40519 RVA: 0x0032B1C8 File Offset: 0x003293C8
		private void DoFloatAdd()
		{
			for (int i = 0; i < this.floatVariables.Length; i++)
			{
				this.addTo.Value += this.floatVariables[i].Value;
			}
		}

		// Token: 0x04008399 RID: 33689
		[UIHint(UIHint.Variable)]
		[Tooltip("The float variables to add.")]
		public FsmFloat[] floatVariables;

		// Token: 0x0400839A RID: 33690
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Add to this variable.")]
		public FsmFloat addTo;

		// Token: 0x0400839B RID: 33691
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;
	}
}
