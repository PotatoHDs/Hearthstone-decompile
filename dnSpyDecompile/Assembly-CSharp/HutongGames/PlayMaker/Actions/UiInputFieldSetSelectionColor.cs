using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E7F RID: 3711
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the selection color of a UI InputField component. This is the color of the highlighter to show what characters are selected.")]
	public class UiInputFieldSetSelectionColor : ComponentAction<InputField>
	{
		// Token: 0x0600A915 RID: 43285 RVA: 0x00351C01 File Offset: 0x0034FE01
		public override void Reset()
		{
			this.gameObject = null;
			this.selectionColor = null;
			this.resetOnExit = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A916 RID: 43286 RVA: 0x00351C20 File Offset: 0x0034FE20
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.inputField = this.cachedComponent;
			}
			this.originalValue = this.inputField.selectionColor;
			this.DoSetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A917 RID: 43287 RVA: 0x00351C79 File Offset: 0x0034FE79
		public override void OnUpdate()
		{
			this.DoSetValue();
		}

		// Token: 0x0600A918 RID: 43288 RVA: 0x00351C81 File Offset: 0x0034FE81
		private void DoSetValue()
		{
			if (this.inputField != null)
			{
				this.inputField.selectionColor = this.selectionColor.Value;
			}
		}

		// Token: 0x0600A919 RID: 43289 RVA: 0x00351CA7 File Offset: 0x0034FEA7
		public override void OnExit()
		{
			if (this.inputField == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.inputField.selectionColor = this.originalValue;
			}
		}

		// Token: 0x04008FC0 RID: 36800
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008FC1 RID: 36801
		[RequiredField]
		[Tooltip("The color of the highlighter to show what characters are selected for the UI InputField component.")]
		public FsmColor selectionColor;

		// Token: 0x04008FC2 RID: 36802
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04008FC3 RID: 36803
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x04008FC4 RID: 36804
		private InputField inputField;

		// Token: 0x04008FC5 RID: 36805
		private Color originalValue;
	}
}
