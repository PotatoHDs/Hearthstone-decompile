using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Sets the visibility on a game object and its children. Will properly Show/Hide Actors in the hierarchy.")]
	public class ActorSetVisibilityRecursiveAction : FsmStateAction
	{
		public FsmOwnerDefault m_GameObject;

		[Tooltip("Should objects be set to visible or invisible?")]
		public FsmBool m_Visible;

		[Tooltip("Don't touch the Actor's SpellTable when setting visibility")]
		public FsmBool m_IgnoreSpells;

		[Tooltip("Resets to the initial visibility once it leaves the state")]
		public bool m_ResetOnExit;

		[Tooltip("Should children of the Game Object be affected?")]
		public bool m_IncludeChildren;

		private Map<GameObject, bool> m_initialVisibility = new Map<GameObject, bool>();

		public override void Reset()
		{
			m_GameObject = null;
			m_Visible = false;
			m_IgnoreSpells = false;
			m_ResetOnExit = false;
			m_IncludeChildren = true;
			m_initialVisibility.Clear();
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (ownerDefaultTarget != null)
			{
				if (m_ResetOnExit)
				{
					SaveInitialVisibility(ownerDefaultTarget);
				}
				SetVisibility(ownerDefaultTarget);
			}
			Finish();
		}

		public override void OnExit()
		{
			if (m_ResetOnExit)
			{
				RestoreInitialVisibility();
			}
		}

		private void SaveInitialVisibility(GameObject go)
		{
			Actor component = go.GetComponent<Actor>();
			if (component != null)
			{
				m_initialVisibility[go] = component.IsShown();
				return;
			}
			Renderer component2 = go.GetComponent<Renderer>();
			if (component2 != null)
			{
				m_initialVisibility[go] = component2.enabled;
			}
			if (!m_IncludeChildren)
			{
				return;
			}
			foreach (Transform item in go.transform)
			{
				SaveInitialVisibility(item.gameObject);
			}
		}

		private void RestoreInitialVisibility()
		{
			foreach (KeyValuePair<GameObject, bool> item in m_initialVisibility)
			{
				GameObject key = item.Key;
				bool value = item.Value;
				Actor component = key.GetComponent<Actor>();
				if (component != null)
				{
					if (value)
					{
						ShowActor(component);
					}
					else
					{
						HideActor(component);
					}
				}
				else
				{
					key.GetComponent<Renderer>().enabled = value;
				}
			}
		}

		private void SetVisibility(GameObject go)
		{
			Actor component = go.GetComponent<Actor>();
			if (component != null)
			{
				if (m_Visible.Value)
				{
					ShowActor(component);
				}
				else
				{
					HideActor(component);
				}
				return;
			}
			Renderer component2 = go.GetComponent<Renderer>();
			if (component2 != null)
			{
				component2.enabled = m_Visible.Value;
			}
			if (!m_IncludeChildren)
			{
				return;
			}
			foreach (Transform item in go.transform)
			{
				SetVisibility(item.gameObject);
			}
		}

		private void ShowActor(Actor actor)
		{
			actor.Show(m_IgnoreSpells.Value);
		}

		private void HideActor(Actor actor)
		{
			actor.Hide(m_IgnoreSpells.Value);
		}
	}
}
