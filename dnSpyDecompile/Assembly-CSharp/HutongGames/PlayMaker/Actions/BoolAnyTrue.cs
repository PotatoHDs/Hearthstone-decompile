using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BDE RID: 3038
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Tests if any of the given Bool Variables are True.")]
	public class BoolAnyTrue : FsmStateAction
	{
		// Token: 0x06009CE0 RID: 40160 RVA: 0x00326D23 File Offset: 0x00324F23
		public override void Reset()
		{
			this.boolVariables = null;
			this.sendEvent = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009CE1 RID: 40161 RVA: 0x00326D41 File Offset: 0x00324F41
		public override void OnEnter()
		{
			this.DoAnyTrue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009CE2 RID: 40162 RVA: 0x00326D57 File Offset: 0x00324F57
		public override void OnUpdate()
		{
			this.DoAnyTrue();
		}

		// Token: 0x06009CE3 RID: 40163 RVA: 0x00326D60 File Offset: 0x00324F60
		private void DoAnyTrue()
		{
			if (this.boolVariables.Length == 0)
			{
				return;
			}
			this.storeResult.Value = false;
			for (int i = 0; i < this.boolVariables.Length; i++)
			{
				if (this.boolVariables[i].Value)
				{
					base.Fsm.Event(this.sendEvent);
					this.storeResult.Value = true;
					return;
				}
			}
		}

		// Token: 0x04008259 RID: 33369
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Bool variables to check.")]
		public FsmBool[] boolVariables;

		// Token: 0x0400825A RID: 33370
		[Tooltip("Event to send if any of the Bool variables are True.")]
		public FsmEvent sendEvent;

		// Token: 0x0400825B RID: 33371
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a Bool variable.")]
		public FsmBool storeResult;

		// Token: 0x0400825C RID: 33372
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;
	}
}
