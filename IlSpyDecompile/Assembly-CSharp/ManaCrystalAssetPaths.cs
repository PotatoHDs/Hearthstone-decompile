using System;
using UnityEngine;

[Serializable]
public class ManaCrystalAssetPaths
{
	public ManaCrystalType m_Type;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_ResourcePath;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_SoundOnAddPath;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_SoundOnSpendPath;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_SoundOnRefreshPath;

	[CustomEditField(T = EditType.MATERIAL)]
	public Material m_tempManaCrystalMaterial;

	[CustomEditField(T = EditType.MATERIAL)]
	public Material m_tempManaCrystalProposedQuadMaterial;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_phoneLargeResource;
}
