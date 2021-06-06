using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E70 RID: 3696
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the selection color of a UI InputField component. This is the color of the highlighter to show what characters are selected")]
	public class UiInputFieldGetSelectionColor : ComponentAction<InputField>
	{
		// Token: 0x0600A8CA RID: 43210 RVA: 0x00351037 File Offset: 0x0034F237
		public override void Reset()
		{
			this.selectionColor = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A8CB RID: 43211 RVA: 0x00351048 File Offset: 0x0034F248
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.inputField = this.cachedComponent;
			}
			this.DoGetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A8CC RID: 43212 RVA: 0x00351090 File Offset: 0x0034F290
		public override void OnUpdate()
		{
			this.DoGetValue();
		}

		// Token: 0x0600A8CD RID: 43213 RVA: 0x00351098 File Offset: 0x0034F298
		private void DoGetValue()
		{
			if (this.inputField != null)
			{
				this.selectionColor.Value = this.inputField.selectionColor;
			}
		}

		// Token: 0x04008F6F RID: 36719
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F70 RID: 36720
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("This is the color of the highlighter to show what characters are selected of the UI InputField component.")]
		public FsmColor selectionColor;

		// Token: 0x04008F71 RID: 36721
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x04008F72 RID: 36722
		private InputField inputField;
	}
}
