using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E9C RID: 3740
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the isOn property of a UI Toggle component.")]
	public class UiToggleSetIsOn : ComponentAction<Toggle>
	{
		// Token: 0x0600A9B0 RID: 43440 RVA: 0x00353476 File Offset: 0x00351676
		public override void Reset()
		{
			this.gameObject = null;
			this.isOn = null;
			this.resetOnExit = null;
		}

		// Token: 0x0600A9B1 RID: 43441 RVA: 0x00353490 File Offset: 0x00351690
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this._toggle = this.cachedComponent;
			}
			this.DoSetValue();
			base.Finish();
		}

		// Token: 0x0600A9B2 RID: 43442 RVA: 0x003534D0 File Offset: 0x003516D0
		private void DoSetValue()
		{
			if (this._toggle != null)
			{
				this._originalValue = this._toggle.isOn;
				this._toggle.isOn = this.isOn.Value;
			}
		}

		// Token: 0x0600A9B3 RID: 43443 RVA: 0x00353507 File Offset: 0x00351707
		public override void OnExit()
		{
			if (this._toggle == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this._toggle.isOn = this._originalValue;
			}
		}

		// Token: 0x0400905C RID: 36956
		[RequiredField]
		[CheckForComponent(typeof(Toggle))]
		[Tooltip("The GameObject with the UI Toggle component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400905D RID: 36957
		[RequiredField]
		[Tooltip("Should the toggle be on?")]
		public FsmBool isOn;

		// Token: 0x0400905E RID: 36958
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x0400905F RID: 36959
		private Toggle _toggle;

		// Token: 0x04009060 RID: 36960
		private bool _originalValue;
	}
}
