using System;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F7C RID: 3964
	[ActionCategory("Pegasus")]
	[Tooltip("Sets the visibility on a game object and its children.")]
	public class SetVisibilityRecursiveDelayAction : FsmStateAction
	{
		// Token: 0x0600AD7B RID: 44411 RVA: 0x003612B3 File Offset: 0x0035F4B3
		public override void Reset()
		{
			this.gameObject = null;
			this.visible = false;
			this.m_Delay = 0f;
			this.includeChildren = false;
			this.m_initialVisibility.Clear();
		}

		// Token: 0x0600AD7C RID: 44412 RVA: 0x003612EA File Offset: 0x0035F4EA
		public override void OnEnter()
		{
			this.m_timerSec = 0f;
		}

		// Token: 0x0600AD7D RID: 44413 RVA: 0x003612F8 File Offset: 0x0035F4F8
		public override void OnUpdate()
		{
			this.m_timerSec += Time.deltaTime;
			float num = this.m_Delay.IsNone ? 0f : this.m_Delay.Value;
			if (this.m_timerSec < num)
			{
				return;
			}
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

		// Token: 0x04009457 RID: 37975
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009458 RID: 37976
		[Tooltip("Should the objects be set to visible or invisible?")]
		public FsmBool visible;

		// Token: 0x04009459 RID: 37977
		public FsmFloat m_Delay;

		// Token: 0x0400945A RID: 37978
		public bool includeChildren;

		// Token: 0x0400945B RID: 37979
		private Dictionary<Renderer, bool> m_initialVisibility = new Dictionary<Renderer, bool>();

		// Token: 0x0400945C RID: 37980
		private float m_timerSec;
	}
}
