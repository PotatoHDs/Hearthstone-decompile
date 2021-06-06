using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BB2 RID: 2994
	[ActionCategory(ActionCategory.AnimateVariables)]
	[Tooltip("Easing Animation - Vector3")]
	public class EaseVector3 : EaseFsmAction
	{
		// Token: 0x06009C2F RID: 39983 RVA: 0x003248B0 File Offset: 0x00322AB0
		public override void Reset()
		{
			base.Reset();
			this.vector3Variable = null;
			this.fromValue = null;
			this.toValue = null;
			this.finishInNextStep = false;
		}

		// Token: 0x06009C30 RID: 39984 RVA: 0x003248D4 File Offset: 0x00322AD4
		public override void OnEnter()
		{
			base.OnEnter();
			this.fromFloats = new float[3];
			this.fromFloats[0] = this.fromValue.Value.x;
			this.fromFloats[1] = this.fromValue.Value.y;
			this.fromFloats[2] = this.fromValue.Value.z;
			this.toFloats = new float[3];
			this.toFloats[0] = this.toValue.Value.x;
			this.toFloats[1] = this.toValue.Value.y;
			this.toFloats[2] = this.toValue.Value.z;
			this.resultFloats = new float[3];
			this.finishInNextStep = false;
			this.vector3Variable.Value = this.fromValue.Value;
		}

		// Token: 0x06009C31 RID: 39985 RVA: 0x003234E0 File Offset: 0x003216E0
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06009C32 RID: 39986 RVA: 0x003249B8 File Offset: 0x00322BB8
		public override void OnUpdate()
		{
			base.OnUpdate();
			if (!this.vector3Variable.IsNone && this.isRunning)
			{
				this.vector3Variable.Value = new Vector3(this.resultFloats[0], this.resultFloats[1], this.resultFloats[2]);
			}
			if (this.finishInNextStep)
			{
				base.Finish();
				if (this.finishEvent != null)
				{
					base.Fsm.Event(this.finishEvent);
				}
			}
			if (this.finishAction && !this.finishInNextStep)
			{
				if (!this.vector3Variable.IsNone)
				{
					this.vector3Variable.Value = new Vector3(this.reverse.IsNone ? this.toValue.Value.x : (this.reverse.Value ? this.fromValue.Value.x : this.toValue.Value.x), this.reverse.IsNone ? this.toValue.Value.y : (this.reverse.Value ? this.fromValue.Value.y : this.toValue.Value.y), this.reverse.IsNone ? this.toValue.Value.z : (this.reverse.Value ? this.fromValue.Value.z : this.toValue.Value.z));
				}
				this.finishInNextStep = true;
			}
		}

		// Token: 0x040081B5 RID: 33205
		[RequiredField]
		public FsmVector3 fromValue;

		// Token: 0x040081B6 RID: 33206
		[RequiredField]
		public FsmVector3 toValue;

		// Token: 0x040081B7 RID: 33207
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector3Variable;

		// Token: 0x040081B8 RID: 33208
		private bool finishInNextStep;
	}
}
