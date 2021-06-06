using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Hearthstone.UI;
using UnityEngine;

public class SceneUtils
{
	public static void SetLayer(GameObject go, int layer, int? ignoredLayer = null)
	{
		if (!ignoredLayer.HasValue || go.layer != ignoredLayer.Value)
		{
			go.layer = layer;
		}
		foreach (Transform item in go.transform)
		{
			SetLayer(item.gameObject, layer, ignoredLayer);
		}
	}

	public static void SetLayer(Component c, int layer)
	{
		SetLayer(c.gameObject, layer);
	}

	public static void SetLayer(GameObject go, GameLayer layer)
	{
		SetLayer(go, (int)layer);
	}

	public static void SetLayer(Component c, GameLayer layer)
	{
		SetLayer(c.gameObject, (int)layer);
	}

	public static void SetInvisibleRenderer(Renderer renderer, bool show, ref Map<Renderer, int> originalLayers)
	{
		if (originalLayers == null)
		{
			originalLayers = new Map<Renderer, int>();
		}
		if (renderer != null)
		{
			int layer = renderer.gameObject.layer;
			int value = layer;
			if (show && layer == 28 && !originalLayers.TryGetValue(renderer, out value))
			{
				value = layer;
			}
			if (!show && layer != 28)
			{
				originalLayers[renderer] = layer;
				value = 28;
			}
			renderer.gameObject.layer = value;
		}
	}

	public static void SetHiddenAndDisableInput(GameObject go, bool disable, ref Map<Renderer, int> originalLayers)
	{
		Map<Renderer, int> tempLayers = originalLayers;
		WalkSelfAndChildren(go.transform, delegate(Transform current)
		{
			Component[] components = current.GetComponents<Component>();
			Renderer renderer = null;
			PegUIElement pegUIElement = null;
			UberText uberText = null;
			Actor actor = null;
			WidgetInstance widgetInstance = null;
			CustomWidgetBehavior customWidgetBehavior = null;
			Component[] array = components;
			foreach (Component component in array)
			{
				if (component is Renderer)
				{
					renderer = component as Renderer;
				}
				else if (component is PegUIElement)
				{
					pegUIElement = component as PegUIElement;
				}
				else if (component is UberText)
				{
					uberText = component as UberText;
				}
				else if (component is Actor)
				{
					actor = component as Actor;
				}
				else if (component is WidgetInstance)
				{
					widgetInstance = component as WidgetInstance;
				}
				else if (component is CustomWidgetBehavior)
				{
					customWidgetBehavior = component as CustomWidgetBehavior;
				}
			}
			if (renderer != null)
			{
				SetInvisibleRenderer(renderer, !disable, ref tempLayers);
			}
			if (pegUIElement != null)
			{
				pegUIElement.SetEnabled(!disable);
			}
			if (uberText != null)
			{
				if (disable)
				{
					uberText.Hide();
				}
				else
				{
					uberText.Show();
				}
			}
			if (actor != null)
			{
				if (disable)
				{
					actor.Hide();
				}
				else
				{
					actor.Show();
				}
			}
			if (customWidgetBehavior != null)
			{
				if (disable)
				{
					customWidgetBehavior.Hide();
				}
				else
				{
					customWidgetBehavior.Show();
				}
			}
			if (widgetInstance != null)
			{
				if (disable)
				{
					widgetInstance.Hide();
				}
				else
				{
					widgetInstance.Show();
				}
			}
			return widgetInstance == null && uberText == null && actor == null && customWidgetBehavior == null;
		});
		originalLayers = tempLayers;
	}

	public static void ReplaceLayer(GameObject parentObject, GameLayer newLayer, GameLayer oldLayer)
	{
		if (parentObject.layer == (int)oldLayer)
		{
			parentObject.layer = (int)newLayer;
		}
		foreach (Transform item in parentObject.transform)
		{
			ReplaceLayer(item.gameObject, newLayer, oldLayer);
		}
	}

