using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CF7 RID: 3319
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Sends an Event in the next frame. Useful if you want to loop states every frame.")]
	public class NextFrameEvent : FsmStateAction
	{
		// Token: 0x0600A1C8 RID: 41416 RVA: 0x0033936E File Offset: 0x0033756E
		public override void Reset()
		{
			this.sendEvent = null;
		}

		// Token: 0x0600A1C9 RID: 41417 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void OnEnter()
		{
		}

		// Token: 0x0600A1CA RID: 41418 RVA: 0x00339377 File Offset: 0x00337577
		public override void OnUpdate()
		{
			base.Finish();
			base.Fsm.Event(this.sendEvent);
		}

		// Token: 0x040087E1 RID: 34785
		[RequiredField]
		public FsmEvent sendEvent;
	}
}
