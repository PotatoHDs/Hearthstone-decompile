using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C6C RID: 3180
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets a Game Object's Layer and stores it in an Int Variable.")]
	public class GetLayer : FsmStateAction
	{
		// Token: 0x06009F6D RID: 40813 RVA: 0x0032E839 File Offset: 0x0032CA39
		public override void Reset()
		{
			this.gameObject = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009F6E RID: 40814 RVA: 0x0032E850 File Offset: 0x0032CA50
		public override void OnEnter()
		{
			this.DoGetLayer();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009F6F RID: 40815 RVA: 0x0032E866 File Offset: 0x0032CA66
		public override void OnUpdate()
		{
			this.DoGetLayer();
		}

		// Token: 0x06009F70 RID: 40816 RVA: 0x0032E86E File Offset: 0x0032CA6E
		private void DoGetLayer()
		{
			if (this.gameObject.Value == null)
			{
				return;
			}
			this.storeResult.Value = this.gameObject.Value.layer;
		}

		// Token: 0x0400850A RID: 34058
		[RequiredField]
		public FsmGameObject gameObject;

		// Token: 0x0400850B RID: 34059
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt storeResult;

		// Token: 0x0400850C RID: 34060
		public bool everyFrame;
	}
}
