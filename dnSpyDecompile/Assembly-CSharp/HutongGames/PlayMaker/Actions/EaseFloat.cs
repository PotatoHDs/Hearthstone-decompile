using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BAF RID: 2991
	[ActionCategory(ActionCategory.AnimateVariables)]
	[Tooltip("Easing Animation - Float")]
	public class EaseFloat : EaseFsmAction
	{
		// Token: 0x06009C00 RID: 39936 RVA: 0x003236E4 File Offset: 0x003218E4
		public override void Reset()
		{
			base.Reset();
			this.floatVariable = null;
			this.fromValue = null;
			this.toValue = null;
			this.finishInNextStep = false;
		}

		// Token: 0x06009C01 RID: 39937 RVA: 0x00323708 File Offset: 0x00321908
		public override void OnEnter()
		{
			base.OnEnter();
			this.fromFloats = new float[1];
			this.fromFloats[0] = this.fromValue.Value;
			this.toFloats = new float[1];
			this.toFloats[0] = this.toValue.Value;
			this.resultFloats = new float[1];
			this.finishInNextStep = false;
			this.floatVariable.Value = this.fromValue.Value;
		}

		// Token: 0x06009C02 RID: 39938 RVA: 0x003234E0 File Offset: 0x003216E0
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06009C03 RID: 39939 RVA: 0x00323784 File Offset: 0x00321984
		public override void OnUpdate()
		{
			base.OnUpdate();
			if (!this.floatVariable.IsNone && this.isRunning)
			{
				this.floatVariable.Value = this.resultFloats[0];
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
				if (!this.floatVariable.IsNone)
				{
					this.floatVariable.Value = (this.reverse.IsNone ? this.toValue.Value : (this.reverse.Value ? this.fromValue.Value : this.toValue.Value));
				}
				this.finishInNextStep = true;
			}
		}

		// Token: 0x04008198 RID: 33176
		[RequiredField]
		public FsmFloat fromValue;

		// Token: 0x04008199 RID: 33177
		[RequiredField]
		public FsmFloat toValue;

		// Token: 0x0400819A RID: 33178
		[UIHint(UIHint.Variable)]
		public FsmFloat floatVariable;

		// Token: 0x0400819B RID: 33179
		private bool finishInNextStep;
	}
}
