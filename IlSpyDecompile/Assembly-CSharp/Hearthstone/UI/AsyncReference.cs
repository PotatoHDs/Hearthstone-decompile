using System;
using Hearthstone.UI.Internal;
using UnityEngine;

namespace Hearthstone.UI
{
	[Serializable]
	public class AsyncReference
	{
		[SerializeField]
		private NestedReference m_nestedReference;

		public UnityEngine.Object Object { get; private set; }

		public bool IsReady { get; private set; }

		private event Action<UnityEngine.Object> m_readyListeners;

		public void RegisterReadyListener<T>(Action<T> listener) where T : UnityEngine.Object
		{
			m_readyListeners += delegate(UnityEngine.Object a)
			{
				listener(a as T);
			};
			Resolve<T>(null);
		}

		private void HandleReady(UnityEngine.Object target)
		{
			IsReady = true;
			Object = target;
			if (this.m_readyListeners != null)
			{
				this.m_readyListeners(Object);
				this.m_readyListeners = null;
			}
		}

		private void Resolve<T>(object unused) where T : UnityEngine.Object
		{
			IAsyncInitializationBehavior behaviorToWaitFor = null;
			UnityEngine.Object target = null;
			Action<INestedReferenceResolver> action = delegate(INestedReferenceResolver resolver)
			{
				if (!resolver.IsReady)
				{
					behaviorToWaitFor = resolver;
				}
			};
			Component component = m_nestedReference.RootObject as Component;
			if (component != null)
			{
				WidgetInstance widgetInstance = (m_nestedReference.RootObject as WidgetInstance) ?? component.GetComponent<WidgetInstance>();
				bool flag = m_nestedReference.TargetObjectIds == null || m_nestedReference.TargetObjectIds.Length == 0;
				if (widgetInstance != null && flag)
				{
					target = widgetInstance;
					action(widgetInstance);
				}
				else
				{
					NestedReference.Resolve(m_nestedReference.RootObject, m_nestedReference.TargetObjectIds, out target, action);
				}
			}
			if (behaviorToWaitFor == null && target != null)
			{
				if (!(target is T))
				{
					Component component2 = target as Component;
					if (component2 != null)
					{
						target = component2.GetComponent(typeof(T));
					}
					if (target == null)
					{
						WidgetInstance widgetInstance2 = component2 as WidgetInstance;
						if (widgetInstance2 != null && widgetInstance2.Widget != null)
						{
							target = widgetInstance2.Widget.GetComponent(typeof(T));
						}
					}
				}
				IAsyncInitializationBehavior asyncInitializationBehavior = target as IAsyncInitializationBehavior;
				if (asyncInitializationBehavior != null && !asyncInitializationBehavior.IsReady)
				{
					behaviorToWaitFor = asyncInitializationBehavior;
				}
			}
			if (behaviorToWaitFor != null)
			{
				behaviorToWaitFor.RegisterReadyListener(Resolve<T>);
			}
			else
			{
				HandleReady(target);
			}
		}
	}
}
