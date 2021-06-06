using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BAA RID: 2986
	[ActionCategory(ActionCategory.AnimateVariables)]
	[Tooltip("Animates the value of a Float Variable FROM-TO with assistance of Deformation Curve.")]
	public class CurveFloat : CurveFsmAction
	{
		// Token: 0x06009BE7 RID: 39911 RVA: 0x00321EF3 File Offset: 0x003200F3
		public override void Reset()
		{
			base.Reset();
			this.floatVariable = new FsmFloat
			{
				UseVariable = true
			};
			this.toValue = new FsmFloat
			{
				UseVariable = true
			};
			this.fromValue = new FsmFloat
			{
				UseVariable = true
			};
		}

		// Token: 0x06009BE8 RID: 39912 RVA: 0x00321F34 File Offset: 0x00320134
		public override void OnEnter()
		{
			base.OnEnter();
			this.finishInNextStep = false;
			this.resultFloats = new float[1];
			this.fromFloats = new float[1];
			this.fromFloats[0] = (this.fromValue.IsNone ? 0f : this.fromValue.Value);
			this.toFloats = new float[1];
			this.toFloats[0] = (this.toValue.IsNone ? 0f : this.toValue.Value);
			this.calculations = new CurveFsmAction.Calculation[1];
			this.calculations[0] = this.calculation;
			this.curves = new AnimationCurve[1];
			this.curves[0] = this.animCurve.curve;
			base.Init();
		}

		// Token: 0x06009BE9 RID: 39913 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void OnExit()
		{
		}

		// Token: 0x06009BEA RID: 39914 RVA: 0x00322000 File Offset: 0x00320200
		public override void OnUpdate()
		{
			base.OnUpdate();
			if (!this.floatVariable.IsNone && this.isRunning)
			{
				this.floatVariable.Value = this.resultFloats[0];
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
				if (!this.floatVariable.IsNone)
				{
					this.floatVariable.Value = this.resultFloats[0];
				}
				this.finishInNextStep = true;
			}
		}

		// Token: 0x0400815E RID: 33118
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat floatVariable;

		// Token: 0x0400815F RID: 33119
		[RequiredField]
		public FsmFloat fromValue;

		// Token: 0x04008160 RID: 33120
		[RequiredField]
		public FsmFloat toValue;

		// Token: 0x04008161 RID: 33121
		[RequiredField]
		public FsmAnimationCurve animCurve;

		// Token: 0x04008162 RID: 33122
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to otherwise linear move between fromValue and toValue.")]
		public CurveFsmAction.Calculation calculation;

		// Token: 0x04008163 RID: 33123
		private bool finishInNextStep;
	}
}