	public static string LayerMaskToString(LayerMask mask)
	{
		if ((int)mask == 0)
		{
			return "[NO LAYERS]";
		}
		StringBuilder stringBuilder = new StringBuilder("[");
		foreach (GameLayer value in Enum.GetValues(typeof(GameLayer)))
		{
			if (((int)mask & value.LayerBit()) != 0)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(", ");
			}
		}
		if (stringBuilder.Length == 1)
		{
			return "[NO LAYERS]";
		}
		stringBuilder.Remove(stringBuilder.Length - 2, 2);
		stringBuilder.Append("]");
		return stringBuilder.ToString();
	}

	public static void SetRenderQueue(GameObject go, int renderQueue, bool includeInactive = false)
	{
		Renderer[] componentsInChildren = go.GetComponentsInChildren<Renderer>(includeInactive);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Material material = componentsInChildren[i].GetMaterial();
			if (!(material == null))
			{
				material.renderQueue = renderQueue;
			}
		}
	}

	public static GameObject FindChild(GameObject parentObject, string name)
	{
		if (parentObject.name.Equals(name, StringComparison.OrdinalIgnoreCase))
		{
			return parentObject;
		}
		foreach (Transform item in parentObject.transform)
		{
			GameObject gameObject = FindChild(item.gameObject, name);
			if ((bool)gameObject)
			{
				return gameObject;
			}
		}
		return null;
	}

	public static GameObject FindChildBySubstring(GameObject parentObject, string substr)
	{
		if (parentObject.name.IndexOf(substr, StringComparison.OrdinalIgnoreCase) >= 0)
		{
			return parentObject;
		}
		foreach (Transform item in parentObject.transform)
		{
			GameObject gameObject = FindChildBySubstring(item.gameObject, substr);
			if ((bool)gameObject)
			{
				return gameObject;
			}
		}
		return null;
	}

	public static Transform FindFirstChild(Transform parent)
	{
		IEnumerator enumerator = parent.GetEnumerator();
		try
		{
			if (enumerator.MoveNext())
			{
				return (Transform)enumerator.Current;
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
		return null;
	}

	public static bool IsAncestorOf(GameObject ancestor, GameObject descendant)
	{
		return IsAncestorOf(ancestor.transform, descendant.transform);
	}

	public static bool IsAncestorOf(Component ancestor, Component descendant)
	{
		Transform transform = descendant.transform;
		while (transform != null)
		{
			if (transform == ancestor.transform)
			{
				return true;
			}
			transform = transform.parent;
		}
		return false;
	}

	public static bool IsDescendantOf(GameObject descendant, GameObject ancestor)
	{
		return IsDescendantOf(descendant.transform, ancestor.transform);
	}

	public static bool IsDescendantOf(GameObject descendant, Component ancestor)
	{
		return IsDescendantOf(descendant.transform, ancestor.transform);
	}

	public static bool IsDescendantOf(Component descendant, GameObject ancestor)
	{
		return IsDescendantOf(descendant.transform, ancestor.transform);
	}

	public static bool IsDescendantOf(Component descendant, Component ancestor)
	{
		if (descendant == ancestor)
		{
			return true;
		}
		foreach (Transform item in ancestor.transform)
		{
			if (IsDescendantOf(descendant, item))
			{
				return true;
			}
		}
		return false;
	}

	public static T FindComponentInParents<T>(Component child) where T : Component
	{
		if (child == null)
		{
			return null;
		}
		Transform parent = child.transform.parent;
		while (parent != null)
		{
			T component = parent.GetComponent<T>();
			if ((UnityEngine.Object)component != (UnityEngine.Object)null)
			{
				return component;
			}
			parent = parent.parent;
		}
		return null;
	}

	public static T FindComponentInParents<T>(GameObject child) where T : Component
	{
		if (child == null)
		{
			return null;
		}
		return FindComponentInParents<T>(child.transform);
	}

	public static T FindComponentInThisOrParents<T>(Component start) where T : Component
	{
		if (start == null)
		{
			return null;
		}
		Transform transform = start.transform;
		while (transform != null)
		{
			T component = transform.GetComponent<T>();
			if ((UnityEngine.Object)component != (UnityEngine.Object)null)
			{
				return component;
			}
			transform = transform.parent;
		}
		return null;
	}

	public static T FindComponentInThisOrParents<T>(GameObject start) where T : Component
	{
		if (start == null)
		{
			return null;
		}
		return FindComponentInThisOrParents<T>(start.transform);
	}

	public static T GetComponentInChildrenOnly<T>(GameObject go) where T : Component
	{
		if (go != null)
		{
			foreach (Transform item in go.transform)
			{
				T componentInChildren = item.GetComponentInChildren<T>();
				if ((UnityEngine.Object)componentInChildren != (UnityEngine.Object)null)
				{
					return componentInChildren;
				}
			}
		}
		return null;
	}

	public static T GetComponentInChildrenOnly<T>(Component c) where T : Component
	{
		if (c == null)
		{
			return null;
		}
		return GetComponentInChildrenOnly<T>(c.gameObject);
	}

	public static T[] GetComponentsInChildrenOnly<T>(Component c) where T : Component
	{
		if (c == null)
		{
			return new T[0];
		}
		return GetComponentsInChildrenOnly<T>(c.gameObject);
	}

	public static T[] GetComponentsInChildrenOnly<T>(GameObject go) where T : Component
	{
		return GetComponentsInChildrenOnly<T>(go, includeInactive: false);
	}

	public static T[] GetComponentsInChildrenOnly<T>(Component c, bool includeInactive) where T : Component
	{
		if (c == null)
		{
			return new T[0];
		}
		return GetComponentsInChildrenOnly<T>(c.gameObject, includeInactive);
	}

	public static T[] GetComponentsInChildrenOnly<T>(GameObject go, bool includeInactive) where T : Component
	{
		if (go != null)
		{
			List<T> list = new List<T>();
			foreach (Transform item in go.transform)
			{
				T[] componentsInChildren = item.GetComponentsInChildren<T>(includeInactive);
				list.AddRange(componentsInChildren);
			}
			return list.ToArray();
		}
		return new T[0];
	}

	public static GameObject FindTopParent(Component c)
	{
		Transform transform = c.transform;
		while (transform.parent != null)
		{
			transform = transform.parent;
		}
		return transform.gameObject;
	}

	public static GameObject FindTopParent(GameObject go)
	{
		return FindTopParent(go.transform);
	}

	public static GameObject FindChildByTag(GameObject go, string tag, bool includeInactive = false)
	{
		Transform[] componentsInChildren = go.GetComponentsInChildren<Transform>(includeInactive);
		if (componentsInChildren == null)
		{
			return null;
		}
		Transform[] array = componentsInChildren;
		foreach (Transform transform in array)
		{
			if (transform.CompareTag(tag))
			{
				return transform.gameObject;
			}
		}
		return null;
	}

	public static void EnableRenderers(Component c, bool enable)
	{
		EnableRenderers(c.gameObject, enable);
	}

	public static void EnableRenderers(GameObject go, bool enable)
	{
		EnableRenderers(go, enable, includeInactive: false);
	}

	public static void EnableRenderers(Component c, bool enable, bool includeInactive)
	{
		EnableRenderers(c.gameObject, enable, includeInactive: false);
	}

	public static void EnableRenderers(GameObject go, bool enable, bool includeInactive)
	{
		Renderer[] componentsInChildren = go.GetComponentsInChildren<Renderer>(includeInactive);
		if (componentsInChildren != null)
		{
			Renderer[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = enable;
			}
		}
	}

	public static void EnableColliders(Component c, bool enable)
	{
		EnableColliders(c.gameObject, enable);
	}

	public static void EnableColliders(GameObject go, bool enable)
	{
		Collider[] componentsInChildren = go.GetComponentsInChildren<Collider>();
		if (componentsInChildren != null)
		{
			Collider[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = enable;
			}
		}
	}

	public static void EnableRenderersAndColliders(Component c, bool enable)
	{
		EnableRenderersAndColliders(c.gameObject, enable);
	}

	public static void EnableRenderersAndColliders(GameObject go, bool enable)
	{
		Collider component = go.GetComponent<Collider>();
		if (component != null)
		{
			component.enabled = enable;
		}
		Renderer component2 = go.GetComponent<Renderer>();
		if (component2 != null)
		{
			component2.enabled = enable;
		}
		foreach (Transform item in go.transform)
		{
			EnableRenderersAndColliders(item.gameObject, enable);
		}
	}

	public static void ResizeBoxCollider(GameObject go, Component worldCorner1, Component worldCorner2)
	{
		ResizeBoxCollider(go.GetComponent<Collider>(), worldCorner1.transform.position, worldCorner2.transform.position);
	}

	public static void ResizeBoxCollider(GameObject go, GameObject worldCorner1, Component worldCorner2)
	{
		ResizeBoxCollider(go.GetComponent<Collider>(), worldCorner1.transform.position, worldCorner2.transform.position);
	}

	public static void ResizeBoxCollider(GameObject go, Component worldCorner1, GameObject worldCorner2)
	{
		ResizeBoxCollider(go.GetComponent<Collider>(), worldCorner1.transform.position, worldCorner2.transform.position);
	}

	public static void ResizeBoxCollider(GameObject go, GameObject worldCorner1, GameObject worldCorner2)
	{
		ResizeBoxCollider(go.GetComponent<Collider>(), worldCorner1.transform.position, worldCorner2.transform.position);
	}

	public static void ResizeBoxCollider(Component c, Component worldCorner1, Component worldCorner2)
	{
		ResizeBoxCollider(c, worldCorner1.transform.position, worldCorner2.transform.position);
	}

	public static void ResizeBoxCollider(Component c, GameObject worldCorner1, Component worldCorner2)
	{
		ResizeBoxCollider(c, worldCorner1.transform.position, worldCorner2.transform.position);
	}

	public static void ResizeBoxCollider(Component c, Component worldCorner1, GameObject worldCorner2)
	{
		ResizeBoxCollider(c, worldCorner1.transform.position, worldCorner2.transform.position);
	}

	public static void ResizeBoxCollider(Component c, GameObject worldCorner1, GameObject worldCorner2)
	{
		ResizeBoxCollider(c, worldCorner1.transform.position, worldCorner2.transform.position);
	}

	public static void ResizeBoxCollider(GameObject go, Bounds bounds)
	{
		ResizeBoxCollider(go.GetComponent<Collider>(), bounds.min, bounds.max);
	}

	public static void ResizeBoxCollider(Component c, Bounds bounds)
	{
		ResizeBoxCollider(c.GetComponent<Collider>(), bounds.min, bounds.max);
	}

	public static void ResizeBoxCollider(GameObject go, Vector3 worldCorner1, Vector3 worldCorner2)
	{
		ResizeBoxCollider(go.GetComponent<Collider>(), worldCorner1, worldCorner2);
	}

	public static void ResizeBoxCollider(Component c, Vector3 worldCorner1, Vector3 worldCorner2)
	{
		Vector3 lhs = c.transform.InverseTransformPoint(worldCorner1);
		Vector3 rhs = c.transform.InverseTransformPoint(worldCorner2);
		Vector3 vector = Vector3.Min(lhs, rhs);
		Vector3 vector2 = Vector3.Max(lhs, rhs);
		BoxCollider component = c.GetComponent<BoxCollider>();
		component.center = 0.5f * (vector + vector2);
		component.size = vector2 - vector;
	}

	public static Transform CreateBone(GameObject template)
	{
		GameObject gameObject = new GameObject($"{template.name}Bone");
		gameObject.transform.parent = template.transform.parent;
		TransformUtil.CopyLocal(gameObject, template);
		return gameObject.transform;
	}

	public static Transform CreateBone(Component template)
	{
		return CreateBone(template.gameObject);
	}

	public static void SetHideFlags(UnityEngine.Object obj, HideFlags flags)
	{
	}

	public static void WalkSelfAndChildren(Transform transform, Func<Transform, bool> action)
	{
		if (action(transform))
		{
			int num = 0;
			while (transform != null && num < transform.childCount)
			{
				WalkSelfAndChildren(transform.GetChild(num), action);
				num++;
			}
		}
	}

	public static T GetComponentOnSelfOrParent<T>(Transform transform) where T : Component
	{
		Transform transform2 = transform;
		while (transform2 != null)
		{
			T component = transform2.GetComponent<T>();
			if ((UnityEngine.Object)component != (UnityEngine.Object)null)
			{
				return component;
			}
			transform2 = transform2.parent;
		}
		return null;
	}

	public static void DestroyChildren(Transform parent)
	{
		while (parent.childCount > 0)
		{
			UnityEngine.Object.DestroyImmediate(parent.GetChild(0).gameObject);
		}
	}

	public static void SafeDestroyChildren(Transform parent)
	{
		Transform[] array = new Transform[parent.childCount];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = parent.GetChild(i);
		}
		Transform[] array2 = array;
		for (int j = 0; j < array2.Length; j++)
		{
			SafeDestroy(array2[j].gameObject);
		}
	}

	public static void SafeDestroy(GameObject go)
	{
		UnityEngine.Object.DestroyImmediate(go);
	}
}
