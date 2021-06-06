using System.Collections;
using UnityEngine;

public class RankChangeStar : MonoBehaviour
{
	public MeshRenderer m_starMeshRenderer;

	public MeshRenderer m_bottomGlowRenderer;

	public MeshRenderer m_topGlowRenderer;

	public void BlackOut()
	{
		m_starMeshRenderer.enabled = false;
	}

	public void UnBlackOut()
	{
		m_starMeshRenderer.enabled = true;
	}

	public void FadeIn()
	{
		GetComponent<PlayMakerFSM>().SendEvent("FadeIn");
	}

	public void Spawn()
	{
		GetComponent<PlayMakerFSM>().SendEvent("Spawn");
	}

	public void Reset()
	{
		GetComponent<PlayMakerFSM>().SendEvent("Reset");
	}

	public void Blink(float delay)
	{
		StartCoroutine(DelayedBlink(delay));
	}

	public IEnumerator DelayedBlink(float delay)
	{
		yield return new WaitForSeconds(delay);
		GetComponent<PlayMakerFSM>().SendEvent("Blink");
	}

	public void Burst(float delay)
	{
		StartCoroutine(DelayedBurst(delay));
	}

	public IEnumerator DelayedBurst(float delay)
	{
		yield return new WaitForSeconds(delay);
		UnBlackOut();
		GetComponent<PlayMakerFSM>().SendEvent("Burst");
	}

	public IEnumerator DelayedDespawn(float delay)
	{
		yield return new WaitForSeconds(delay);
		GetComponent<PlayMakerFSM>().SendEvent("DeSpawn");
	}

	public void Despawn()
	{
		GetComponent<PlayMakerFSM>().SendEvent("DeSpawn");
	}

	public void Wipe(float delay)
	{
		StartCoroutine(DelayedWipe(delay));
	}

	public IEnumerator DelayedWipe(float delay)
	{
		yield return new WaitForSeconds(delay);
		GetComponent<PlayMakerFSM>().SendEvent("Wipe");
	}
}
