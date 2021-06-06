using System;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x020009F5 RID: 2549
public class TemporaryWidgetInstance : MonoBehaviour
{
	// Token: 0x060089FF RID: 35327 RVA: 0x002C3B8B File Offset: 0x002C1D8B
	private void Start()
	{
		this.EnforceHaveInstance(this.m_shouldLoad);
	}

	// Token: 0x06008A00 RID: 35328 RVA: 0x002C3B99 File Offset: 0x002C1D99
	private void OnDestroy()
	{
		if (this.m_instance != null)
		{
			this.DestroyInstance();
		}
	}

	// Token: 0x170007BC RID: 1980
	// (get) Token: 0x06008A01 RID: 35329 RVA: 0x002C3BAF File Offset: 0x002C1DAF
	// (set) Token: 0x06008A02 RID: 35330 RVA: 0x002C3BB7 File Offset: 0x002C1DB7
	[Overridable]
	public bool ShouldLoad
	{
		get
		{
			return this.m_shouldLoad;
		}
		set
		{
			this.m_shouldLoad = value;
			this.EnforceHaveInstance(this.m_shouldLoad);
		}
	}

	// Token: 0x170007BD RID: 1981
	// (get) Token: 0x06008A03 RID: 35331 RVA: 0x002C3BCC File Offset: 0x002C1DCC
	public WidgetInstance Instance
	{
		get
		{
			return this.m_instance;
		}
	}

	// Token: 0x170007BE RID: 1982
	// (get) Token: 0x06008A04 RID: 35332 RVA: 0x002C3BD4 File Offset: 0x002C1DD4
	public bool IsReady
	{
		get
		{
			return !this.m_shouldLoad || (!(this.m_instance == null) && this.m_instance.IsReady);
		}
	}

	// Token: 0x170007BF RID: 1983
	// (get) Token: 0x06008A05 RID: 35333 RVA: 0x002C3BFB File Offset: 0x002C1DFB
	public bool IsChangingStates
	{
		get
		{
			return this.m_instance != null && this.m_instance.IsChangingStates;
		}
	}

	// Token: 0x06008A06 RID: 35334 RVA: 0x002C3C18 File Offset: 0x002C1E18
	private void EnforceHaveInstance(bool haveInstance)
	{
		if (!Application.isPlaying)
		{
			return;
		}
		if (haveInstance && this.m_instance == null)
		{
			this.CreateInstance();
			return;
		}
		if (!haveInstance && this.m_instance != null)
		{
			this.DestroyInstance();
		}
	}

	// Token: 0x06008A07 RID: 35335 RVA: 0x002C3C54 File Offset: 0x002C1E54
	private void CreateInstance()
	{
		if (base.transform == null || this.m_instance != null)
		{
			return;
		}
		this.m_instance = WidgetInstance.Create(this.m_prefabPath, false);
		GameObject gameObject = this.m_instance.gameObject;
		gameObject.name = this.m_widgetPrefab.name;
		gameObject.transform.SetParent(base.transform, false);
	}

	// Token: 0x06008A08 RID: 35336 RVA: 0x002C3CBD File Offset: 0x002C1EBD
	private void DestroyInstance()
	{
		if (this.m_instance == null)
		{
			return;
		}
		UnityEngine.Object.Destroy(this.m_instance.gameObject);
		this.m_instance = null;
	}

	// Token: 0x04007358 RID: 29528
	[SerializeField]
	private GameObject m_widgetPrefab;

	// Token: 0x04007359 RID: 29529
	[SerializeField]
	private bool m_shouldLoad;

	// Token: 0x0400735A RID: 29530
	private WidgetInstance m_instance;

	// Token: 0x0400735B RID: 29531
	[SerializeField]
	[HideInInspector]
	private string m_prefabPath;
}
