using UnityEngine;

public class CardCrafting_WepPartSetParent : MonoBehaviour
{
	public GameObject m_Parent;

	public GameObject m_WepParts;

	private void Start()
	{
		if (!m_Parent)
		{
			Debug.LogError("Animation Event Set Parent is null!");
			base.enabled = false;
		}
	}

	public void SetParentWepParts()
	{
		if ((bool)m_Parent)
		{
			m_WepParts.transform.parent = m_Parent.transform;
		}
	}
}
