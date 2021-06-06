using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BAD RID: 2989
	[ActionCategory(ActionCategory.AnimateVariables)]
	[Tooltip("Animates the value of a Vector3 Variable FROM-TO with assistance of Deformation Curves.")]
	public class CurveVector3 : CurveFsmAction
	{
		// Token: 0x06009BF6 RID: 39926 RVA: 0x003230BB File Offset: 0x003212BB
		public override void Reset()
		{
			base.Reset();
			this.vectorVariable = new FsmVector3
			{
				UseVariable = true
			};
			this.toValue = new FsmVector3
			{
				UseVariable = true
			};
			this.fromValue = new FsmVector3
			{
				UseVariable = true
			};
		}

		// Token: 0x06009BF7 RID: 39927 RVA: 0x003230FC File Offset: 0x003212FC
		public override void OnEnter()
		{
			base.OnEnter();
			this.finishInNextStep = false;
			this.resultFloats = new float[3];
			this.fromFloats = new float[3];
			this.fromFloats[0] = (this.fromValue.IsNone ? 0f : this.fromValue.Value.x);
			this.fromFloats[1] = (this.fromValue.IsNone ? 0f : this.fromValue.Value.y);
			this.fromFloats[2] = (this.fromValue.IsNone ? 0f : this.fromValue.Value.z);
			this.toFloats = new float[3];
			this.toFloats[0] = (this.toValue.IsNone ? 0f : this.toValue.Value.x);
			this.toFloats[1] = (this.toValue.IsNone ? 0f : this.toValue.Value.y);
			this.toFloats[2] = (this.toValue.IsNone ? 0f : this.toValue.Value.z);
			this.curves = new AnimationCurve[3];
			this.curves[0] = this.curveX.curve;
			this.curves[1] = this.curveY.curve;
			this.curves[2] = this.curveZ.curve;
			this.calculations = new CurveFsmAction.Calculation[3];
			this.calculations[0] = this.calculationX;
			this.calculations[1] = this.calculationY;
			this.calculations[2] = this.calculationZ;
			base.Init();
		}

		// Token: 0x06009BF8 RID: 39928 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void OnExit()
		{
		}

		// Token: 0x06009BF9 RID: 39929 RVA: 0x003232C4 File Offset: 0x003214C4
		public override void OnUpdate()
		{
			base.OnUpdate();
			if (!this.vectorVariable.IsNone && this.isRunning)
			{
				this.vct = new Vector3(this.resultFloats[0], this.resultFloats[1], this.resultFloats[2]);
				this.vectorVariable.Value = this.vct;
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
				if (!this.vectorVariable.IsNone)
				{
					this.vct = new Vector3(this.resultFloats[0], this.resultFloats[1], this.resultFloats[2]);
					this.vectorVariable.Value = this.vct;
				}
				this.finishInNextStep = true;
			}
		}

		// Token: 0x04008189 RID: 33161
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 vectorVariable;

		// Token: 0x0400818A RID: 33162
		[RequiredField]
		public FsmVector3 fromValue;

		// Token: 0x0400818B RID: 33163
		[RequiredField]
		public FsmVector3 toValue;

		// Token: 0x0400818C RID: 33164
		[RequiredField]
		public FsmAnimationCurve curveX;

		// Token: 0x0400818D RID: 33165
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to otherwise linear move between fromValue.x and toValue.x.")]
		public CurveFsmAction.Calculation calculationX;

		// Token: 0x0400818E RID: 33166
		[RequiredField]
		public FsmAnimationCurve curveY;

		// Token: 0x0400818F RID: 33167
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to otherwise linear move between fromValue.y and toValue.y.")]
		public CurveFsmAction.Calculation calculationY;

		// Token: 0x04008190 RID: 33168
		[RequiredField]
		public FsmAnimationCurve curveZ;

		// Token: 0x04008191 RID: 33169
		[Tooltip("Calculation lets you set a type of curve deformation that will be applied to otherwise linear move between fromValue.z and toValue.z.")]
		public CurveFsmAction.Calculation calculationZ;

		// Token: 0x04008192 RID: 33170
		private Vector3 vct;

		// Token: 0x04008193 RID: 33171
		private bool finishInNextStep;
	}
}
