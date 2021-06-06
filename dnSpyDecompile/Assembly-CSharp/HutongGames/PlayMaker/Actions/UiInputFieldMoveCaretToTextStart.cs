using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E76 RID: 3702
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Move Caret to text start in a UI InputField component. Optionally select from the current caret position")]
	public class UiInputFieldMoveCaretToTextStart : ComponentAction<InputField>
	{
		// Token: 0x0600A8E6 RID: 43238 RVA: 0x00351456 File Offset: 0x0034F656
		public override void Reset()
		{
			this.gameObject = null;
			this.shift = true;
		}

		// Token: 0x0600A8E7 RID: 43239 RVA: 0x0035146C File Offset: 0x0034F66C
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

		// Token: 0x0600A8E8 RID: 43240 RVA: 0x003514AC File Offset: 0x0034F6AC
		private void DoAction()
		{
			if (this.inputField != null)
			{
				this.inputField.MoveTextStart(this.shift.Value);
			}
		}

		// Token: 0x04008F91 RID: 36753
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F92 RID: 36754
		[Tooltip("Define if we select or not from the current caret position. Default is true = no selection")]
		public FsmBool shift;

		// Token: 0x04008F93 RID: 36755
		private InputField inputField;
	}
}
