using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E79 RID: 3705
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Catches UI InputField onValueChanged event. Store the new value and/or send events. Event string data also contains the new value.")]
	public class UiInputFieldOnValueChangeEvent : ComponentAction<InputField>
	{
		// Token: 0x0600A8F4 RID: 43252 RVA: 0x003516D1 File Offset: 0x0034F8D1
		public override void Reset()
		{
			this.gameObject = null;
			this.text = null;
			this.eventTarget = FsmEventTarget.Self;
			this.sendEvent = null;
		}

		// Token: 0x0600A8F5 RID: 43253 RVA: 0x003516F4 File Offset: 0x0034F8F4
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.inputField = this.cachedComponent;
				if (this.inputField != null)
				{
					this.inputField.onValueChanged.AddListener(new UnityAction<string>(this.DoOnValueChange));
				}
			}
		}

		// Token: 0x0600A8F6 RID: 43254 RVA: 0x00351752 File Offset: 0x0034F952
		public override void OnExit()
		{
			if (this.inputField != null)
			{
				this.inputField.onValueChanged.RemoveListener(new UnityAction<string>(this.DoOnValueChange));
			}
		}

		// Token: 0x0600A8F7 RID: 43255 RVA: 0x0035177E File Offset: 0x0034F97E
		public void DoOnValueChange(string value)
		{
			this.text.Value = value;
			Fsm.EventData.StringData = value;
			base.SendEvent(this.eventTarget, this.sendEvent);
		}

		// Token: 0x04008F9F RID: 36767
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008FA0 RID: 36768
		[Tooltip("Where to send the event.")]
		public FsmEventTarget eventTarget;

		// Token: 0x04008FA1 RID: 36769
		[Tooltip("Send this event when value changed.")]
		public FsmEvent sendEvent;

		// Token: 0x04008FA2 RID: 36770
		[Tooltip("Store new value in string variable.")]
		[UIHint(UIHint.Variable)]
		public FsmString text;

		// Token: 0x04008FA3 RID: 36771
		private InputField inputField;
	}
}
