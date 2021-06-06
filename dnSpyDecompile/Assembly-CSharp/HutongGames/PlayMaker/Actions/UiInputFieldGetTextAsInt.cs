using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E73 RID: 3699
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the text value of a UI InputField component as an Int.")]
	public class UiInputFieldGetTextAsInt : ComponentAction<InputField>
	{
		// Token: 0x0600A8D9 RID: 43225 RVA: 0x00351237 File Offset: 0x0034F437
		public override void Reset()
		{
			this.value = null;
			this.isInt = null;
			this.isIntEvent = null;
			this.isNotIntEvent = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A8DA RID: 43226 RVA: 0x0035125C File Offset: 0x0034F45C
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

		// Token: 0x0600A8DB RID: 43227 RVA: 0x003512A4 File Offset: 0x0034F4A4
		public override void OnUpdate()
		{
			this.DoGetTextValue();
		}

		// Token: 0x0600A8DC RID: 43228 RVA: 0x003512AC File Offset: 0x0034F4AC
		private void DoGetTextValue()
		{
			if (this.inputField == null)
			{
				return;
			}
			this._success = int.TryParse(this.inputField.text, out this._value);
			this.value.Value = this._value;
			this.isInt.Value = this._success;
			base.Fsm.Event(this._success ? this.isIntEvent : this.isNotIntEvent);
		}

		// Token: 0x04008F80 RID: 36736
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F81 RID: 36737
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the text value as an int.")]
		public FsmInt value;

		// Token: 0x04008F82 RID: 36738
		[UIHint(UIHint.Variable)]
		[Tooltip("True if text resolves to an int")]
		public FsmBool isInt;

		// Token: 0x04008F83 RID: 36739
		[Tooltip("Event to send if text resolves to an int")]
		public FsmEvent isIntEvent;

		// Token: 0x04008F84 RID: 36740
		[Tooltip("Event to send if text does NOT resolve to an int")]
		public FsmEvent isNotIntEvent;

		// Token: 0x04008F85 RID: 36741
		public bool everyFrame;

		// Token: 0x04008F86 RID: 36742
		private InputField inputField;

		// Token: 0x04008F87 RID: 36743
		private int _value;

		// Token: 0x04008F88 RID: 36744
		private bool _success;
	}
}
