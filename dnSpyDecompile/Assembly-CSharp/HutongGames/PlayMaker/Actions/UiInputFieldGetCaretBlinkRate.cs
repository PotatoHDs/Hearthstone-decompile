using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E6B RID: 3691
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the caret's blink rate of a UI InputField component.")]
	public class UiInputFieldGetCaretBlinkRate : ComponentAction<InputField>
	{
		// Token: 0x0600A8B4 RID: 43188 RVA: 0x00350CC0 File Offset: 0x0034EEC0
		public override void Reset()
		{
			this.caretBlinkRate = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A8B5 RID: 43189 RVA: 0x00350CD0 File Offset: 0x0034EED0
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

		// Token: 0x0600A8B6 RID: 43190 RVA: 0x00350D18 File Offset: 0x0034EF18
		public override void OnUpdate()
		{
			this.DoGetValue();
		}

		// Token: 0x0600A8B7 RID: 43191 RVA: 0x00350D20 File Offset: 0x0034EF20
		private void DoGetValue()
		{
			if (this.inputField != null)
			{
				this.caretBlinkRate.Value = this.inputField.caretBlinkRate;
			}
		}

		// Token: 0x04008F55 RID: 36693
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F56 RID: 36694
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The caret's blink rate for the UI InputField component.")]
		public FsmFloat caretBlinkRate;

		// Token: 0x04008F57 RID: 36695
		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;

		// Token: 0x04008F58 RID: 36696
		private InputField inputField;
	}
}
