using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E53 RID: 3667
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the interactable flag of a UI Selectable component.")]
	public class UiGetIsInteractable : ComponentAction<Selectable>
	{
		// Token: 0x0600A843 RID: 43075 RVA: 0x0034F456 File Offset: 0x0034D656
		public override void Reset()
		{
			this.gameObject = null;
			this.isInteractable = null;
			this.isInteractableEvent = null;
			this.isNotInteractableEvent = null;
		}

		// Token: 0x0600A844 RID: 43076 RVA: 0x0034F474 File Offset: 0x0034D674
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.selectable = this.cachedComponent;
			}
			this.DoGetValue();
			base.Finish();
		}

		// Token: 0x0600A845 RID: 43077 RVA: 0x0034F4B4 File Offset: 0x0034D6B4
		private void DoGetValue()
		{
			if (this.selectable == null)
			{
				return;
			}
			bool flag = this.selectable.IsInteractable();
			this.isInteractable.Value = flag;
			if (flag)
			{
				base.Fsm.Event(this.isInteractableEvent);
				return;
			}
			base.Fsm.Event(this.isNotInteractableEvent);
		}

		// Token: 0x04008ECA RID: 36554
		[RequiredField]
		[CheckForComponent(typeof(Selectable))]
		[Tooltip("The GameObject with the UI Selectable component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008ECB RID: 36555
		[Tooltip("The Interactable value")]
		[UIHint(UIHint.Variable)]
		public FsmBool isInteractable;

		// Token: 0x04008ECC RID: 36556
		[Tooltip("Event sent if Component is Interactable")]
		public FsmEvent isInteractableEvent;

		// Token: 0x04008ECD RID: 36557
		[Tooltip("Event sent if Component is not Interactable")]
		public FsmEvent isNotInteractableEvent;

		// Token: 0x04008ECE RID: 36558
		private Selectable selectable;

		// Token: 0x04008ECF RID: 36559
		private bool originalState;
	}
}
