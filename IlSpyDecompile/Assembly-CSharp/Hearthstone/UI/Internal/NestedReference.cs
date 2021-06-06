using System;
using System.Reflection;
using UnityEngine;

namespace Hearthstone.UI.Internal
{
	[Serializable]
	public struct NestedReference
	{
		public enum TargetTypes
		{
			Unknown,
			String,
			Color,
			Float,
			Bool,
			Int,
			Material,
			Double,
			Long,
			Component,
			Texture,
			Vector2,
			Vector3,
			Vector4,
			Enum,
			Mesh
		}

		public enum Scope
		{
			Default,
			Children,
			Foreign
		}

		[SerializeField]
		private long[] m_targetObjectIds;

		[SerializeField]
		private UnityEngine.Object m_targetObject;

		[SerializeField]
		private string m_targetPath;

		[SerializeField]
		private TargetTypes m_targetType;

		[SerializeField]
		private string m_stringType;

		private Action<object> m_readyListener;

		private object m_payload;

		public UnityEngine.Object RootObject => m_targetObject;

		public long[] TargetObjectIds => m_targetObjectIds;

		public TargetTypes TargetType => m_targetType;

		public bool Resolve(out NestedReferenceTargetInfo targetInfo)
		{
			return Resolve(m_targetObject, m_targetObjectIds, m_targetPath, out targetInfo);
		}

		public bool Resolve(out GameObject gameObject)
		{
			gameObject = null;
			if (!Resolve(out NestedReferenceTargetInfo targetInfo))
			{
				return false;
			}
			Component component = targetInfo.Target as Component;
			if (component != null)
			{
				gameObject = component.gameObject;
				return true;
			}
			gameObject = targetInfo.Target as GameObject;
			return gameObject != null;
		}

		public static bool Resolve(UnityEngine.Object root, long[] objectIds, string propertyPath, out NestedReferenceTargetInfo property, Action<INestedReferenceResolver> visitor = null)
		{
			property = default(NestedReferenceTargetInfo);
			if (!Resolve(root, objectIds, out property.Target, visitor))
			{
				return false;
			}
			if (string.IsNullOrEmpty(propertyPath))
			{
				return true;
			}
			IDynamicPropertyResolver dynamicPropertyResolver = DynamicPropertyResolvers.TryGetResolver(property.Target);
			if (dynamicPropertyResolver != null)
			{
				if (dynamicPropertyResolver.GetDynamicPropertyValue(propertyPath, out var _))
				{
					property.Path = propertyPath;
					return true;
				}
				return false;
			}
			string[] array = propertyPath.Split('.');
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

		public bool TryGetValueFromTarget<T>(out T value)
		{
			value = default(T);
			if (!Resolve(m_targetObject, m_targetObjectIds, out var target))
			{
				return false;
			}
			IDynamicPropertyResolver dynamicPropertyResolver = DynamicPropertyResolvers.TryGetResolver(target);
			if (dynamicPropertyResolver != null && dynamicPropertyResolver.GetDynamicPropertyValue(m_targetPath, out var value2))
			{
				if (value2 is T)
				{
					value = (T)value2;
					return true;
				}
				Log.All.PrintError("TryGetPropertyValue on NestedReference invalid type!");
				return false;
			}
			string[] array = m_targetPath.Split('.');
			Type type = target.GetType();
			object obj = target;
			for (int i = 0; i < array.Length; i++)
			{
				PropertyInfo property = type.GetProperty(array[i], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (!(property != null))
				{
					continue;
				}
				obj = property.GetValue(obj);
				if (i == array.Length - 1)
				{
					if (obj is T)
					{
						value = (T)obj;
						return true;
					}
					Log.UIStatus.PrintError("TryGetPropertyValue on NestedReference invalid type!");
				}
				type = property.PropertyType;
			}
			return false;
		}

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
			for (int i = 0; i < objectIds.Length; i++)
			{
				if (nestedReferenceResolver == null)
				{
					break;
				}
				visitor?.Invoke(nestedReferenceResolver);
				target = nestedReferenceResolver.GetComponentById(objectIds[i]);
				if (target == null)
				{
					return false;
				}
				nestedReferenceResolver = target as INestedReferenceResolver;
			}
			return target != null;
		}

		public void RegisterReadyListener(Action<object> action, object payload = null)
		{
			m_readyListener = action;
			m_payload = payload;
			RegisterReadyHandler(null);
		}

		public void RemoveReadyOrInactiveListener()
		{
			m_readyListener = null;
			m_payload = null;
		}

		private void RegisterReadyHandler(object unused)
		{
			IAsyncInitializationBehavior behaviorToWaitFor = null;
			if (RootObject == null || TargetObjectIds == null || TargetObjectIds.Length == 0)
			{
				if (m_readyListener != null)
				{
					m_readyListener(m_payload);
				}
				return;
			}
			Resolve(RootObject, TargetObjectIds, out var target, delegate(INestedReferenceResolver resolver)
			{
				if (!resolver.IsReady)
				{
					behaviorToWaitFor = resolver;
				}
			});
			if (behaviorToWaitFor == null && target != null)
			{
				IAsyncInitializationBehavior asyncInitializationBehavior = target as IAsyncInitializationBehavior;
				if (asyncInitializationBehavior != null && !asyncInitializationBehavior.IsReady)
				{
					behaviorToWaitFor = asyncInitializationBehavior;
				}
			}
			if (behaviorToWaitFor != null)
			{
				behaviorToWaitFor.RegisterReadyListener(RegisterReadyHandler);
			}
			else if (m_readyListener != null)
			{
				m_readyListener(m_payload);
			}
		}
	}
}
