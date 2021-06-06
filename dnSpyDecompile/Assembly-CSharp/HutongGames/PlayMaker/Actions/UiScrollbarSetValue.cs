using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E89 RID: 3721
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the position value of a UI Scrollbar component. Ranges from 0.0 to 1.0.")]
	public class UiScrollbarSetValue : ComponentAction<UnityEngine.UI.Scrollbar>
	{
		// Token: 0x0600A94B RID: 43339 RVA: 0x0035241F File Offset: 0x0035061F
		public override void Reset()
		{
			this.gameObject = null;
			this.value = null;
			this.resetOnExit = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A94C RID: 43340 RVA: 0x00352440 File Offset: 0x00350640
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.scrollbar = this.cachedComponent;
			}
			this.originalValue = this.scrollbar.value;
			this.DoSetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A94D RID: 43341 RVA: 0x00352499 File Offset: 0x00350699
		public override void OnUpdate()
		{
			this.DoSetValue();
		}

		// Token: 0x0600A94E RID: 43342 RVA: 0x003524A1 File Offset: 0x003506A1
		private void DoSetValue()
		{
			if (this.scrollbar != null)
			{
				this.scrollbar.value = this.value.Value;
			}
		}

		// Token: 0x0600A94F RID: 43343 RVA: 0x003524C7 File Offset: 0x003506C7
		public override void OnExit()
		{
			if (this.scrollbar == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.scrollbar.value = this.originalValue;
			}
		}

		// Token: 0x04008FF4 RID: 36852
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Scrollbar))]
		[Tooltip("The GameObject with the UI Scrollbar component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008FF5 RID: 36853
		[RequiredField]
		[Tooltip("The position's value of the UI Scrollbar component. Ranges from 0.0 to 1.0.")]
		[HasFloatSlider(0f, 1f)]
		public FsmFloat value;

		// Token: 0x04008FF6 RID: 36854
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04008FF7 RID: 36855
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x04008FF8 RID: 36856
		private UnityEngine.UI.Scrollbar scrollbar;

		// Token: 0x04008FF9 RID: 36857
		private float originalValue;
	}
}
