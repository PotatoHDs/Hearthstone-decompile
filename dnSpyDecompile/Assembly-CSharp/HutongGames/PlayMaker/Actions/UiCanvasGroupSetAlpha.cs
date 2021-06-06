using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E31 RID: 3633
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Set Group Alpha.")]
	public class UiCanvasGroupSetAlpha : ComponentAction<CanvasGroup>
	{
		// Token: 0x0600A7AE RID: 42926 RVA: 0x0034D463 File Offset: 0x0034B663
		public override void Reset()
		{
			this.gameObject = null;
			this.alpha = null;
			this.resetOnExit = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A7AF RID: 42927 RVA: 0x0034D484 File Offset: 0x0034B684
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.component = this.cachedComponent;
			}
			this.originalValue = this.component.alpha;
			this.DoSetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A7B0 RID: 42928 RVA: 0x0034D4DD File Offset: 0x0034B6DD
		public override void OnUpdate()
		{
			this.DoSetValue();
		}

		// Token: 0x0600A7B1 RID: 42929 RVA: 0x0034D4E5 File Offset: 0x0034B6E5
		private void DoSetValue()
		{
			if (this.component != null)
			{
				this.component.alpha = this.alpha.Value;
			}
		}

		// Token: 0x0600A7B2 RID: 42930 RVA: 0x0034D50B File Offset: 0x0034B70B
		public override void OnExit()
		{
			if (this.component == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.component.alpha = this.originalValue;
			}
		}

		// Token: 0x04008E38 RID: 36408
		[RequiredField]
		[CheckForComponent(typeof(CanvasGroup))]
		[Tooltip("The GameObject with a UI CanvasGroup component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008E39 RID: 36409
		[RequiredField]
		[Tooltip("The alpha of the UI component.")]
		public FsmFloat alpha;

		// Token: 0x04008E3A RID: 36410
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04008E3B RID: 36411
		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;

		// Token: 0x04008E3C RID: 36412
		private CanvasGroup component;

		// Token: 0x04008E3D RID: 36413
		private float originalValue;
	}
}
