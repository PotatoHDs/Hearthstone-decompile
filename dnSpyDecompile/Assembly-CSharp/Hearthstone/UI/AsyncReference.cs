using System;
using Hearthstone.UI.Internal;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02000FEE RID: 4078
	[Serializable]
	public class AsyncReference
	{
		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x0600B15C RID: 45404 RVA: 0x0036B729 File Offset: 0x00369929
		// (set) Token: 0x0600B15B RID: 45403 RVA: 0x0036B720 File Offset: 0x00369920
		public UnityEngine.Object Object { get; private set; }

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x0600B15E RID: 45406 RVA: 0x0036B73A File Offset: 0x0036993A
		// (set) Token: 0x0600B15D RID: 45405 RVA: 0x0036B731 File Offset: 0x00369931
		public bool IsReady { get; private set; }

		// Token: 0x140000A7 RID: 167
		// (add) Token: 0x0600B15F RID: 45407 RVA: 0x0036B744 File Offset: 0x00369944
		// (remove) Token: 0x0600B160 RID: 45408 RVA: 0x0036B77C File Offset: 0x0036997C
		private event Action<UnityEngine.Object> m_readyListeners;

		// Token: 0x0600B161 RID: 45409 RVA: 0x0036B7B4 File Offset: 0x003699B4
		public void RegisterReadyListener<T>(Action<T> listener) where T : UnityEngine.Object
		{
			this.m_readyListeners += delegate(UnityEngine.Object a)
			{
				listener(a as T);
			};
			this.Resolve<T>(null);
		}

		// Token: 0x0600B162 RID: 45410 RVA: 0x0036B7E7 File Offset: 0x003699E7
		private void HandleReady(UnityEngine.Object target)
		{
			this.IsReady = true;
			this.Object = target;
			if (this.m_readyListeners != null)
			{
				this.m_readyListeners(this.Object);
				this.m_readyListeners = null;
			}
		}

		// Token: 0x0600B163 RID: 45411 RVA: 0x0036B818 File Offset: 0x00369A18
		private void Resolve<T>(object unused) where T : UnityEngine.Object
		{
			IAsyncInitializationBehavior behaviorToWaitFor = null;
			UnityEngine.Object @object = null;
			Action<INestedReferenceResolver> action = delegate(INestedReferenceResolver resolver)
			{
				if (!resolver.IsReady)
				{
					behaviorToWaitFor = resolver;
				}
			};
			Component component = this.m_nestedReference.RootObject as Component;
			if (component != null)
			{
				WidgetInstance widgetInstance = (this.m_nestedReference.RootObject as WidgetInstance) ?? component.GetComponent<WidgetInstance>();
				bool flag = this.m_nestedReference.TargetObjectIds == null || this.m_nestedReference.TargetObjectIds.Length == 0;
				if (widgetInstance != null && flag)
				{
					@object = widgetInstance;
					action(widgetInstance);
				}
				else
				{
					NestedReference.Resolve(this.m_nestedReference.RootObject, this.m_nestedReference.TargetObjectIds, out @object, action);
				}
			}
			if (behaviorToWaitFor == null && @object != null)
			{
				if (!(@object is T))
				{
					Component component2 = @object as Component;
					if (component2 != null)
					{
						@object = component2.GetComponent(typeof(T));
					}
					if (@object == null)
					{
						WidgetInstance widgetInstance2 = component2 as WidgetInstance;
						if (widgetInstance2 != null && widgetInstance2.Widget != null)
						{
							@object = widgetInstance2.Widget.GetComponent(typeof(T));
						}
					}
				}
				IAsyncInitializationBehavior asyncInitializationBehavior = @object as IAsyncInitializationBehavior;
				if (asyncInitializationBehavior != null && !asyncInitializationBehavior.IsReady)
				{
					behaviorToWaitFor = asyncInitializationBehavior;
				}
			}
			if (behaviorToWaitFor != null)
			{
				behaviorToWaitFor.RegisterReadyListener(new Action<object>(this.Resolve<T>), null, true);
				return;
			}
			this.HandleReady(@object);
		}

		// Token: 0x0400959E RID: 38302
		[SerializeField]
		private NestedReference m_nestedReference;
	}
}
