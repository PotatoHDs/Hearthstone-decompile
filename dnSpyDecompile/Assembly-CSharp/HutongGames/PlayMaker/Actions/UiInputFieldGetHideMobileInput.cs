using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E6D RID: 3693
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the Hide Mobile Input value of a UI InputField component.")]
	public class UiInputFieldGetHideMobileInput : ComponentAction<InputField>
	{
		// Token: 0x0600A8BE RID: 43198 RVA: 0x00350E01 File Offset: 0x0034F001
		public override void Reset()
		{
			this.hideMobileInput = null;
			this.mobileInputHiddenEvent = null;
			this.mobileInputShownEvent = null;
		}

		// Token: 0x0600A8BF RID: 43199 RVA: 0x00350E18 File Offset: 0x0034F018
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

		// Token: 0x0600A8C0 RID: 43200 RVA: 0x00350E58 File Offset: 0x0034F058
		private void DoGetValue()
		{
			if (this.inputField == null)
			{
				return;
			}
			this.hideMobileInput.Value = this.inputField.shouldHideMobileInput;
			base.Fsm.Event(this.inputField.shouldHideMobileInput ? this.mobileInputHiddenEvent : this.mobileInputShownEvent);
		}

		// Token: 0x04008F5F RID: 36703
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F60 RID: 36704
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the Hide Mobile flag value of the UI InputField component.")]
		public FsmBool hideMobileInput;

		// Token: 0x04008F61 RID: 36705
		[Tooltip("Event sent if hide mobile input property is true")]
		public FsmEvent mobileInputHiddenEvent;

		// Token: 0x04008F62 RID: 36706
		[Tooltip("Event sent if hide mobile input property is false")]
		public FsmEvent mobileInputShownEvent;

		// Token: 0x04008F63 RID: 36707
		private InputField inputField;
	}
}
