using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BE1 RID: 3041
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Tests if all the Bool Variables are False.\nSend an event or store the result.")]
	public class BoolNoneTrue : FsmStateAction
	{
		// Token: 0x06009CEC RID: 40172 RVA: 0x00326E69 File Offset: 0x00325069
		public override void Reset()
		{
			this.boolVariables = null;
			this.sendEvent = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009CED RID: 40173 RVA: 0x00326E87 File Offset: 0x00325087
		public override void OnEnter()
		{
			this.DoNoneTrue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009CEE RID: 40174 RVA: 0x00326E9D File Offset: 0x0032509D
		public override void OnUpdate()
		{
			this.DoNoneTrue();
		}

		// Token: 0x06009CEF RID: 40175 RVA: 0x00326EA8 File Offset: 0x003250A8
		private void DoNoneTrue()
		{
			if (this.boolVariables.Length == 0)
			{
				return;
			}
			bool flag = true;
			for (int i = 0; i < this.boolVariables.Length; i++)
			{
				if (this.boolVariables[i].Value)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				base.Fsm.Event(this.sendEvent);
			}
			this.storeResult.Value = flag;
		}

		// Token: 0x04008262 RID: 33378
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Bool variables to check.")]
		public FsmBool[] boolVariables;

		// Token: 0x04008263 RID: 33379
		[Tooltip("Event to send if none of the Bool variables are True.")]
		public FsmEvent sendEvent;

		// Token: 0x04008264 RID: 33380
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a Bool variable.")]
		public FsmBool storeResult;

		// Token: 0x04008265 RID: 33381
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;
	}
}
