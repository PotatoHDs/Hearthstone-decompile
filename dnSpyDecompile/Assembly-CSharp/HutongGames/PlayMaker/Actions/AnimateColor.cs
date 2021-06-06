using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BA4 RID: 2980
	[ActionCategory(ActionCategory.AnimateVariables)]
	[Tooltip("Animates the value of a Color Variable using an Animation Curve.")]
	public class AnimateColor : AnimateFsmAction
	{
		// Token: 0x06009BC5 RID: 39877 RVA: 0x00320886 File Offset: 0x0031EA86
		public override void Reset()
		{
			base.Reset();
			this.colorVariable = new FsmColor
			{
				UseVariable = true
			};
		}

		// Token: 0x06009BC6 RID: 39878 RVA: 0x003208A0 File Offset: 0x0031EAA0
		public override void OnEnter()
		{
			base.OnEnter();
			this.finishInNextStep = false;
			this.resultFloats = new float[4];
			this.fromFloats = new float[4];
			this.fromFloats[0] = (this.colorVariable.IsNone ? 0f : this.colorVariable.Value.r);
			this.fromFloats[1] = (this.colorVariable.IsNone ? 0f : this.colorVariable.Value.g);
			this.fromFloats[2] = (this.colorVariable.IsNone ? 0f : this.colorVariable.Value.b);
			this.fromFloats[3] = (this.colorVariable.IsNone ? 0f : this.colorVariable.Value.a);
			this.curves = new AnimationCurve[4];
			this.curves[0] = this.curveR.curve;
			this.curves[1] = this.curveG.curve;
			this.curves[2] = this.curveB.curve;
			this.curves[3] = this.curveA.curve;
			this.calculations = new AnimateFsmAction.Calculation[4];
			this.calculations[0] = this.calculationR;
			this.calculations[1] = this.calculationG;
			this.calculations[2] = this.calculationB;
			this.calculations[3] = this.calculationA;
			base.Init();
			if (Math.Abs(this.delay.Value) < 0.01f)
			{
				this.UpdateVariableValue();
			}
		}

		// Token: 0x06009BC7 RID: 39879 RVA: 0x00320A41 File Offset: 0x0031EC41
		private void UpdateVariableValue()
		{
			if (!this.colorVariable.IsNone)
			{
				this.colorVariable.Value = new Color(this.resultFloats[0], this.resultFloats[1], this.resultFloats[2], this.resultFloats[3]);
			}
		}

		// Token: 0x06009BC8 RID: 39880 RVA: 0x00320A80 File Offset: 0x0031EC80
		public override void OnUpdate()
		{
			base.OnUpdate();
			if (this.isRunning)
			{
				this.UpdateVariableValue();
			}
			if (this.finishInNextStep && !this.looping)
			{
				base.Finish();
				base.Fsm.Event(this.finishEvent);
			}
			if (this.finishAction && !this.finishInNextStep)
			{
				this.UpdateVariableValue();
				this.finishInNextStep = true;
			}
		}

		// Token: 0x0400811A RID: 33050
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmColor colorVariable;

		// Token: 0x0400811B RID: 33051
		[RequiredField]
		public FsmAnimationCurve curveR;

		// Token: 0x0400811C RID: 33052
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to colorVariable.r.")]
		public AnimateFsmAction.Calculation calculationR;

		// Token: 0x0400811D RID: 33053
		[RequiredField]
		public FsmAnimationCurve curveG;

		// Token: 0x0400811E RID: 33054
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to colorVariable.g.")]
		public AnimateFsmAction.Calculation calculationG;

		// Token: 0x0400811F RID: 33055
		[RequiredField]
		public FsmAnimationCurve curveB;

		// Token: 0x04008120 RID: 33056
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to colorVariable.b.")]
		public AnimateFsmAction.Calculation calculationB;

		// Token: 0x04008121 RID: 33057
		[RequiredField]
		public FsmAnimationCurve curveA;

		// Token: 0x04008122 RID: 33058
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to colorVariable.a.")]
		public AnimateFsmAction.Calculation calculationA;

		// Token: 0x04008123 RID: 33059
		private bool finishInNextStep;
	}
}
