using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E88 RID: 3720
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the fractional size of the handle of a UI Scrollbar component. Ranges from 0.0 to 1.0.")]
	public class UiScrollbarSetSize : ComponentAction<UnityEngine.UI.Scrollbar>
	{
		// Token: 0x0600A945 RID: 43333 RVA: 0x0035233E File Offset: 0x0035053E
		public override void Reset()
		{
			this.gameObject = null;
			this.value = null;
			this.resetOnExit = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A946 RID: 43334 RVA: 0x0035235C File Offset: 0x0035055C
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.scrollbar = this.cachedComponent;
			}
			if (this.resetOnExit.Value)
			{
				this.originalValue = this.scrollbar.size;
			}
			this.DoSetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A947 RID: 43335 RVA: 0x003523C2 File Offset: 0x003505C2
		public override void OnUpdate()
		{
			this.DoSetValue();
		}

		// Token: 0x0600A948 RID: 43336 RVA: 0x003523CA File Offset: 0x003505CA
		private void DoSetValue()
		{
			if (this.scrollbar != null)
			{
				this.scrollbar.size = this.value.Value;
			}
		}

		// Token: 0x0600A949 RID: 43337 RVA: 0x003523F0 File Offset: 0x003505F0
		public override void OnExit()
		{
			if (this.scrollbar == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.scrollbar.size = this.originalValue;
			}
		}

		// Token: 0x04008FEE RID: 36846
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Scrollbar))]
		[Tooltip("The GameObject with the UI Scrollbar component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008FEF RID: 36847
		[RequiredField]
		[Tooltip("The fractional size of the handle for the UI Scrollbar. Ranges from 0.0 to 1.0.")]
		[HasFloatSlider(0f, 1f)]
		public FsmFloat value;

		// Token: 0x04008FF0 RID: 36848
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04008FF1 RID: 36849
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x04008FF2 RID: 36850
		private UnityEngine.UI.Scrollbar scrollbar;

		// Token: 0x04008FF3 RID: 36851
		private float originalValue;
	}
}
