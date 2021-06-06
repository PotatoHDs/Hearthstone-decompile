using System;
using UnityEngine;

[Serializable]
public class PlatformDependentVector3 : PlatformDependentValue<Vector3>
{
	public PlatformDependentVector3(PlatformCategory t)
		: base(t)
	{
	}
}
