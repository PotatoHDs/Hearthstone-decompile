using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E9B RID: 3739
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Catches onValueChanged event in a UI Toggle component. Store the new value and/or send events. Event bool data will contain the new Toggle value")]
	public class UiToggleOnValueChangedEvent : ComponentAction<Toggle>
	{
		// Token: 0x0600A9AB RID: 43435 RVA: 0x00353350 File Offset: 0x00351550
		public override void Reset()
		{
			this.gameObject = null;
			this.eventTarget = FsmEventTarget.Self;
			this.sendEvent = null;
			this.value = null;
		}

		// Token: 0x0600A9AC RID: 43436 RVA: 0x00353374 File Offset: 0x00351574
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				base.LogError("Missing GameObject");
				return;
			}
			if (this.toggle != null)
			{
				this.toggle.onValueChanged.RemoveListener(new UnityAction<bool>(this.DoOnValueChanged));
			}
			this.toggle = this.cachedComponent;
			if (this.toggle != null)
			{
				this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.DoOnValueChanged));
				return;
			}
			base.LogError("Missing UI.Toggle on " + ownerDefaultTarget.name);
		}

		// Token: 0x0600A9AD RID: 43437 RVA: 0x0035341F File Offset: 0x0035161F
		public override void OnExit()
		{
			if (this.toggle != null)
			{
				this.toggle.onValueChanged.RemoveListener(new UnityAction<bool>(this.DoOnValueChanged));
			}
		}

		// Token: 0x0600A9AE RID: 43438 RVA: 0x0035344B File Offset: 0x0035164B
		public void DoOnValueChanged(bool _value)
		{
			this.value.Value = _value;
			Fsm.EventData.BoolData = _value;
			base.SendEvent(this.eventTarget, this.sendEvent);
		}

		// Token: 0x04009057 RID: 36951
		[RequiredField]
		[CheckForComponent(typeof(Toggle))]
		[Tooltip("The GameObject with the UI Toggle component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009058 RID: 36952
		[Tooltip("Where to send the event.")]
		public FsmEventTarget eventTarget;

		// Token: 0x04009059 RID: 36953
		[Tooltip("Send this event when the value changes.")]
		public FsmEvent sendEvent;

		// Token: 0x0400905A RID: 36954
		[Tooltip("Store the new value in bool variable.")]
		[UIHint(UIHint.Variable)]
		public FsmBool value;

		// Token: 0x0400905B RID: 36955
		private Toggle toggle;
	}
}
