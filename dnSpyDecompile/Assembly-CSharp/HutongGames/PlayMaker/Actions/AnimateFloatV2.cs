using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BA5 RID: 2981
	[ActionCategory(ActionCategory.AnimateVariables)]
	[Tooltip("Animates the value of a Float Variable using an Animation Curve.")]
	public class AnimateFloatV2 : AnimateFsmAction
	{
		// Token: 0x06009BCA RID: 39882 RVA: 0x00320AED File Offset: 0x0031ECED
		public override void Reset()
		{
			base.Reset();
			this.floatVariable = new FsmFloat
			{
				UseVariable = true
			};
		}

		// Token: 0x06009BCB RID: 39883 RVA: 0x00320B08 File Offset: 0x0031ED08
		public override void OnEnter()
		{
			base.OnEnter();
			this.finishInNextStep = false;
			this.resultFloats = new float[1];
			this.fromFloats = new float[1];
			this.fromFloats[0] = (this.floatVariable.IsNone ? 0f : this.floatVariable.Value);
			this.calculations = new AnimateFsmAction.Calculation[1];
			this.calculations[0] = this.calculation;
			this.curves = new AnimationCurve[1];
			this.curves[0] = this.animCurve.curve;
			base.Init();
		}

		// Token: 0x06009BCC RID: 39884 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void OnExit()
		{
		}

		// Token: 0x06009BCD RID: 39885 RVA: 0x00320BA0 File Offset: 0x0031EDA0
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

		// Token: 0x04008124 RID: 33060
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat floatVariable;

		// Token: 0x04008125 RID: 33061
		[RequiredField]
		public FsmAnimationCurve animCurve;

		// Token: 0x04008126 RID: 33062
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to floatVariable")]
		public AnimateFsmAction.Calculation calculation;

		// Token: 0x04008127 RID: 33063
		private bool finishInNextStep;
	}
}
