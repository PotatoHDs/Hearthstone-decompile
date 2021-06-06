using System;
using UnityEngine;

// Token: 0x02000091 RID: 145
public class FriendListItemFooter : PegUIElement, ITouchListItem
{
	// Token: 0x17000075 RID: 117
	// (get) Token: 0x06000924 RID: 2340 RVA: 0x00036714 File Offset: 0x00034914
	public Bounds LocalBounds
	{
		get
		{
			Bounds bounds = TransformUtil.ComputeSetPointBounds(this.GetComponent<Collider>());
			Vector3 center = base.transform.InverseTransformPoint(bounds.center);
			return new Bounds(center, bounds.size);
		}
	}

	// Token: 0x17000076 RID: 118
	// (get) Token: 0x06000925 RID: 2341 RVA: 0x0003674D File Offset: 0x0003494D
	// (set) Token: 0x06000926 RID: 2342 RVA: 0x0003675A File Offset: 0x0003495A
	public string Text
	{
		get
		{
			return this.m_Text.Text;
		}
		set
		{
			this.m_Text.Text = value;
		}
	}

	// Token: 0x17000077 RID: 119
	// (get) Token: 0x06000927 RID: 2343 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public bool IsHeader
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x06000928 RID: 2344 RVA: 0x000261C2 File Offset: 0x000243C2
	// (set) Token: 0x06000929 RID: 2345 RVA: 0x00036768 File Offset: 0x00034968
	public bool Visible
	{
		get
		{
			return base.gameObject.activeSelf;
		}
		set
		{
			base.gameObject.SetActive(value);
		}
	}

	// Token: 0x0600092A RID: 2346 RVA: 0x00036776 File Offset: 0x00034976
	public new T GetComponent<T>() where T : Component
	{
		return base.GetComponent<T>();
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x0003677E File Offset: 0x0003497E
	protected override void Awake()
	{
		base.Awake();
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void OnScrollOutOfView()
	{
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x00036786 File Offset: 0x00034986
	GameObject ITouchListItem.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x0600092F RID: 2351 RVA: 0x0003678E File Offset: 0x0003498E
	Transform ITouchListItem.get_transform()
	{
		return base.transform;
	}

	// Token: 0x04000634 RID: 1588
	public UberText m_Text;
}
