using System;
using UnityEngine;

[Serializable]
public class SpellPath
{
	public SpellPathType m_Type;

	public string m_PathName;

	public Vector3 m_FirstNodeOffset;

	public Vector3 m_LastNodeOffset;
}
