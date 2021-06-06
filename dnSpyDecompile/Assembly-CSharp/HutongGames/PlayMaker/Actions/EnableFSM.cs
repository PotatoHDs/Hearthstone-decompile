using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C1F RID: 3103
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Enables/Disables an FSM component on a GameObject.")]
	public class EnableFSM : FsmStateAction
	{
		// Token: 0x06009E0D RID: 40461 RVA: 0x0032A838 File Offset: 0x00328A38
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.enable = true;
			this.resetOnExit = true;
		}

		// Token: 0x06009E0E RID: 40462 RVA: 0x0032A869 File Offset: 0x00328A69
		public override void OnEnter()
		{
			this.DoEnableFSM();
			base.Finish();
		}

		// Token: 0x06009E0F RID: 40463 RVA: 0x0032A878 File Offset: 0x00328A78
		private void DoEnableFSM()
		{
			GameObject gameObject = (this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : this.gameObject.GameObject.Value;
			if (gameObject == null)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.fsmName.Value))
			{
				foreach (PlayMakerFSM playMakerFSM in gameObject.GetComponents<PlayMakerFSM>())
				{
					if (playMakerFSM.FsmName == this.fsmName.Value)
					{
						this.fsmComponent = playMakerFSM;
						break;
					}
				}
			}
			else
			{
				this.fsmComponent = gameObject.GetComponent<PlayMakerFSM>();
			}
			if (this.fsmComponent == null)
			{
				base.LogError("Missing FsmComponent!");
				return;
			}
			this.fsmComponent.enabled = this.enable.Value;
		}

		// Token: 0x06009E10 RID: 40464 RVA: 0x0032A93F File Offset: 0x00328B3F
		public override void OnExit()
		{
			if (this.fsmComponent == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.fsmComponent.enabled = !this.enable.Value;
			}
		}

		// Token: 0x04008366 RID: 33638
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008367 RID: 33639
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on GameObject. Useful if you have more than one FSM on a GameObject.")]
		public FsmString fsmName;

		// Token: 0x04008368 RID: 33640
		[Tooltip("Set to True to enable, False to disable.")]
		public FsmBool enable;

		// Token: 0x04008369 RID: 33641
		[Tooltip("Reset the initial enabled state when exiting the state.")]
		public FsmBool resetOnExit;

		// Token: 0x0400836A RID: 33642
		private PlayMakerFSM fsmComponent;
	}
}
