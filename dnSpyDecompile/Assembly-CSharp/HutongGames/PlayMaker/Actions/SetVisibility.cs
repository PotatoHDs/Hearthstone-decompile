using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E02 RID: 3586
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Sets the visibility of a GameObject. Note: this action sets the GameObject Renderer's enabled state.")]
	public class SetVisibility : ComponentAction<Renderer>
	{
		// Token: 0x0600A6D5 RID: 42709 RVA: 0x0034A6D7 File Offset: 0x003488D7
		public override void Reset()
		{
			this.gameObject = null;
			this.toggle = false;
			this.visible = false;
			this.resetOnExit = true;
			this.initialVisibility = false;
		}

		// Token: 0x0600A6D6 RID: 42710 RVA: 0x0034A706 File Offset: 0x00348906
		public override void OnEnter()
		{
			this.DoSetVisibility(base.Fsm.GetOwnerDefaultTarget(this.gameObject));
			base.Finish();
		}

		// Token: 0x0600A6D7 RID: 42711 RVA: 0x0034A728 File Offset: 0x00348928
		private void DoSetVisibility(GameObject go)
		{
			if (!base.UpdateCache(go))
			{
				return;
			}
			this.initialVisibility = base.renderer.enabled;
			if (!this.toggle.Value)
			{
				base.renderer.enabled = this.visible.Value;
				return;
			}
			base.renderer.enabled = !base.renderer.enabled;
		}

		// Token: 0x0600A6D8 RID: 42712 RVA: 0x0034A78D File Offset: 0x0034898D
		public override void OnExit()
		{
			if (this.resetOnExit)
			{
				this.ResetVisibility();
			}
		}

		// Token: 0x0600A6D9 RID: 42713 RVA: 0x0034A79D File Offset: 0x0034899D
		private void ResetVisibility()
		{
			if (base.renderer != null)
			{
				base.renderer.enabled = this.initialVisibility;
			}
		}

		// Token: 0x04008D53 RID: 36179
		[RequiredField]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008D54 RID: 36180
		[Tooltip("Should the object visibility be toggled?\nHas priority over the 'visible' setting")]
		public FsmBool toggle;

		// Token: 0x04008D55 RID: 36181
		[Tooltip("Should the object be set to visible or invisible?")]
		public FsmBool visible;

		// Token: 0x04008D56 RID: 36182
		[Tooltip("Resets to the initial visibility when it leaves the state")]
		public bool resetOnExit;

		// Token: 0x04008D57 RID: 36183
		private bool initialVisibility;
	}
}
