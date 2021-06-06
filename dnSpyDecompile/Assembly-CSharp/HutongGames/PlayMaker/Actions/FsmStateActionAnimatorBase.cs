using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BB4 RID: 2996
	public abstract class FsmStateActionAnimatorBase : FsmStateAction
	{
		// Token: 0x06009C38 RID: 39992
		public abstract void OnActionUpdate();

		// Token: 0x06009C39 RID: 39993 RVA: 0x00324C6C File Offset: 0x00322E6C
		public override void Reset()
		{
			this.everyFrame = false;
			this.everyFrameOption = FsmStateActionAnimatorBase.AnimatorFrameUpdateSelector.OnUpdate;
		}

		// Token: 0x06009C3A RID: 39994 RVA: 0x00324C7C File Offset: 0x00322E7C
		public override void OnPreprocess()
		{
			if (this.everyFrameOption == FsmStateActionAnimatorBase.AnimatorFrameUpdateSelector.OnAnimatorMove)
			{
				base.Fsm.HandleAnimatorMove = true;
			}
			if (this.everyFrameOption == FsmStateActionAnimatorBase.AnimatorFrameUpdateSelector.OnAnimatorIK)
			{
				base.Fsm.HandleAnimatorIK = true;
			}
		}

		// Token: 0x06009C3B RID: 39995 RVA: 0x00324CA8 File Offset: 0x00322EA8
		public override void OnUpdate()
		{
			if (this.everyFrameOption == FsmStateActionAnimatorBase.AnimatorFrameUpdateSelector.OnUpdate)
			{
				this.OnActionUpdate();
			}
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009C3C RID: 39996 RVA: 0x00324CC6 File Offset: 0x00322EC6
		public override void DoAnimatorMove()
		{
			if (this.everyFrameOption == FsmStateActionAnimatorBase.AnimatorFrameUpdateSelector.OnAnimatorMove)
			{
				this.OnActionUpdate();
			}
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009C3D RID: 39997 RVA: 0x00324CE5 File Offset: 0x00322EE5
		public override void DoAnimatorIK(int layerIndex)
		{
			this.IklayerIndex = layerIndex;
			if (this.everyFrameOption == FsmStateActionAnimatorBase.AnimatorFrameUpdateSelector.OnAnimatorIK)
			{
				this.OnActionUpdate();
			}
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x040081BF RID: 33215
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		// Token: 0x040081C0 RID: 33216
		[Tooltip("Select when to perform the action, during OnUpdate, OnAnimatorMove, OnAnimatorIK")]
		public FsmStateActionAnimatorBase.AnimatorFrameUpdateSelector everyFrameOption;

		// Token: 0x040081C1 RID: 33217
		protected int IklayerIndex;

		// Token: 0x0200278E RID: 10126
		public enum AnimatorFrameUpdateSelector
		{
			// Token: 0x0400F497 RID: 62615
			OnUpdate,
			// Token: 0x0400F498 RID: 62616
			OnAnimatorMove,
			// Token: 0x0400F499 RID: 62617
			OnAnimatorIK
		}
	}
}
