using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E85 RID: 3717
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Catches UI Scrollbar onValueChanged event. Store the new value and/or send events. Event float data will contain the new Scrollbar value")]
	public class UiScrollbarOnValueChanged : ComponentAction<UnityEngine.UI.Scrollbar>
	{
		// Token: 0x0600A935 RID: 43317 RVA: 0x0035202E File Offset: 0x0035022E
		public override void Reset()
		{
			this.gameObject = null;
			this.eventTarget = FsmEventTarget.Self;
			this.sendEvent = null;
			this.value = null;
		}

		// Token: 0x0600A936 RID: 43318 RVA: 0x00352050 File Offset: 0x00350250
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.scrollbar = this.cachedComponent;
				if (this.scrollbar != null)
				{
					this.scrollbar.onValueChanged.AddListener(new UnityAction<float>(this.DoOnValueChanged));
				}
			}
		}

		// Token: 0x0600A937 RID: 43319 RVA: 0x003520AE File Offset: 0x003502AE
		public override void OnExit()
		{
			if (this.scrollbar != null)
			{
				this.scrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.DoOnValueChanged));
			}
		}

		// Token: 0x0600A938 RID: 43320 RVA: 0x003520DA File Offset: 0x003502DA
		public void DoOnValueChanged(float _value)
		{
			this.value.Value = _value;
			Fsm.EventData.FloatData = _value;
			base.SendEvent(this.eventTarget, this.sendEvent);
		}

		// Token: 0x04008FDD RID: 36829
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Scrollbar))]
		[Tooltip("The GameObject with the UI Scrollbar component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008FDE RID: 36830
		[Tooltip("Where to send the event.")]
		public FsmEventTarget eventTarget;

		// Token: 0x04008FDF RID: 36831
		[Tooltip("Send this event when the UI Scrollbar value changes.")]
		public FsmEvent sendEvent;

		// Token: 0x04008FE0 RID: 36832
		[Tooltip("Store new value in float variable.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat value;

		// Token: 0x04008FE1 RID: 36833
		private UnityEngine.UI.Scrollbar scrollbar;
	}
}
