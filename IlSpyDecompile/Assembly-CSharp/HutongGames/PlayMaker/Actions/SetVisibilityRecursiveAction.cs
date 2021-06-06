using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Sets the visibility on a game object and its children.")]
	public class SetVisibilityRecursiveAction : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[Tooltip("Should the objects be set to visible or invisible?")]
		public FsmBool visible;

		[Tooltip("Resets to the initial visibility once\nit leaves the state")]
		public bool resetOnExit;

		public bool includeChildren;

		private Dictionary<Renderer, bool> m_initialVisibility = new Dictionary<Renderer, bool>();

		public override void Reset()
		{
			gameObject = null;
			visible = false;
			resetOnExit = true;
			includeChildren = false;
			m_initialVisibility.Clear();
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget == null)
			{
				Finish();
				return;
			}
			if (includeChildren)
			{
				Renderer[] componentsInChildren = ownerDefaultTarget.GetComponentsInChildren<Renderer>();
				if (componentsInChildren != null)
				{
					Renderer[] array = componentsInChildren;
					foreach (Renderer renderer in array)
					{
						m_initialVisibility[renderer] = renderer.enabled;
						renderer.enabled = visible.Value;
					}
				}
			}
			else
			{
				Renderer component = ownerDefaultTarget.GetComponent<Renderer>();
				if (component != null)
				{
					m_initialVisibility[component] = component.enabled;
					component.enabled = visible.Value;
				}
			}
			Finish();
		}

		public override void OnExit()
		{
			if (!resetOnExit)
			{
				return;
			}
			foreach (KeyValuePair<Renderer, bool> item in m_initialVisibility)
			{
				Renderer key = item.Key;
				if (!(key == null))
				{
					key.enabled = item.Value;
				}
			}
		}
	}
}
