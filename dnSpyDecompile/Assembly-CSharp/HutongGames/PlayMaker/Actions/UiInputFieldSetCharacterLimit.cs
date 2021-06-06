using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E7C RID: 3708
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the maximum number of characters that the user can type into a UI InputField component. Optionally reset on exit")]
	public class UiInputFieldSetCharacterLimit : ComponentAction<InputField>
	{
		// Token: 0x0600A905 RID: 43269 RVA: 0x0035197F File Offset: 0x0034FB7F
		public override void Reset()
		{
			this.gameObject = null;
			this.characterLimit = null;
			this.resetOnExit = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A906 RID: 43270 RVA: 0x003519A0 File Offset: 0x0034FBA0
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.inputField = this.cachedComponent;
			}
			this.originalValue = this.inputField.characterLimit;
			this.DoSetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A907 RID: 43271 RVA: 0x003519F9 File Offset: 0x0034FBF9
		public override void OnUpdate()
		{
			this.DoSetValue();
		}

		// Token: 0x0600A908 RID: 43272 RVA: 0x00351A01 File Offset: 0x0034FC01
		private void DoSetValue()
		{
			if (this.inputField != null)
			{
				this.inputField.characterLimit = this.characterLimit.Value;
			}
		}

		// Token: 0x0600A909 RID: 43273 RVA: 0x00351A27 File Offset: 0x0034FC27
		public override void OnExit()
		{
			if (this.inputField == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.inputField.characterLimit = this.originalValue;
			}
		}

		// Token: 0x04008FB0 RID: 36784
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008FB1 RID: 36785
		[RequiredField]
		[Tooltip("The maximum number of characters that the user can type into the UI InputField component. 0 = infinite")]
		public FsmInt characterLimit;

		// Token: 0x04008FB2 RID: 36786
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04008FB3 RID: 36787
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x04008FB4 RID: 36788
		private InputField inputField;

		// Token: 0x04008FB5 RID: 36789
		private int originalValue;
	}
}
