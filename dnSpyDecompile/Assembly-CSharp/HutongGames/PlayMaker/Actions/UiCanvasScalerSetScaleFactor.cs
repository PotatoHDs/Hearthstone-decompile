using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E34 RID: 3636
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the ScaleFactor of a CanvasScaler.")]
	public class UiCanvasScalerSetScaleFactor : ComponentAction<CanvasScaler>
	{
		// Token: 0x0600A7BF RID: 42943 RVA: 0x0034D832 File Offset: 0x0034BA32
		public override void Reset()
		{
			this.gameObject = null;
			this.scaleFactor = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A7C0 RID: 42944 RVA: 0x0034D84C File Offset: 0x0034BA4C
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.component = this.cachedComponent;
			}
			this.DoSetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A7C1 RID: 42945 RVA: 0x0034D894 File Offset: 0x0034BA94
		public override void OnUpdate()
		{
			this.DoSetValue();
		}

		// Token: 0x0600A7C2 RID: 42946 RVA: 0x0034D89C File Offset: 0x0034BA9C
		private void DoSetValue()
		{
			if (this.component != null)
			{
				this.component.scaleFactor = this.scaleFactor.Value;
			}
		}

		// Token: 0x04008E4E RID: 36430
		[RequiredField]
		[CheckForComponent(typeof(CanvasScaler))]
		[Tooltip("The GameObject with a UI CanvasScaler component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008E4F RID: 36431
		[RequiredField]
		[Tooltip("The scaleFactor of the UI CanvasScaler.")]
		public FsmFloat scaleFactor;

		// Token: 0x04008E50 RID: 36432
		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;

		// Token: 0x04008E51 RID: 36433
		private CanvasScaler component;
	}
}
