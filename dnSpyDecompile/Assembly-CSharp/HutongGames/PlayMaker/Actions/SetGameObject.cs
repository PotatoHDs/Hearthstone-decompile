using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DCA RID: 3530
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Sets the value of a Game Object Variable.")]
	public class SetGameObject : FsmStateAction
	{
		// Token: 0x0600A5E3 RID: 42467 RVA: 0x003481ED File Offset: 0x003463ED
		public override void Reset()
		{
			this.variable = null;
			this.gameObject = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A5E4 RID: 42468 RVA: 0x00348204 File Offset: 0x00346404
		public override void OnEnter()
		{
			this.variable.Value = this.gameObject.Value;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A5E5 RID: 42469 RVA: 0x0034822A File Offset: 0x0034642A
		public override void OnUpdate()
		{
			this.variable.Value = this.gameObject.Value;
		}

		// Token: 0x04008C93 RID: 35987
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmGameObject variable;

		// Token: 0x04008C94 RID: 35988
		public FsmGameObject gameObject;

		// Token: 0x04008C95 RID: 35989
		public bool everyFrame;
	}
}
