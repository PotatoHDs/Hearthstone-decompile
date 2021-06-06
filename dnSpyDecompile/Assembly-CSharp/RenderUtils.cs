using System;
using UnityEngine;

// Token: 0x020009EF RID: 2543
public static class RenderUtils
{
	// Token: 0x06008981 RID: 35201 RVA: 0x002C22EE File Offset: 0x002C04EE
	public static void SetAlpha(Component c, float alpha)
	{
		RenderUtils.SetAlpha(c.gameObject, alpha, false);
	}

	// Token: 0x06008982 RID: 35202 RVA: 0x002C22FD File Offset: 0x002C04FD
	public static void SetAlpha(Component c, float alpha, bool includeInactive)
	{
		RenderUtils.SetAlpha(c.gameObject, alpha, includeInactive);
	}

	// Token: 0x06008983 RID: 35203 RVA: 0x002C230C File Offset: 0x002C050C
	public static void SetAlpha(GameObject go, float alpha)
	{
		RenderUtils.SetAlpha(go, alpha, false);
	}

	// Token: 0x06008984 RID: 35204 RVA: 0x002C2318 File Offset: 0x002C0518
	public static void SetAlpha(GameObject go, float alpha, bool includeInactive)
	{
		foreach (Renderer renderer in go.GetComponentsInChildren<Renderer>(includeInactive))
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
		foreach (UberText uberText in go.GetComponentsInChildren<UberText>(includeInactive))
		{
			Color textColor = uberText.TextColor;
			uberText.TextColor = new Color(textColor.r, textColor.g, textColor.b, alpha);
		}
	}

	// Token: 0x06008985 RID: 35205 RVA: 0x002C2458 File Offset: 0x002C0658
	public static float GetMainTextureScaleX(GameObject go)
	{
		return RenderUtils.GetMainTextureScaleX(go.GetComponent<Renderer>());
	}

	// Token: 0x06008986 RID: 35206 RVA: 0x002C2465 File Offset: 0x002C0665
	public static float GetMainTextureScaleX(Component c)
	{
		return RenderUtils.GetMainTextureScaleX(c.GetComponent<Renderer>());
	}

	// Token: 0x06008987 RID: 35207 RVA: 0x002C2472 File Offset: 0x002C0672
	public static float GetMainTextureScaleX(Renderer r)
	{
		return r.GetMaterial().mainTextureScale.x;
	}

	// Token: 0x06008988 RID: 35208 RVA: 0x002C2484 File Offset: 0x002C0684
	public static void SetMainTextureScaleX(Component c, float x)
	{
		RenderUtils.SetMainTextureScaleX(c.GetComponent<Renderer>(), x);
	}

	// Token: 0x06008989 RID: 35209 RVA: 0x002C2492 File Offset: 0x002C0692
	public static void SetMainTextureScaleX(GameObject go, float x)
	{
		RenderUtils.SetMainTextureScaleX(go.GetComponent<Renderer>(), x);
	}

	// Token: 0x0600898A RID: 35210 RVA: 0x002C24A0 File Offset: 0x002C06A0
	public static void SetMainTextureScaleX(Renderer r, float x)
	{
		Material material = r.GetMaterial();
		Vector2 mainTextureScale = material.mainTextureScale;
		mainTextureScale.x = x;
		material.mainTextureScale = mainTextureScale;
	}

	// Token: 0x0600898B RID: 35211 RVA: 0x002C24C8 File Offset: 0x002C06C8
	public static float GetMainTextureScaleY(GameObject go)
	{
		return RenderUtils.GetMainTextureScaleY(go.GetComponent<Renderer>());
	}

	// Token: 0x0600898C RID: 35212 RVA: 0x002C24D5 File Offset: 0x002C06D5
	public static float GetMainTextureScaleY(Component c)
	{
		return RenderUtils.GetMainTextureScaleY(c.GetComponent<Renderer>());
	}

	// Token: 0x0600898D RID: 35213 RVA: 0x002C24E2 File Offset: 0x002C06E2
	public static float GetMainTextureScaleY(Renderer r)
	{
		return r.GetMaterial().mainTextureScale.y;
	}

	// Token: 0x0600898E RID: 35214 RVA: 0x002C24F4 File Offset: 0x002C06F4
	public static void SetMainTextureScaleY(Component c, float y)
	{
		RenderUtils.SetMainTextureScaleY(c.GetComponent<Renderer>(), y);
	}

