using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EB8 RID: 3768
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Interpolates between 2 Vector3 values over a specified Time.")]
	public class Vector3Interpolate : FsmStateAction
	{
		// Token: 0x0600AA2A RID: 43562 RVA: 0x0035488C File Offset: 0x00352A8C
		public override void Reset()
		{
			this.mode = InterpolationType.Linear;
			this.fromVector = new FsmVector3
			{
				UseVariable = true
			};
			this.toVector = new FsmVector3
			{
				UseVariable = true
			};
			this.time = 1f;
			this.storeResult = null;
			this.finishEvent = null;
			this.realTime = false;
		}

		// Token: 0x0600AA2B RID: 43563 RVA: 0x003548E9 File Offset: 0x00352AE9
		public override void OnEnter()
		{
			this.startTime = FsmTime.RealtimeSinceStartup;
			this.currentTime = 0f;
			if (this.storeResult == null)
			{
				base.Finish();
				return;
			}
			this.storeResult.Value = this.fromVector.Value;
		}

		// Token: 0x0600AA2C RID: 43564 RVA: 0x00354928 File Offset: 0x00352B28
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
			float num = this.currentTime / this.time.Value;
			InterpolationType interpolationType = this.mode;
			if (interpolationType != InterpolationType.Linear && interpolationType == InterpolationType.EaseInOut)
			{
				num = Mathf.SmoothStep(0f, 1f, num);
			}
			this.storeResult.Value = Vector3.Lerp(this.fromVector.Value, this.toVector.Value, num);
			if (num >= 1f)
			{
				if (this.finishEvent != null)
				{
					base.Fsm.Event(this.finishEvent);
				}
				base.Finish();
			}
		}

		// Token: 0x040090C4 RID: 37060
		public InterpolationType mode;

		// Token: 0x040090C5 RID: 37061
		[RequiredField]
		public FsmVector3 fromVector;

		// Token: 0x040090C6 RID: 37062
		[RequiredField]
		public FsmVector3 toVector;

		// Token: 0x040090C7 RID: 37063
		[RequiredField]
		public FsmFloat time;

		// Token: 0x040090C8 RID: 37064
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 storeResult;

		// Token: 0x040090C9 RID: 37065
		public FsmEvent finishEvent;

		// Token: 0x040090CA RID: 37066
		[Tooltip("Ignore TimeScale")]
		public bool realTime;

		// Token: 0x040090CB RID: 37067
		private float startTime;

		// Token: 0x040090CC RID: 37068
		private float currentTime;
	}
}
