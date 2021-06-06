using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E6E RID: 3694
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the focused state of a UI InputField component.")]
	public class UiInputFieldGetIsFocused : ComponentAction<InputField>
	{
		// Token: 0x0600A8C2 RID: 43202 RVA: 0x00350EB0 File Offset: 0x0034F0B0
		public override void Reset()
		{
			this.isFocused = null;
			this.isfocusedEvent = null;
			this.isNotFocusedEvent = null;
		}

		// Token: 0x0600A8C3 RID: 43203 RVA: 0x00350EC8 File Offset: 0x0034F0C8
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

		// Token: 0x0600A8C4 RID: 43204 RVA: 0x00350F08 File Offset: 0x0034F108
		private void DoGetValue()
		{
			if (this.inputField == null)
			{
				return;
			}
			this.isFocused.Value = this.inputField.isFocused;
			base.Fsm.Event(this.inputField.isFocused ? this.isfocusedEvent : this.isNotFocusedEvent);
		}

		// Token: 0x04008F64 RID: 36708
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F65 RID: 36709
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the is focused flag value of the UI InputField component.")]
		public FsmBool isFocused;

		// Token: 0x04008F66 RID: 36710
		[Tooltip("Event sent if inputField is focused")]
		public FsmEvent isfocusedEvent;

		// Token: 0x04008F67 RID: 36711
		[Tooltip("Event sent if nputField is not focused")]
		public FsmEvent isNotFocusedEvent;

		// Token: 0x04008F68 RID: 36712
		private InputField inputField;
	}
}
