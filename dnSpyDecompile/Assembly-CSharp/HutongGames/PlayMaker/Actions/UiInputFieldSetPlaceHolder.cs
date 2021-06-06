using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E7E RID: 3710
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the placeholder of a UI InputField component. Optionally reset on exit")]
	public class UiInputFieldSetPlaceHolder : ComponentAction<InputField>
	{
		// Token: 0x0600A910 RID: 43280 RVA: 0x00351B16 File Offset: 0x0034FD16
		public override void Reset()
		{
			this.gameObject = null;
			this.placeholder = null;
			this.resetOnExit = null;
		}

		// Token: 0x0600A911 RID: 43281 RVA: 0x00351B30 File Offset: 0x0034FD30
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.inputField = this.cachedComponent;
			}
			this.originalValue = this.inputField.placeholder;
			this.DoSetValue();
			base.Finish();
		}

		// Token: 0x0600A912 RID: 43282 RVA: 0x00351B84 File Offset: 0x0034FD84
		private void DoSetValue()
		{
			if (this.inputField != null)
			{
				GameObject value = this.placeholder.Value;
				if (value == null)
				{
					this.inputField.placeholder = null;
					return;
				}
				this.inputField.placeholder = value.GetComponent<Graphic>();
			}
		}

		// Token: 0x0600A913 RID: 43283 RVA: 0x00351BD2 File Offset: 0x0034FDD2
		public override void OnExit()
		{
			if (this.inputField == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.inputField.placeholder = this.originalValue;
			}
		}

		// Token: 0x04008FBB RID: 36795
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008FBC RID: 36796
		[RequiredField]
		[CheckForComponent(typeof(Graphic))]
		[Tooltip("The placeholder (any graphic UI Component) for the UI InputField component.")]
		public FsmGameObject placeholder;

		// Token: 0x04008FBD RID: 36797
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04008FBE RID: 36798
		private InputField inputField;

		// Token: 0x04008FBF RID: 36799
		private Graphic originalValue;
	}
}
