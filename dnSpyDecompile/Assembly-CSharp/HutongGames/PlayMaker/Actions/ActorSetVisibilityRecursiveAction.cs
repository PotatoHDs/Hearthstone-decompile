using System;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F0E RID: 3854
	[ActionCategory("Pegasus")]
	[Tooltip("Sets the visibility on a game object and its children. Will properly Show/Hide Actors in the hierarchy.")]
	public class ActorSetVisibilityRecursiveAction : FsmStateAction
	{
		// Token: 0x0600ABB1 RID: 43953 RVA: 0x00359882 File Offset: 0x00357A82
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_Visible = false;
			this.m_IgnoreSpells = false;
			this.m_ResetOnExit = false;
			this.m_IncludeChildren = true;
			this.m_initialVisibility.Clear();
		}

		// Token: 0x0600ABB2 RID: 43954 RVA: 0x003598BC File Offset: 0x00357ABC
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget != null)
			{
				if (this.m_ResetOnExit)
				{
					this.SaveInitialVisibility(ownerDefaultTarget);
				}
				this.SetVisibility(ownerDefaultTarget);
			}
			base.Finish();
		}

		// Token: 0x0600ABB3 RID: 43955 RVA: 0x00359900 File Offset: 0x00357B00
		public override void OnExit()
		{
			if (this.m_ResetOnExit)
			{
				this.RestoreInitialVisibility();
			}
		}

		// Token: 0x0600ABB4 RID: 43956 RVA: 0x00359910 File Offset: 0x00357B10
		private void SaveInitialVisibility(GameObject go)
		{
			Actor component = go.GetComponent<Actor>();
			if (component != null)
			{
				this.m_initialVisibility[go] = component.IsShown();
				return;
			}
			Renderer component2 = go.GetComponent<Renderer>();
			if (component2 != null)
			{
				this.m_initialVisibility[go] = component2.enabled;
			}
			if (this.m_IncludeChildren)
			{
				foreach (object obj in go.transform)
				{
					Transform transform = (Transform)obj;
					this.SaveInitialVisibility(transform.gameObject);
				}
			}
		}

		// Token: 0x0600ABB5 RID: 43957 RVA: 0x003599C0 File Offset: 0x00357BC0
		private void RestoreInitialVisibility()
		{
			foreach (KeyValuePair<GameObject, bool> keyValuePair in this.m_initialVisibility)
			{
				GameObject key = keyValuePair.Key;
				bool value = keyValuePair.Value;
				Actor component = key.GetComponent<Actor>();
				if (component != null)
				{
					if (value)
					{
						this.ShowActor(component);
					}
					else
					{
						this.HideActor(component);
					}
				}
				else
				{
					key.GetComponent<Renderer>().enabled = value;
				}
			}
		}

		// Token: 0x0600ABB6 RID: 43958 RVA: 0x00359A54 File Offset: 0x00357C54
		private void SetVisibility(GameObject go)
		{
			Actor component = go.GetComponent<Actor>();
			if (!(component != null))
			{
				Renderer component2 = go.GetComponent<Renderer>();
				if (component2 != null)
				{
					component2.enabled = this.m_Visible.Value;
				}
				if (this.m_IncludeChildren)
				{
					foreach (object obj in go.transform)
					{
						Transform transform = (Transform)obj;
						this.SetVisibility(transform.gameObject);
					}
				}
				return;
			}
			if (this.m_Visible.Value)
			{
				this.ShowActor(component);
				return;
			}
			this.HideActor(component);
		}

		// Token: 0x0600ABB7 RID: 43959 RVA: 0x00359B0C File Offset: 0x00357D0C
		private void ShowActor(Actor actor)
		{
			actor.Show(this.m_IgnoreSpells.Value);
		}

		// Token: 0x0600ABB8 RID: 43960 RVA: 0x00359B1F File Offset: 0x00357D1F
		private void HideActor(Actor actor)
		{
			actor.Hide(this.m_IgnoreSpells.Value);
		}

		// Token: 0x04009268 RID: 37480
		public FsmOwnerDefault m_GameObject;

		// Token: 0x04009269 RID: 37481
		[Tooltip("Should objects be set to visible or invisible?")]
		public FsmBool m_Visible;

		// Token: 0x0400926A RID: 37482
		[Tooltip("Don't touch the Actor's SpellTable when setting visibility")]
		public FsmBool m_IgnoreSpells;

		// Token: 0x0400926B RID: 37483
		[Tooltip("Resets to the initial visibility once it leaves the state")]
		public bool m_ResetOnExit;

		// Token: 0x0400926C RID: 37484
		[Tooltip("Should children of the Game Object be affected?")]
		public bool m_IncludeChildren;

		// Token: 0x0400926D RID: 37485
		private Map<GameObject, bool> m_initialVisibility = new Map<GameObject, bool>();
	}
}
