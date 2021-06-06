using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E2F RID: 3631
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Enable or disable Canvas Raycasting. Optionally reset on state exit")]
	public class UiCanvasEnableRaycast : ComponentAction<PlayMakerCanvasRaycastFilterProxy>
	{
		// Token: 0x0600A7A5 RID: 42917 RVA: 0x0034D32B File Offset: 0x0034B52B
		public override void Reset()
		{
			this.gameObject = null;
			this.enableRaycasting = false;
			this.resetOnExit = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A7A6 RID: 42918 RVA: 0x0034D350 File Offset: 0x0034B550
		public override void OnPreprocess()
		{
			if (this.gameObject == null)
			{
				this.gameObject = new FsmOwnerDefault();
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCacheAddComponent(ownerDefaultTarget))
			{
				this.raycastFilterProxy = this.cachedComponent;
			}
		}

		// Token: 0x0600A7A7 RID: 42919 RVA: 0x0034D398 File Offset: 0x0034B598
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCacheAddComponent(ownerDefaultTarget))
			{
				this.raycastFilterProxy = this.cachedComponent;
				this.originalValue = this.raycastFilterProxy.RayCastingEnabled;
			}
			this.DoAction();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A7A8 RID: 42920 RVA: 0x0034D3F1 File Offset: 0x0034B5F1
		public override void OnUpdate()
		{
			this.DoAction();
		}

		// Token: 0x0600A7A9 RID: 42921 RVA: 0x0034D3F9 File Offset: 0x0034B5F9
		private void DoAction()
		{
			if (this.raycastFilterProxy != null)
			{
				this.raycastFilterProxy.RayCastingEnabled = this.enableRaycasting.Value;
			}
		}

		// Token: 0x0600A7AA RID: 42922 RVA: 0x0034D41F File Offset: 0x0034B61F
		public override void OnExit()
		{
			if (this.raycastFilterProxy == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.raycastFilterProxy.RayCastingEnabled = this.originalValue;
			}
		}

		// Token: 0x04008E32 RID: 36402
		[RequiredField]
		[Tooltip("The GameObject to enable or disable Canvas Raycasting on.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008E33 RID: 36403
		public FsmBool enableRaycasting;

		// Token: 0x04008E34 RID: 36404
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04008E35 RID: 36405
		public bool everyFrame;

		// Token: 0x04008E36 RID: 36406
		[SerializeField]
		private PlayMakerCanvasRaycastFilterProxy raycastFilterProxy;

		// Token: 0x04008E37 RID: 36407
		private bool originalValue;
	}
}
