using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E7D RID: 3709
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the Hide Mobile Input property of a UI InputField component.")]
	public class UiInputFieldSetHideMobileInput : ComponentAction<InputField>
	{
		// Token: 0x0600A90B RID: 43275 RVA: 0x00351A56 File Offset: 0x0034FC56
		public override void Reset()
		{
			this.gameObject = null;
			this.hideMobileInput = null;
			this.resetOnExit = null;
		}

		// Token: 0x0600A90C RID: 43276 RVA: 0x00351A70 File Offset: 0x0034FC70
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.inputField = this.cachedComponent;
			}
			this.originalValue = this.inputField.shouldHideMobileInput;
			this.DoSetValue();
			base.Finish();
		}

		// Token: 0x0600A90D RID: 43277 RVA: 0x00351AC1 File Offset: 0x0034FCC1
		private void DoSetValue()
		{
			if (this.inputField != null)
			{
				this.inputField.shouldHideMobileInput = this.hideMobileInput.Value;
			}
		}

		// Token: 0x0600A90E RID: 43278 RVA: 0x00351AE7 File Offset: 0x0034FCE7
		public override void OnExit()
		{
			if (this.inputField == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.inputField.shouldHideMobileInput = this.originalValue;
			}
		}

		// Token: 0x04008FB6 RID: 36790
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008FB7 RID: 36791
		[RequiredField]
		[UIHint(UIHint.TextArea)]
		[Tooltip("The Hide Mobile Input flag value of the UI InputField component.")]
		public FsmBool hideMobileInput;

		// Token: 0x04008FB8 RID: 36792
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04008FB9 RID: 36793
		private InputField inputField;

		// Token: 0x04008FBA RID: 36794
		private bool originalValue;
	}
}
