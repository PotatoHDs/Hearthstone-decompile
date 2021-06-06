using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E58 RID: 3672
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the interactable flag of a UI Selectable component.")]
	public class UiSetIsInteractable : FsmStateAction
	{
		// Token: 0x0600A85B RID: 43099 RVA: 0x0034FBBB File Offset: 0x0034DDBB
		public override void Reset()
		{
			this.gameObject = null;
			this.isInteractable = null;
			this.resetOnExit = false;
		}

		// Token: 0x0600A85C RID: 43100 RVA: 0x0034FBD8 File Offset: 0x0034DDD8
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				this._selectable = ownerDefaultTarget.GetComponent<Selectable>();
			}
			if (this._selectable != null && this.resetOnExit.Value)
			{
				this._originalState = this._selectable.IsInteractable();
			}
			this.DoSetValue();
			base.Finish();
		}

		// Token: 0x0600A85D RID: 43101 RVA: 0x0034FC44 File Offset: 0x0034DE44
		private void DoSetValue()
		{
			if (this._selectable != null)
			{
				this._selectable.interactable = this.isInteractable.Value;
			}
		}

		// Token: 0x0600A85E RID: 43102 RVA: 0x0034FC6A File Offset: 0x0034DE6A
		public override void OnExit()
		{
			if (this._selectable == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this._selectable.interactable = this._originalState;
			}
		}

		// Token: 0x04008EF4 RID: 36596
		[RequiredField]
		[CheckForComponent(typeof(Selectable))]
		[Tooltip("The GameObject with the UI Selectable component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008EF5 RID: 36597
		[Tooltip("The Interactable value")]
		public FsmBool isInteractable;

		// Token: 0x04008EF6 RID: 36598
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04008EF7 RID: 36599
		private Selectable _selectable;

		// Token: 0x04008EF8 RID: 36600
		private bool _originalState;
	}
}
