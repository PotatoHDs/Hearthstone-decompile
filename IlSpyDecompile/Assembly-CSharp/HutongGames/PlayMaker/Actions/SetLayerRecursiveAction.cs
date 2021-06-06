using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Sets the layer on a game object and its children.")]
	public class SetLayerRecursiveAction : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[Tooltip("Layer number")]
		public GameLayer layer;

		[Tooltip("Resets to the initial layer once\nit leaves the state")]
		public bool resetOnExit;

		public bool includeChildren;

		private Map<GameObject, GameLayer> m_initialLayer = new Map<GameObject, GameLayer>();

		public override void Reset()
		{
			gameObject = null;
			layer = GameLayer.Default;
			resetOnExit = true;
			includeChildren = false;
			m_initialLayer.Clear();
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
				Transform[] componentsInChildren = ownerDefaultTarget.GetComponentsInChildren<Transform>();
				if (componentsInChildren != null)
				{
					Transform[] array = componentsInChildren;
					foreach (Transform transform in array)
					{
						m_initialLayer[transform.gameObject] = (GameLayer)transform.gameObject.layer;
						transform.gameObject.layer = (int)layer;
					}
				}
			}
			else
			{
				Transform component = ownerDefaultTarget.GetComponent<Transform>();
				if (component != null)
				{
					m_initialLayer[component.gameObject] = (GameLayer)component.gameObject.layer;
					component.gameObject.layer = (int)layer;
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
			foreach (KeyValuePair<GameObject, GameLayer> item in m_initialLayer)
			{
				GameObject key = item.Key;
				if (!(key == null))
				{
					key.layer = (int)item.Value;
				}
			}
		}
	}
}
