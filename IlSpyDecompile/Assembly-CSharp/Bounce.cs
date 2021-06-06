using System;
using System.Collections;
using UnityEngine;

public class Bounce : MonoBehaviour
{
	public float m_BounceSpeed = 3.5f;

	public float m_BounceAmount = 3f;

	public int m_BounceCount = 3;

	public float m_Bounceness = 0.2f;

	public float m_Delay;

	public bool m_PlayOnStart;

	private Vector3 m_StartingPosition;

	private float m_BounceAmountOverTime;

	private void Start()
	{
		if (m_PlayOnStart)
		{
			StartAnimation();
		}
	}

	private void Update()
	{
	}

	public void StartAnimation()
	{
		m_BounceAmountOverTime = m_BounceAmount;
		m_StartingPosition = base.transform.position;
		StartCoroutine("BounceAnimation");
	}

	private IEnumerator BounceAnimation()
	{
		yield return new WaitForSeconds(m_Delay);
		for (int c = 0; c < m_BounceCount; c++)
		{
			float time = 0f;
			for (float i = 0f; i < 1f; i += Time.deltaTime * m_BounceSpeed)
			{
				time += Time.deltaTime * m_BounceSpeed;
				Vector3 startingPosition = m_StartingPosition;
				float num = Mathf.Sin(time * (float)Math.PI);
				if (num < 0f)
				{
					break;
				}
				base.transform.position = new Vector3(startingPosition.x, startingPosition.y + num * m_BounceAmountOverTime, startingPosition.z);
				yield return null;
			}
			m_BounceAmountOverTime *= m_Bounceness;
			yield return null;
		}
		base.transform.position = m_StartingPosition;
	}
}
