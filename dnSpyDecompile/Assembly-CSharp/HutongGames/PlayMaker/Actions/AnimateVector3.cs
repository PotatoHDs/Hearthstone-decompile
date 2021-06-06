using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BA8 RID: 2984
	[ActionCategory(ActionCategory.AnimateVariables)]
	[Tooltip("Animates the value of a Vector3 Variable using an Animation Curve.")]
	public class AnimateVector3 : AnimateFsmAction
	{
		// Token: 0x06009BDD RID: 39901 RVA: 0x0032196D File Offset: 0x0031FB6D
		public override void Reset()
		{
			base.Reset();
			this.vectorVariable = new FsmVector3
			{
				UseVariable = true
			};
		}

		// Token: 0x06009BDE RID: 39902 RVA: 0x00321988 File Offset: 0x0031FB88
		public override void OnEnter()
		{
			base.OnEnter();
			this.finishInNextStep = false;
			this.resultFloats = new float[3];
			this.fromFloats = new float[3];
			this.fromFloats[0] = (this.vectorVariable.IsNone ? 0f : this.vectorVariable.Value.x);
			this.fromFloats[1] = (this.vectorVariable.IsNone ? 0f : this.vectorVariable.Value.y);
			this.fromFloats[2] = (this.vectorVariable.IsNone ? 0f : this.vectorVariable.Value.z);
			this.curves = new AnimationCurve[3];
			this.curves[0] = this.curveX.curve;
			this.curves[1] = this.curveY.curve;
			this.curves[2] = this.curveZ.curve;
			this.calculations = new AnimateFsmAction.Calculation[3];
			this.calculations[0] = this.calculationX;
			this.calculations[1] = this.calculationY;
			this.calculations[2] = this.calculationZ;
			base.Init();
			if (Math.Abs(this.delay.Value) < 0.01f)
			{
				this.UpdateVariableValue();
			}
		}

		// Token: 0x06009BDF RID: 39903 RVA: 0x00321ADC File Offset: 0x0031FCDC
		private void UpdateVariableValue()
		{
			if (!this.vectorVariable.IsNone)
			{
				this.vectorVariable.Value = new Vector3(this.resultFloats[0], this.resultFloats[1], this.resultFloats[2]);
			}
		}

		// Token: 0x06009BE0 RID: 39904 RVA: 0x00321B14 File Offset: 0x0031FD14
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

		// Token: 0x04008149 RID: 33097
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 vectorVariable;

		// Token: 0x0400814A RID: 33098
		[RequiredField]
		public FsmAnimationCurve curveX;

		// Token: 0x0400814B RID: 33099
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to vectorVariable.x.")]
		public AnimateFsmAction.Calculation calculationX;

		// Token: 0x0400814C RID: 33100
		[RequiredField]
		public FsmAnimationCurve curveY;

		// Token: 0x0400814D RID: 33101
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to vectorVariable.y.")]
		public AnimateFsmAction.Calculation calculationY;

		// Token: 0x0400814E RID: 33102
		[RequiredField]
		public FsmAnimationCurve curveZ;

		// Token: 0x0400814F RID: 33103
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to vectorVariable.z.")]
		public AnimateFsmAction.Calculation calculationZ;

		// Token: 0x04008150 RID: 33104
		private bool finishInNextStep;
	}
}
