using System;
using UnityEngine;

[Serializable]
public class BoxLightInfo
{
	public Light m_Light;

	public Color m_Color = Color.white;

	public float m_Intensity;

	public float m_Range;

	public float m_SpotAngle;
}
