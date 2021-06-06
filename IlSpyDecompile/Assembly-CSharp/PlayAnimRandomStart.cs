using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimRandomStart : MonoBehaviour
{
	public List<GameObject> m_Bubbles;

	public float minWait;

	public float maxWait = 10f;

	public float MinSpeed = 0.2f;

	public float MaxSpeed = 1.1f;

	public string animName = "Bubble1";

	private void Start()
	{
		StartCoroutine(PlayRandomBubbles());
	}

	private IEnumerator PlayRandomBubbles()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(minWait, maxWait));
			int index = Random.Range(0, m_Bubbles.Count);
			GameObject gameObject = m_Bubbles[index];
			if (!(gameObject == null))
			{
				gameObject.GetComponent<Animation>().Play();
				gameObject.GetComponent<Animation>()[animName].speed = Random.Range(MinSpeed, MaxSpeed);
			}
		}
	}
}
