using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C99 RID: 3225
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Immediately return to the previously active state.")]
	public class GotoPreviousState : FsmStateAction
	{
		// Token: 0x0600A034 RID: 41012 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void Reset()
		{
		}

		// Token: 0x0600A035 RID: 41013 RVA: 0x003303A8 File Offset: 0x0032E5A8
		public override void OnEnter()
		{
			if (base.Fsm.PreviousActiveState != null)
			{
				base.Log("Goto Previous State: " + base.Fsm.PreviousActiveState.Name);
				base.Fsm.GotoPreviousState();
			}
			base.Finish();
		}
	}
}
