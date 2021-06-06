using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CC2 RID: 3266
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Checks if an Object has a Component. Optionally remove the Component on exiting the state.")]
	public class HasComponent : FsmStateAction
	{
		// Token: 0x0600A0B3 RID: 41139 RVA: 0x00332195 File Offset: 0x00330395
		public override void Reset()
		{
			this.aComponent = null;
			this.gameObject = null;
			this.trueEvent = null;
			this.falseEvent = null;
			this.component = null;
			this.store = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A0B4 RID: 41140 RVA: 0x003321C8 File Offset: 0x003303C8
		public override void OnEnter()
		{
			this.DoHasComponent((this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : this.gameObject.GameObject.Value);
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A0B5 RID: 41141 RVA: 0x00332203 File Offset: 0x00330403
		public override void OnUpdate()
		{
			this.DoHasComponent((this.gameObject.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : this.gameObject.GameObject.Value);
		}

		// Token: 0x0600A0B6 RID: 41142 RVA: 0x00332230 File Offset: 0x00330430
		public override void OnExit()
		{
			if (this.removeOnExit.Value && this.aComponent != null)
			{
				UnityEngine.Object.Destroy(this.aComponent);
			}
		}

		// Token: 0x0600A0B7 RID: 41143 RVA: 0x00332258 File Offset: 0x00330458
		private void DoHasComponent(GameObject go)
		{
			if (go == null)
			{
				if (!this.store.IsNone)
				{
					this.store.Value = false;
				}
				base.Fsm.Event(this.falseEvent);
				return;
			}
			this.aComponent = go.GetComponent(ReflectionUtils.GetGlobalType(this.component.Value));
			if (!this.store.IsNone)
			{
				this.store.Value = (this.aComponent != null);
			}
			base.Fsm.Event((this.aComponent != null) ? this.trueEvent : this.falseEvent);
		}

		// Token: 0x04008643 RID: 34371
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008644 RID: 34372
		[RequiredField]
		[UIHint(UIHint.ScriptComponent)]
		public FsmString component;

		// Token: 0x04008645 RID: 34373
		public FsmBool removeOnExit;

		// Token: 0x04008646 RID: 34374
		public FsmEvent trueEvent;

		// Token: 0x04008647 RID: 34375
		public FsmEvent falseEvent;

		// Token: 0x04008648 RID: 34376
		[UIHint(UIHint.Variable)]
		public FsmBool store;

		// Token: 0x04008649 RID: 34377
		public bool everyFrame;

		// Token: 0x0400864A RID: 34378
		private Component aComponent;
	}
}
