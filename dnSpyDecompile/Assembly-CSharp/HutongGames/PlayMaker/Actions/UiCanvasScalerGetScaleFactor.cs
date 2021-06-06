using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E33 RID: 3635
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Get the ScaleFactor of a CanvasScaler.")]
	public class UiCanvasScalerGetScaleFactor : ComponentAction<CanvasScaler>
	{
		// Token: 0x0600A7BA RID: 42938 RVA: 0x0034D79D File Offset: 0x0034B99D
		public override void Reset()
		{
			this.gameObject = null;
			this.scaleFactor = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A7BB RID: 42939 RVA: 0x0034D7B4 File Offset: 0x0034B9B4
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.component = this.cachedComponent;
			}
			this.DoGetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A7BC RID: 42940 RVA: 0x0034D7FC File Offset: 0x0034B9FC
		public override void OnUpdate()
		{
			this.DoGetValue();
		}

		// Token: 0x0600A7BD RID: 42941 RVA: 0x0034D804 File Offset: 0x0034BA04
		private void DoGetValue()
		{
			if (this.component != null)
			{
				this.scaleFactor.Value = this.component.scaleFactor;
			}
		}

		// Token: 0x04008E4A RID: 36426
		[RequiredField]
		[CheckForComponent(typeof(CanvasScaler))]
		[Tooltip("The GameObject with a UI CanvasScaler component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008E4B RID: 36427
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The scaleFactor of the CanvasScaler component.")]
		public FsmFloat scaleFactor;

		// Token: 0x04008E4C RID: 36428
		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;

		// Token: 0x04008E4D RID: 36429
		private CanvasScaler component;
	}
}
