using System;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F75 RID: 3957
	[ActionCategory("Pegasus")]
	[Tooltip("Sets the layer on a game object and its children.")]
	public class SetLayerRecursiveAction : FsmStateAction
	{
		// Token: 0x0600AD5D RID: 44381 RVA: 0x00360B30 File Offset: 0x0035ED30
		public override void Reset()
		{
			this.gameObject = null;
			this.layer = GameLayer.Default;
			this.resetOnExit = true;
			this.includeChildren = false;
			this.m_initialLayer.Clear();
		}

		// Token: 0x0600AD5E RID: 44382 RVA: 0x00360B5C File Offset: 0x0035ED5C
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
				Transform[] componentsInChildren = ownerDefaultTarget.GetComponentsInChildren<Transform>();
				if (componentsInChildren != null)
				{
					foreach (Transform transform in componentsInChildren)
					{
						this.m_initialLayer[transform.gameObject] = (GameLayer)transform.gameObject.layer;
						transform.gameObject.layer = (int)this.layer;
					}
				}
			}
			else
			{
				Transform component = ownerDefaultTarget.GetComponent<Transform>();
				if (component != null)
				{
					this.m_initialLayer[component.gameObject] = (GameLayer)component.gameObject.layer;
					component.gameObject.layer = (int)this.layer;
				}
			}
			base.Finish();
		}

		// Token: 0x0600AD5F RID: 44383 RVA: 0x00360C30 File Offset: 0x0035EE30
		public override void OnExit()
		{
			if (!this.resetOnExit)
			{
				return;
			}
			foreach (KeyValuePair<GameObject, GameLayer> keyValuePair in this.m_initialLayer)
			{
				GameObject key = keyValuePair.Key;
				if (!(key == null))
				{
					key.layer = (int)keyValuePair.Value;
				}
			}
		}

		// Token: 0x04009439 RID: 37945
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400943A RID: 37946
		[Tooltip("Layer number")]
		public GameLayer layer;

		// Token: 0x0400943B RID: 37947
		[Tooltip("Resets to the initial layer once\nit leaves the state")]
		public bool resetOnExit;

		// Token: 0x0400943C RID: 37948
		public bool includeChildren;

		// Token: 0x0400943D RID: 37949
		private Map<GameObject, GameLayer> m_initialLayer = new Map<GameObject, GameLayer>();
	}
}
