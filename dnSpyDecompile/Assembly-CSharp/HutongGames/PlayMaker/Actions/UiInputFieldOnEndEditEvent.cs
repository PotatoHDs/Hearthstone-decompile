using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E77 RID: 3703
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Fires an event when editing ended in a UI InputField component. Event string data will contain the text value, and the boolean will be true is it was a cancel action")]
	public class UiInputFieldOnEndEditEvent : ComponentAction<InputField>
	{
		// Token: 0x0600A8EA RID: 43242 RVA: 0x003514D2 File Offset: 0x0034F6D2
		public override void Reset()
		{
			this.gameObject = null;
			this.sendEvent = null;
			this.text = null;
			this.wasCanceled = null;
		}

		// Token: 0x0600A8EB RID: 43243 RVA: 0x003514F0 File Offset: 0x0034F6F0
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

		// Token: 0x0600A8EC RID: 43244 RVA: 0x0035154E File Offset: 0x0034F74E
		public override void OnExit()
		{
			if (this.inputField != null)
			{
				this.inputField.onEndEdit.RemoveListener(new UnityAction<string>(this.DoOnEndEdit));
			}
		}

		// Token: 0x0600A8ED RID: 43245 RVA: 0x0035157C File Offset: 0x0034F77C
		public void DoOnEndEdit(string value)
		{
			this.text.Value = value;
			this.wasCanceled.Value = this.inputField.wasCanceled;
			Fsm.EventData.StringData = value;
			Fsm.EventData.BoolData = this.inputField.wasCanceled;
			base.SendEvent(this.eventTarget, this.sendEvent);
			base.Finish();
		}

		// Token: 0x04008F94 RID: 36756
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the UI InputField component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F95 RID: 36757
		[Tooltip("Where to send the event.")]
		public FsmEventTarget eventTarget;

		// Token: 0x04008F96 RID: 36758
		[Tooltip("Send this event when editing ended.")]
		public FsmEvent sendEvent;

		// Token: 0x04008F97 RID: 36759
		[Tooltip("The content of the InputField when edited ended")]
		[UIHint(UIHint.Variable)]
		public FsmString text;

		// Token: 0x04008F98 RID: 36760
		[Tooltip("The canceled state of the InputField when edited ended")]
		[UIHint(UIHint.Variable)]
		public FsmBool wasCanceled;

		// Token: 0x04008F99 RID: 36761
		private InputField inputField;
	}
}
