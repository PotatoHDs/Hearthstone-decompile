using System;
using UnityEngine;

// Token: 0x0200009E RID: 158
public class MobileFriendListItem : MonoBehaviour, ISelectableTouchListItem, ITouchListItem
{
	// Token: 0x14000011 RID: 17
	// (add) Token: 0x060009D6 RID: 2518 RVA: 0x00038658 File Offset: 0x00036858
	// (remove) Token: 0x060009D7 RID: 2519 RVA: 0x00038690 File Offset: 0x00036890
	public event Action OnScrollOutOfViewEvent;

	// Token: 0x17000091 RID: 145
	// (get) Token: 0x060009D8 RID: 2520 RVA: 0x000386C5 File Offset: 0x000368C5
	// (set) Token: 0x060009D9 RID: 2521 RVA: 0x000386CD File Offset: 0x000368CD
	public MobileFriendListItem.TypeFlags Type { get; set; }

	// Token: 0x17000092 RID: 146
	// (get) Token: 0x060009DA RID: 2522 RVA: 0x000386D6 File Offset: 0x000368D6
	public Bounds LocalBounds
	{
		get
		{
			return this.m_localBounds;
		}
	}

	// Token: 0x17000093 RID: 147
	// (get) Token: 0x060009DB RID: 2523 RVA: 0x000386DE File Offset: 0x000368DE
	public bool Selectable
	{
		get
		{
			return this.Type == MobileFriendListItem.TypeFlags.Friend || this.Type == MobileFriendListItem.TypeFlags.NearbyPlayer;
		}
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x000386F4 File Offset: 0x000368F4
	public void SetParent(ITouchListItem parent)
	{
		this.m_parent = parent;
	}

	// Token: 0x060009DD RID: 2525 RVA: 0x000386FD File Offset: 0x000368FD
	public void SetShowObject(GameObject showobj)
	{
		this.m_showObject = showobj;
	}

	// Token: 0x17000094 RID: 148
	// (get) Token: 0x060009DE RID: 2526 RVA: 0x00038706 File Offset: 0x00036906
	public bool IsHeader
	{
		get
		{
			return MobileFriendListItem.ItemIsHeader(this.Type);
		}
	}

	// Token: 0x060009DF RID: 2527 RVA: 0x00038713 File Offset: 0x00036913
	public static bool ItemIsHeader(MobileFriendListItem.TypeFlags typeFlags)
	{
		return (typeFlags & MobileFriendListItem.TypeFlags.Header) > (MobileFriendListItem.TypeFlags)0;
	}

	// Token: 0x17000095 RID: 149
	// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0003871B File Offset: 0x0003691B
	// (set) Token: 0x060009E1 RID: 2529 RVA: 0x00038732 File Offset: 0x00036932
	public bool Visible
	{
		get
		{
			return this.m_parent == null || this.m_parent.Visible;
		}
		set
		{
			if (this.m_showObject == null)
			{
				return;
			}
			if (value != this.m_showObject.activeSelf)
			{
				this.m_showObject.SetActive(value);
			}
		}
	}

	// Token: 0x060009E2 RID: 2530 RVA: 0x00038760 File Offset: 0x00036960
	private void Awake()
	{
		Transform parent = base.transform.parent;
		TransformProps transformProps = new TransformProps();
		TransformUtil.CopyWorld(transformProps, base.transform);
		base.transform.parent = null;
		TransformUtil.Identity(base.transform);
		this.m_localBounds = this.ComputeWorldBounds();
		base.transform.parent = parent;
		TransformUtil.CopyWorld(base.transform, transformProps);
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x000387C8 File Offset: 0x000369C8
	public bool IsSelected()
	{
		FriendListUIElement component = this.GetComponent<FriendListUIElement>();
		return component != null && component.IsSelected();
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x000387F0 File Offset: 0x000369F0
	public void Selected()
	{
		FriendListUIElement component = this.GetComponent<FriendListUIElement>();
		if (component != null)
		{
			component.SetSelected(true);
		}
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x00038814 File Offset: 0x00036A14
	public void Unselected()
	{
		FriendListUIElement component = this.GetComponent<FriendListUIElement>();
		if (component != null)
		{
			component.SetSelected(false);
		}
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x00038838 File Offset: 0x00036A38
	public Bounds ComputeWorldBounds()
	{
		return TransformUtil.ComputeSetPointBounds(base.gameObject);
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x00038845 File Offset: 0x00036A45
	public new T GetComponent<T>() where T : Component
	{
		return base.GetComponent<T>();
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x0003884D File Offset: 0x00036A4D
	public void OnScrollOutOfView()
	{
		if (this.OnScrollOutOfViewEvent != null)
		{
			this.OnScrollOutOfViewEvent();
		}
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x00036786 File Offset: 0x00034986
	GameObject ITouchListItem.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x0003678E File Offset: 0x0003498E
	Transform ITouchListItem.get_transform()
	{
		return base.transform;
	}

	// Token: 0x04000677 RID: 1655
	private Bounds m_localBounds;

	// Token: 0x04000678 RID: 1656
	private ITouchListItem m_parent;

	// Token: 0x04000679 RID: 1657
	private GameObject m_showObject;

	// Token: 0x020013A5 RID: 5029
	[Flags]
	public enum TypeFlags
	{
		// Token: 0x0400A74D RID: 42829
		FoundFiresideGathering = 256,
		// Token: 0x0400A74E RID: 42830
		Request = 128,
		// Token: 0x0400A74F RID: 42831
		CurrentFiresideGathering = 64,
		// Token: 0x0400A750 RID: 42832
		FiresideGatheringPlayer = 32,
		// Token: 0x0400A751 RID: 42833
		FiresideGatheringFooter = 16,
		// Token: 0x0400A752 RID: 42834
		NearbyPlayer = 8,
		// Token: 0x0400A753 RID: 42835
		CurrentGame = 4,
		// Token: 0x0400A754 RID: 42836
		Friend = 2,
		// Token: 0x0400A755 RID: 42837
		Header = 1
	}
}
