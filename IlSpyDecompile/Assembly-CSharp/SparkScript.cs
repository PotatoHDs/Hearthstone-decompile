using UnityEngine;

public class SparkScript : MonoBehaviour
{
	public AudioClip clip1;

	public AudioClip clip2;

	private void Awake()
	{
		AudioSource component = GetComponent<AudioSource>();
		if (Random.value >= 0.5f)
		{
			component.clip = clip1;
		}
		else
		{
			component.clip = clip2;
		}
	}
}
