using System;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F7A RID: 3962
	[ActionCategory("Pegasus")]
	[Tooltip("Sets the visibility on UberText objects and its UberText children.")]
	public class SetUberTextVisibilityRecursiveAction : FsmStateAction
	{
		// Token: 0x0600AD73 RID: 44403 RVA: 0x00360FA1 File Offset: 0x0035F1A1
		public override void Reset()
		{
			this.gameObject = null;
			this.visible = false;
			this.resetOnExit = false;
			this.includeChildren = false;
			this.m_initialVisibility.Clear();
		}

		// Token: 0x0600AD74 RID: 44404 RVA: 0x00360FD0 File Offset: 0x0035F1D0
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
				UberText[] componentsInChildren = ownerDefaultTarget.GetComponentsInChildren<UberText>();
				if (componentsInChildren != null)
				{
					foreach (UberText uberText in componentsInChildren)
					{
						this.m_initialVisibility[uberText] = !uberText.isHidden();
						if (this.visible.Value)
						{
							uberText.Show();
						}
						else
						{
							uberText.Hide();
						}
					}
				}
			}
			else
			{
				UberText component = ownerDefaultTarget.GetComponent<UberText>();
				if (component != null)
				{
					this.m_initialVisibility[component] = !component.isHidden();
					if (this.visible.Value)
					{
						component.Show();
					}
					else
					{
						component.Hide();
					}
				}
			}
			base.Finish();
		}

		// Token: 0x0600AD75 RID: 44405 RVA: 0x003610AC File Offset: 0x0035F2AC
		public override void OnExit()
		{
			if (!this.resetOnExit)
			{
				return;
			}
			foreach (KeyValuePair<UberText, bool> keyValuePair in this.m_initialVisibility)
			{
				UberText key = keyValuePair.Key;
				if (!(key == null))
				{
					if (keyValuePair.Value)
					{
						key.Show();
					}
					else
					{
						key.Hide();
					}
				}
			}
		}

		// Token: 0x0400944D RID: 37965
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400944E RID: 37966
		[Tooltip("Should the objects be set to visible or invisible?")]
		public FsmBool visible;

		// Token: 0x0400944F RID: 37967
		[Tooltip("Resets to the initial visibility once\nit leaves the state")]
		public bool resetOnExit;

		// Token: 0x04009450 RID: 37968
		public bool includeChildren;

		// Token: 0x04009451 RID: 37969
		private Map<UberText, bool> m_initialVisibility = new Map<UberText, bool>();
	}
}
