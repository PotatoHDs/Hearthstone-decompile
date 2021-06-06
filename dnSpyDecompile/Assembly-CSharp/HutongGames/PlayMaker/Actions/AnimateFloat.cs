using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BA3 RID: 2979
	[ActionCategory(ActionCategory.AnimateVariables)]
	[Tooltip("Animates the value of a Float Variable using an Animation Curve.")]
	public class AnimateFloat : FsmStateAction
	{
		// Token: 0x06009BC1 RID: 39873 RVA: 0x003206F4 File Offset: 0x0031E8F4
		public override void Reset()
		{
			this.animCurve = null;
			this.floatVariable = null;
			this.finishEvent = null;
			this.realTime = false;
		}

		// Token: 0x06009BC2 RID: 39874 RVA: 0x00320714 File Offset: 0x0031E914
		public override void OnEnter()
		{
			this.startTime = FsmTime.RealtimeSinceStartup;
			this.currentTime = 0f;
			if (this.animCurve != null && this.animCurve.curve != null && this.animCurve.curve.keys.Length != 0)
			{
				this.endTime = this.animCurve.curve.keys[this.animCurve.curve.length - 1].time;
				this.looping = ActionHelpers.IsLoopingWrapMode(this.animCurve.curve.postWrapMode);
				this.floatVariable.Value = this.animCurve.curve.Evaluate(0f);
				return;
			}
			base.Finish();
		}

		// Token: 0x06009BC3 RID: 39875 RVA: 0x003207D8 File Offset: 0x0031E9D8
		public override void OnUpdate()
		{
			if (this.realTime)
			{
				this.currentTime = FsmTime.RealtimeSinceStartup - this.startTime;
			}
			else
			{
				this.currentTime += Time.deltaTime;
			}
			if (this.animCurve != null && this.animCurve.curve != null && this.floatVariable != null)
			{
				this.floatVariable.Value = this.animCurve.curve.Evaluate(this.currentTime);
			}
			if (this.currentTime >= this.endTime)
			{
				if (!this.looping)
				{
					base.Finish();
				}
				if (this.finishEvent != null)
				{
					base.Fsm.Event(this.finishEvent);
				}
			}
		}

		// Token: 0x04008112 RID: 33042
		[RequiredField]
		[Tooltip("The animation curve to use.")]
		public FsmAnimationCurve animCurve;

		// Token: 0x04008113 RID: 33043
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The float variable to set.")]
		public FsmFloat floatVariable;

		// Token: 0x04008114 RID: 33044
		[Tooltip("Optionally send an Event when the animation finishes.")]
		public FsmEvent finishEvent;

		// Token: 0x04008115 RID: 33045
		[Tooltip("Ignore TimeScale. Useful if the game is paused.")]
		public bool realTime;

		// Token: 0x04008116 RID: 33046
		private float startTime;

		// Token: 0x04008117 RID: 33047
		private float currentTime;

		// Token: 0x04008118 RID: 33048
		private float endTime;

		// Token: 0x04008119 RID: 33049
		private bool looping;
	}
}
