using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BA9 RID: 2985
	[ActionCategory(ActionCategory.AnimateVariables)]
	[Tooltip("Animates the value of a Color Variable FROM-TO with assistance of Deformation Curves.")]
	public class CurveColor : CurveFsmAction
	{
		// Token: 0x06009BE2 RID: 39906 RVA: 0x00321B79 File Offset: 0x0031FD79
		public override void Reset()
		{
			base.Reset();
			this.colorVariable = new FsmColor
			{
				UseVariable = true
			};
			this.toValue = new FsmColor
			{
				UseVariable = true
			};
			this.fromValue = new FsmColor
			{
				UseVariable = true
			};
		}

		// Token: 0x06009BE3 RID: 39907 RVA: 0x00321BB8 File Offset: 0x0031FDB8
		public override void OnEnter()
		{
			base.OnEnter();
			this.finishInNextStep = false;
			this.resultFloats = new float[4];
			this.fromFloats = new float[4];
			this.fromFloats[0] = (this.fromValue.IsNone ? 0f : this.fromValue.Value.r);
			this.fromFloats[1] = (this.fromValue.IsNone ? 0f : this.fromValue.Value.g);
			this.fromFloats[2] = (this.fromValue.IsNone ? 0f : this.fromValue.Value.b);
			this.fromFloats[3] = (this.fromValue.IsNone ? 0f : this.fromValue.Value.a);
			this.toFloats = new float[4];
			this.toFloats[0] = (this.toValue.IsNone ? 0f : this.toValue.Value.r);
			this.toFloats[1] = (this.toValue.IsNone ? 0f : this.toValue.Value.g);
			this.toFloats[2] = (this.toValue.IsNone ? 0f : this.toValue.Value.b);
			this.toFloats[3] = (this.toValue.IsNone ? 0f : this.toValue.Value.a);
			this.curves = new AnimationCurve[4];
			this.curves[0] = this.curveR.curve;
			this.curves[1] = this.curveG.curve;
			this.curves[2] = this.curveB.curve;
			this.curves[3] = this.curveA.curve;
			this.calculations = new CurveFsmAction.Calculation[4];
			this.calculations[0] = this.calculationR;
			this.calculations[1] = this.calculationG;
			this.calculations[2] = this.calculationB;
			this.calculations[3] = this.calculationA;
			base.Init();
		}

		// Token: 0x06009BE4 RID: 39908 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void OnExit()
		{
		}

		// Token: 0x06009BE5 RID: 39909 RVA: 0x00321DF8 File Offset: 0x0031FFF8
		public override void OnUpdate()
		{
			base.OnUpdate();
			if (!this.colorVariable.IsNone && this.isRunning)
			{
				this.clr = new Color(this.resultFloats[0], this.resultFloats[1], this.resultFloats[2], this.resultFloats[3]);
				this.colorVariable.Value = this.clr;
			}
			if (this.finishInNextStep && !this.looping)
			{
				base.Finish();
				if (this.finishEvent != null)
				{
					base.Fsm.Event(this.finishEvent);
				}
			}
			if (this.finishAction && !this.finishInNextStep)
			{
				if (!this.colorVariable.IsNone)
				{
					this.clr = new Color(this.resultFloats[0], this.resultFloats[1], this.resultFloats[2], this.resultFloats[3]);
					this.colorVariable.Value = this.clr;
				}
				this.finishInNextStep = true;
			}
		}

		// Token: 0x04008151 RID: 33105
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmColor colorVariable;

		// Token: 0x04008152 RID: 33106
		[RequiredField]
		public FsmColor fromValue;

		// Token: 0x04008153 RID: 33107
		[RequiredField]
		public FsmColor toValue;

		// Token: 0x04008154 RID: 33108
		[RequiredField]
		public FsmAnimationCurve curveR;

		// Token: 0x04008155 RID: 33109
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to otherwise linear move between fromValue.Red and toValue.Rec.")]
		public CurveFsmAction.Calculation calculationR;

		// Token: 0x04008156 RID: 33110
		[RequiredField]
		public FsmAnimationCurve curveG;

		// Token: 0x04008157 RID: 33111
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to otherwise linear move between fromValue.Green and toValue.Green.")]
		public CurveFsmAction.Calculation calculationG;

		// Token: 0x04008158 RID: 33112
		[RequiredField]
		public FsmAnimationCurve curveB;

		// Token: 0x04008159 RID: 33113
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to otherwise linear move between fromValue.Blue and toValue.Blue.")]
		public CurveFsmAction.Calculation calculationB;

		// Token: 0x0400815A RID: 33114
		[RequiredField]
		public FsmAnimationCurve curveA;

		// Token: 0x0400815B RID: 33115
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to otherwise linear move between fromValue.Alpha and toValue.Alpha.")]
		public CurveFsmAction.Calculation calculationA;

		// Token: 0x0400815C RID: 33116
		private Color clr;

		// Token: 0x0400815D RID: 33117
		private bool finishInNextStep;
	}
}
