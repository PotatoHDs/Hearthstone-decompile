using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02000FF8 RID: 4088
	[AddComponentMenu("")]
	[ExecuteAlways]
	public class PopupRoot : MonoBehaviour
	{
		// Token: 0x140000A8 RID: 168
		// (add) Token: 0x0600B19A RID: 45466 RVA: 0x0036C4C8 File Offset: 0x0036A6C8
		// (remove) Token: 0x0600B19B RID: 45467 RVA: 0x0036C500 File Offset: 0x0036A700
		public event Action<PopupRoot> OnDestroyed;

		// Token: 0x140000A9 RID: 169
		// (add) Token: 0x0600B19C RID: 45468 RVA: 0x0036C538 File Offset: 0x0036A738
		// (remove) Token: 0x0600B19D RID: 45469 RVA: 0x0036C570 File Offset: 0x0036A770
		public event Action<PopupRoot> OnDisabled;

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x0600B19E RID: 45470 RVA: 0x0036C5A5 File Offset: 0x0036A7A5
		public PopupCamera PopupCamera
		{
			get
			{
				return this.m_popupCamera;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x0600B19F RID: 45471 RVA: 0x0036C5AD File Offset: 0x0036A7AD
		public bool IsEnabled
		{
			get
			{
				return this.m_enabled;
			}
		}

		// Token: 0x0600B1A0 RID: 45472 RVA: 0x0036C5B8 File Offset: 0x0036A7B8
		public static void ApplyPopupRendering(PopupRoot popupRoot, Transform objectRoot, HashSet<IPopupRendering> popupRenderingCache)
		{
			if (popupRoot == null || !popupRoot.IsEnabled)
			{
				return;
			}
			List<IPopupRendering> newPopupComponents = new List<IPopupRendering>();
			Action<object> <>9__1;
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
							IPopupRendering popupRendering2 = components[i] as IPopupRendering;
							if (popupRendering2 != null)
							{
								newPopupComponents.Add(popupRendering2);
								flag2 |= !popupRendering2.ShouldPropagatePopupRendering();
							}
						}
					}
					List<IAsyncInitializationBehavior> asyncBehaviors = AsyncBehaviorUtils.GetAsyncBehaviors(child);
					if (asyncBehaviors != null)
					{
						foreach (IAsyncInitializationBehavior asyncInitializationBehavior in asyncBehaviors)
						{
							if (child != popupRoot.transform)
							{
								IAsyncInitializationBehavior asyncInitializationBehavior2 = asyncInitializationBehavior;
								Action<object> listener;
								if ((listener = <>9__1) == null)
								{
									listener = (<>9__1 = delegate(object obj)
									{
										foreach (object obj2 in ((Transform)obj))
										{
											Transform objectRoot2 = (Transform)obj2;
											PopupRoot.ApplyPopupRendering(popupRoot, objectRoot2, popupRenderingCache);
										}
									});
								}
								asyncInitializationBehavior2.RegisterReadyListener(listener, child, false);
							}
						}
					}
				}
				return flag && !flag2;
			});
			foreach (IPopupRendering popupRendering in newPopupComponents)
			{
				popupRendering.EnablePopupRendering(popupRoot);
				popupRenderingCache.Add(popupRendering);
			}
		}

		// Token: 0x0600B1A1 RID: 45473 RVA: 0x0036C66C File Offset: 0x0036A86C
		public void EnablePopupRendering(PopupCamera popupCamera)
		{
			this.m_enabled = true;
			this.m_popupCamera = popupCamera;
			this.ApplyPopupRendering(base.transform);
		}

		// Token: 0x0600B1A2 RID: 45474 RVA: 0x0036C688 File Offset: 0x0036A888
		public void ApplyPopupRendering(Transform popupRoot)
		{
			PopupRoot.ApplyPopupRendering(this, popupRoot, this.m_popupComponents);
		}

		// Token: 0x0600B1A3 RID: 45475 RVA: 0x0036C698 File Offset: 0x0036A898
		public void DisablePopupRendering()
		{
			this.m_enabled = false;
			foreach (IPopupRendering popupRendering in this.m_popupComponents)
			{
				if (popupRendering != null && !(popupRendering as UnityEngine.Object == null))
				{
					popupRendering.DisablePopupRendering();
				}
			}
			this.m_popupComponents.Clear();
		}

		// Token: 0x0600B1A4 RID: 45476 RVA: 0x0036C710 File Offset: 0x0036A910
		private void OnDisable()
		{
			this.DisablePopupRendering();
			if (this.OnDisabled != null)
			{
				this.OnDisabled(this);
			}
		}

		// Token: 0x0600B1A5 RID: 45477 RVA: 0x0036C72C File Offset: 0x0036A92C
		private void OnDestroy()
		{
			if (this.OnDestroyed != null)
			{
				this.OnDestroyed(this);
			}
		}

		// Token: 0x040095BE RID: 38334
		private HashSet<IPopupRendering> m_popupComponents = new HashSet<IPopupRendering>();

		// Token: 0x040095BF RID: 38335
		private PopupCamera m_popupCamera;

		// Token: 0x040095C0 RID: 38336
		private bool m_enabled;
	}
}
