using System;
using UnityEngine;

public static class VectorUtils
{
	public static Vector2 Abs(Vector2 vector)
	{
		return new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
	}

	public static Vector2 CreateFromAngle(float degrees)
	{
		float f = (float)Math.PI / 180f * degrees;
		return new Vector2(Mathf.Cos(f), Mathf.Sin(f));
	}

	public static Vector3 Abs(Vector3 vector)
	{
		return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
	}
}
