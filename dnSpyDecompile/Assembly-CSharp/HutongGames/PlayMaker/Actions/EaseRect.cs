using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BB1 RID: 2993
	[ActionCategory("AnimateVariables")]
	[Tooltip("Easing Animation - Rect.")]
	public class EaseRect : EaseFsmAction
	{
		// Token: 0x06009C2A RID: 39978 RVA: 0x00324547 File Offset: 0x00322747
		public override void Reset()
		{
			base.Reset();
			this.rectVariable = null;
			this.fromValue = null;
			this.toValue = null;
			this.finishInNextStep = false;
		}

		// Token: 0x06009C2B RID: 39979 RVA: 0x0032456C File Offset: 0x0032276C
		public override void OnEnter()
		{
			base.OnEnter();
			this.fromFloats = new float[4];
			this.fromFloats[0] = this.fromValue.Value.x;
			this.fromFloats[1] = this.fromValue.Value.y;
			this.fromFloats[2] = this.fromValue.Value.width;
			this.fromFloats[3] = this.fromValue.Value.height;
			this.toFloats = new float[4];
			this.toFloats[0] = this.toValue.Value.x;
			this.toFloats[1] = this.toValue.Value.y;
			this.toFloats[2] = this.toValue.Value.width;
			this.toFloats[3] = this.toValue.Value.height;
			this.resultFloats = new float[4];
			this.finishInNextStep = false;
			this.rectVariable.Value = this.fromValue.Value;
		}

		// Token: 0x06009C2C RID: 39980 RVA: 0x003234E0 File Offset: 0x003216E0
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06009C2D RID: 39981 RVA: 0x00324698 File Offset: 0x00322898
		public override void OnUpdate()
		{
			base.OnUpdate();
			if (!this.rectVariable.IsNone && this.isRunning)
			{
				this.rectVariable.Value = new Rect(this.resultFloats[0], this.resultFloats[1], this.resultFloats[2], this.resultFloats[3]);
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
				if (!this.rectVariable.IsNone)
				{
					this.rectVariable.Value = new Rect(this.reverse.IsNone ? this.toValue.Value.x : (this.reverse.Value ? this.fromValue.Value.x : this.toValue.Value.x), this.reverse.IsNone ? this.toValue.Value.y : (this.reverse.Value ? this.fromValue.Value.y : this.toValue.Value.y), this.reverse.IsNone ? this.toValue.Value.width : (this.reverse.Value ? this.fromValue.Value.width : this.toValue.Value.width), this.reverse.IsNone ? this.toValue.Value.height : (this.reverse.Value ? this.fromValue.Value.height : this.toValue.Value.height));
				}
				this.finishInNextStep = true;
			}
		}

		// Token: 0x040081B1 RID: 33201
		[RequiredField]
		public FsmRect fromValue;

		// Token: 0x040081B2 RID: 33202
		[RequiredField]
		public FsmRect toValue;

		// Token: 0x040081B3 RID: 33203
		[UIHint(UIHint.Variable)]
		public FsmRect rectVariable;

		// Token: 0x040081B4 RID: 33204
		private bool finishInNextStep;
	}
}
