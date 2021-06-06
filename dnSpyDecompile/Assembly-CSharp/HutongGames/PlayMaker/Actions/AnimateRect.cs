using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BA7 RID: 2983
	[ActionCategory("AnimateVariables")]
	[Tooltip("Animates the value of a Rect Variable using an Animation Curve.")]
	public class AnimateRect : AnimateFsmAction
	{
		// Token: 0x06009BD8 RID: 39896 RVA: 0x00321701 File Offset: 0x0031F901
		public override void Reset()
		{
			base.Reset();
			this.rectVariable = new FsmRect
			{
				UseVariable = true
			};
		}

		// Token: 0x06009BD9 RID: 39897 RVA: 0x0032171C File Offset: 0x0031F91C
		public override void OnEnter()
		{
			base.OnEnter();
			this.finishInNextStep = false;
			this.resultFloats = new float[4];
			this.fromFloats = new float[4];
			this.fromFloats[0] = (this.rectVariable.IsNone ? 0f : this.rectVariable.Value.x);
			this.fromFloats[1] = (this.rectVariable.IsNone ? 0f : this.rectVariable.Value.y);
			this.fromFloats[2] = (this.rectVariable.IsNone ? 0f : this.rectVariable.Value.width);
			this.fromFloats[3] = (this.rectVariable.IsNone ? 0f : this.rectVariable.Value.height);
			this.curves = new AnimationCurve[4];
			this.curves[0] = this.curveX.curve;
			this.curves[1] = this.curveY.curve;
			this.curves[2] = this.curveW.curve;
			this.curves[3] = this.curveH.curve;
			this.calculations = new AnimateFsmAction.Calculation[4];
			this.calculations[0] = this.calculationX;
			this.calculations[1] = this.calculationY;
			this.calculations[2] = this.calculationW;
			this.calculations[3] = this.calculationH;
			base.Init();
			if (Math.Abs(this.delay.Value) < 0.01f)
			{
				this.UpdateVariableValue();
			}
		}

		// Token: 0x06009BDA RID: 39898 RVA: 0x003218C9 File Offset: 0x0031FAC9
		private void UpdateVariableValue()
		{
			if (!this.rectVariable.IsNone)
			{
				this.rectVariable.Value = new Rect(this.resultFloats[0], this.resultFloats[1], this.resultFloats[2], this.resultFloats[3]);
			}
		}

		// Token: 0x06009BDB RID: 39899 RVA: 0x00321908 File Offset: 0x0031FB08
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

		// Token: 0x0400813F RID: 33087
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmRect rectVariable;

		// Token: 0x04008140 RID: 33088
		[RequiredField]
		public FsmAnimationCurve curveX;

		// Token: 0x04008141 RID: 33089
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to rectVariable.x.")]
		public AnimateFsmAction.Calculation calculationX;

		// Token: 0x04008142 RID: 33090
		[RequiredField]
		public FsmAnimationCurve curveY;

		// Token: 0x04008143 RID: 33091
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to rectVariable.y.")]
		public AnimateFsmAction.Calculation calculationY;

		// Token: 0x04008144 RID: 33092
		[RequiredField]
		public FsmAnimationCurve curveW;

		// Token: 0x04008145 RID: 33093
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to rectVariable.width.")]
		public AnimateFsmAction.Calculation calculationW;

		// Token: 0x04008146 RID: 33094
		[RequiredField]
		public FsmAnimationCurve curveH;

		// Token: 0x04008147 RID: 33095
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to rectVariable.height.")]
		public AnimateFsmAction.Calculation calculationH;

		// Token: 0x04008148 RID: 33096
		private bool finishInNextStep;
	}
}
