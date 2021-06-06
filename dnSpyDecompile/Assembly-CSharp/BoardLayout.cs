using System;
using UnityEngine;

// Token: 0x02000866 RID: 2150
public class BoardLayout : MonoBehaviour
{
	// Token: 0x06007424 RID: 29732 RVA: 0x002542B6 File Offset: 0x002524B6
	public void Awake()
	{
		if (LoadingScreen.Get() != null)
		{
			LoadingScreen.Get().NotifyMainSceneObjectAwoke(base.gameObject);
		}
	}

	// Token: 0x06007425 RID: 29733 RVA: 0x002542D5 File Offset: 0x002524D5
	public Transform FindBone(string name)
	{
		return this.m_BoneParent.Find(name);
	}

	// Token: 0x06007426 RID: 29734 RVA: 0x002542E4 File Offset: 0x002524E4
	public Collider FindCollider(string name)
	{
		Transform transform = this.m_ColliderParent.Find(name);
		if (!(transform == null))
		{
			return transform.GetComponent<Collider>();
		}
		return null;
	}

	// Token: 0x04005C44 RID: 23620
	public Transform m_BoneParent;

	// Token: 0x04005C45 RID: 23621
	public Transform m_ColliderParent;
}
