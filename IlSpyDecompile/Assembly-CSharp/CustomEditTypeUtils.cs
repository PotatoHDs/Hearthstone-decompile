public static class CustomEditTypeUtils
{
	public static string GetExtension(EditType type)
	{
		switch (type)
		{
		case EditType.TEXTURE:
		case EditType.CARD_TEXTURE:
			return "tif";
		case EditType.MATERIAL:
			return "mat";
		case EditType.ARTBUNDLE:
		case EditType.UBERANIMATION:
			return "asset";
		case EditType.MESH:
			return "fbx";
		default:
			return "prefab";
		}
	}
}
