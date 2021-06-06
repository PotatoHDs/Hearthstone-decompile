using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BDD RID: 3037
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Tests if all the given Bool Variables are True.")]
	public class BoolAllTrue : FsmStateAction
	{
		// Token: 0x06009CDB RID: 40155 RVA: 0x00326C88 File Offset: 0x00324E88
		public override void Reset()
		{
			this.boolVariables = null;
			this.sendEvent = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009CDC RID: 40156 RVA: 0x00326CA6 File Offset: 0x00324EA6
		public override void OnEnter()
		{
			this.DoAllTrue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009CDD RID: 40157 RVA: 0x00326CBC File Offset: 0x00324EBC
		public override void OnUpdate()
		{
			this.DoAllTrue();
		}

		// Token: 0x06009CDE RID: 40158 RVA: 0x00326CC4 File Offset: 0x00324EC4
		private void DoAllTrue()
		{
			if (this.boolVariables.Length == 0)
			{
				return;
			}
			bool flag = true;
			for (int i = 0; i < this.boolVariables.Length; i++)
			{
				if (!this.boolVariables[i].Value)
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

		// Token: 0x04008255 RID: 33365
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Readonly]
		[Tooltip("The Bool variables to check.")]
		public FsmBool[] boolVariables;

		// Token: 0x04008256 RID: 33366
		[Tooltip("Event to send if all the Bool variables are True.")]
		public FsmEvent sendEvent;

		// Token: 0x04008257 RID: 33367
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a Bool variable.")]
		public FsmBool storeResult;

		// Token: 0x04008258 RID: 33368
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;
	}
}
