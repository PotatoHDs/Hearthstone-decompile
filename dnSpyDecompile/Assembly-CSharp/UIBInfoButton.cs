using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000AF3 RID: 2803
[CustomEditClass]
public class UIBInfoButton : PegUIElement
{
	// Token: 0x0600951D RID: 38173 RVA: 0x00304C34 File Offset: 0x00302E34
	protected override void Awake()
	{
		base.Awake();
		UIBHighlight uibhighlight = base.GetComponent<UIBHighlight>();
		if (uibhighlight == null)
		{
			uibhighlight = base.gameObject.AddComponent<UIBHighlight>();
		}
		this.m_UIBHighlight = uibhighlight;
		if (this.m_UIBHighlight != null)
		{
			this.m_UIBHighlight.m_MouseOverHighlight = this.m_Highlight;
			this.m_UIBHighlight.m_HideMouseOverOnPress = false;
		}
	}

	// Token: 0x0600951E RID: 38174 RVA: 0x00304C95 File Offset: 0x00302E95
	public void Select()
	{
		this.Depress();
	}

	// Token: 0x0600951F RID: 38175 RVA: 0x00304C9D File Offset: 0x00302E9D
	public void Deselect()
	{
		this.Raise();
	}

	// Token: 0x06009520 RID: 38176 RVA: 0x00304CA8 File Offset: 0x00302EA8
	private void Raise()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			this.m_UpBone.localPosition,
			"time",
			0.1f,
			"easeType",
			iTween.EaseType.linear,
			"isLocal",
			true
		});
		iTween.MoveTo(this.m_RootObject, args);
	}

	// Token: 0x06009521 RID: 38177 RVA: 0x00304D20 File Offset: 0x00302F20
	private void Depress()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			this.m_DownBone.localPosition,
			"time",
			0.1f,
			"easeType",
			iTween.EaseType.linear,
			"isLocal",
			true
		});
		iTween.MoveTo(this.m_RootObject, args);
	}

	// Token: 0x04007D0E RID: 32014
	private const float RAISE_TIME = 0.1f;

	// Token: 0x04007D0F RID: 32015
	private const float DEPRESS_TIME = 0.1f;

	// Token: 0x04007D10 RID: 32016
	[CustomEditField(Sections = "Button Objects")]
	[SerializeField]
	public GameObject m_RootObject;

	// Token: 0x04007D11 RID: 32017
	[CustomEditField(Sections = "Button Objects")]
	[SerializeField]
	public Transform m_UpBone;

	// Token: 0x04007D12 RID: 32018
	[CustomEditField(Sections = "Button Objects")]
	[SerializeField]
	public Transform m_DownBone;

	// Token: 0x04007D13 RID: 32019
	[CustomEditField(Sections = "Highlight")]
	[SerializeField]
	public GameObject m_Highlight;

	// Token: 0x04007D14 RID: 32020
	private UIBHighlight m_UIBHighlight;
}
