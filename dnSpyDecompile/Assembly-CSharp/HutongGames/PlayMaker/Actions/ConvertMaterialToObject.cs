using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BFD RID: 3069
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Converts a Material variable to an Object variable. Useful if you want to use Set Property (which only works on Object variables).")]
	public class ConvertMaterialToObject : FsmStateAction
	{
		// Token: 0x06009D91 RID: 40337 RVA: 0x003293B4 File Offset: 0x003275B4
		public override void Reset()
		{
			this.materialVariable = null;
			this.objectVariable = null;
			this.everyFrame = false;
		}

		// Token: 0x06009D92 RID: 40338 RVA: 0x003293CB File Offset: 0x003275CB
		public override void OnEnter()
		{
			this.DoConvertMaterialToObject();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009D93 RID: 40339 RVA: 0x003293E1 File Offset: 0x003275E1
		public override void OnUpdate()
		{
			this.DoConvertMaterialToObject();
		}

		// Token: 0x06009D94 RID: 40340 RVA: 0x003293E9 File Offset: 0x003275E9
		private void DoConvertMaterialToObject()
		{
			this.objectVariable.Value = this.materialVariable.Value;
		}

		// Token: 0x040082FD RID: 33533
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Material variable to convert to an Object.")]
		public FsmMaterial materialVariable;

		// Token: 0x040082FE RID: 33534
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in an Object variable.")]
		public FsmObject objectVariable;

		// Token: 0x040082FF RID: 33535
		[Tooltip("Repeat every frame. Useful if the Material variable is changing.")]
		public bool everyFrame;
	}
}
