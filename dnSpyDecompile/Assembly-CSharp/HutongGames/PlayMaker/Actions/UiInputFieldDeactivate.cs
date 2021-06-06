using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E6A RID: 3690
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Deactivate a UI InputField to stop the processing of Events and send OnSubmit if not canceled. Optionally Activate on state exit")]
	public class UiInputFieldDeactivate : ComponentAction<InputField>
	{
		// Token: 0x0600A8AF RID: 43183 RVA: 0x00350C2C File Offset: 0x0034EE2C
		public override void Reset()
		{
			this.gameObject = null;
			this.activateOnExit = null;
		}

		// Token: 0x0600A8B0 RID: 43184 RVA: 0x00350C3C File Offset: 0x0034EE3C
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.inputField = this.cachedComponent;
			}
			this.DoAction();
			base.Finish();
		}

		// Token: 0x0600A8B1 RID: 43185 RVA: 0x00350C7C File Offset: 0x0034EE7C
		private void DoAction()
		{
			if (this.inputField != null)
			{
				this.inputField.DeactivateInputField();
			}
		}

		// Token: 0x0600A8B2 RID: 43186 RVA: 0x00350C97 File Offset: 0x0034EE97
		public override void OnExit()
		{
			if (this.inputField == null)
			{
				return;
			}
			if (this.activateOnExit.Value)
			{
				this.inputField.ActivateInputField();
			}
		}

		// Token: 0x04008F52 RID: 36690
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F53 RID: 36691
		[Tooltip("Activate when exiting this state.")]
		public FsmBool activateOnExit;

		// Token: 0x04008F54 RID: 36692
		private InputField inputField;
	}
}
