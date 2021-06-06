using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BE4 RID: 3044
	[Obsolete("This action is obsolete; use Send Event with Event Target instead.")]
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Sends an Event to all FSMs in the scene or to all FSMs on a Game Object.\nNOTE: This action won't work on the very first frame of the game...")]
	public class BroadcastEvent : FsmStateAction
	{
		// Token: 0x06009CFA RID: 40186 RVA: 0x00327051 File Offset: 0x00325251
		public override void Reset()
		{
			this.broadcastEvent = null;
			this.gameObject = null;
			this.sendToChildren = false;
			this.excludeSelf = false;
		}

		// Token: 0x06009CFB RID: 40187 RVA: 0x0032707C File Offset: 0x0032527C
		public override void OnEnter()
		{
			if (!string.IsNullOrEmpty(this.broadcastEvent.Value))
			{
				if (this.gameObject.Value != null)
				{
					base.Fsm.BroadcastEventToGameObject(this.gameObject.Value, this.broadcastEvent.Value, this.sendToChildren.Value, this.excludeSelf.Value);
				}
				else
				{
					base.Fsm.BroadcastEvent(this.broadcastEvent.Value, this.excludeSelf.Value);
				}
			}
			base.Finish();
		}

		// Token: 0x0400826F RID: 33391
		[RequiredField]
		public FsmString broadcastEvent;

		// Token: 0x04008270 RID: 33392
		[Tooltip("Optionally specify a game object to broadcast the event to all FSMs on that game object.")]
		public FsmGameObject gameObject;

		// Token: 0x04008271 RID: 33393
		[Tooltip("Broadcast to all FSMs on the game object's children.")]
		public FsmBool sendToChildren;

		// Token: 0x04008272 RID: 33394
		public FsmBool excludeSelf;
	}
}
