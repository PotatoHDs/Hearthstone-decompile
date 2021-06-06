using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000092 RID: 146
public class FriendListItemHeader : PegUIElement, ITouchListItem
{
	// Token: 0x17000079 RID: 121
	// (get) Token: 0x06000930 RID: 2352 RVA: 0x00036796 File Offset: 0x00034996
	// (set) Token: 0x06000931 RID: 2353 RVA: 0x0003679E File Offset: 0x0003499E
	public GameObject Background { get; set; }

	// Token: 0x1700007A RID: 122
	// (get) Token: 0x06000932 RID: 2354 RVA: 0x000367A7 File Offset: 0x000349A7
	// (set) Token: 0x06000933 RID: 2355 RVA: 0x000367AF File Offset: 0x000349AF
	public Bounds LocalBounds { get; private set; }

	// Token: 0x06000934 RID: 2356 RVA: 0x000367B8 File Offset: 0x000349B8
	public void SetText(string text)
	{
		this.m_Text.Text = text;
	}

	// Token: 0x1700007B RID: 123
	// (get) Token: 0x06000935 RID: 2357 RVA: 0x000052EC File Offset: 0x000034EC
	public bool IsHeader
	{
		get
		{
			return true;
		}
	}

	// Token: 0x1700007C RID: 124
	// (get) Token: 0x06000936 RID: 2358 RVA: 0x000367C6 File Offset: 0x000349C6
	// (set) Token: 0x06000937 RID: 2359 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public bool Visible
	{
		get
		{
			return this.IsShowingContents;
		}
		set
		{
		}
	}

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x06000938 RID: 2360 RVA: 0x000367CE File Offset: 0x000349CE
	public bool IsShowingContents
	{
		get
		{
			return this.m_ShowContents;
		}
	}

	// Token: 0x1700007E RID: 126
	// (get) Token: 0x06000939 RID: 2361 RVA: 0x000367D6 File Offset: 0x000349D6
	// (set) Token: 0x0600093A RID: 2362 RVA: 0x000367DE File Offset: 0x000349DE
	public MobileFriendListItem.TypeFlags SubType { get; set; }

	// Token: 0x1700007F RID: 127
	// (get) Token: 0x0600093B RID: 2363 RVA: 0x000367E7 File Offset: 0x000349E7
	// (set) Token: 0x0600093C RID: 2364 RVA: 0x000367EF File Offset: 0x000349EF
	public Option Option { get; set; }

	// Token: 0x0600093D RID: 2365 RVA: 0x000367F8 File Offset: 0x000349F8
	public void SetInitialShowContents(bool show)
	{
		this.m_ShowContents = show;
		if (this.m_Arrow != null)
		{
			this.m_Arrow.transform.rotation = this.GetCurrentBoneTransform().rotation;
		}
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x0003682C File Offset: 0x00034A2C
	public void AddToggleListener(FriendListItemHeader.ToggleContentsFunc func, object userdata)
	{
		FriendListItemHeader.ToggleContentsListener toggleContentsListener = new FriendListItemHeader.ToggleContentsListener();
		toggleContentsListener.SetCallback(func);
		toggleContentsListener.SetUserData(userdata);
		this.m_ToggleEventListeners.Add(toggleContentsListener);
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x00036859 File Offset: 0x00034A59
	public void ClearToggleListeners()
	{
		this.m_ToggleEventListeners.Clear();
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x00036866 File Offset: 0x00034A66
	public new T GetComponent<T>() where T : Component
	{
		return base.GetComponent<T>();
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x00036870 File Offset: 0x00034A70
	public void SetToggleEnabled(bool enabled)
	{
		this.m_toggleEnabled = enabled;
		if (!enabled)
		{
			this.m_ShowContents = true;
			if (this.m_Arrow != null)
			{
				TransformUtil.SetLocalPosX(this.m_Text, this.m_textXOffsetWhenToggleDisabled);
			}
		}
		if (this.m_Arrow != null)
		{
			this.m_Arrow.gameObject.SetActive(enabled);
		}
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x000368CC File Offset: 0x00034ACC
	protected override void Awake()
	{
		base.Awake();
		this.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnHeaderButtonReleased));
		if (this.m_multiSlice == null)
		{
			this.m_multiSlice = base.GetComponentInChildren<MultiSliceElement>();
			if (this.m_multiSlice)
			{
				this.m_multiSlice.UpdateSlices();
			}
		}
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x00036928 File Offset: 0x00034B28
	protected virtual void OnHeaderButtonReleased(UIEvent e)
	{
		if (!this.m_toggleEnabled)
		{
			return;
		}
		this.m_ShowContents = !this.m_ShowContents;
		FriendListItemHeader.ToggleContentsListener[] array = this.m_ToggleEventListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(this.m_ShowContents);
		}
		this.UpdateFoldoutArrow();
	}

	// Token: 0x06000944 RID: 2372 RVA: 0x0003697C File Offset: 0x00034B7C
	private void UpdateFoldoutArrow()
	{
		if (this.m_Arrow == null || this.m_FoldinBone == null || this.m_FoldoutBone == null)
		{
			return;
		}
		iTween.RotateTo(this.m_Arrow, this.GetCurrentBoneTransform().rotation.eulerAngles, this.m_AnimRotateTime);
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x000369D8 File Offset: 0x00034BD8
	private Transform GetCurrentBoneTransform()
	{
		if (!this.m_ShowContents)
		{
			return this.m_FoldinBone;
		}
		return this.m_FoldoutBone;
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void OnScrollOutOfView()
	{
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x00036786 File Offset: 0x00034986
	GameObject ITouchListItem.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x0003678E File Offset: 0x0003498E
	Transform ITouchListItem.get_transform()
	{
		return base.transform;
	}

	// Token: 0x04000635 RID: 1589
	public UberText m_Text;

	// Token: 0x04000636 RID: 1590
	public GameObject m_Arrow;

	// Token: 0x04000637 RID: 1591
	public Transform m_FoldinBone;

	// Token: 0x04000638 RID: 1592
	public Transform m_FoldoutBone;

	// Token: 0x04000639 RID: 1593
	public float m_AnimRotateTime = 0.25f;

	// Token: 0x0400063A RID: 1594
	public bool m_toggleEnabled = true;

	// Token: 0x0400063B RID: 1595
	public float m_textXOffsetWhenToggleDisabled = -0.2f;

	// Token: 0x0400063D RID: 1597
	private List<FriendListItemHeader.ToggleContentsListener> m_ToggleEventListeners = new List<FriendListItemHeader.ToggleContentsListener>();

	// Token: 0x0400063E RID: 1598
	private bool m_ShowContents = true;

	// Token: 0x0400063F RID: 1599
	private MultiSliceElement m_multiSlice;

	// Token: 0x0200139A RID: 5018
	// (Invoke) Token: 0x0600D800 RID: 55296
	public delegate void ToggleContentsFunc(bool show, object userdata);

	// Token: 0x0200139B RID: 5019
	private class ToggleContentsListener : EventListener<FriendListItemHeader.ToggleContentsFunc>
	{
		// Token: 0x0600D803 RID: 55299 RVA: 0x003ED7AC File Offset: 0x003EB9AC
		public void Fire(bool show)
		{
			this.m_callback(show, this.m_userData);
		}
	}
}
