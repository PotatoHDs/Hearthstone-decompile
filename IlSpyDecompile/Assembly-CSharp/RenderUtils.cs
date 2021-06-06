using UnityEngine;

public static class RenderUtils
{
	public static void SetAlpha(Component c, float alpha)
	{
		SetAlpha(c.gameObject, alpha, includeInactive: false);
	}

	public static void SetAlpha(Component c, float alpha, bool includeInactive)
	{
		SetAlpha(c.gameObject, alpha, includeInactive);
	}

	public static void SetAlpha(GameObject go, float alpha)
	{
		SetAlpha(go, alpha, includeInactive: false);
	}

	public static void SetAlpha(GameObject go, float alpha, bool includeInactive)
	{
		Renderer[] componentsInChildren = go.GetComponentsInChildren<Renderer>(includeInactive);
		foreach (Renderer renderer in componentsInChildren)
		{
			foreach (Material material in renderer.GetMaterials())
			{
				if (material.HasProperty("_Color"))
				{
					Color color = material.color;
					color.a = alpha;
					material.color = color;
				}
				else if (material.HasProperty("_TintColor"))
				{
					Color color2 = material.GetColor("_TintColor");
					color2.a = alpha;
					material.SetColor("_TintColor", color2);
				}
			}
			if (renderer.GetComponent<Light>() != null)
			{
				Color color3 = renderer.GetComponent<Light>().color;
				color3.a = alpha;
				renderer.GetComponent<Light>().color = color3;
			}
		}
		UberText[] componentsInChildren2 = go.GetComponentsInChildren<UberText>(includeInactive);
		foreach (UberText obj in componentsInChildren2)
		{
			Color textColor = obj.TextColor;
			obj.TextColor = new Color(textColor.r, textColor.g, textColor.b, alpha);
		}
	}

	public static float GetMainTextureScaleX(GameObject go)
	{
		return GetMainTextureScaleX(go.GetComponent<Renderer>());
	}

	public static float GetMainTextureScaleX(Component c)
	{
		return GetMainTextureScaleX(c.GetComponent<Renderer>());
	}

	public static float GetMainTextureScaleX(Renderer r)
	{
		return r.GetMaterial().mainTextureScale.x;
	}

	public static void SetMainTextureScaleX(Component c, float x)
	{
		SetMainTextureScaleX(c.GetComponent<Renderer>(), x);
	}

	public static void SetMainTextureScaleX(GameObject go, float x)
	{
		SetMainTextureScaleX(go.GetComponent<Renderer>(), x);
	}

	public static void SetMainTextureScaleX(Renderer r, float x)
	{
		Material material = r.GetMaterial();
		Vector2 mainTextureScale = material.mainTextureScale;
		mainTextureScale.x = x;
		material.mainTextureScale = mainTextureScale;
	}

	public static float GetMainTextureScaleY(GameObject go)
	{
		return GetMainTextureScaleY(go.GetComponent<Renderer>());
	}

	public static float GetMainTextureScaleY(Component c)
	{
		return GetMainTextureScaleY(c.GetComponent<Renderer>());
	}

	public static float GetMainTextureScaleY(Renderer r)
	{
		return r.GetMaterial().mainTextureScale.y;
	}

	public static void SetMainTextureScaleY(Component c, float y)
	{
		SetMainTextureScaleY(c.GetComponent<Renderer>(), y);
	}

	public static void SetMainTextureScaleY(GameObject go, float y)
	{
		SetMainTextureScaleY(go.GetComponent<Renderer>(), y);
	}

	public static void SetMainTextureScaleY(Renderer r, float y)
	{
		Material material = r.GetMaterial();
		Vector2 mainTextureScale = material.mainTextureScale;
		mainTextureScale.y = y;
		material.mainTextureScale = mainTextureScale;
	}

	public static float GetMainTextureOffsetX(GameObject go)
	{
		return GetMainTextureOffsetX(go.GetComponent<Renderer>());
	}

	public static float GetMainTextureOffsetX(Component c)
	{
		return GetMainTextureOffsetX(c.GetComponent<Renderer>());
	}

	public static float GetMainTextureOffsetX(Renderer r)
	{
		return r.GetMaterial().mainTextureOffset.x;
	}

	public static void SetMainTextureOffsetX(Component c, float x)
	{
		SetMainTextureOffsetY(c.GetComponent<Renderer>(), x);
	}

	public static void SetMainTextureOffsetX(GameObject go, float x)
	{
		SetMainTextureOffsetY(go.GetComponent<Renderer>(), x);
	}

	public static void SetMainTextureOffsetX(Renderer r, float x)
	{
		Material material = r.GetMaterial();
		Vector2 mainTextureOffset = material.mainTextureOffset;
		mainTextureOffset.x = x;
		material.mainTextureOffset = mainTextureOffset;
	}

	public static float GetMainTextureOffsetY(GameObject go)
	{
		return GetMainTextureOffsetY(go.GetComponent<Renderer>());
	}

	public static float GetMainTextureOffsetY(Component c)
	{
		return GetMainTextureOffsetY(c.GetComponent<Renderer>());
	}

	public static float GetMainTextureOffsetY(Renderer r)
	{
		return r.GetMaterial().mainTextureOffset.y;
	}

	public static void SetMainTextureOffsetY(Component c, float y)
	{
		SetMainTextureOffsetY(c.GetComponent<Renderer>(), y);
	}

	public static void SetMainTextureOffsetY(GameObject go, float y)
	{
		SetMainTextureOffsetY(go.GetComponent<Renderer>(), y);
	}

	public static void SetMainTextureOffsetY(Renderer r, float y)
	{
		Material material = r.GetMaterial();
		Vector2 mainTextureOffset = material.mainTextureOffset;
		mainTextureOffset.y = y;
		material.mainTextureOffset = mainTextureOffset;
	}
}
