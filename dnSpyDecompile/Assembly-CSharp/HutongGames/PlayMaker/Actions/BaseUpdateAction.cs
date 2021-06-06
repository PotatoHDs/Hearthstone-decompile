using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BDA RID: 3034
	public abstract class BaseUpdateAction : FsmStateAction
	{
		// Token: 0x06009CCA RID: 40138
		public abstract void OnActionUpdate();

		// Token: 0x06009CCB RID: 40139 RVA: 0x0032691B File Offset: 0x00324B1B
		public override void Reset()
		{
			this.everyFrame = false;
			this.updateType = BaseUpdateAction.UpdateType.OnUpdate;
		}

		// Token: 0x06009CCC RID: 40140 RVA: 0x0032692B File Offset: 0x00324B2B
		public override void OnPreprocess()
		{
			if (this.updateType == BaseUpdateAction.UpdateType.OnFixedUpdate)
			{
				base.Fsm.HandleFixedUpdate = true;
				return;
			}
			if (this.updateType == BaseUpdateAction.UpdateType.OnLateUpdate)
			{
				base.Fsm.HandleLateUpdate = true;
			}
		}

		// Token: 0x06009CCD RID: 40141 RVA: 0x00326958 File Offset: 0x00324B58
		public override void OnUpdate()
		{
			if (this.updateType == BaseUpdateAction.UpdateType.OnUpdate)
			{
				this.OnActionUpdate();
			}
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009CCE RID: 40142 RVA: 0x00326976 File Offset: 0x00324B76
		public override void OnLateUpdate()
		{
			if (this.updateType == BaseUpdateAction.UpdateType.OnLateUpdate)
			{
				this.OnActionUpdate();
			}
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009CCF RID: 40143 RVA: 0x00326995 File Offset: 0x00324B95
		public override void OnFixedUpdate()
		{
			if (this.updateType == BaseUpdateAction.UpdateType.OnFixedUpdate)
			{
				this.OnActionUpdate();
			}
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x04008244 RID: 33348
		[ActionSection("Update type")]
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		// Token: 0x04008245 RID: 33349
		public BaseUpdateAction.UpdateType updateType;

		// Token: 0x02002792 RID: 10130
		public enum UpdateType
		{
			// Token: 0x0400F4A6 RID: 62630
			OnUpdate,
			// Token: 0x0400F4A7 RID: 62631
			OnLateUpdate,
			// Token: 0x0400F4A8 RID: 62632
			OnFixedUpdate
		}
	}
}
