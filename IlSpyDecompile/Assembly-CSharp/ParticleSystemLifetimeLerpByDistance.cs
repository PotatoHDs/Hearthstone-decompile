using System;
using UnityEngine;

public class ParticleSystemLifetimeLerpByDistance : MonoBehaviour
{
	[Serializable]
	public class ScaledObject
	{
		public ParticleSystem component;

		public float startLifetimeMin = 0.6f;

		public float startLifetimeMax = 1.2f;

		public float minDistance = 1f;

		public float maxDistance = 4f;
	}

	public GameObject targetObject;

	public ScaledObject[] properties;

	private void Start()
	{
	}

	private void Update()
	{
		float value = Vector3.Distance(base.transform.position, targetObject.transform.position);
		ScaledObject[] array = properties;
		foreach (ScaledObject scaledObject in array)
		{
			ParticleSystem.MainModule main = scaledObject.component.main;
			main.startLifetime = Mathf.Lerp(scaledObject.startLifetimeMin, scaledObject.startLifetimeMax, (Mathf.Clamp(value, scaledObject.minDistance, scaledObject.maxDistance) - scaledObject.minDistance) / (scaledObject.maxDistance - scaledObject.minDistance));
		}
	}
}
