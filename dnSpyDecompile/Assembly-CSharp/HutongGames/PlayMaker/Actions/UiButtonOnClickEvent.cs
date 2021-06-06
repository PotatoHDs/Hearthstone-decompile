using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E5C RID: 3676
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sends an event when a UI Button is clicked.")]
	public class UiButtonOnClickEvent : ComponentAction<Button>
	{
		// Token: 0x0600A870 RID: 43120 RVA: 0x0035008C File Offset: 0x0034E28C
		public override void Reset()
		{
			this.gameObject = null;
			this.sendEvent = null;
		}

		// Token: 0x0600A871 RID: 43121 RVA: 0x0035009C File Offset: 0x0034E29C
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				base.LogError("Missing GameObject ");
				return;
			}
			if (this.button != null)
			{
				this.button.onClick.RemoveListener(new UnityAction(this.DoOnClick));
			}
			this.button = this.cachedComponent;
			if (this.button != null)
			{
				this.button.onClick.AddListener(new UnityAction(this.DoOnClick));
				return;
			}
			base.LogError("Missing UI.Button on " + ownerDefaultTarget.name);
		}

		// Token: 0x0600A872 RID: 43122 RVA: 0x00350147 File Offset: 0x0034E347
		public override void OnExit()
		{
			if (this.button != null)
			{
				this.button.onClick.RemoveListener(new UnityAction(this.DoOnClick));
			}
		}

		// Token: 0x0600A873 RID: 43123 RVA: 0x00350173 File Offset: 0x0034E373
		public void DoOnClick()
		{
			base.SendEvent(this.eventTarget, this.sendEvent);
		}

		// Token: 0x04008F0D RID: 36621
		[RequiredField]
		[CheckForComponent(typeof(Button))]
		[Tooltip("The GameObject with the UI Button component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008F0E RID: 36622
		[Tooltip("Where to send the event.")]
		public FsmEventTarget eventTarget;

		// Token: 0x04008F0F RID: 36623
		[Tooltip("Send this event when Clicked.")]
		public FsmEvent sendEvent;

		// Token: 0x04008F10 RID: 36624
		private Button button;
	}
}
