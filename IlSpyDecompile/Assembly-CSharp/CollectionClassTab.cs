using UnityEngine;

public class CollectionClassTab : BookTab
{
	private TAG_CLASS m_classTag;

	public void Init(TAG_CLASS classTag)
	{
		m_classTag = classTag;
		Init();
	}

	public TAG_CLASS GetClass()
	{
		return m_classTag;
	}

	protected override Vector2 GetTextureOffset()
	{
		if (CollectionPageManager.s_classTextureOffsets.ContainsKey(m_classTag))
		{
			return CollectionPageManager.s_classTextureOffsets[m_classTag];
		}
		Debug.LogWarning($"CollectionClassTab.GetTextureOffset(): No class texture offsets exist for class {m_classTag}");
		return Vector2.zero;
	}
}
