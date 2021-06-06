public class NestedPrefab : NestedPrefabBase
{
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_Prefab;

	protected override void LoadPrefab()
	{
		LoadPrefab(m_Prefab);
	}
}
