using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x020009F0 RID: 2544
public class SceneUtils
{
	// Token: 0x0600899D RID: 35229 RVA: 0x002C25FC File Offset: 0x002C07FC
	public static void SetLayer(GameObject go, int layer, int? ignoredLayer = null)
	{
		if (ignoredLayer == null || go.layer != ignoredLayer.Value)
		{
			go.layer = layer;
		}
		foreach (object obj in go.transform)
		{
			SceneUtils.SetLayer(((Transform)obj).gameObject, layer, ignoredLayer);
		}
	}

	// Token: 0x0600899E RID: 35230 RVA: 0x002C2678 File Offset: 0x002C0878
	public static void SetLayer(Component c, int layer)
	{
		SceneUtils.SetLayer(c.gameObject, layer, null);
	}

	// Token: 0x0600899F RID: 35231 RVA: 0x002C269C File Offset: 0x002C089C
	public static void SetLayer(GameObject go, GameLayer layer)
	{
		SceneUtils.SetLayer(go, (int)layer, null);
	}

	// Token: 0x060089A0 RID: 35232 RVA: 0x002C26BC File Offset: 0x002C08BC
	public static void SetLayer(Component c, GameLayer layer)
	{
		SceneUtils.SetLayer(c.gameObject, (int)layer, null);
	}

	// Token: 0x060089A1 RID: 35233 RVA: 0x002C26E0 File Offset: 0x002C08E0
	public static void SetInvisibleRenderer(Renderer renderer, bool show, ref Map<Renderer, int> originalLayers)
	{
		if (originalLayers == null)
		{
			originalLayers = new Map<Renderer, int>();
		}
		if (renderer != null)
		{
			int layer = renderer.gameObject.layer;
			int layer2 = layer;
			if (show && layer == 28 && !originalLayers.TryGetValue(renderer, out layer2))
			{
				layer2 = layer;
			}
			if (!show && layer != 28)
			{
				originalLayers[renderer] = layer;
				layer2 = 28;
			}
			renderer.gameObject.layer = layer2;
		}
	}

