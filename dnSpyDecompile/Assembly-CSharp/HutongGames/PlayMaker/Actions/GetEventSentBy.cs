using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E0C RID: 3596
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Gets the sender of the last event.")]
	public class GetEventSentBy : FsmStateAction
	{
		// Token: 0x0600A702 RID: 42754 RVA: 0x0034B3B1 File Offset: 0x003495B1
		public override void Reset()
		{
			this.sentByGameObject = null;
			this.gameObjectName = null;
			this.fsmName = null;
		}

		// Token: 0x0600A703 RID: 42755 RVA: 0x0034B3C8 File Offset: 0x003495C8
		public override void OnEnter()
		{
			if (Fsm.EventData.SentByGameObject != null)
			{
				this.sentByGameObject.Value = Fsm.EventData.SentByGameObject;
			}
			else if (Fsm.EventData.SentByFsm != null)
			{
				this.sentByGameObject.Value = Fsm.EventData.SentByFsm.GameObject;
				this.fsmName.Value = Fsm.EventData.SentByFsm.Name;
			}
			else
			{
				this.sentByGameObject.Value = null;
				this.fsmName.Value = "";
			}
			if (this.sentByGameObject.Value != null)
			{
				this.gameObjectName.Value = this.sentByGameObject.Value.name;
			}
			base.Finish();
		}

		// Token: 0x04008D88 RID: 36232
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the GameObject that sent the event.")]
		public FsmGameObject sentByGameObject;

		// Token: 0x04008D89 RID: 36233
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the name of the GameObject that sent the event.")]
		public FsmString gameObjectName;

		// Token: 0x04008D8A RID: 36234
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the name of the FSM that sent the event.")]
		public FsmString fsmName;
	}
}
