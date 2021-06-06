using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E6F RID: 3695
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the placeHolder GameObject of a UI InputField component.")]
	public class UiInputFieldGetPlaceHolder : ComponentAction<InputField>
	{
		// Token: 0x0600A8C6 RID: 43206 RVA: 0x00350F60 File Offset: 0x0034F160
		public override void Reset()
		{
			this.placeHolder = null;
			this.placeHolderDefined = null;
			this.foundEvent = null;
			this.notFoundEvent = null;
		}

		// Token: 0x0600A8C7 RID: 43207 RVA: 0x00350F80 File Offset: 0x0034F180
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.inputField = this.cachedComponent;
			}
			this.DoGetValue();
			base.Finish();
		}

		// Token: 0x0600A8C8 RID: 43208 RVA: 0x00350FC0 File Offset: 0x0034F1C0
		private void DoGetValue()
		{
			if (this.inputField == null)
			{
				return;
			}
			Graphic placeholder = this.inputField.placeholder;
			this.placeHolderDefined.Value = (placeholder != null);
			if (placeholder != null)
			{
				this.placeHolder.Value = placeholder.gameObject;
				base.Fsm.Event(this.foundEvent);
				return;
			}
			base.Fsm.Event(this.notFoundEvent);
		}

		// Token: 0x04008F69 RID: 36713
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F6A RID: 36714
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the placeholder for the UI InputField component.")]
		public FsmGameObject placeHolder;

		// Token: 0x04008F6B RID: 36715
		[Tooltip("true if placeholder is found")]
		public FsmBool placeHolderDefined;

		// Token: 0x04008F6C RID: 36716
		[Tooltip("Event sent if no placeholder is defined")]
		public FsmEvent foundEvent;

		// Token: 0x04008F6D RID: 36717
		[Tooltip("Event sent if a placeholder is defined")]
		public FsmEvent notFoundEvent;

		// Token: 0x04008F6E RID: 36718
		private InputField inputField;
	}
}
