using System;
using System.Reflection;
using UnityEngine;

namespace Hearthstone.UI.Internal
{
	// Token: 0x02001055 RID: 4181
	[Serializable]
	public struct NestedReference
	{
		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x0600B51B RID: 46363 RVA: 0x0037B4D2 File Offset: 0x003796D2
		public UnityEngine.Object RootObject
		{
			get
			{
				return this.m_targetObject;
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x0600B51C RID: 46364 RVA: 0x0037B4DA File Offset: 0x003796DA
		public long[] TargetObjectIds
		{
			get
			{
				return this.m_targetObjectIds;
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x0600B51D RID: 46365 RVA: 0x0037B4E2 File Offset: 0x003796E2
		public NestedReference.TargetTypes TargetType
		{
			get
			{
				return this.m_targetType;
			}
		}

		// Token: 0x0600B51E RID: 46366 RVA: 0x0037B4EA File Offset: 0x003796EA
		public bool Resolve(out NestedReferenceTargetInfo targetInfo)
		{
			return NestedReference.Resolve(this.m_targetObject, this.m_targetObjectIds, this.m_targetPath, out targetInfo, null);
		}

		// Token: 0x0600B51F RID: 46367 RVA: 0x0037B508 File Offset: 0x00379708
		public bool Resolve(out GameObject gameObject)
		{
			gameObject = null;
			NestedReferenceTargetInfo nestedReferenceTargetInfo;
			if (!this.Resolve(out nestedReferenceTargetInfo))
			{
				return false;
			}
			Component component = nestedReferenceTargetInfo.Target as Component;
			if (component != null)
			{
				gameObject = component.gameObject;
				return true;
			}
			gameObject = (nestedReferenceTargetInfo.Target as GameObject);
			return gameObject != null;
		}

		// Token: 0x0600B520 RID: 46368 RVA: 0x0037B558 File Offset: 0x00379758
		public static bool Resolve(UnityEngine.Object root, long[] objectIds, string propertyPath, out NestedReferenceTargetInfo property, Action<INestedReferenceResolver> visitor = null)
		{
			property = default(NestedReferenceTargetInfo);
			if (!NestedReference.Resolve(root, objectIds, out property.Target, visitor))
			{
				return false;
			}
			if (string.IsNullOrEmpty(propertyPath))
			{
				return true;
			}
			IDynamicPropertyResolver dynamicPropertyResolver = DynamicPropertyResolvers.TryGetResolver(property.Target);
			if (dynamicPropertyResolver == null)
			{
				string[] array = propertyPath.Split(new char[]
				{
					'.'
				});
				Type type = property.Target.GetType();
				for (int i = 0; i < array.Length; i++)
				{
					PropertyInfo property2 = type.GetProperty(array[i], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					if (property2 != null)
					{
						if (i == array.Length - 1)
						{
							property.Property = property2;
							return true;
						}
						type = property2.PropertyType;
					}
				}
				return false;
			}
			object obj;
			if (dynamicPropertyResolver.GetDynamicPropertyValue(propertyPath, out obj))
			{
				property.Path = propertyPath;
				return true;
			}
			return false;
		}

		// Token: 0x0600B521 RID: 46369 RVA: 0x0037B614 File Offset: 0x00379814
		public bool TryGetValueFromTarget<T>(out T value)
		{
			value = default(T);
			UnityEngine.Object @object;
			if (!NestedReference.Resolve(this.m_targetObject, this.m_targetObjectIds, out @object, null))
			{
				return false;
			}
			IDynamicPropertyResolver dynamicPropertyResolver = DynamicPropertyResolvers.TryGetResolver(@object);
			object obj;
			if (dynamicPropertyResolver == null || !dynamicPropertyResolver.GetDynamicPropertyValue(this.m_targetPath, out obj))
			{
				string[] array = this.m_targetPath.Split(new char[]
				{
					'.'
				});
				Type type = @object.GetType();
				object obj2 = @object;
				for (int i = 0; i < array.Length; i++)
				{
					PropertyInfo property = type.GetProperty(array[i], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					if (property != null)
					{
						obj2 = property.GetValue(obj2);
						if (i == array.Length - 1)
						{
							if (obj2 is T)
							{
								value = (T)((object)obj2);
								return true;
							}
							Log.UIStatus.PrintError("TryGetPropertyValue on NestedReference invalid type!", Array.Empty<object>());
						}
						type = property.PropertyType;
					}
				}
				return false;
			}
			if (obj is T)
			{
				value = (T)((object)obj);
				return true;
			}
			Log.All.PrintError("TryGetPropertyValue on NestedReference invalid type!", Array.Empty<object>());
			return false;
		}

		// Token: 0x0600B522 RID: 46370 RVA: 0x0037B71C File Offset: 0x0037991C
		public static bool Resolve(UnityEngine.Object root, long[] objectIds, out UnityEngine.Object target, Action<INestedReferenceResolver> visitor = null)
		{
			target = null;
			if (root == null)
			{
				return false;
			}
			if (objectIds == null || objectIds.Length == 0)
			{
				target = root;
				return true;
			}
			INestedReferenceResolver nestedReferenceResolver = root as INestedReferenceResolver;
			int num = 0;
			while (num < objectIds.Length && nestedReferenceResolver != null)
			{
				if (visitor != null)
				{
					visitor(nestedReferenceResolver);
				}
				target = nestedReferenceResolver.GetComponentById(objectIds[num]);
				if (target == null)
				{
					return false;
				}
				nestedReferenceResolver = (target as INestedReferenceResolver);
				num++;
			}
			return target != null;
		}

		// Token: 0x0600B523 RID: 46371 RVA: 0x0037B78C File Offset: 0x0037998C
		public void RegisterReadyListener(Action<object> action, object payload = null)
		{
			this.m_readyListener = action;
			this.m_payload = payload;
			this.RegisterReadyHandler(null);
		}

		// Token: 0x0600B524 RID: 46372 RVA: 0x0037B7A3 File Offset: 0x003799A3
		public void RemoveReadyOrInactiveListener()
		{
			this.m_readyListener = null;
			this.m_payload = null;
		}

		// Token: 0x0600B525 RID: 46373 RVA: 0x0037B7B4 File Offset: 0x003799B4
		private void RegisterReadyHandler(object unused)
		{
			IAsyncInitializationBehavior behaviorToWaitFor = null;
			if (this.RootObject == null || this.TargetObjectIds == null || this.TargetObjectIds.Length == 0)
			{
				if (this.m_readyListener != null)
				{
					this.m_readyListener(this.m_payload);
				}
				return;
			}
			UnityEngine.Object @object;
			NestedReference.Resolve(this.RootObject, this.TargetObjectIds, out @object, delegate(INestedReferenceResolver resolver)
			{
				if (!resolver.IsReady)
				{
					behaviorToWaitFor = resolver;
				}
			});
			if (behaviorToWaitFor == null && @object != null)
			{
				IAsyncInitializationBehavior asyncInitializationBehavior = @object as IAsyncInitializationBehavior;
				if (asyncInitializationBehavior != null && !asyncInitializationBehavior.IsReady)
				{
					behaviorToWaitFor = asyncInitializationBehavior;
				}
			}
			if (behaviorToWaitFor != null)
			{
				behaviorToWaitFor.RegisterReadyListener(new Action<object>(this.RegisterReadyHandler), null, true);
				return;
			}
			if (this.m_readyListener != null)
			{
				this.m_readyListener(this.m_payload);
			}
		}

		// Token: 0x0400971C RID: 38684
		[SerializeField]
		private long[] m_targetObjectIds;

		// Token: 0x0400971D RID: 38685
		[SerializeField]
		private UnityEngine.Object m_targetObject;

		// Token: 0x0400971E RID: 38686
		[SerializeField]
		private string m_targetPath;

		// Token: 0x0400971F RID: 38687
		[SerializeField]
		private NestedReference.TargetTypes m_targetType;

		// Token: 0x04009720 RID: 38688
		[SerializeField]
		private string m_stringType;

		// Token: 0x04009721 RID: 38689
		private Action<object> m_readyListener;

		// Token: 0x04009722 RID: 38690
		private object m_payload;

		// Token: 0x02002865 RID: 10341
		public enum TargetTypes
		{
			// Token: 0x0400F992 RID: 63890
			Unknown,
			// Token: 0x0400F993 RID: 63891
			String,
			// Token: 0x0400F994 RID: 63892
			Color,
			// Token: 0x0400F995 RID: 63893
			Float,
			// Token: 0x0400F996 RID: 63894
			Bool,
			// Token: 0x0400F997 RID: 63895
			Int,
			// Token: 0x0400F998 RID: 63896
			Material,
			// Token: 0x0400F999 RID: 63897
			Double,
			// Token: 0x0400F99A RID: 63898
			Long,
			// Token: 0x0400F99B RID: 63899
			Component,
			// Token: 0x0400F99C RID: 63900
			Texture,
			// Token: 0x0400F99D RID: 63901
			Vector2,
			// Token: 0x0400F99E RID: 63902
			Vector3,
			// Token: 0x0400F99F RID: 63903
			Vector4,
			// Token: 0x0400F9A0 RID: 63904
			Enum,
			// Token: 0x0400F9A1 RID: 63905
			Mesh
		}

		// Token: 0x02002866 RID: 10342
		public enum Scope
		{
			// Token: 0x0400F9A3 RID: 63907
			Default,
			// Token: 0x0400F9A4 RID: 63908
			Children,
			// Token: 0x0400F9A5 RID: 63909
			Foreign
		}
	}
}
