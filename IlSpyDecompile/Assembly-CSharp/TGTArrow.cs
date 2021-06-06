using UnityEngine;

public class TGTArrow : MonoBehaviour
{
	public GameObject m_ArrowRoot;

	public GameObject m_ArrowMesh;

	public GameObject m_Trail;

	public ParticleSystem m_BullseyeParticles;

	private void onEnable()
	{
		m_ArrowRoot.transform.localEulerAngles = new Vector3(0f, 170f, 0f);
	}

	public void FireArrow(bool randomRotation)
	{
		if (randomRotation)
		{
			Vector3 localEulerAngles = m_ArrowMesh.transform.localEulerAngles;
			m_ArrowMesh.transform.localEulerAngles = new Vector3(localEulerAngles.x + Random.Range(0f, 360f), localEulerAngles.y, localEulerAngles.z);
			m_ArrowRoot.transform.localEulerAngles = new Vector3(Random.Range(0f, 20f), Random.Range(160f, 180f), 0f);
		}
		ArrowAnimation();
	}

	public void ArrowAnimation()
	{
		m_Trail.SetActive(value: true);
		m_Trail.GetComponent<Renderer>().GetMaterial().SetColor("_Color", new Color(0.15f, 0.15f, 0.15f, 0.15f));
		iTween.ColorTo(m_Trail, iTween.Hash("color", Color.clear, "time", 0.1f, "oncomplete", "OnAnimationComplete"));
		Vector3 localPosition = m_ArrowRoot.transform.localPosition;
		iTween.MoveFrom(m_ArrowRoot, iTween.Hash("position", new Vector3(localPosition.x, localPosition.y, localPosition.z + 0.4f), "islocal", true, "time", 0.05f, "easetype", iTween.EaseType.easeOutQuart));
	}

	public void OnAnimationComplete()
	{
		m_Trail.SetActive(value: false);
	}

	public void Bullseye()
	{
		m_BullseyeParticles.Play();
	}
}
