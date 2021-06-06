using System;

// Token: 0x020009AD RID: 2477
public static class CustomEditTypeUtils
{
	// Token: 0x060086FC RID: 34556 RVA: 0x002B97D4 File Offset: 0x002B79D4
	public static string GetExtension(EditType type)
	{
		if (type <= EditType.CARD_TEXTURE)
		{
			if (type == EditType.MATERIAL)
			{
				return "mat";
			}
			if (type - EditType.TEXTURE <= 1)
			{
				return "tif";
			}
		}
		else
		{
			if (type - EditType.ARTBUNDLE <= 1)
			{
				return "asset";
			}
			if (type == EditType.MESH)
			{
				return "fbx";
			}
		}
		return "prefab";
	}
}
