using System;
using UnityEngine;

[Serializable]
public class Vector3_MobileOverride : MobileOverrideValue<Vector3>
{
	public Vector3_MobileOverride()
	{
	}

	public Vector3_MobileOverride(Vector3 defaultValue)
		: base(defaultValue)
	{
	}
}
