using System;
using UnityEngine;

[Serializable]
public class BoxDiskStateInfo
{
	public Vector3 m_MainMenuRotation = new Vector3(0f, 0f, 180f);

	public float m_MainMenuDelaySec = 0.1f;

	public float m_MainMenuRotateSec = 0.17f;

	public iTween.EaseType m_MainMenuRotateEaseType;

	public Vector3 m_LoadingRotation = new Vector3(0f, 0f, 0f);

	public float m_LoadingDelaySec = 0.1f;

	public float m_LoadingRotateSec = 0.17f;

	public iTween.EaseType m_LoadingRotateEaseType;
}