	// Token: 0x060089A2 RID: 35234 RVA: 0x002C2748 File Offset: 0x002C0948
	public static void SetHiddenAndDisableInput(GameObject go, bool disable, ref Map<Renderer, int> originalLayers)
	{
		Map<Renderer, int> tempLayers = originalLayers;
		SceneUtils.WalkSelfAndChildren(go.transform, delegate(Transform current)
		{
			Component[] components = current.GetComponents<Component>();
			Renderer renderer = null;
			PegUIElement pegUIElement = null;
			UberText uberText = null;
			Actor actor = null;
			WidgetInstance widgetInstance = null;
			CustomWidgetBehavior customWidgetBehavior = null;
			foreach (Component component in components)
			{
				if (component is Renderer)
				{
					renderer = (component as Renderer);
				}
				else if (component is PegUIElement)
				{
					pegUIElement = (component as PegUIElement);
				}
				else if (component is UberText)
				{
					uberText = (component as UberText);
				}
				else if (component is Actor)
				{
					actor = (component as Actor);
				}
				else if (component is WidgetInstance)
				{
					widgetInstance = (component as WidgetInstance);
				}
				else if (component is CustomWidgetBehavior)
				{
					customWidgetBehavior = (component as CustomWidgetBehavior);
				}
			}
			if (renderer != null)
			{
				SceneUtils.SetInvisibleRenderer(renderer, !disable, ref tempLayers);
			}
			if (pegUIElement != null)
			{
				pegUIElement.SetEnabled(!disable, false);
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

	// Token: 0x060089A3 RID: 35235 RVA: 0x002C278C File Offset: 0x002C098C
	public static void ReplaceLayer(GameObject parentObject, GameLayer newLayer, GameLayer oldLayer)
	{
		if (parentObject.layer == (int)oldLayer)
		{
			parentObject.layer = (int)newLayer;
		}
		foreach (object obj in parentObject.transform)
		{
			SceneUtils.ReplaceLayer(((Transform)obj).gameObject, newLayer, oldLayer);
		}
	}

	// Token: 0x060089A4 RID: 35236 RVA: 0x002C27FC File Offset: 0x002C09FC
	public static string LayerMaskToString(LayerMask mask)
	{
		if (mask == 0)
		{
			return "[NO LAYERS]";
		}
		StringBuilder stringBuilder = new StringBuilder("[");
		foreach (object obj in Enum.GetValues(typeof(GameLayer)))
		{
			GameLayer gameLayer = (GameLayer)obj;
			if ((mask & gameLayer.LayerBit()) != 0)
			{
				stringBuilder.Append(gameLayer);
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

	// Token: 0x060089A5 RID: 35237 RVA: 0x002C28CC File Offset: 0x002C0ACC
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

	// Token: 0x060089A6 RID: 35238 RVA: 0x002C2908 File Offset: 0x002C0B08
	public static GameObject FindChild(GameObject parentObject, string name)
	{
		if (parentObject.name.Equals(name, StringComparison.OrdinalIgnoreCase))
		{
			return parentObject;
		}
		foreach (object obj in parentObject.transform)
		{
			GameObject gameObject = SceneUtils.FindChild(((Transform)obj).gameObject, name);
			if (gameObject)
			{
				return gameObject;
			}
		}
		return null;
	}

	// Token: 0x060089A7 RID: 35239 RVA: 0x002C2988 File Offset: 0x002C0B88
	public static GameObject FindChildBySubstring(GameObject parentObject, string substr)
	{
		if (parentObject.name.IndexOf(substr, StringComparison.OrdinalIgnoreCase) >= 0)
		{
			return parentObject;
		}
		foreach (object obj in parentObject.transform)
		{
			GameObject gameObject = SceneUtils.FindChildBySubstring(((Transform)obj).gameObject, substr);
			if (gameObject)
			{
				return gameObject;
			}
		}
		return null;
	}

	// Token: 0x060089A8 RID: 35240 RVA: 0x002C2A08 File Offset: 0x002C0C08
	public static Transform FindFirstChild(Transform parent)
	{
		using (IEnumerator enumerator = parent.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				return (Transform)enumerator.Current;
			}
		}
		return null;
	}

	// Token: 0x060089A9 RID: 35241 RVA: 0x002C2A5C File Offset: 0x002C0C5C
	public static bool IsAncestorOf(GameObject ancestor, GameObject descendant)
	{
		return SceneUtils.IsAncestorOf(ancestor.transform, descendant.transform);
	}

	// Token: 0x060089AA RID: 35242 RVA: 0x002C2A70 File Offset: 0x002C0C70
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

	// Token: 0x060089AB RID: 35243 RVA: 0x002C2AA7 File Offset: 0x002C0CA7
	public static bool IsDescendantOf(GameObject descendant, GameObject ancestor)
	{
		return SceneUtils.IsDescendantOf(descendant.transform, ancestor.transform);
	}

	// Token: 0x060089AC RID: 35244 RVA: 0x002C2ABA File Offset: 0x002C0CBA
	public static bool IsDescendantOf(GameObject descendant, Component ancestor)
	{
		return SceneUtils.IsDescendantOf(descendant.transform, ancestor.transform);
	}

	// Token: 0x060089AD RID: 35245 RVA: 0x002C2ACD File Offset: 0x002C0CCD
	public static bool IsDescendantOf(Component descendant, GameObject ancestor)
	{
		return SceneUtils.IsDescendantOf(descendant.transform, ancestor.transform);
	}

	// Token: 0x060089AE RID: 35246 RVA: 0x002C2AE0 File Offset: 0x002C0CE0
	public static bool IsDescendantOf(Component descendant, Component ancestor)
	{
		if (descendant == ancestor)
		{
			return true;
		}
		foreach (object obj in ancestor.transform)
		{
			Transform ancestor2 = (Transform)obj;
			if (SceneUtils.IsDescendantOf(descendant, ancestor2))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060089AF RID: 35247 RVA: 0x002C2B50 File Offset: 0x002C0D50
	public static T FindComponentInParents<T>(Component child) where T : Component
	{
		if (child == null)
		{
			return default(T);
		}
		Transform parent = child.transform.parent;
		while (parent != null)
		{
			T component = parent.GetComponent<T>();
			if (component != null)
			{
				return component;
			}
			parent = parent.parent;
		}
		return default(T);
	}

	// Token: 0x060089B0 RID: 35248 RVA: 0x002C2BB0 File Offset: 0x002C0DB0
	public static T FindComponentInParents<T>(GameObject child) where T : Component
	{
		if (child == null)
		{
			return default(T);
		}
		return SceneUtils.FindComponentInParents<T>(child.transform);
	}

	// Token: 0x060089B1 RID: 35249 RVA: 0x002C2BDC File Offset: 0x002C0DDC
	public static T FindComponentInThisOrParents<T>(Component start) where T : Component
	{
		if (start == null)
		{
			return default(T);
		}
		Transform transform = start.transform;
		while (transform != null)
		{
			T component = transform.GetComponent<T>();
			if (component != null)
			{
				return component;
			}
			transform = transform.parent;
		}
		return default(T);
	}

	// Token: 0x060089B2 RID: 35250 RVA: 0x002C2C38 File Offset: 0x002C0E38
	public static T FindComponentInThisOrParents<T>(GameObject start) where T : Component
	{
		if (start == null)
		{
			return default(T);
		}
		return SceneUtils.FindComponentInThisOrParents<T>(start.transform);
	}

	// Token: 0x060089B3 RID: 35251 RVA: 0x002C2C64 File Offset: 0x002C0E64
	public static T GetComponentInChildrenOnly<T>(GameObject go) where T : Component
	{
		if (go != null)
		{
			foreach (object obj in go.transform)
			{
				T componentInChildren = ((Transform)obj).GetComponentInChildren<T>();
				if (componentInChildren != null)
				{
					return componentInChildren;
				}
			}
		}
		return default(T);
	}

	// Token: 0x060089B4 RID: 35252 RVA: 0x002C2CE4 File Offset: 0x002C0EE4
	public static T GetComponentInChildrenOnly<T>(Component c) where T : Component
	{
		if (c == null)
		{
			return default(T);
		}
		return SceneUtils.GetComponentInChildrenOnly<T>(c.gameObject);
	}

	// Token: 0x060089B5 RID: 35253 RVA: 0x002C2D0F File Offset: 0x002C0F0F
	public static T[] GetComponentsInChildrenOnly<T>(Component c) where T : Component
	{
		if (c == null)
		{
			return new T[0];
		}
		return SceneUtils.GetComponentsInChildrenOnly<T>(c.gameObject);
	}

	// Token: 0x060089B6 RID: 35254 RVA: 0x002C2D2C File Offset: 0x002C0F2C
	public static T[] GetComponentsInChildrenOnly<T>(GameObject go) where T : Component
	{
		return SceneUtils.GetComponentsInChildrenOnly<T>(go, false);
	}

	// Token: 0x060089B7 RID: 35255 RVA: 0x002C2D35 File Offset: 0x002C0F35
	public static T[] GetComponentsInChildrenOnly<T>(Component c, bool includeInactive) where T : Component
	{
		if (c == null)
		{
			return new T[0];
		}
		return SceneUtils.GetComponentsInChildrenOnly<T>(c.gameObject, includeInactive);
	}

	// Token: 0x060089B8 RID: 35256 RVA: 0x002C2D54 File Offset: 0x002C0F54
	public static T[] GetComponentsInChildrenOnly<T>(GameObject go, bool includeInactive) where T : Component
	{
		if (go != null)
		{
			List<T> list = new List<T>();
			foreach (object obj in go.transform)
			{
				T[] componentsInChildren = ((Transform)obj).GetComponentsInChildren<T>(includeInactive);
				list.AddRange(componentsInChildren);
			}
			return list.ToArray();
		}
		return new T[0];
	}

	// Token: 0x060089B9 RID: 35257 RVA: 0x002C2DD0 File Offset: 0x002C0FD0
	public static GameObject FindTopParent(Component c)
	{
		Transform transform = c.transform;
		while (transform.parent != null)
		{
			transform = transform.parent;
		}
		return transform.gameObject;
	}

	// Token: 0x060089BA RID: 35258 RVA: 0x002C2E01 File Offset: 0x002C1001
	public static GameObject FindTopParent(GameObject go)
	{
		return SceneUtils.FindTopParent(go.transform);
	}

	// Token: 0x060089BB RID: 35259 RVA: 0x002C2E10 File Offset: 0x002C1010
	public static GameObject FindChildByTag(GameObject go, string tag, bool includeInactive = false)
	{
		Transform[] componentsInChildren = go.GetComponentsInChildren<Transform>(includeInactive);
		if (componentsInChildren == null)
		{
			return null;
		}
		foreach (Transform transform in componentsInChildren)
		{
			if (transform.CompareTag(tag))
			{
				return transform.gameObject;
			}
		}
		return null;
	}

	// Token: 0x060089BC RID: 35260 RVA: 0x002C2E4F File Offset: 0x002C104F
	public static void EnableRenderers(Component c, bool enable)
	{
		SceneUtils.EnableRenderers(c.gameObject, enable);
	}

	// Token: 0x060089BD RID: 35261 RVA: 0x002C2E5D File Offset: 0x002C105D
	public static void EnableRenderers(GameObject go, bool enable)
	{
		SceneUtils.EnableRenderers(go, enable, false);
	}

	// Token: 0x060089BE RID: 35262 RVA: 0x002C2E67 File Offset: 0x002C1067
	public static void EnableRenderers(Component c, bool enable, bool includeInactive)
	{
		SceneUtils.EnableRenderers(c.gameObject, enable, false);
	}

	// Token: 0x060089BF RID: 35263 RVA: 0x002C2E78 File Offset: 0x002C1078
	public static void EnableRenderers(GameObject go, bool enable, bool includeInactive)
	{
		Renderer[] componentsInChildren = go.GetComponentsInChildren<Renderer>(includeInactive);
		if (componentsInChildren == null)
		{
			return;
		}
		Renderer[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = enable;
		}
	}

	// Token: 0x060089C0 RID: 35264 RVA: 0x002C2EAA File Offset: 0x002C10AA
	public static void EnableColliders(Component c, bool enable)
	{
		SceneUtils.EnableColliders(c.gameObject, enable);
	}

	// Token: 0x060089C1 RID: 35265 RVA: 0x002C2EB8 File Offset: 0x002C10B8
	public static void EnableColliders(GameObject go, bool enable)
	{
		Collider[] componentsInChildren = go.GetComponentsInChildren<Collider>();
		if (componentsInChildren == null)
		{
			return;
		}
		Collider[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = enable;
		}
	}

	// Token: 0x060089C2 RID: 35266 RVA: 0x002C2EE9 File Offset: 0x002C10E9
	public static void EnableRenderersAndColliders(Component c, bool enable)
	{
		SceneUtils.EnableRenderersAndColliders(c.gameObject, enable);
	}

	// Token: 0x060089C3 RID: 35267 RVA: 0x002C2EF8 File Offset: 0x002C10F8
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
		foreach (object obj in go.transform)
		{
			SceneUtils.EnableRenderersAndColliders(((Transform)obj).gameObject, enable);
		}
	}

	// Token: 0x060089C4 RID: 35268 RVA: 0x002C2F84 File Offset: 0x002C1184
	public static void ResizeBoxCollider(GameObject go, Component worldCorner1, Component worldCorner2)
	{
		SceneUtils.ResizeBoxCollider(go.GetComponent<Collider>(), worldCorner1.transform.position, worldCorner2.transform.position);
	}

	// Token: 0x060089C5 RID: 35269 RVA: 0x002C2FA7 File Offset: 0x002C11A7
	public static void ResizeBoxCollider(GameObject go, GameObject worldCorner1, Component worldCorner2)
	{
		SceneUtils.ResizeBoxCollider(go.GetComponent<Collider>(), worldCorner1.transform.position, worldCorner2.transform.position);
	}

	// Token: 0x060089C6 RID: 35270 RVA: 0x002C2FCA File Offset: 0x002C11CA
	public static void ResizeBoxCollider(GameObject go, Component worldCorner1, GameObject worldCorner2)
	{
		SceneUtils.ResizeBoxCollider(go.GetComponent<Collider>(), worldCorner1.transform.position, worldCorner2.transform.position);
	}

	// Token: 0x060089C7 RID: 35271 RVA: 0x002C2FED File Offset: 0x002C11ED
	public static void ResizeBoxCollider(GameObject go, GameObject worldCorner1, GameObject worldCorner2)
	{
		SceneUtils.ResizeBoxCollider(go.GetComponent<Collider>(), worldCorner1.transform.position, worldCorner2.transform.position);
	}

	// Token: 0x060089C8 RID: 35272 RVA: 0x002C3010 File Offset: 0x002C1210
	public static void ResizeBoxCollider(Component c, Component worldCorner1, Component worldCorner2)
	{
		SceneUtils.ResizeBoxCollider(c, worldCorner1.transform.position, worldCorner2.transform.position);
	}

	// Token: 0x060089C9 RID: 35273 RVA: 0x002C302E File Offset: 0x002C122E
	public static void ResizeBoxCollider(Component c, GameObject worldCorner1, Component worldCorner2)
	{
		SceneUtils.ResizeBoxCollider(c, worldCorner1.transform.position, worldCorner2.transform.position);
	}

	// Token: 0x060089CA RID: 35274 RVA: 0x002C304C File Offset: 0x002C124C
	public static void ResizeBoxCollider(Component c, Component worldCorner1, GameObject worldCorner2)
	{
		SceneUtils.ResizeBoxCollider(c, worldCorner1.transform.position, worldCorner2.transform.position);
	}

	// Token: 0x060089CB RID: 35275 RVA: 0x002C306A File Offset: 0x002C126A
	public static void ResizeBoxCollider(Component c, GameObject worldCorner1, GameObject worldCorner2)
	{
		SceneUtils.ResizeBoxCollider(c, worldCorner1.transform.position, worldCorner2.transform.position);
	}

	// Token: 0x060089CC RID: 35276 RVA: 0x002C3088 File Offset: 0x002C1288
	public static void ResizeBoxCollider(GameObject go, Bounds bounds)
	{
		SceneUtils.ResizeBoxCollider(go.GetComponent<Collider>(), bounds.min, bounds.max);
	}

	// Token: 0x060089CD RID: 35277 RVA: 0x002C30A3 File Offset: 0x002C12A3
	public static void ResizeBoxCollider(Component c, Bounds bounds)
	{
		SceneUtils.ResizeBoxCollider(c.GetComponent<Collider>(), bounds.min, bounds.max);
	}

	// Token: 0x060089CE RID: 35278 RVA: 0x002C30BE File Offset: 0x002C12BE
	public static void ResizeBoxCollider(GameObject go, Vector3 worldCorner1, Vector3 worldCorner2)
	{
		SceneUtils.ResizeBoxCollider(go.GetComponent<Collider>(), worldCorner1, worldCorner2);
	}

	// Token: 0x060089CF RID: 35279 RVA: 0x002C30D0 File Offset: 0x002C12D0
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

	// Token: 0x060089D0 RID: 35280 RVA: 0x002C312E File Offset: 0x002C132E
	public static Transform CreateBone(GameObject template)
	{
		GameObject gameObject = new GameObject(string.Format("{0}Bone", template.name));
		gameObject.transform.parent = template.transform.parent;
		TransformUtil.CopyLocal(gameObject, template);
		return gameObject.transform;
	}

	// Token: 0x060089D1 RID: 35281 RVA: 0x002C3167 File Offset: 0x002C1367
	public static Transform CreateBone(Component template)
	{
		return SceneUtils.CreateBone(template.gameObject);
	}

	// Token: 0x060089D2 RID: 35282 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public static void SetHideFlags(UnityEngine.Object obj, HideFlags flags)
	{
	}

	// Token: 0x060089D3 RID: 35283 RVA: 0x002C3174 File Offset: 0x002C1374
	public static void WalkSelfAndChildren(Transform transform, Func<Transform, bool> action)
	{
		if (!action(transform))
		{
			return;
		}
		int num = 0;
		while (transform != null && num < transform.childCount)
		{
			SceneUtils.WalkSelfAndChildren(transform.GetChild(num), action);
			num++;
		}
	}

	// Token: 0x060089D4 RID: 35284 RVA: 0x002C31B4 File Offset: 0x002C13B4
	public static T GetComponentOnSelfOrParent<T>(Transform transform) where T : Component
	{
		Transform transform2 = transform;
		while (transform2 != null)
		{
			T component = transform2.GetComponent<T>();
			if (component != null)
			{
				return component;
			}
			transform2 = transform2.parent;
		}
		return default(T);
	}

	// Token: 0x060089D5 RID: 35285 RVA: 0x002C31F5 File Offset: 0x002C13F5
	public static void DestroyChildren(Transform parent)
	{
		while (parent.childCount > 0)
		{
			UnityEngine.Object.DestroyImmediate(parent.GetChild(0).gameObject);
		}
	}

	// Token: 0x060089D6 RID: 35286 RVA: 0x002C3214 File Offset: 0x002C1414
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
			SceneUtils.SafeDestroy(array2[j].gameObject);
		}
	}

	// Token: 0x060089D7 RID: 35287 RVA: 0x002C3262 File Offset: 0x002C1462
	public static void SafeDestroy(GameObject go)
	{
		UnityEngine.Object.DestroyImmediate(go);
	}
}
