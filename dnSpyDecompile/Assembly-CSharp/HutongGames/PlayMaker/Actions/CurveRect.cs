using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BAC RID: 2988
	[ActionCategory("AnimateVariables")]
	[Tooltip("Animates the value of a Rect Variable FROM-TO with assistance of Deformation Curves.")]
	public class CurveRect : CurveFsmAction
	{
		// Token: 0x06009BF1 RID: 39921 RVA: 0x00322D32 File Offset: 0x00320F32
		public override void Reset()
		{
			base.Reset();
			this.rectVariable = new FsmRect
			{
				UseVariable = true
			};
			this.toValue = new FsmRect
			{
				UseVariable = true
			};
			this.fromValue = new FsmRect
			{
				UseVariable = true
			};
		}

		// Token: 0x06009BF2 RID: 39922 RVA: 0x00322D70 File Offset: 0x00320F70
		public override void OnEnter()
		{
			base.OnEnter();
			this.finishInNextStep = false;
			this.resultFloats = new float[4];
			this.fromFloats = new float[4];
			this.fromFloats[0] = (this.fromValue.IsNone ? 0f : this.fromValue.Value.x);
			this.fromFloats[1] = (this.fromValue.IsNone ? 0f : this.fromValue.Value.y);
			this.fromFloats[2] = (this.fromValue.IsNone ? 0f : this.fromValue.Value.width);
			this.fromFloats[3] = (this.fromValue.IsNone ? 0f : this.fromValue.Value.height);
			this.toFloats = new float[4];
			this.toFloats[0] = (this.toValue.IsNone ? 0f : this.toValue.Value.x);
			this.toFloats[1] = (this.toValue.IsNone ? 0f : this.toValue.Value.y);
			this.toFloats[2] = (this.toValue.IsNone ? 0f : this.toValue.Value.width);
			this.toFloats[3] = (this.toValue.IsNone ? 0f : this.toValue.Value.height);
			this.curves = new AnimationCurve[4];
			this.curves[0] = this.curveX.curve;
			this.curves[1] = this.curveY.curve;
			this.curves[2] = this.curveW.curve;
			this.curves[3] = this.curveH.curve;
			this.calculations = new CurveFsmAction.Calculation[4];
			this.calculations[0] = this.calculationX;
			this.calculations[1] = this.calculationY;
			this.calculations[2] = this.calculationW;
			this.calculations[2] = this.calculationH;
			base.Init();
		}

		// Token: 0x06009BF3 RID: 39923 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void OnExit()
		{
		}

		// Token: 0x06009BF4 RID: 39924 RVA: 0x00322FC8 File Offset: 0x003211C8
		public override void OnUpdate()
		{
			base.OnUpdate();
			if (!this.rectVariable.IsNone && this.isRunning)
			{
				this.rct = new Rect(this.resultFloats[0], this.resultFloats[1], this.resultFloats[2], this.resultFloats[3]);
				this.rectVariable.Value = this.rct;
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
				if (!this.rectVariable.IsNone)
				{
					this.rct = new Rect(this.resultFloats[0], this.resultFloats[1], this.resultFloats[2], this.resultFloats[3]);
					this.rectVariable.Value = this.rct;
				}
				this.finishInNextStep = true;
			}
		}

		// Token: 0x0400817C RID: 33148
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmRect rectVariable;

		// Token: 0x0400817D RID: 33149
		[RequiredField]
		public FsmRect fromValue;

		// Token: 0x0400817E RID: 33150
		[RequiredField]
		public FsmRect toValue;

		// Token: 0x0400817F RID: 33151
		[RequiredField]
		public FsmAnimationCurve curveX;

		// Token: 0x04008180 RID: 33152
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to otherwise linear move between fromValue.x and toValue.x.")]
		public CurveFsmAction.Calculation calculationX;

		// Token: 0x04008181 RID: 33153
		[RequiredField]
		public FsmAnimationCurve curveY;

		// Token: 0x04008182 RID: 33154
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to otherwise linear move between fromValue.y and toValue.y.")]
		public CurveFsmAction.Calculation calculationY;

		// Token: 0x04008183 RID: 33155
		[RequiredField]
		public FsmAnimationCurve curveW;

		// Token: 0x04008184 RID: 33156
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to otherwise linear move between fromValue.width and toValue.width.")]
		public CurveFsmAction.Calculation calculationW;

		// Token: 0x04008185 RID: 33157
		[RequiredField]
		public FsmAnimationCurve curveH;

		// Token: 0x04008186 RID: 33158
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to otherwise linear move between fromValue.height and toValue.height.")]
		public CurveFsmAction.Calculation calculationH;

		// Token: 0x04008187 RID: 33159
		private Rect rct;

		// Token: 0x04008188 RID: 33160
		private bool finishInNextStep;
	}
}
