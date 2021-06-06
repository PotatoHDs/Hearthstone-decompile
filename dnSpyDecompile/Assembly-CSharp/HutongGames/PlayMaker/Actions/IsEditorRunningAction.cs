using System;
using Blizzard.T5.Core;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F4D RID: 3917
	[ActionCategory("Pegasus")]
	[Tooltip("Is Editor Running sends Events based on the result.")]
	public class IsEditorRunningAction : FsmStateAction
	{
		// Token: 0x0600ACB7 RID: 44215 RVA: 0x0035E1E2 File Offset: 0x0035C3E2
		public override void Reset()
		{
			this.trueEvent = null;
			this.falseEvent = null;
			this.storeResult = null;
		}

		// Token: 0x0600ACB8 RID: 44216 RVA: 0x0035E1F9 File Offset: 0x0035C3F9
		public override void OnEnter()
		{
			this.IsEditorRunning();
			base.Finish();
		}

		// Token: 0x0600ACB9 RID: 44217 RVA: 0x0035E207 File Offset: 0x0035C407
		public override void OnUpdate()
		{
			this.IsEditorRunning();
		}

		// Token: 0x0600ACBA RID: 44218 RVA: 0x0035E210 File Offset: 0x0035C410
		private void IsEditorRunning()
		{
			this.storeResult.Value = GeneralUtils.IsEditorPlaying();
			if (this.storeResult.Value)
			{
				base.Fsm.Event(this.trueEvent);
				return;
			}
			base.Fsm.Event(this.falseEvent);
		}

		// Token: 0x0400938A RID: 37770
		[RequiredField]
		[Tooltip("Event to use if Editor is running.")]
		public FsmEvent trueEvent;

		// Token: 0x0400938B RID: 37771
		[Tooltip("Event to use if Editor is NOT running.")]
		public FsmEvent falseEvent;

		// Token: 0x0400938C RID: 37772
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the true/false result in a bool variable.")]
		public FsmBool storeResult;
	}
}
