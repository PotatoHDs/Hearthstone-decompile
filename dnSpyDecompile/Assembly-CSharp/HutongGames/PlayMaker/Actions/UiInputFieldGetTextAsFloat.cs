using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E72 RID: 3698
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the text value of a UI InputField component as a float.")]
	public class UiInputFieldGetTextAsFloat : ComponentAction<InputField>
	{
		// Token: 0x0600A8D4 RID: 43220 RVA: 0x00351146 File Offset: 0x0034F346
		public override void Reset()
		{
			this.value = null;
			this.isFloat = null;
			this.isFloatEvent = null;
			this.isNotFloatEvent = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A8D5 RID: 43221 RVA: 0x0035116C File Offset: 0x0034F36C
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

		// Token: 0x0600A8D6 RID: 43222 RVA: 0x003511B4 File Offset: 0x0034F3B4
		public override void OnUpdate()
		{
			this.DoGetTextValue();
		}

		// Token: 0x0600A8D7 RID: 43223 RVA: 0x003511BC File Offset: 0x0034F3BC
		private void DoGetTextValue()
		{
			if (this.inputField == null)
			{
				return;
			}
			this._success = float.TryParse(this.inputField.text, out this._value);
			this.value.Value = this._value;
			this.isFloat.Value = this._success;
			base.Fsm.Event(this._success ? this.isFloatEvent : this.isNotFloatEvent);
		}

		// Token: 0x04008F77 RID: 36727
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F78 RID: 36728
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The text value as a float of the UI InputField component.")]
		public FsmFloat value;

		// Token: 0x04008F79 RID: 36729
		[UIHint(UIHint.Variable)]
		[Tooltip("true if text resolves to a float")]
		public FsmBool isFloat;

		// Token: 0x04008F7A RID: 36730
		[Tooltip("true if text resolves to a float")]
		public FsmEvent isFloatEvent;

		// Token: 0x04008F7B RID: 36731
		[Tooltip("Event sent if text does not resolves to a float")]
		public FsmEvent isNotFloatEvent;

		// Token: 0x04008F7C RID: 36732
		public bool everyFrame;

		// Token: 0x04008F7D RID: 36733
		private InputField inputField;

		// Token: 0x04008F7E RID: 36734
		private float _value;

		// Token: 0x04008F7F RID: 36735
		private bool _success;
	}
}
