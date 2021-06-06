using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E80 RID: 3712
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the text value of a UI InputField component.")]
	public class UiInputFieldSetText : ComponentAction<InputField>
	{
		// Token: 0x0600A91B RID: 43291 RVA: 0x00351CD6 File Offset: 0x0034FED6
		public override void Reset()
		{
			this.gameObject = null;
			this.text = null;
			this.resetOnExit = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A91C RID: 43292 RVA: 0x00351CF4 File Offset: 0x0034FEF4
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.inputField = this.cachedComponent;
			}
			this.originalString = this.inputField.text;
			this.DoSetTextValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A91D RID: 43293 RVA: 0x00351D4D File Offset: 0x0034FF4D
		public override void OnUpdate()
		{
			this.DoSetTextValue();
		}

		// Token: 0x0600A91E RID: 43294 RVA: 0x00351D55 File Offset: 0x0034FF55
		private void DoSetTextValue()
		{
			if (this.inputField != null)
			{
				this.inputField.text = this.text.Value;
			}
		}

		// Token: 0x0600A91F RID: 43295 RVA: 0x00351D7B File Offset: 0x0034FF7B
		public override void OnExit()
		{
			if (this.inputField == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.inputField.text = this.originalString;
			}
		}

		// Token: 0x04008FC6 RID: 36806
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008FC7 RID: 36807
		[UIHint(UIHint.TextArea)]
		[Tooltip("The text of the UI InputField component.")]
		public FsmString text;

		// Token: 0x04008FC8 RID: 36808
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04008FC9 RID: 36809
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x04008FCA RID: 36810
		private InputField inputField;

		// Token: 0x04008FCB RID: 36811
		private string originalString;
	}
}
