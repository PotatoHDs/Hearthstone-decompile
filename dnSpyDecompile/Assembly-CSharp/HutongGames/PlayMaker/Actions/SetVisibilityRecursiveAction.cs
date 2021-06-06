using System;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F7B RID: 3963
	[ActionCategory("Pegasus")]
	[Tooltip("Sets the visibility on a game object and its children.")]
	public class SetVisibilityRecursiveAction : FsmStateAction
	{
		// Token: 0x0600AD77 RID: 44407 RVA: 0x0036113F File Offset: 0x0035F33F
		public override void Reset()
		{
			this.gameObject = null;
			this.visible = false;
			this.resetOnExit = true;
			this.includeChildren = false;
			this.m_initialVisibility.Clear();
		}

		// Token: 0x0600AD78 RID: 44408 RVA: 0x00361170 File Offset: 0x0035F370
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				base.Finish();
				return;
			}
			if (this.includeChildren)
			{
				Renderer[] componentsInChildren = ownerDefaultTarget.GetComponentsInChildren<Renderer>();
				if (componentsInChildren != null)
				{
					foreach (Renderer renderer in componentsInChildren)
					{
						this.m_initialVisibility[renderer] = renderer.enabled;
						renderer.enabled = this.visible.Value;
					}
				}
			}
			else
			{
				Renderer component = ownerDefaultTarget.GetComponent<Renderer>();
				if (component != null)
				{
					this.m_initialVisibility[component] = component.enabled;
					component.enabled = this.visible.Value;
				}
			}
			base.Finish();
		}

		// Token: 0x0600AD79 RID: 44409 RVA: 0x0036122C File Offset: 0x0035F42C
		public override void OnExit()
		{
			if (!this.resetOnExit)
			{
				return;
			}
			foreach (KeyValuePair<Renderer, bool> keyValuePair in this.m_initialVisibility)
			{
				Renderer key = keyValuePair.Key;
				if (!(key == null))
				{
					key.enabled = keyValuePair.Value;
				}
			}
		}

		// Token: 0x04009452 RID: 37970
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009453 RID: 37971
		[Tooltip("Should the objects be set to visible or invisible?")]
		public FsmBool visible;

		// Token: 0x04009454 RID: 37972
		[Tooltip("Resets to the initial visibility once\nit leaves the state")]
		public bool resetOnExit;

		// Token: 0x04009455 RID: 37973
		public bool includeChildren;

		// Token: 0x04009456 RID: 37974
		private Dictionary<Renderer, bool> m_initialVisibility = new Dictionary<Renderer, bool>();
	}
}
