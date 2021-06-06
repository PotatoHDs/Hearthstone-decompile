using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E71 RID: 3697
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the text value of a UI InputField component.")]
	public class UiInputFieldGetText : ComponentAction<InputField>
	{
		// Token: 0x0600A8CF RID: 43215 RVA: 0x003510BE File Offset: 0x0034F2BE
		public override void Reset()
		{
			this.text = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A8D0 RID: 43216 RVA: 0x003510D0 File Offset: 0x0034F2D0
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.inputField = this.cachedComponent;
			}
			this.DoGetTextValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A8D1 RID: 43217 RVA: 0x00351118 File Offset: 0x0034F318
		public override void OnUpdate()
		{
			this.DoGetTextValue();
		}

		// Token: 0x0600A8D2 RID: 43218 RVA: 0x00351120 File Offset: 0x0034F320
		private void DoGetTextValue()
		{
			if (this.inputField != null)
			{
				this.text.Value = this.inputField.text;
			}
		}

		// Token: 0x04008F73 RID: 36723
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F74 RID: 36724
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The text value of the UI InputField component.")]
		public FsmString text;

		// Token: 0x04008F75 RID: 36725
		public bool everyFrame;

		// Token: 0x04008F76 RID: 36726
		private InputField inputField;
	}
}
