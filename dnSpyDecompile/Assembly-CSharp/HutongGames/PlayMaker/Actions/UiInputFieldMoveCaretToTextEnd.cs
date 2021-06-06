using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E75 RID: 3701
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Move Caret to text end in a UI InputField component. Optionally select from the current caret position")]
	public class UiInputFieldMoveCaretToTextEnd : ComponentAction<InputField>
	{
		// Token: 0x0600A8E2 RID: 43234 RVA: 0x003513D8 File Offset: 0x0034F5D8
		public override void Reset()
		{
			this.gameObject = null;
			this.shift = true;
		}

		// Token: 0x0600A8E3 RID: 43235 RVA: 0x003513F0 File Offset: 0x0034F5F0
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

		// Token: 0x0600A8E4 RID: 43236 RVA: 0x00351430 File Offset: 0x0034F630
		private void DoAction()
		{
			if (this.inputField != null)
			{
				this.inputField.MoveTextEnd(this.shift.Value);
			}
		}

		// Token: 0x04008F8E RID: 36750
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F8F RID: 36751
		[Tooltip("Define if we select or not from the current caret position. Default is true = no selection")]
		public FsmBool shift;

		// Token: 0x04008F90 RID: 36752
		private InputField inputField;
	}
}
