using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E78 RID: 3704
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Fires an event when user submits from a UI InputField component. \nThis only fires if the user press Enter, not when field looses focus or user escaped the field.\nEvent string data will contain the text value.")]
	public class UiInputFieldOnSubmitEvent : ComponentAction<InputField>
	{
		// Token: 0x0600A8EF RID: 43247 RVA: 0x003515E3 File Offset: 0x0034F7E3
		public override void Reset()
		{
			this.gameObject = null;
			this.eventTarget = FsmEventTarget.Self;
			this.sendEvent = null;
			this.text = null;
		}

		// Token: 0x0600A8F0 RID: 43248 RVA: 0x00351608 File Offset: 0x0034F808
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.inputField = this.cachedComponent;
				if (this.inputField != null)
				{
					this.inputField.onEndEdit.AddListener(new UnityAction<string>(this.DoOnEndEdit));
				}
			}
		}

		// Token: 0x0600A8F1 RID: 43249 RVA: 0x00351666 File Offset: 0x0034F866
		public override void OnExit()
		{
			if (this.inputField != null)
			{
				this.inputField.onEndEdit.RemoveListener(new UnityAction<string>(this.DoOnEndEdit));
			}
		}

		// Token: 0x0600A8F2 RID: 43250 RVA: 0x00351692 File Offset: 0x0034F892
		public void DoOnEndEdit(string value)
		{
			if (this.inputField.wasCanceled)
			{
				return;
			}
			this.text.Value = value;
			Fsm.EventData.StringData = value;
			base.SendEvent(this.eventTarget, this.sendEvent);
			base.Finish();
		}

		// Token: 0x04008F9A RID: 36762
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F9B RID: 36763
		[Tooltip("Where to send the event.")]
		public FsmEventTarget eventTarget;

		// Token: 0x04008F9C RID: 36764
		[Tooltip("Send this event when editing ended.")]
		public FsmEvent sendEvent;

		// Token: 0x04008F9D RID: 36765
		[Tooltip("The content of the InputField when submitting")]
		[UIHint(UIHint.Variable)]
		public FsmString text;

		// Token: 0x04008F9E RID: 36766
		private InputField inputField;
	}
}
