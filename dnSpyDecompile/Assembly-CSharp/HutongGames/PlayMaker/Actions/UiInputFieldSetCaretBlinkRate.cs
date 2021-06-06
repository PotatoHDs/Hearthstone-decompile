using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E7B RID: 3707
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the caret's blink rate of a UI InputField component.")]
	public class UiInputFieldSetCaretBlinkRate : ComponentAction<InputField>
	{
		// Token: 0x0600A8FF RID: 43263 RVA: 0x003518AA File Offset: 0x0034FAAA
		public override void Reset()
		{
			this.gameObject = null;
			this.caretBlinkRate = null;
			this.resetOnExit = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A900 RID: 43264 RVA: 0x003518C8 File Offset: 0x0034FAC8
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.inputField = this.cachedComponent;
			}
			this.originalValue = this.inputField.caretBlinkRate;
			this.DoSetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A901 RID: 43265 RVA: 0x00351921 File Offset: 0x0034FB21
		public override void OnUpdate()
		{
			this.DoSetValue();
		}

		// Token: 0x0600A902 RID: 43266 RVA: 0x00351929 File Offset: 0x0034FB29
		private void DoSetValue()
		{
			if (this.inputField != null)
			{
				this.inputField.caretBlinkRate = (float)this.caretBlinkRate.Value;
			}
		}

		// Token: 0x0600A903 RID: 43267 RVA: 0x00351950 File Offset: 0x0034FB50
		public override void OnExit()
		{
			if (this.inputField == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.inputField.caretBlinkRate = this.originalValue;
			}
		}

		// Token: 0x04008FAA RID: 36778
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008FAB RID: 36779
		[RequiredField]
		[Tooltip("The caret's blink rate for the UI InputField component.")]
		public FsmInt caretBlinkRate;

		// Token: 0x04008FAC RID: 36780
		[Tooltip("Deactivate when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04008FAD RID: 36781
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x04008FAE RID: 36782
		private InputField inputField;

		// Token: 0x04008FAF RID: 36783
		private float originalValue;
	}
}
