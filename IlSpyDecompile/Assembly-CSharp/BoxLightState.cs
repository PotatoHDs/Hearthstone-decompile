using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BoxLightState
{
	public BoxLightStateType m_Type;

	public float m_DelaySec;

	public float m_TransitionSec = 0.5f;

	public iTween.EaseType m_TransitionEaseType = iTween.EaseType.linear;

	public Spell m_Spell;

	public Color m_AmbientColor = new Color(43f / 85f, 121f / 255f, 121f / 255f, 1f);

	public List<BoxLightInfo> m_LightInfos;
}
