using UnityEngine;

[ExecuteAlways]
[CustomEditClass]
public class UIBFollowObject : MonoBehaviour
{
	public GameObject m_rootObject;

	public GameObject m_objectToFollow;

	public Vector3 m_offset;

	public bool m_useWorldOffset;

	public void UpdateFollowPosition()
	{
		if (!(m_rootObject == null) && !(m_objectToFollow == null))
		{
			Vector3 position = m_objectToFollow.transform.position;
			if (m_offset.sqrMagnitude > 0f)
			{
				position += (Vector3)(m_objectToFollow.transform.localToWorldMatrix * m_offset);
			}
			m_rootObject.transform.position = position;
		}
	}
}
