using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C8D RID: 3213
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets a Game Object's Tag and stores it in a String Variable.")]
	public class GetTag : FsmStateAction
	{
		// Token: 0x06009FFC RID: 40956 RVA: 0x0032FAC0 File Offset: 0x0032DCC0
		public override void Reset()
		{
			this.gameObject = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009FFD RID: 40957 RVA: 0x0032FAD7 File Offset: 0x0032DCD7
		public override void OnEnter()
		{
			this.DoGetTag();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009FFE RID: 40958 RVA: 0x0032FAED File Offset: 0x0032DCED
		public override void OnUpdate()
		{
			this.DoGetTag();
		}

		// Token: 0x06009FFF RID: 40959 RVA: 0x0032FAF5 File Offset: 0x0032DCF5
		private void DoGetTag()
		{
			if (this.gameObject.Value == null)
			{
				return;
			}
			this.storeResult.Value = this.gameObject.Value.tag;
		}

		// Token: 0x0400857E RID: 34174
		[RequiredField]
		public FsmGameObject gameObject;

		// Token: 0x0400857F RID: 34175
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString storeResult;

		// Token: 0x04008580 RID: 34176
		public bool everyFrame;
	}
}
