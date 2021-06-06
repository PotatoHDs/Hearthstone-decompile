using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C39 RID: 3129
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Sets how subsequent events sent in this state are handled.")]
	public class FsmEventOptions : FsmStateAction
	{
		// Token: 0x06009E84 RID: 40580 RVA: 0x0032BAFA File Offset: 0x00329CFA
		public override void Reset()
		{
			this.sendToFsmComponent = null;
			this.sendToGameObject = null;
			this.fsmName = "";
			this.sendToChildren = false;
			this.broadcastToAll = false;
		}

		// Token: 0x06009E85 RID: 40581 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void OnUpdate()
		{
		}

		// Token: 0x040083D6 RID: 33750
		public PlayMakerFSM sendToFsmComponent;

		// Token: 0x040083D7 RID: 33751
		public FsmGameObject sendToGameObject;

		// Token: 0x040083D8 RID: 33752
		public FsmString fsmName;

		// Token: 0x040083D9 RID: 33753
		public FsmBool sendToChildren;

		// Token: 0x040083DA RID: 33754
		public FsmBool broadcastToAll;
	}
}
