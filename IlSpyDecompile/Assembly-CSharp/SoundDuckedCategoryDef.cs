using System;
using Assets;

[Serializable]
public class SoundDuckedCategoryDef
{
	public Global.SoundCategory m_Category;

	public float m_Volume = 0.2f;

	public float m_BeginSec = 0.7f;

	public iTween.EaseType m_BeginEaseType = iTween.EaseType.linear;

	public float m_RestoreSec = 0.7f;

	public iTween.EaseType m_RestoreEaseType = iTween.EaseType.linear;
}
