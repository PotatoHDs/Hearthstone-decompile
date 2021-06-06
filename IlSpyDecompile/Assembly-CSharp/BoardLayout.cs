using UnityEngine;

public class BoardLayout : MonoBehaviour
{
	public Transform m_BoneParent;

	public Transform m_ColliderParent;

	public void Awake()
	{
		if (LoadingScreen.Get() != null)
		{
			LoadingScreen.Get().NotifyMainSceneObjectAwoke(base.gameObject);
		}
	}

	public Transform FindBone(string name)
	{
		return m_BoneParent.Find(name);
	}

	public Collider FindCollider(string name)
	{
		Transform transform = m_ColliderParent.Find(name);
		if (!(transform == null))
		{
			return transform.GetComponent<Collider>();
		}
		return null;
	}
}
