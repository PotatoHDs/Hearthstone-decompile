using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C1D RID: 3101
	[ActionCategory(ActionCategory.ScriptControl)]
	[Tooltip("Enables/Disables a Behaviour on a GameObject. Optionally reset the Behaviour on exit - useful if you want the Behaviour to be active only while this state is active.")]
	public class EnableBehaviour : FsmStateAction
	{
		// Token: 0x06009E03 RID: 40451 RVA: 0x0032A644 File Offset: 0x00328844
		public override void Reset()
		{
			this.gameObject = null;
			this.behaviour = null;
			this.component = null;
			this.enable = true;
			this.resetOnExit = true;
		}

		// Token: 0x06009E04 RID: 40452 RVA: 0x0032A673 File Offset: 0x00328873
		public override void OnEnter()
		{
			this.DoEnableBehaviour(base.Fsm.GetOwnerDefaultTarget(this.gameObject));
			base.Finish();
		}

		// Token: 0x06009E05 RID: 40453 RVA: 0x0032A694 File Offset: 0x00328894
		private void DoEnableBehaviour(GameObject go)
		{
			if (go == null)
			{
				return;
			}
			if (this.component != null)
			{
				this.componentTarget = (this.component as Behaviour);
			}
			else
			{
				this.componentTarget = (go.GetComponent(ReflectionUtils.GetGlobalType(this.behaviour.Value)) as Behaviour);
			}
			if (this.componentTarget == null)
			{
				base.LogWarning(" " + go.name + " missing behaviour: " + this.behaviour.Value);
				return;
			}
			this.componentTarget.enabled = this.enable.Value;
		}

		// Token: 0x06009E06 RID: 40454 RVA: 0x0032A738 File Offset: 0x00328938
		public override void OnExit()
		{
			if (this.componentTarget == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.componentTarget.enabled = !this.enable.Value;
			}
		}

		// Token: 0x06009E07 RID: 40455 RVA: 0x0032A770 File Offset: 0x00328970
		public override string ErrorCheck()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null || this.component != null || this.behaviour.IsNone || string.IsNullOrEmpty(this.behaviour.Value))
			{
				return null;
			}
			if (!(ownerDefaultTarget.GetComponent(ReflectionUtils.GetGlobalType(this.behaviour.Value)) as Behaviour != null))
			{
				return "Behaviour missing";
			}
			return null;
		}

		// Token: 0x0400835E RID: 33630
		[RequiredField]
		[Tooltip("The GameObject that owns the Behaviour.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400835F RID: 33631
		[UIHint(UIHint.Behaviour)]
		[Tooltip("The name of the Behaviour to enable/disable.")]
		public FsmString behaviour;

		// Token: 0x04008360 RID: 33632
		[Tooltip("Optionally drag a component directly into this field (behavior name will be ignored).")]
		public Component component;

		// Token: 0x04008361 RID: 33633
		[RequiredField]
		[Tooltip("Set to True to enable, False to disable.")]
		public FsmBool enable;

		// Token: 0x04008362 RID: 33634
		public FsmBool resetOnExit;

		// Token: 0x04008363 RID: 33635
		private Behaviour componentTarget;
	}
}
