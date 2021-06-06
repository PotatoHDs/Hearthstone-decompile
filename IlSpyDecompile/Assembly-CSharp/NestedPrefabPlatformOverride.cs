public class NestedPrefabPlatformOverride : NestedPrefabBase
{
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_Prefab;

	protected override void LoadPrefab()
	{
		LoadPrefab(m_Prefab);
	}
}
