using UnityEngine;

public class AnimationEventSetParent : MonoBehaviour
{
	public GameObject m_Parent;

	private void Start()
	{
		if (!m_Parent)
		{
			Debug.LogError("Animation Event Set Parent is null!");
			base.enabled = false;
		}
	}

	public void SetParent()
	{
		if ((bool)m_Parent)
		{
			base.transform.parent = m_Parent.transform;
		}
	}
}
