using System;
using UnityEngine;

[Serializable]
public class HighlightRenderState
{
	public ActorStateType m_StateType;

	public Material m_Material;

	public Vector3 m_Offset = Vector3.zero;
}
