using UnityEngine;

public class CancelMask : MonoBehaviour
{
	public GameObject m_root;

	public void Trigger()
	{
		m_root.gameObject.SetActive(value: false);
		Object.Destroy(base.gameObject);
	}
}
