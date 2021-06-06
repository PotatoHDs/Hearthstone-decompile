using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F70 RID: 3952
	[ActionCategory("Pegasus")]
	[Tooltip("Sets the alpha on a game object and its children.")]
	public class SetAlphaRecursiveAction : FsmStateAction
	{
		// Token: 0x0600AD44 RID: 44356 RVA: 0x00360731 File Offset: 0x0035E931
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_Alpha = 0f;
			this.m_EveryFrame = false;
			this.m_IncludeChildren = false;
		}

		// Token: 0x0600AD45 RID: 44357 RVA: 0x00360758 File Offset: 0x0035E958
		public override void OnEnter()
		{
			if (base.Fsm.GetOwnerDefaultTarget(this.m_GameObject) == null)
			{
				base.Finish();
				return;
			}
			this.UpdateAlpha();
			if (!this.m_EveryFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AD46 RID: 44358 RVA: 0x0036078E File Offset: 0x0035E98E
		public override void OnUpdate()
		{
			this.UpdateAlpha();
		}

		// Token: 0x0600AD47 RID: 44359 RVA: 0x00360798 File Offset: 0x0035E998
		private void UpdateAlpha()
		{
			if (this.m_Alpha.IsNone)
			{
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			RenderUtils.SetAlpha(ownerDefaultTarget, this.m_Alpha.Value);
		}

		// Token: 0x04009425 RID: 37925
		public FsmOwnerDefault m_GameObject;

		// Token: 0x04009426 RID: 37926
		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_Alpha;

		// Token: 0x04009427 RID: 37927
		public bool m_EveryFrame;

		// Token: 0x04009428 RID: 37928
		public bool m_IncludeChildren;
	}
}
