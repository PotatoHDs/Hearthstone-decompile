using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E7A RID: 3706
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the Asterix Character of a UI InputField component.")]
	public class UiInputFieldSetAsterixChar : ComponentAction<InputField>
	{
		// Token: 0x0600A8F9 RID: 43257 RVA: 0x003517A9 File Offset: 0x0034F9A9
		public override void Reset()
		{
			this.gameObject = null;
			this.asterixChar = "*";
			this.resetOnExit = null;
		}

		// Token: 0x0600A8FA RID: 43258 RVA: 0x003517CC File Offset: 0x0034F9CC
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.inputField = this.cachedComponent;
			}
			this.originalValue = this.inputField.asteriskChar;
			this.DoSetValue();
			base.Finish();
		}

		// Token: 0x0600A8FB RID: 43259 RVA: 0x00351820 File Offset: 0x0034FA20
		private void DoSetValue()
		{
			char asteriskChar = UiInputFieldSetAsterixChar.__char__;
			if (this.asterixChar.Value.Length > 0)
			{
				asteriskChar = this.asterixChar.Value[0];
			}
			if (this.inputField != null)
			{
				this.inputField.asteriskChar = asteriskChar;
			}
		}

		// Token: 0x0600A8FC RID: 43260 RVA: 0x00351872 File Offset: 0x0034FA72
		public override void OnExit()
		{
			if (this.inputField == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.inputField.asteriskChar = this.originalValue;
			}
		}

		// Token: 0x04008FA4 RID: 36772
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008FA5 RID: 36773
		[RequiredField]
		[Tooltip("The asterix Character used for password field type of the UI InputField component. Only the first character will be used, the rest of the string will be ignored")]
		public FsmString asterixChar;

		// Token: 0x04008FA6 RID: 36774
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04008FA7 RID: 36775
		private InputField inputField;

		// Token: 0x04008FA8 RID: 36776
		private char originalValue;

		// Token: 0x04008FA9 RID: 36777
		private static char __char__ = ' ';
	}
}
