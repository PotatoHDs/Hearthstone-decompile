using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D41 RID: 3393
	public abstract class QuaternionBaseAction : FsmStateAction
	{
		// Token: 0x0600A344 RID: 41796 RVA: 0x0033EBFC File Offset: 0x0033CDFC
		public override void Awake()
		{
			if (this.everyFrame)
			{
				QuaternionBaseAction.everyFrameOptions everyFrameOptions = this.everyFrameOption;
				if (everyFrameOptions == QuaternionBaseAction.everyFrameOptions.FixedUpdate)
				{
					base.Fsm.HandleFixedUpdate = true;
					return;
				}
				if (everyFrameOptions != QuaternionBaseAction.everyFrameOptions.LateUpdate)
				{
					return;
				}
				base.Fsm.HandleLateUpdate = true;
			}
		}

		// Token: 0x0400898F RID: 35215
		[Tooltip("Repeat every frame. Useful if any of the values are changing.")]
		public bool everyFrame;

		// Token: 0x04008990 RID: 35216
		[Tooltip("Defines how to perform the action when 'every Frame' is enabled.")]
		public QuaternionBaseAction.everyFrameOptions everyFrameOption;

		// Token: 0x020027A0 RID: 10144
		public enum everyFrameOptions
		{
			// Token: 0x0400F4ED RID: 62701
			Update,
			// Token: 0x0400F4EE RID: 62702
			FixedUpdate,
			// Token: 0x0400F4EF RID: 62703
			LateUpdate
		}
	}
}
