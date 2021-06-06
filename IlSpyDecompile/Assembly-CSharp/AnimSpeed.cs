using UnityEngine;

public class AnimSpeed : MonoBehaviour
{
	public float animspeed = 1f;

	private void Awake()
	{
		foreach (AnimationState item in GetComponent<Animation>())
		{
			item.speed = animspeed;
		}
	}
}
