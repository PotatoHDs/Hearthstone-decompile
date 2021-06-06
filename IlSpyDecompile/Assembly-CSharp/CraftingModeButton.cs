using System.Collections;
using UnityEngine;

public class CraftingModeButton : UIBButton
{
	public GameObject m_dustBottle;

	public GameObject m_activeGlow;

	public ParticleSystem m_dustShower;

	public Vector3 m_jarJiggleRotation = new Vector3(0f, 30f, 0f);

	public GameObject m_textObject;

	public MeshRenderer m_mainMesh;

	public Material m_enabledMaterial;

	public Material m_disabledMaterial;

	private bool m_isGlowEnabled;

	private bool m_showDustBottle;

	private bool m_isJiggling;

	private Coroutine m_jiggleCoroutine;

	public void ShowActiveGlow(bool show)
	{
		m_isGlowEnabled = show;
		m_activeGlow.SetActive(show);
	}

	public void ShowDustBottle(bool show)
	{
		m_showDustBottle = show;
		m_dustBottle.SetActive(show);
		if (show)
		{
			StartBottleJiggle();
		}
	}

	private void StartBottleJiggle()
	{
		if (m_jiggleCoroutine != null)
		{
			StopCoroutine(m_jiggleCoroutine);
			iTween.Stop(m_dustBottle.gameObject);
		}
		BottleJiggle();
	}

	private void BottleJiggle()
	{
		m_jiggleCoroutine = StartCoroutine(Jiggle());
	}

	private IEnumerator Jiggle()
	{
		yield return new WaitForSeconds(1f);
		m_dustShower.Play();
		Hashtable args = iTween.Hash("amount", m_jarJiggleRotation, "time", 0.5f, "oncomplete", "BottleJiggle", "oncompletetarget", base.gameObject);
		iTween.PunchRotation(m_dustBottle.gameObject, args);
	}

	public void Enable(bool enabled)
	{
		SetEnabled(enabled);
		m_activeGlow.SetActive(enabled && m_isGlowEnabled);
		m_textObject.SetActive(enabled);
		m_dustShower.gameObject.SetActive(enabled);
		if (enabled)
		{
			m_dustBottle.SetActive(m_showDustBottle);
		}
		else
		{
			m_dustBottle.SetActive(value: false);
		}
		m_mainMesh.SetSharedMaterial(enabled ? m_enabledMaterial : m_disabledMaterial);
	}
}
