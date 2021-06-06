using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E9A RID: 3738
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the isOn value of a UI Toggle component. Optionally send events")]
	public class UiToggleGetIsOn : ComponentAction<Toggle>
	{
		// Token: 0x0600A9A6 RID: 43430 RVA: 0x00353287 File Offset: 0x00351487
		public override void Reset()
		{
			this.gameObject = null;
			this.value = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A9A7 RID: 43431 RVA: 0x003532A0 File Offset: 0x003514A0
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this._toggle = this.cachedComponent;
			}
			this.DoGetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A9A8 RID: 43432 RVA: 0x003532E8 File Offset: 0x003514E8
		public override void OnUpdate()
		{
			this.DoGetValue();
		}

		// Token: 0x0600A9A9 RID: 43433 RVA: 0x003532F0 File Offset: 0x003514F0
		private void DoGetValue()
		{
			if (this._toggle == null)
			{
				return;
			}
			this.value.Value = this._toggle.isOn;
			base.Fsm.Event(this._toggle.isOn ? this.isOnEvent : this.isOffEvent);
		}

		// Token: 0x04009051 RID: 36945
		[RequiredField]
		[CheckForComponent(typeof(Toggle))]
		[Tooltip("The GameObject with the UI Toggle component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009052 RID: 36946
		[UIHint(UIHint.Variable)]
		[Tooltip("The isOn Value of the UI Toggle component.")]
		public FsmBool value;

		// Token: 0x04009053 RID: 36947
		[Tooltip("Event sent when isOn Value is true.")]
		public FsmEvent isOnEvent;

		// Token: 0x04009054 RID: 36948
		[Tooltip("Event sent when isOn Value is false.")]
		public FsmEvent isOffEvent;

		// Token: 0x04009055 RID: 36949
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x04009056 RID: 36950
		private Toggle _toggle;
	}
}
