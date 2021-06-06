using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E74 RID: 3700
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the cancel state of a UI InputField component. This relates to the last onEndEdit Event")]
	public class UiInputFieldGetWasCanceled : ComponentAction<InputField>
	{
		// Token: 0x0600A8DE RID: 43230 RVA: 0x00351327 File Offset: 0x0034F527
		public override void Reset()
		{
			this.wasCanceled = null;
			this.wasCanceledEvent = null;
			this.wasNotCanceledEvent = null;
		}

		// Token: 0x0600A8DF RID: 43231 RVA: 0x00351340 File Offset: 0x0034F540
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.inputField = this.cachedComponent;
			}
			this.DoGetValue();
			base.Finish();
		}

		// Token: 0x0600A8E0 RID: 43232 RVA: 0x00351380 File Offset: 0x0034F580
		private void DoGetValue()
		{
			if (this.inputField == null)
			{
				return;
			}
			this.wasCanceled.Value = this.inputField.wasCanceled;
			base.Fsm.Event(this.inputField.wasCanceled ? this.wasCanceledEvent : this.wasNotCanceledEvent);
		}

		// Token: 0x04008F89 RID: 36745
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F8A RID: 36746
		[UIHint(UIHint.Variable)]
		[Tooltip("The was canceled flag value of the UI InputField component.")]
		public FsmBool wasCanceled;

		// Token: 0x04008F8B RID: 36747
		[Tooltip("Event sent if inputField was canceled")]
		public FsmEvent wasCanceledEvent;

		// Token: 0x04008F8C RID: 36748
		[Tooltip("Event sent if inputField was not canceled")]
		public FsmEvent wasNotCanceledEvent;

		// Token: 0x04008F8D RID: 36749
		private InputField inputField;
	}
}
