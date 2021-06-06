using UnityEngine;

public class RandomAnimatorSpeed : MonoBehaviour
{
	public float minSpeed = 0.5f;

	public float maxSpeed = 1.5f;

	private void Start()
	{
		Animator component = GetComponent<Animator>();
		if (!(component == null))
		{
			component.speed = Random.Range(minSpeed, maxSpeed);
		}
	}
}
