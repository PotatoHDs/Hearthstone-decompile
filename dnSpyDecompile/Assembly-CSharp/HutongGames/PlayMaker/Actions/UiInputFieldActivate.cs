using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E69 RID: 3689
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Activate a UI InputField component to begin processing Events. Optionally Deactivate on state exit")]
	public class UiInputFieldActivate : ComponentAction<InputField>
	{
		// Token: 0x0600A8AA RID: 43178 RVA: 0x00350B90 File Offset: 0x0034ED90
		public override void Reset()
		{
			this.gameObject = null;
			this.deactivateOnExit = null;
		}

		// Token: 0x0600A8AB RID: 43179 RVA: 0x00350BA0 File Offset: 0x0034EDA0
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

		// Token: 0x0600A8AC RID: 43180 RVA: 0x00350BE0 File Offset: 0x0034EDE0
		private void DoAction()
		{
			if (this.inputField != null)
			{
				this.inputField.ActivateInputField();
			}
		}

		// Token: 0x0600A8AD RID: 43181 RVA: 0x00350BFB File Offset: 0x0034EDFB
		public override void OnExit()
		{
			if (this.inputField == null)
			{
				return;
			}
			if (this.deactivateOnExit.Value)
			{
				this.inputField.DeactivateInputField();
			}
		}

		// Token: 0x04008F4F RID: 36687
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F50 RID: 36688
		[Tooltip("Reset when exiting this state.")]
		public FsmBool deactivateOnExit;

		// Token: 0x04008F51 RID: 36689
		private InputField inputField;
	}
}
