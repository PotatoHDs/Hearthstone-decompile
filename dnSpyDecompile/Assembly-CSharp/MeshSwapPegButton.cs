using System;
using UnityEngine;

// Token: 0x02000AD0 RID: 2768
public class MeshSwapPegButton : PegUIElement
{
	// Token: 0x06009392 RID: 37778 RVA: 0x002FDBC4 File Offset: 0x002FBDC4
	protected override void Awake()
	{
		this.originalPosition = this.upState.transform.localPosition;
		base.Awake();
		this.SetState(PegUIElement.InteractionState.Up);
		Bounds boundsOfChildren = TransformUtil.GetBoundsOfChildren(base.gameObject);
		if (base.GetComponent<MeshRenderer>() != null)
		{
			base.GetComponent<MeshRenderer>().bounds.SetMinMax(boundsOfChildren.min, boundsOfChildren.max);
		}
	}

	// Token: 0x06009393 RID: 37779 RVA: 0x002FDC2F File Offset: 0x002FBE2F
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		this.SetState(PegUIElement.InteractionState.Over);
	}

	// Token: 0x06009394 RID: 37780 RVA: 0x002FDC46 File Offset: 0x002FBE46
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		this.SetState(PegUIElement.InteractionState.Up);
	}

	// Token: 0x06009395 RID: 37781 RVA: 0x002FDC5D File Offset: 0x002FBE5D
	protected override void OnPress()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		this.SetState(PegUIElement.InteractionState.Down);
	}

	// Token: 0x06009396 RID: 37782 RVA: 0x002FDC2F File Offset: 0x002FBE2F
	protected override void OnRelease()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		this.SetState(PegUIElement.InteractionState.Over);
	}

	// Token: 0x06009397 RID: 37783 RVA: 0x002FDC74 File Offset: 0x002FBE74
	public void SetButtonText(string s)
	{
		this.buttonText.text = s;
	}

	// Token: 0x06009398 RID: 37784 RVA: 0x002FDC82 File Offset: 0x002FBE82
	public void SetButtonID(int id)
	{
		this.m_buttonID = id;
	}

	// Token: 0x06009399 RID: 37785 RVA: 0x002FDC8B File Offset: 0x002FBE8B
	public int GetButtonID()
	{
		return this.m_buttonID;
	}

	// Token: 0x0600939A RID: 37786 RVA: 0x002FDC94 File Offset: 0x002FBE94
	public void SetState(PegUIElement.InteractionState state)
	{
		if (this.overState != null)
		{
			this.overState.SetActive(false);
		}
		if (this.disabledState != null)
		{
			this.disabledState.SetActive(false);
		}
		if (this.upState != null)
		{
			this.upState.SetActive(false);
		}
		if (this.downState != null)
		{
			this.downState.SetActive(false);
		}
		this.SetEnabled(true, false);
		switch (state)
		{
		case PegUIElement.InteractionState.Over:
			this.overState.SetActive(true);
			return;
		case PegUIElement.InteractionState.Down:
			this.downState.transform.localPosition = this.originalPosition + this.downOffset;
			this.downState.SetActive(true);
			return;
		case PegUIElement.InteractionState.Up:
			this.upState.SetActive(true);
			this.downState.transform.localPosition = this.originalPosition;
			return;
		case PegUIElement.InteractionState.Disabled:
			this.disabledState.SetActive(true);
			this.SetEnabled(false, false);
			return;
		default:
			return;
		}
	}

	// Token: 0x0600939B RID: 37787 RVA: 0x002FDD9C File Offset: 0x002FBF9C
	public void Show()
	{
		base.gameObject.SetActive(true);
		this.SetState(PegUIElement.InteractionState.Up);
	}

	// Token: 0x0600939C RID: 37788 RVA: 0x00028167 File Offset: 0x00026367
	public void Hide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x04007B9D RID: 31645
	public GameObject upState;

	// Token: 0x04007B9E RID: 31646
	public GameObject overState;

	// Token: 0x04007B9F RID: 31647
	public GameObject downState;

	// Token: 0x04007BA0 RID: 31648
	public GameObject disabledState;

	// Token: 0x04007BA1 RID: 31649
	public Vector3 downOffset;

	// Token: 0x04007BA2 RID: 31650
	private Vector3 originalPosition;

	// Token: 0x04007BA3 RID: 31651
	private Vector3 originalScale;

	// Token: 0x04007BA4 RID: 31652
	private int m_buttonID;

	// Token: 0x04007BA5 RID: 31653
	public TextMesh buttonText;
}
