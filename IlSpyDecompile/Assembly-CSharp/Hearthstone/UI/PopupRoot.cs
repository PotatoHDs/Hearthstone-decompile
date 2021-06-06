using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone.UI
{
	[AddComponentMenu("")]
	[ExecuteAlways]
	public class PopupRoot : MonoBehaviour
	{
		private HashSet<IPopupRendering> m_popupComponents = new HashSet<IPopupRendering>();

		private PopupCamera m_popupCamera;

		private bool m_enabled;

		public PopupCamera PopupCamera => m_popupCamera;

		public bool IsEnabled => m_enabled;

		public event Action<PopupRoot> OnDestroyed;

		public event Action<PopupRoot> OnDisabled;

		public static void ApplyPopupRendering(PopupRoot popupRoot, Transform objectRoot, HashSet<IPopupRendering> popupRenderingCache)
		{
			if (popupRoot == null || !popupRoot.IsEnabled)
			{
				return;
			}
			List<IPopupRendering> newPopupComponents = new List<IPopupRendering>();
			SceneUtils.WalkSelfAndChildren(objectRoot, delegate(Transform child)
			{
				bool flag = child.GetComponent<PopupRoot>() == null || child == popupRoot.transform;
				bool flag2 = false;
				if (flag)
				{
					Component component = child.GetComponent<Renderer>();
					if (component == null)
					{
						component = child.GetComponent<BoxCollider>();
					}
					if (component != null && component.gameObject.GetComponent<PopupRenderer>() == null)
					{
						component.gameObject.AddComponent<PopupRenderer>();
					}
					MonoBehaviour[] components = child.GetComponents<MonoBehaviour>();
					for (int i = 0; i < components.Length; i++)
					{
						if (!(components[i] == null))
						{
							IPopupRendering popupRendering = components[i] as IPopupRendering;
							if (popupRendering != null)
							{
								newPopupComponents.Add(popupRendering);
								flag2 |= !popupRendering.ShouldPropagatePopupRendering();
							}
						}
					}
					List<IAsyncInitializationBehavior> asyncBehaviors = AsyncBehaviorUtils.GetAsyncBehaviors(child);
					if (asyncBehaviors != null)
					{
						foreach (IAsyncInitializationBehavior item in asyncBehaviors)
						{
							if (child != popupRoot.transform)
							{
								item.RegisterReadyListener(delegate(object obj)
								{
									foreach (Transform item2 in (Transform)obj)
									{
										ApplyPopupRendering(popupRoot, item2, popupRenderingCache);
									}
								}, child, callImmediatelyIfReady: false);
							}
						}
					}
				}
				return flag && !flag2;
			});
			foreach (IPopupRendering item3 in newPopupComponents)
			{
				item3.EnablePopupRendering(popupRoot);
				popupRenderingCache.Add(item3);
			}
		}

		public void EnablePopupRendering(PopupCamera popupCamera)
		{
			m_enabled = true;
			m_popupCamera = popupCamera;
			ApplyPopupRendering(base.transform);
		}

		public void ApplyPopupRendering(Transform popupRoot)
		{
			ApplyPopupRendering(this, popupRoot, m_popupComponents);
		}

		public void DisablePopupRendering()
		{
			m_enabled = false;
			foreach (IPopupRendering popupComponent in m_popupComponents)
			{
				if (popupComponent != null && !(popupComponent as UnityEngine.Object == null))
				{
					popupComponent.DisablePopupRendering();
				}
			}
			m_popupComponents.Clear();
		}

		private void OnDisable()
		{
			DisablePopupRendering();
			if (this.OnDisabled != null)
			{
				this.OnDisabled(this);
			}
		}

		private void OnDestroy()
		{
			if (this.OnDestroyed != null)
			{
				this.OnDestroyed(this);
			}
		}
	}
}
