using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CCB RID: 3275
	[ActionCategory(ActionCategory.ScriptControl)]
	[Tooltip("Invokes a Method in a Behaviour attached to a Game Object. See Unity InvokeMethod docs.")]
	public class InvokeMethod : FsmStateAction
	{
		// Token: 0x0600A0E0 RID: 41184 RVA: 0x00332828 File Offset: 0x00330A28
		public override void Reset()
		{
			this.gameObject = null;
			this.behaviour = null;
			this.methodName = "";
			this.delay = null;
			this.repeating = false;
			this.repeatDelay = 1f;
			this.cancelOnExit = false;
		}

		// Token: 0x0600A0E1 RID: 41185 RVA: 0x00332882 File Offset: 0x00330A82
		public override void OnEnter()
		{
			this.DoInvokeMethod(base.Fsm.GetOwnerDefaultTarget(this.gameObject));
			base.Finish();
		}

		// Token: 0x0600A0E2 RID: 41186 RVA: 0x003328A4 File Offset: 0x00330AA4
		private void DoInvokeMethod(GameObject go)
		{
			if (go == null)
			{
				return;
			}
			this.component = (go.GetComponent(ReflectionUtils.GetGlobalType(this.behaviour.Value)) as MonoBehaviour);
			if (this.component == null)
			{
				base.LogWarning("InvokeMethod: " + go.name + " missing behaviour: " + this.behaviour.Value);
				return;
			}
			if (this.repeating.Value)
			{
				this.component.InvokeRepeating(this.methodName.Value, this.delay.Value, this.repeatDelay.Value);
				return;
			}
			this.component.Invoke(this.methodName.Value, this.delay.Value);
		}

		// Token: 0x0600A0E3 RID: 41187 RVA: 0x0033296C File Offset: 0x00330B6C
		public override void OnExit()
		{
			if (this.component == null)
			{
				return;
			}
			if (this.cancelOnExit.Value)
			{
				this.component.CancelInvoke(this.methodName.Value);
			}
		}

		// Token: 0x0400866D RID: 34413
		[RequiredField]
		[Tooltip("The game object that owns the behaviour.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400866E RID: 34414
		[RequiredField]
		[UIHint(UIHint.Script)]
		[Tooltip("The behaviour that contains the method.")]
		public FsmString behaviour;

		// Token: 0x0400866F RID: 34415
		[RequiredField]
		[UIHint(UIHint.Method)]
		[Tooltip("The name of the method to invoke.")]
		public FsmString methodName;

		// Token: 0x04008670 RID: 34416
		[HasFloatSlider(0f, 10f)]
		[Tooltip("Optional time delay in seconds.")]
		public FsmFloat delay;

		// Token: 0x04008671 RID: 34417
		[Tooltip("Call the method repeatedly.")]
		public FsmBool repeating;

		// Token: 0x04008672 RID: 34418
		[HasFloatSlider(0f, 10f)]
		[Tooltip("Delay between repeated calls in seconds.")]
		public FsmFloat repeatDelay;

		// Token: 0x04008673 RID: 34419
		[Tooltip("Stop calling the method when the state is exited.")]
		public FsmBool cancelOnExit;

		// Token: 0x04008674 RID: 34420
		private MonoBehaviour component;
	}
}
