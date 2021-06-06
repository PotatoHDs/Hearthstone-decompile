using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Sets the visibility on a game object and its children.")]
	public class SetVisibilityRecursiveDelayAction : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[Tooltip("Should the objects be set to visible or invisible?")]
		public FsmBool visible;

		public FsmFloat m_Delay;

		public bool includeChildren;

		private Dictionary<Renderer, bool> m_initialVisibility = new Dictionary<Renderer, bool>();

		private float m_timerSec;

		public override void Reset()
		{
			gameObject = null;
			visible = false;
			m_Delay = 0f;
			includeChildren = false;
			m_initialVisibility.Clear();
		}

		public override void OnEnter()
		{
			m_timerSec = 0f;
		}

		public override void OnUpdate()
		{
			m_timerSec += Time.deltaTime;
			float num = (m_Delay.IsNone ? 0f : m_Delay.Value);
			if (m_timerSec < num)
			{
				return;
			}
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
	}
}
