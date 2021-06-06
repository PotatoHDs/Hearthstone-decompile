using UnityEngine;

public class VS : MonoBehaviour
{
	public GameObject m_shadow;

	private void Start()
	{
		SetDefaults();
	}

	private void OnDestroy()
	{
		SetDefaults();
	}

	private void SetDefaults()
	{
		ActivateShadow(active: false);
	}

	public void ActivateShadow(bool active = true)
	{
		m_shadow.SetActive(active);
	}

	public void ActivateAnimation(bool active = true)
	{
		base.gameObject.GetComponentInChildren<Animation>().enabled = active;
	}
}
