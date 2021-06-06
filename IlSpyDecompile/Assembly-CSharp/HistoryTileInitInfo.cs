using System.Collections.Generic;
using UnityEngine;

public class HistoryTileInitInfo : HistoryItemInitInfo
{
	public HistoryInfoType m_type;

	public List<HistoryInfo> m_childInfos;

	public Texture m_fatigueTexture;

	public Texture m_burnedCardsTexture;

	public Material m_fullTileMaterial;

	public Material m_halfTileMaterial;

	public bool m_dead;

	public bool m_burned;

	public bool m_isPoisonous;

	public int m_splatAmount;
}