	// Token: 0x0600898F RID: 35215 RVA: 0x002C2502 File Offset: 0x002C0702
	public static void SetMainTextureScaleY(GameObject go, float y)
	{
		RenderUtils.SetMainTextureScaleY(go.GetComponent<Renderer>(), y);
	}

	// Token: 0x06008990 RID: 35216 RVA: 0x002C2510 File Offset: 0x002C0710
	public static void SetMainTextureScaleY(Renderer r, float y)
	{
		Material material = r.GetMaterial();
		Vector2 mainTextureScale = material.mainTextureScale;
		mainTextureScale.y = y;
		material.mainTextureScale = mainTextureScale;
	}

	// Token: 0x06008991 RID: 35217 RVA: 0x002C2538 File Offset: 0x002C0738
	public static float GetMainTextureOffsetX(GameObject go)
	{
		return RenderUtils.GetMainTextureOffsetX(go.GetComponent<Renderer>());
	}

	// Token: 0x06008992 RID: 35218 RVA: 0x002C2545 File Offset: 0x002C0745
	public static float GetMainTextureOffsetX(Component c)
	{
		return RenderUtils.GetMainTextureOffsetX(c.GetComponent<Renderer>());
	}

	// Token: 0x06008993 RID: 35219 RVA: 0x002C2552 File Offset: 0x002C0752
	public static float GetMainTextureOffsetX(Renderer r)
	{
		return r.GetMaterial().mainTextureOffset.x;
	}

	// Token: 0x06008994 RID: 35220 RVA: 0x002C2564 File Offset: 0x002C0764
	public static void SetMainTextureOffsetX(Component c, float x)
	{
		RenderUtils.SetMainTextureOffsetY(c.GetComponent<Renderer>(), x);
	}

	// Token: 0x06008995 RID: 35221 RVA: 0x002C2572 File Offset: 0x002C0772
	public static void SetMainTextureOffsetX(GameObject go, float x)
	{
		RenderUtils.SetMainTextureOffsetY(go.GetComponent<Renderer>(), x);
	}

	// Token: 0x06008996 RID: 35222 RVA: 0x002C2580 File Offset: 0x002C0780
	public static void SetMainTextureOffsetX(Renderer r, float x)
	{
		Material material = r.GetMaterial();
		Vector2 mainTextureOffset = material.mainTextureOffset;
		mainTextureOffset.x = x;
		material.mainTextureOffset = mainTextureOffset;
	}

	// Token: 0x06008997 RID: 35223 RVA: 0x002C25A8 File Offset: 0x002C07A8
	public static float GetMainTextureOffsetY(GameObject go)
	{
		return RenderUtils.GetMainTextureOffsetY(go.GetComponent<Renderer>());
	}

	// Token: 0x06008998 RID: 35224 RVA: 0x002C25B5 File Offset: 0x002C07B5
	public static float GetMainTextureOffsetY(Component c)
	{
		return RenderUtils.GetMainTextureOffsetY(c.GetComponent<Renderer>());
	}

	// Token: 0x06008999 RID: 35225 RVA: 0x002C25C2 File Offset: 0x002C07C2
	public static float GetMainTextureOffsetY(Renderer r)
	{
		return r.GetMaterial().mainTextureOffset.y;
	}

	// Token: 0x0600899A RID: 35226 RVA: 0x002C2564 File Offset: 0x002C0764
	public static void SetMainTextureOffsetY(Component c, float y)
	{
		RenderUtils.SetMainTextureOffsetY(c.GetComponent<Renderer>(), y);
	}

	// Token: 0x0600899B RID: 35227 RVA: 0x002C2572 File Offset: 0x002C0772
	public static void SetMainTextureOffsetY(GameObject go, float y)
	{
		RenderUtils.SetMainTextureOffsetY(go.GetComponent<Renderer>(), y);
	}

	// Token: 0x0600899C RID: 35228 RVA: 0x002C25D4 File Offset: 0x002C07D4
	public static void SetMainTextureOffsetY(Renderer r, float y)
	{
		Material material = r.GetMaterial();
		Vector2 mainTextureOffset = material.mainTextureOffset;
		mainTextureOffset.y = y;
		material.mainTextureOffset = mainTextureOffset;
	}
}
