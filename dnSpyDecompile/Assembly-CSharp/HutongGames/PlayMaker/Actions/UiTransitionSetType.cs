using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E5A RID: 3674
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the transition type of a UI Selectable component.")]
	public class UiTransitionSetType : ComponentAction<Selectable>
	{
		// Token: 0x0600A864 RID: 43108 RVA: 0x0034FDC6 File Offset: 0x0034DFC6
		public override void Reset()
		{
			this.gameObject = null;
			this.transition = Selectable.Transition.ColorTint;
			this.resetOnExit = false;
		}

		// Token: 0x0600A865 RID: 43109 RVA: 0x0034FDE4 File Offset: 0x0034DFE4
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.selectable = this.cachedComponent;
			}
			if (this.selectable != null && this.resetOnExit.Value)
			{
				this.originalTransition = this.selectable.transition;
			}
			this.DoSetValue();
			base.Finish();
		}

		// Token: 0x0600A866 RID: 43110 RVA: 0x0034FE50 File Offset: 0x0034E050
		private void DoSetValue()
		{
			if (this.selectable != null)
			{
				this.selectable.transition = this.transition;
			}
		}

		// Token: 0x0600A867 RID: 43111 RVA: 0x0034FE71 File Offset: 0x0034E071
		public override void OnExit()
		{
			if (this.selectable == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.selectable.transition = this.originalTransition;
			}
		}

		// Token: 0x04008F01 RID: 36609
		[RequiredField]
		[CheckForComponent(typeof(Selectable))]
		[Tooltip("The GameObject with the UI Selectable component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F02 RID: 36610
		[Tooltip("The transition value")]
		public Selectable.Transition transition;

		// Token: 0x04008F03 RID: 36611
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04008F04 RID: 36612
		private Selectable selectable;

		// Token: 0x04008F05 RID: 36613
		private Selectable.Transition originalTransition;
	}
}
