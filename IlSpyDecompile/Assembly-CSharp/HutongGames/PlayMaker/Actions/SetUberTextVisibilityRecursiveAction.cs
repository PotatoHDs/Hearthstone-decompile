using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Sets the visibility on UberText objects and its UberText children.")]
	public class SetUberTextVisibilityRecursiveAction : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[Tooltip("Should the objects be set to visible or invisible?")]
		public FsmBool visible;

		[Tooltip("Resets to the initial visibility once\nit leaves the state")]
		public bool resetOnExit;

		public bool includeChildren;

		private Map<UberText, bool> m_initialVisibility = new Map<UberText, bool>();

		public override void Reset()
		{
			gameObject = null;
			visible = false;
			resetOnExit = false;
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
				UberText[] componentsInChildren = ownerDefaultTarget.GetComponentsInChildren<UberText>();
				if (componentsInChildren != null)
				{
					UberText[] array = componentsInChildren;
					foreach (UberText uberText in array)
					{
						m_initialVisibility[uberText] = !uberText.isHidden();
						if (visible.Value)
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
					m_initialVisibility[component] = !component.isHidden();
					if (visible.Value)
					{
						component.Show();
					}
					else
					{
						component.Hide();
					}
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
			foreach (KeyValuePair<UberText, bool> item in m_initialVisibility)
			{
				UberText key = item.Key;
				if (!(key == null))
				{
					if (item.Value)
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
	}
}
